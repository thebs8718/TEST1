import os
import shutil
from datetime import datetime, timedelta
import time
import threading
import tkinter as tk
from tkinter import messagebox
import logging
from logging.handlers import RotatingFileHandler  # 引入 RotatingFileHandler

# 定义源目录和目标目录
source_dir = r"\\172.28.129.1\bsppm1wcc1_e\na\PudPM\log\hmi"
target_dir = r"E:\WinCC_L1_log"

# 定义存储 last_number 的文件路径
last_number_file = os.path.join(target_dir, "last_number.txt")

# 创建目标目录（如果不存在）
os.makedirs(target_dir, exist_ok=True)

# 配置日志，将日志文件保存到目标路径
log_file = os.path.join(target_dir, "file_collection.log")  # 日志文件路径
logging.basicConfig(
    level=logging.INFO,  # 设置日志级别为 INFO
    format="%(asctime)s [%(levelname)s] %(message)s",  # 定义日志格式
    handlers=[
        RotatingFileHandler(
            log_file,  # 日志文件路径
            maxBytes=10 * 1024 * 1024,  # 文件大小限制为 10MB
            backupCount=5  # 最多保留 5 个备份文件
        ),
        logging.StreamHandler()  # 输出到控制台
    ]
)

# 读取或初始化 last_number
# def load_last_number():
#     if os.path.exists(last_number_file):
#         with open(last_number_file, "r") as f:
#             try:
#                 return int(f.read().strip())  # 从文件中读取 last_number
#             except ValueError:
#                 return 0  # 如果文件内容无效，返回默认值 0
#     return 0
def load_last_number():
    # 如果 last_number 文件存在，优先从中加载
    if os.path.exists(last_number_file):
        with open(last_number_file, "r") as f:
            try:
                return int(f.read().strip())  # 从文件中读取 last_number
            except ValueError:
                pass  # 如果文件内容无效，忽略并继续查找目标目录

    # 查找目标目录中所有日志文件，按修改时间排序，获取最新的文件序号
    log_files = []
    for root_dir, _, files in os.walk(target_dir):
        for file in files:
            if file.startswith("hmi_") and file.endswith(".log"):
                file_path = os.path.join(root_dir, file)
                log_files.append((file_path, os.path.getmtime(file_path)))

    if log_files:
        # 按修改时间排序，获取最新的文件
        latest_file = max(log_files, key=lambda x: x[1])[0]
        # 从文件名中提取序号
        file_name = os.path.basename(latest_file)
        try:
            # 提取 hmi_XXX_... 中的 XXX 部分
            last_number = int(file_name.split("_")[1])
            return last_number
        except (IndexError, ValueError):
            pass  # 如果文件名格式不正确，忽略并返回默认值

    return 0  # 默认值
# 存储 last_number 到文件
def save_last_number(last_number):
    with open(last_number_file, "w") as f:
        f.write(str(last_number))

# 初始化 last_number
last_number = load_last_number()

# 定义全局变量，用于控制程序运行
running = False

def delete_lastnumber_file(target_dir, last_number):
    """
    删除目标目录中最新日期的 lastnumber 文件，但仅当文件大小 <= 4000KB 时删除。
    """
    # 查找目标目录中所有与 lastnumber 相关的文件
    files = [f for f in os.listdir(target_dir) if f.startswith(f"hmi_{last_number:03}_")]

    if files:
        # 找到最新日期的文件
        latest_file = max(files, key=lambda f: os.path.getmtime(os.path.join(target_dir, f)))
        latest_file_path = os.path.join(target_dir, latest_file)

        # 检查文件大小
        file_size_kb = os.path.getsize(latest_file_path) / 1024  # 文件大小转换为 KB
        if file_size_kb <= 4000:
            # 删除文件
            os.remove(latest_file_path)
            logging.info(f"已删除最新日期的文件：{latest_file}（文件大小：{file_size_kb:.2f}KB）")
        else:
            logging.info(f"文件 {latest_file} 大小超过 4000KB（文件大小：{file_size_kb:.2f}KB），不执行删除操作")
    else:
        logging.info(f"未找到与 lastnumber {last_number} 相关的文件")
# 文件采集逻辑
def collect_files():
    global last_number, running
    while running:
        today = datetime.now()
        today_folder = today.strftime("%Y%m%d")
        today_dir = os.path.join(target_dir, today_folder)
        os.makedirs(today_dir, exist_ok=True)

        logging.info(f"开始遍历，日期：{today_folder}，上次处理的序号：{last_number:03}")

        try:
            # 在采集前，删除最新日期的 lastnumber 文件
            delete_lastnumber_file(today_dir, last_number)
            def get_next_number(current):
                return (current + 1) % 1000

            current_number = last_number
            has_new_files = False

            for _ in range(1000):
                source_filename = f"hmi_{current_number:03}.log"
                source_path = os.path.join(source_dir, source_filename)

                if os.path.exists(source_path):
                    source_mtime = os.path.getmtime(source_path)
                    source_time = datetime.fromtimestamp(source_mtime)

                    current_time = datetime.now()
                    compare_time = current_time - timedelta(minutes=7)

                    if source_time >= compare_time:
                        has_new_files = True
                        formatted_time = source_time.strftime("%Y%m%d%H%M%S")

                        file_exists = False
                        for target_file in os.listdir(today_dir):
                            if target_file.startswith(f"hmi_{current_number:03}_") and target_file.endswith(".log"):
                                target_path = os.path.join(today_dir, target_file)
                                target_mtime = os.path.getmtime(target_path)
                                if target_mtime == source_mtime:
                                    file_exists = True
                                    break

                        if not file_exists:
                            target_filename = f"hmi_{current_number:03}_{formatted_time}.log"
                            target_path = os.path.join(today_dir, target_filename)
                            shutil.copy2(source_path, target_path)
                            logging.info(f"正在复制新文件: {source_filename} 为 {target_filename}")

                        last_number = current_number
                        save_last_number(last_number)

                current_number = get_next_number(current_number)

            if not has_new_files:
                logging.info("当前所有源文件的修改时间小于当前时间减去 5 分钟，停止遍历")

        except Exception as e:
            logging.error(f"发生错误：{e}")

        # 倒计时逻辑
        for remaining in range(300, 0, -1):
            if not running:
                break
            label_countdown.config(text=f"下一次执行倒计时：{remaining} 秒")
            time.sleep(1)

        if not running:
            break
        label_countdown.config(text="下一次执行倒计时：0 秒")
        logging.info("倒计时结束，开始下一次循环。")

# GUI 窗口
def start_collection():
    global running
    if not running:
        running = True
        threading.Thread(target=collect_files, daemon=True).start()
        button_start.config(state=tk.DISABLED)
        button_stop.config(state=tk.NORMAL)
        messagebox.showinfo("提示", "采集程序已启动！")

def stop_collection():
    global running
    if running:
        running = False
        button_start.config(state=tk.NORMAL)
        button_stop.config(state=tk.DISABLED)
        label_countdown.config(text="程序已停止")
        messagebox.showinfo("提示", "采集程序已停止！")

# 创建 GUI 窗口
root = tk.Tk()
root.title("文件采集程序")
root.geometry("500x300")

# 设置字体和颜色
BUTTON_FONT = ("Arial", 14, "bold")
BUTTON_BG = "#4CAF50"  # 绿色背景
BUTTON_FG = "white"  # 白色文字
BUTTON_ACTIVE_BG = "#45a049"  # 按下时的背景色

# 创建开始按钮
button_start = tk.Button(
    root,
    text="开始采集",
    command=start_collection,
    font=BUTTON_FONT,
    bg=BUTTON_BG,
    fg=BUTTON_FG,
    activebackground=BUTTON_ACTIVE_BG,
    padx=20,
    pady=10,
    relief="raised",
    borderwidth=3
)
button_start.pack(pady=20)

# 创建停止按钮
button_stop = tk.Button(
    root,
    text="停止采集",
    command=stop_collection,
    font=BUTTON_FONT,
    bg="#f44336",  # 红色背景
    fg=BUTTON_FG,
    activebackground="#d32f2f",  # 按下时的背景色
    padx=20,
    pady=10,
    relief="raised",
    borderwidth=3,
    state=tk.DISABLED
)
button_stop.pack(pady=20)

# 创建倒计时标签
label_countdown = tk.Label(
    root,
    text="程序未运行",
    font=("Arial", 20),
    fg="#333333"
)
label_countdown.pack(pady=20)

# 运行 GUI 主循环
root.mainloop()

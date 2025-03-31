import os
import shutil
from datetime import datetime, timedelta
import time

# 定义源目录和目标目录
source_dir = r"\\172.28.129.1\bsppm1wcc1_e\na\PudPM\log\hmi"
#source_dir = r"\\192.168.174.129\c\log\hmi"
target_dir = r"E:\WinCC_L1_log"

# 定义存储 last_number 的文件路径
last_number_file = os.path.join(target_dir, "last_number.txt")

# 创建目标目录（如果不存在）
os.makedirs(target_dir, exist_ok=True)

# 读取或初始化 last_number
def load_last_number():
    if os.path.exists(last_number_file):
        with open(last_number_file, "r") as f:
            try:
                return int(f.read().strip())  # 从文件中读取 last_number
            except ValueError:
                return 0  # 如果文件内容无效，返回默认值 0
    return 0

# 存储 last_number 到文件
def save_last_number(last_number):
    with open(last_number_file, "w") as f:
        f.write(str(last_number))

# 初始化 last_number
last_number = load_last_number()
# 主循环
while True:
    # 获取当前日期并创建子文件夹
    today = datetime.now()
    today_folder = today.strftime("%Y%m%d")  # 格式化为 YYYYMMDD
    today_dir = os.path.join(target_dir, today_folder)
    os.makedirs(today_dir, exist_ok=True)  # 创建子文件夹（如果不存在）

    print(f"开始遍历，日期：{today_folder}，上次处理的序号：{last_number:03}")

    try:
        # 定义循环序号函数
        def get_next_number(current):
            return (current + 1) % 1000  # 当 current 是 999 时，返回 0

        # 从 last_number 开始，循环处理 000-999
        current_number = last_number
        has_new_files = False  # 用于记录是否存在修改时间大于等于当前时间减去 5 分钟的文件

        for _ in range(1000):
            source_filename = f"hmi_{current_number:03}.log"
            source_path = os.path.join(source_dir, source_filename)

            # 检查源目录中是否存在对应的文件
            if os.path.exists(source_path):
                # 获取源文件的修改时间
                source_mtime = os.path.getmtime(source_path)
                source_time = datetime.fromtimestamp(source_mtime)

                # 获取当前时间并减去 5 分钟
                current_time = datetime.now()
                compare_time = current_time - timedelta(minutes=6)
                # print(f"current_time：{current_time}")
                # print(f"compare_time：{compare_time}")
                # print(f"source_time：{source_time}")
                # 检查源文件的修改时间是否在 5 分钟之前
                if source_time >= compare_time:
                    has_new_files = True  # 存在修改时间大于等于当前时间减去 5 分钟的文件

                    # 格式化修改时间为字符串
                    formatted_time = source_time.strftime("%Y%m%d%H%M%S")

                    # 检查目标目录中是否存在相同修改时间的文件
                    file_exists = False
                    for target_file in os.listdir(today_dir):
                        if target_file.startswith(f"hmi_{current_number:03}_") and target_file.endswith(".log"):
                            target_path = os.path.join(today_dir, target_file)
                            target_mtime = os.path.getmtime(target_path)
                            if target_mtime == source_mtime:
                                file_exists = True
                                break

                    # 如果目标目录中没有相同修改时间的文件，则复制
                    if not file_exists:
                        target_filename = f"hmi_{current_number:03}_{formatted_time}.log"
                        target_path = os.path.join(today_dir, target_filename)
                        shutil.copy2(source_path, target_path)
                        print(f"正在复制新文件: {source_filename} 为 {target_filename}")
                        print(f"源文件：{source_filename}")
                        print(f"修改时间：{source_time}")
                        print(f"格式化日期时间：{formatted_time}")
                        print(f"目标文件名：{target_filename}")

                    # 更新最后一个处理的序号
                    last_number = current_number
                    save_last_number(last_number)  # 将 last_number 存储到文件

            # 更新当前序号
            current_number = get_next_number(current_number)

        # 如果所有文件的修改时间都早于当前时间减去 5 分钟，则跳出循环
        if not has_new_files:
            print("当前所有源文件的修改时间小于当前时间减去 5 分钟，停止遍历")

    except Exception as e:
        print(f"发生错误：{e}")

    # 等待 5 分钟（300 秒）
    # print("等待 5 分钟（300 秒）")
    # time.sleep(3)
    print("等待 1 分钟（60 秒），倒计时开始：")
    for remaining in range(60, 0, -1):
        print(f"\r倒计时剩余时间：{remaining} 秒", end="", flush=True)  # 实时刷新倒计时显示
        time.sleep(1)
    print("\r倒计时结束，开始下一次循环。", flush=True)
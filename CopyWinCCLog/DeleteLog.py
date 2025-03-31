import os
import time
import shutil
from datetime import datetime, timedelta

# 定义日志目录
log_directory = "D:\WinCC_L1_log"
# 定义日志文件路径
log_file_path = os.path.join(log_directory, "cleanup_log.txt")




def log_message(message):
    """
    将消息记录到日志文件并打印到控制台
    """
    log_time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    log_entry = f"[{log_time}] {message}\n"
    print(log_entry, end="")
    try:
        with open(log_file_path, "a") as log_file:
            log_file.write(log_entry)
    except Exception as e:
        print(f"无法写入日志文件: {e}")


def delete_old_folders():
    try:
        # 获取当前日期
        current_date = datetime.now()
        # 计算一个月前的日期
        one_month_ago = current_date - timedelta(days=15)
        # 遍历日志目录
        for folder_name in os.listdir(log_directory):
            folder_path = os.path.join(log_directory, folder_name)

            # 检查是否是文件夹
            if os.path.isdir(folder_path):
                try:
                    # 解析文件夹名称中的日期
                    folder_date = datetime.strptime(folder_name, "%Y%m%d")

                    # 如果文件夹日期早于一个月前，删除文件夹
                    if folder_date < one_month_ago:
                        log_message(f"删除文件夹: {folder_name}")
                        shutil.rmtree(folder_path)
                except ValueError:
                    # 文件夹名称不符合日期格式，跳过
                    continue
                except Exception as e:
                    log_message(f"删除文件夹 {folder_name} 时发生错误: {e}")
    except Exception as e:
        log_message(f"遍历目录时发生错误: {e}")


# 定时执行删除任务
while True:
    try:
        delete_old_folders()
        # 每隔一天检查一次
        time.sleep(86400)  # 86400秒 = 24小时
    except KeyboardInterrupt:
        log_message("脚本已手动停止")
        break
    except Exception as e:
        log_message(f"脚本运行时发生错误: {e}")

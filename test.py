"""
test
Author:Administrator
Date:2025/4/2

"""
from AutoCheckCopy import InspectionSheetUpdater
# 使用默认配置（当前目录，默认文件名前缀）
updater = InspectionSheetUpdater(
    target_dir='./',
    file_prefix="日常点检表_厚板L2"
)
updater.update_sheet()  # 执行更新
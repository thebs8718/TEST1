# 日常点检表自动更新工具

## 功能说明

本工具用于自动维护日常点检表Excel文件，自动处理每月新建文件、每日添加工作表等工作。

## 安装要求
- Python 3.6+
- 依赖包: openpyxl, chinese_calendar, pywin32

## 使用方法
1. 安装依赖: pip install openpyxl chinese_calendar pywin32
2. 修改配置: 编辑target_dir和file_prefix参数
3. 运行程序: python inspection_sheet_updater.py

## 配置文件
- 目标目录: ./ (可修改)
- 文件前缀: "日常点检表_厚板L2" (可修改)
- 文件名格式: {前缀}(YYYY-MM月份).xlsx

## 工作表规则
1. 每月1日新建当月文件
2. 工作日自动添加当日工作表
3. 自动跳过节假日和周末
4. 新表复制前一工作日的格式内容

## 日志记录
- 日志文件: ./file_collection.log
- 日志轮转: 10MB/文件，保留5个备份

## 注意事项
1. Windows系统建议安装Excel确保完整功能
2. 确保程序有目标目录读写权限
3. 首月使用时需确保存在上月文件
4. 非Windows系统自动保存功能受限
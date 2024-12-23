rem 31日以前のログファイルをログに記録
FORFILES /P .\Logs /D -31 /C "cmd /c echo %date% @file @fdate" >> .\logrotate.log
rem 31日以前のログフォルダを削除する
FORFILES /P .\Logs /D -31 /C "cmd /c rmdir /s /q @path"
exit 0

rem 31���ȑO�̃��O�t�@�C�������O�ɋL�^
FORFILES /P .\Logs /D -31 /C "cmd /c echo %date% @file @fdate" >> .\logrotate.log
rem 31���ȑO�̃��O�t�H���_���폜����
FORFILES /P .\Logs /D -31 /C "cmd /c rmdir /s /q @path"
exit 0

@echo OFF
 :begin
 REM 循环删除指定文件夹下的文件夹
 FOR /d /r ".\" %%b in (bin,obj,public,.vs,.idea,.gitee,.github) do rd /s /q "%%b"
 ECHO 【处理完毕，按任意键退出】
 PAUSE>NUL
 EXIT
 GOTO BEGIN
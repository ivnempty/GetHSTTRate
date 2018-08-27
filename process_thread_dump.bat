@echo off

set t=%date%_%time%
echo %datetime%

SET HOUR=%TIME:~0,2%
IF "%HOUR:~0,1%" == " " SET HOUR=0%HOUR:~1,1%
echo %HOUR%

set filename=%t:~0,4%%t:~5,2%%t:~8,2%_%HOUR%%t:~18,2%%t:~21,2%

echo %filename%

REM dynamic get the PID of Tomcat process
set imgName=Tomcat6.exe
for /F "tokens=1,2" %%i in ('tasklist /FI "IMAGENAME eq %imgName%" /fo table /nh') do set pid=%%j

C:\Temp\PSTools\psexec -s "C:\Program Files\Java\jdk1.7.0_03\bin\jstack.exe" -l %pid% > C:\Temp\TomcatThreadDump\%filename%.txt




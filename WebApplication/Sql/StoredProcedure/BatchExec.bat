@echo off
set DbHost=192.168.210.190,54999\sql2005
set DbName=scms_demo
set DbUser=sa
set DbPass=temp100
for /f %%i in ('dir/b *.sql') do (
osql -S "%DbHost%" -d "%DbName%" -U "%DbUser%" -P "%DbPass%" -i "%%i" -o "%%~ni.log"
)
pause



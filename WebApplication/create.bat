@echo off
Rem C:\WINDOWS\Microsoft.NET\Framework\v3.5\MSBuild.exe Ext.xml /t:Main @Ext.rsp %1

if "%MSBuild_HOME%" == "" set "MSBuild_HOME=C:\WINDOWS\Microsoft.NET\Framework\v3.5"

if "" == "%1" goto errorCreate 
Rem if "" == "%1" set CompanyName=Ext

if ""%1""==""h"" goto printHelpMessage

if not exist "%MSBuild_HOME%\MSBuild.exe" goto MSBuildNotFound
echo.
echo		%date% %time%
echo		MSBuild_HOME:			"%MSBuild_HOME%"
echo		公司关键字:			"%1"

if ""%2""=="""" goto defaultArgs
call "%MSBuild_HOME%\MSBuild.exe" build.xml /t:%2 /p:CompanyName=%1
echo.
echo		%date% %time%
goto end


:defaultArgs
	echo.
	call "%MSBuild_HOME%\MSBuild.exe" build.xml /t:All /p:CompanyName=%1
	echo		%date% %time%
	goto end


:MSBuildNotFound
	echo.
	echo		环境变量 MSBuild_HOME 定义不正确.重新定义环境变量后请重新开打次窗口
	goto end


:printHelpMessage
:errorCreate
	echo.
	echo Usage: create.bat ChunShen All
	echo.
	echo 第一个 ChunShen 是你的公司关键字. 基于这个关键字会创建一个新的目录
	echo.
	echo 第二个 All 是执行任务的关键字,可以缺省.如下可选项:
	echo		Clean					清空扩展目录
	echo		Compile					编译Service,Entity,Persistence,Utility项目
	echo		BuildExt				生成扩展环境
	echo		All					Clean 与 BuildExt,默认
	echo.
	goto end


:end


::删除更新目录，将版本目录文件复制到更新目录
::项目名参数
set solution=%1
::开发版本/稳定版本
set configuration=%2
::当前的版本目录: 20220610157865-1.0.0.1
set versionName=%3
set maxFolders=%4

::删除 update 目录下的文件
cd ../pub/update/%solution%/
rd /s /Q %configuration%\
md %configuration%

cd ../../

::开始复制版本文件到 update 目录
Xcopy /E/S C:\pub\version\%solution%\%configuration%\%versionName%\*.*  C:\pub\update\%solution%\%configuration%\

cd ../script/

::更新版本文件&&检查版本文件夹个数 python .\publish.py C:\pub\version\Action-wpf\developer\  7
if "%maxFolders%" NEQ "" (python .\publish.py C:\pub\version\%solution%\%configuration%\ %maxFolders%) 






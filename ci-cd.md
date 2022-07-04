# 部署

 ### 一.服务器环境的配置

   #### 1.python环境安装      
   ##### 1.1软件版本的要求
    python 版本: 3.9.11

    官网地址：https://www.python.org/downloads/
   
   #### 2.FileZilla服务器的搭建
   ##### 2.1 软件版本的要求
    
    FileZilla Server 版本： 0.9.60

    下载地址： https://filezilla-project.org/download.php?type=server
   
   ##### 2.2 FileZilla Server 的配置
    
    监听端口： 8090

    主动模式随机端口: 8091 (只配置一个端口)

    创建用户和密码并指定本地路径: c:/pub/

  #### 3.ngixn 环境的配置
  ##### 3.1 ngixn 版本要求

    ngixn版本: 1.23.0
    
    下载地址：http://nginx.org/en/download.html

 ##### 3.2 ngxin 配置
   
     监听端口： 8080
     监听的本地路径： c:/pub/

  #### 4.OpenSSH 环境的配置

  ##### 3.1 OpenSSH 版本要求
    
    openssh 版本： 8.9.1.0

    下载地址： https://github.com/PowerShell/Win32-OpenSSH/releases
  
  ##### 3.2 OpenSSH 配置

    端口： 8092


### 二.脚本讲解

 #### 1. create-verion-file.py 
 ##### 1.1 基本功能
    
    发布 developer 版本，该脚本才会工作。

    a.检测当前版本文件是否存在，不存在则创建

    b.根据版本文件检测上次发布是否出现问题，出现问题将上次发布的文件夹删除    
    

##### 1.2 参数详解
    
    参数1：（必须） 发布的版本文件夹路径(绝对路径) C:\pub\version\Action-wpf\developer
    参数2：（必须）版本文件路径（绝对路径）    c:\pub\version.txt


#### 2.copy-res.bat
##### 2.1 基本功能

    a. 将通过 sftp 上传的版本文件复制到更新目录
    b. 如果是 developer 模式， 会调用 publish.py

##### 2.2 参数详解
   
   参数1： （必须）项目名称 
   参数2： （必须）编译模式(developer / stable)
   参数3： （必须）当前发布的版本目录名 (20220610157865-1.0.0.1 / 1.0.0.1)
   参数4： （可选）版本文件夹子文件夹最大个数，当超过这个值就会清理到之前老的文件(developer )


#### 3.publish.py 
##### 3.1 基本功能

    a. developer 模式下检测版本检测历史版本文件个数

    b. 生成最新的版本文件

##### 3.2 参数详解

    参数1：(必须) 需要检测的文件夹路径
    参数2：(必须) 最大的文件夹个数
 

### 三.yml 讲解
 #### 1.dev.yml 
 ##### 1.1 基本功能

    概述： 每次提交 git 都会编译，成功和失败都会 slack 通知    

##### 1.2 每一步的讲解

    1: 代码的迁出

    2: 安装 .net core 6.0.x

    3：安装 msbuild.exe

    4: 设置 node 环境为 14

    5：安装项目依赖

    6：msbuild 编译项目

    7：发布通知


#### 2.developer.yml
##### 2.1 基本功能

    概述： 每天下午7：00 定时编译开发版本, 会将编译的结构复制到更新目录

##### 2.2 每一步的讲解

    1：代码的迁出

    2：安装 .net core 6.0.x

    3： 安装 msbuild.exe

    4：设置 node 环境为 14

    5：安装项目依赖

    6：设置远程版本文件路径

    7：设置更新地址

    8：通过 SSH 连接服务器并执行服务器脚本 create-verion-file.py

    9：获取远程版本文件 version.txt

    10：获取将要发布的版本信息

    11：清空项目, 删除之前发布目录

    12：发布 ClickOnce

    13：将发布的版本通过 sftp 上传到服务器

    14：通过 SSH 连接服务器并执行服务器脚本 copy-res.bat

    15：发布通知


#### 3.stable.yml
##### 3.1 基本功能

    概述：稳定版本发布，需要手动发布, 发布时需要指定发布的版本号信息

##### 3.2 每一步的讲解

    1：代码的迁出

    2：安装 .net core 6.0.x

    3： 安装 msbuild.exe

    4：设置 node 环境为 14

    5：安装项目依赖

    6：设置更新地址

    7：清空项目并删除之前发布目录

    8：发布 ClickOnce

    9：通过 sftp 将发布文件上传到服务器

    10：通过 ssh 连接服务器并执行脚本 copy-res.bat

    11：发布通知


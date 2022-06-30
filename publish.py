from argparse import ArgumentError
from ast import Raise
from fnmatch import fnmatch
import os
import shutil
import sys
from typing import List, Tuple


def GetFileFolderCount(path: str) -> int:    
    dirs = os.listdir(path)    
    total = 0
    for dir in dirs:
        if os.path.isdir(os.path.join(path, dir)):
            total += 1
    
    return total


def GetNewFolderName(path: str) -> str:
    dirs = []    
    
    for dir in os.listdir(path):
        if os.path.isdir(os.path.join(path, dir)):
            dirs.append(dir)
    
    if len(dirs) == 0:
        return ""
        
    dirs.sort(key=lambda dir: os.path.getmtime(os.path.join(path, dir)))

    return dirs[-1]

# 删除老的文件夹
def DeleteOldFolder(path: str):
    dirs = []
    for dir in os.listdir(path):
        if os.path.isdir(os.path.join(path, dir)):
            dirs.append(dir)
    
    if len(dirs) == 0:
        return
        
    dirs.sort(key=lambda dir: os.path.getmtime(os.path.join(path, dir)), reverse=True)

    shutil.rmtree(os.path.join(path, dirs[-1]))


# 文件夹命名规则： 日期-版本号  版本号：1.1.1.1
# 返回前面是大版本，后面是小版本
def GetVersion(folderName: str):
    names = folderName.split('-')
    if len(names) != 2:
        raise RuntimeError(f"文件夹命名不合法, folder: {folderName}")
    
    verStr = names[1]
    # 检测version的合法性
    vers = verStr.split('.')
    if len(vers) != 4:
        raise RuntimeError(f"文件夹命名不合法, folder: {folderName}" )

    return f'{vers[0]}.{vers[1]}.{vers[2]}', vers[3]    

# argv[0]： 检测的文件夹路径
# argv[1]:  当前文件夹最多允许存在的文件夹个数
if __name__ == "__main__":
    if len(sys.argv) != 3:
        raise ArgumentError("参数个数不合法, 参数个数应该为2个, argv[0] 应该为需要检测的文件夹路径, argv[1] 为当前文件夹最多允许存在的文件夹个数")


    checkDir = sys.argv[1]
    dirTotal = int(sys.argv[2])

    if not os.path.exists(checkDir):
        raise ArgumentError("需要检测的文件夹不存在, path: " + checkDir)
    
    # 检查当前文件夹的文件夹个数
    if GetFileFolderCount(checkDir) > dirTotal:
        DeleteOldFolder(checkDir)           # 刪除最老的文件夾        
    

    # 获取最新的版本号 保存到文件夹 
    dirName = GetNewFolderName(checkDir)
    # 解析文件夹名字返回版本号 ? 文件夹的命名方式
    mainVesion, revisionVersion = GetVersion(dirName)

    # 将主版本和修订版本写到文件中
    fo = open("c:/pub/version.txt", "w+")
    fo.write( f'{mainVesion},{revisionVersion}')
    print("!!!执行完成!!!")




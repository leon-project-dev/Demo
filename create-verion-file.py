from argparse import ArgumentError
from ast import Raise
from fnmatch import fnmatch
import os
import shutil
import sys

# 获取版本文件的内容
def GetVersionContent(filePath: str):
    fs = open(filePath)
    return fs.readline()    

# 获取文件夹名字上的版本号
def GetNewFolderVersion(rootPath: str):
    dirs = []    
    
    for dir in os.listdir(rootPath):
        if os.path.isdir(os.path.join(rootPath, dir)):
            dirs.append(dir)
    
    if len(dirs) == 0:
        return ""
        
    dirs.sort(key=lambda dir: os.path.getmtime(os.path.join(rootPath, dir)))
    names = dirs[-1].split('-')
    if len(names) != 2:
        raise RuntimeError(f'folder name illegal, folder: {folderName}')
    
    verStr = names[1]
    # 检测version的合法性
    vers = verStr.split('.')
    if len(vers) != 4:
        raise RuntimeError(f'folder name illegal, folder: {folderName}' )

    return f'{vers[0]}.{vers[1]}.{vers[2]}', vers[3]    
    

# 删除文件夹
def DeleteNewFolder(rootPath: str):
    dirs = []
    for dir in os.listdir(rootPath):
        if os.path.isdir(os.path.join(rootPath, dir)):
            dirs.append(dir)
    
    if len(dirs) == 0:
        return
        
    dirs.sort(key=lambda dir: os.path.getmtime(os.path.join(rootPath, dir)), reverse=False)
    shutil.rmtree(os.path.join(rootPath, dirs[-1]))

# 检测版本文件是否存在
# 不存在就创建
if __name__ == "__main__":
    if len(sys.argv) != 3:
        raise ArgumentError("参数个数不合法, 参数个数应该为1个, argv[1] 是版本文件的路径, argv[2] 是 verison 文件路径")

    verisonPath = sys.argv[1] 
    rootPath = sys.argv[2] 
    if not os.path.exists(rootPath):
        os.makedirs(rootPath)
    
    if not os.path.exists(verisonPath):
        fs = open(verisonPath, "w+")
        fs.write("")
        fs.close()
    else:                                       # 检测之前发布文件是否失败，如果版本文件里面的版本和最新文件夹版本相等，说明没问题，不等说明有问题，需要删掉最新的文件夹
        fileVerison = GetVersionContent(verisonPath)            
        folderVersion = GetNewFolderVersion(rootPath)
        if rootPath != "" and folderVersion != "" and fileVerison != folderVersion:
            DeleteNewFolder(rootPath)        

    print("!!!run success!!!")    
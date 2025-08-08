# GetGitChange
实现获取git仓库的更改并保存到另外一个目录下


# 使用方法
GetGitChange.exe  <source> <destination>
<source> 为git仓库的路径
<destination> 为保存更改的路径

常用命令:

## 命令1
```
GetGitChange.exe  E:\\git E:\\save
```
会把E:\\git仓库的更改保存到E:\\save目录下

## 命令2

```
GetGitChange.exe  E:\\git

```
会把E:\\git仓库的更改保存到当前命令目录下temp目录里


## 删除的文件
delete.txt 里面的文件需要删除

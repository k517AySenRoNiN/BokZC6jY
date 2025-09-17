// 代码生成时间: 2025-09-18 00:39:01
using System;
using System.IO;
using System.Collections.Generic;

// 文件备份和同步工具类
public class FileBackupSyncTool
{
    // 备份文件路径
    private string backupPath;
    // 源文件夹路径
    private string sourcePath;

    // 构造函数
    public FileBackupSyncTool(string sourcePath, string backupPath)
    {
        this.sourcePath = sourcePath;
        this.backupPath = backupPath;
    }

    // 备份文件方法
    public void BackupFiles()
    {
        try
        {
            // 创建备份目录
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            // 获取源目录下所有文件
            var files = Directory.GetFiles(sourcePath);
            foreach (var file in files)
            {
                // 构造备份文件路径
                var backupFile = Path.Combine(backupPath, Path.GetFileName(file));
                // 复制文件到备份目录
                File.Copy(file, backupFile, true);
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"备份文件时发生错误: {ex.Message}");
        }
    }

    // 同步文件方法
    public void SyncFiles()
    {
        try
        {
            // 获取源目录和备份目录中的所有文件
            var sourceFiles = new HashSet<string>(Directory.GetFiles(sourcePath), StringComparer.OrdinalIgnoreCase);
            var backupFiles = new HashSet<string>(Directory.GetFiles(backupPath), StringComparer.OrdinalIgnoreCase);

            // 将备份目录中多余的文件删除
            foreach (var file in backupFiles)
            {
                if (!sourceFiles.Contains(file))
                {
                    File.Delete(file);
                }
            }

            // 将新增的文件复制到备份目录
            foreach (var file in sourceFiles)
            {
                if (!backupFiles.Contains(file))
                {
                    var backupFile = Path.Combine(backupPath, Path.GetFileName(file));
                    File.Copy(file, backupFile, true);
                }
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"同步文件时发生错误: {ex.Message}");
        }
    }
}

// 程序入口类
class Program
{
    static void Main(string[] args)
    {
        // 源文件夹路径和备份文件夹路径
        string sourcePath = @"C:\source";
        string backupPath = @"C:\backup";

        // 创建文件备份和同步工具实例
        var tool = new FileBackupSyncTool(sourcePath, backupPath);

        // 执行备份操作
        tool.BackupFiles();

        // 执行同步操作
        tool.SyncFiles();
    }
}
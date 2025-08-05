// 代码生成时间: 2025-08-05 19:58:09
using System;
using System.Diagnostics;
using System.Threading.Tasks;

// 系统性能监控工具
public class SystemPerformanceMonitor
{
    // 获取CPU使用率
    public double GetCpuUsage()
    {
        var cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        return cpu.NextValue();
    }

    // 获取内存使用量
    public long GetMemoryUsage()
    {
        var memory = new PerformanceCounter("Memory", "Available MBytes");
        return (ComputerInfo.TotalPhysicalMemory - Convert.ToInt64(memory.NextValue())) * 1024 * 1024;
    }

    // 获取磁盘使用量
    public long GetDiskUsage(string drive)
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        foreach (DriveInfo d in allDrives)
        {
            if (d.Name.ToUpper() == drive.ToUpper())
            {
                return d.TotalFreeSpace;
            }
        }
        throw new Exception("Drive not found");
    }

    // 异步获取系统信息
    public async Task<string> GetSystemInfoAsync()
    {
        try
        {
            var cpuUsage = GetCpuUsage();
            var memoryUsage = GetMemoryUsage();
            var systemInfo = $"CPU Usage: {cpuUsage}%, Memory Usage: {memoryUsage} bytes";
            return systemInfo;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // 主程序入口
    public static void Main(string[] args)
    {
        SystemPerformanceMonitor monitor = new SystemPerformanceMonitor();
        try
        {
            Console.WriteLine("System Performance Monitoring Tool");
            Console.WriteLine("CPU Usage: " + monitor.GetCpuUsage() + "%");
            Console.WriteLine("Memory Usage: " + monitor.GetMemoryUsage() + " bytes");
            Console.WriteLine("Disk Usage: " + monitor.GetDiskUsage("C") + " bytes");

            // 异步获取系统信息
            Task<string> task = monitor.GetSystemInfoAsync();
            string systemInfo = task.Result;
            Console.WriteLine(systemInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
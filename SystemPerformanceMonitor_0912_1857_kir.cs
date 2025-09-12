// 代码生成时间: 2025-09-12 18:57:20
using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace SystemPerformanceMonitor
{
    // 系统性能监控控制器
    [ApiController]
    [Route("[controller]/[action]"])
    public class PerformanceMonitorController : ControllerBase
    {
        private const int RefreshInterval = 5000; // 刷新间隔（毫秒）

        // GET: 获取系统性能信息
        [HttpGet]
        public IActionResult GetSystemPerformanceInfo()
        {
            try
            {
                // 记录当前系统的CPU使用率
                float cpuUsage = GetCpuUsage();

                // 记录当前的可用内存
                float availableMemory = GetAvailableMemory();

                // 构建性能信息对象
                var performanceInfo = new {
                    CpuUsage = cpuUsage,
                    AvailableMemory = availableMemory
                };

                return Ok(performanceInfo);
            }
            catch (Exception ex)
            {
                // 错误处理
                return StatusCode(500, $"errors: {ex.Message}");
            }
        }

        // 获取CPU使用率
        private float GetCpuUsage()
        {
            // 使用Process类获取CPU使用率
            return Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds /
                   (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds *
                   Environment.ProcessorCount;
        }

        // 获取可用内存
        private float GetAvailableMemory()
        {
            // 使用PerformanceCounter获取可用内存
            PerformanceCounter memoryCounter = new PerformanceCounter(
                "Memory", "Available MBytes");

            float availableMemory = memoryCounter.NextValue();
            return availableMemory;
        }
    }
}

// 代码生成时间: 2025-09-03 15:11:36
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Timers;

namespace ScheduleTaskApp
{
    /// <summary>
    /// 定时任务调度器
    /// </summary>
    public class ScheduledTaskScheduler : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        private bool _disposed;

        public ScheduledTaskScheduler()
        {
            // 初始化定时器
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimedEvent;
        }

        /// <summary>
        /// 当定时事件触发时调用
        /// </summary>
        /// <param name="source">事件源</param>
        /// <param name="e">事件参数</param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer event triggered at: " + e.SignalTime);
            try
            {
                // 在这里执行定时任务
                ExecuteTask();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// 执行定时任务
        /// </summary>
        private void ExecuteTask()
        {
            // 任务逻辑代码
            // 这里可以放置实际的定时任务逻辑
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 启动定时器
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // 停止定时器
            _timer.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

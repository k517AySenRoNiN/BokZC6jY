// 代码生成时间: 2025-09-24 17:10:21
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;

namespace QuartzDemo
{
    public class ScheduledTaskScheduler : BackgroundService
    {
        private readonly ILogger<ScheduledTaskScheduler> _logger;
        private readonly IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;

        public ScheduledTaskScheduler(ILogger<ScheduledTaskScheduler> logger)
        {
            _logger = logger;
            _schedulerFactory = new StdSchedulerFactory();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // Initialize the scheduler
                _scheduler = await _schedulerFactory.GetScheduler();
                await _scheduler.Start();

                // Define a job and its trigger
                IJobDetail job = JobBuilder.Create<MyJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("myTrigger", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(45)
                        .RepeatForever())
                    .Build();

                // Schedule the job
                await _scheduler.ScheduleJob(job, trigger);

                _logger.LogInformation("Scheduled task scheduled and running.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while scheduling the task.");
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                // Graceful shutdown
                return _scheduler.Shutdown(stoppingToken);
            }
            return Task.CompletedTask;
        }
    }

    [DisallowConcurrentExecution]
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // Your job logic here
            Console.WriteLine("Job executed at: " + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}

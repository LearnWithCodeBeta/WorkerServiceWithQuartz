using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WorkerDemoService.JobFactory;
using WorkerDemoService.Jobs;
using WorkerDemoService.Models;
using WorkerDemoService.Schedular;

namespace WorkerDemoService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<Worker>();
                    //services.AddHostedService<MyService>();

                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<NotificationJob>();

                    services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));

                    services.AddHostedService<MySchedular>();
                });
    }
}

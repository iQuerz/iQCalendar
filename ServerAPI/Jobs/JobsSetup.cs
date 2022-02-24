using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using ServerAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Jobs
{
    public class JobsSetup
    {
        IScheduler _jobScheduler;
        public async Task setupJobs()
        {
            //database stuff
            var optionsBuilder = new DbContextOptionsBuilder<CalendarContext>();
            optionsBuilder.UseSqlite("Data Source=Data/iQCalendarDB.db;");
            CalendarContext Context = new CalendarContext(optionsBuilder.Options);

            //factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _jobScheduler = await factory.GetScheduler();
            await _jobScheduler.Start();

            //jobs
            IJobDetail emailNotificationsJob = JobBuilder.Create<EmailNotificationJob>()
                .WithIdentity("Email Notifications")
                .Build();

            //triggers
            JobDataMap contextDataMap = new JobDataMap();
            contextDataMap.Add("Context", Context);
            int timeOfDay = Context.Settings.FirstOrDefault().NotificationTime;
            ITrigger emailNotificationsTrigger = TriggerBuilder.Create()
                .UsingJobData(contextDataMap)
                .WithCronSchedule($"0 0 {timeOfDay} ? * * *")
                .Build();

            //schedule jobs
            await _jobScheduler.ScheduleJob(emailNotificationsJob, emailNotificationsTrigger);

            // scheduler shutdown in shutdown of settingsBusiness
        }

        public async Task stopJobs()
        {
            await _jobScheduler.Shutdown();
        }
    }
}

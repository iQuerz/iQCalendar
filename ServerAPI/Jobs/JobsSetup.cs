using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

using ServerAPI.Data;

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

            IJobDetail serverLogsJob = JobBuilder.Create<DailyServerLogsJob>()
                .WithIdentity("Server Logs")
                .Build();


            //triggers
            JobDataMap contextDataMap = new JobDataMap();
            contextDataMap.Add("Context", Context);
            int notificationTimeOfDay = Context.Settings.FirstOrDefault().NotificationTime;

            ITrigger emailNotificationsTrigger = TriggerBuilder.Create()
                .UsingJobData(contextDataMap)
                //.StartNow() //testing purposes
                .WithCronSchedule($"0 0 {notificationTimeOfDay} ? * * *")
                .Build();

            ITrigger serverLogsTrigger = TriggerBuilder.Create()
                .UsingJobData(contextDataMap)
                .WithCronSchedule($"0 59 23 ? * * *")
                .Build();

            //schedule jobs
            await _jobScheduler.ScheduleJob(emailNotificationsJob, emailNotificationsTrigger);
            await _jobScheduler.ScheduleJob(serverLogsJob, serverLogsTrigger);
        }

        public async Task stopJobs()
        {
            await _jobScheduler.Shutdown();
        }
    }
}

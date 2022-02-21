using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerAPI.Data;
using Quartz.Impl;
using Quartz;
using ServerAPI.Jobs;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        CalendarContext _context;
        IScheduler _jobScheduler;

        public Logic(CalendarContext context)
        {
            _context = context;
            Task.Run(async () => await SetupJobs());
        }
        internal async Task SetupJobs()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _jobScheduler = await factory.GetScheduler();

            await _jobScheduler.Start();

            IJobDetail emailNotificationsJob = JobBuilder.Create<EmailNotificationJob>()
                .WithIdentity("Email Notifications")
                .Build();

            var accounts = _context.Accounts.ToList();
            var events = _context.Events.ToList();
            var settings = _context.Settings.FirstOrDefault();
            emailNotificationsJob.JobDataMap.Add("events", events);
            emailNotificationsJob.JobDataMap.Add("accounts", accounts);
            emailNotificationsJob.JobDataMap.Add("settings", settings);

            ITrigger emailNotificationsTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(120)
                    .RepeatForever())
                .Build();

            await _jobScheduler.ScheduleJob(emailNotificationsJob, emailNotificationsTrigger);

            // scheduler shutdown in shutdown of settingsBusiness
        }

        internal async Task<bool> Authenticate(HttpRequest request)
        {
            // extract auth header value
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            // its hashed as username:password
            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.ClientPassword)
                return true;
            if (password == account.AdminPassword) // admin should have access to normal stuff too
                return true;

            return false;
        }
        internal async Task<bool> AuthenticateAdmin(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.AdminPassword)
                return true;

            return false;
        }
        internal async Task<bool> AuthenticateServer(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var settings = _context.Settings.FirstOrDefault(s => s.ServerName == username);
            if (password == settings.ServerPassword)
                return true;

            return false;
        }

        private void DecodeAuth(out string username, out string password, string header)
        {
            string encodedCredentials = header.Substring("Basic ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usrAndPass = encoding.GetString(Convert.FromBase64String(encodedCredentials));

            string[] tempArray = usrAndPass.Split(':');

            username = tempArray[0];
            password = tempArray[1];
        }

    }
}

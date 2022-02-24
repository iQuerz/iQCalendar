using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Quartz;
using ServerAPI.Data;
using ServerAPI.Data.Models;

namespace ServerAPI.Jobs
{
    public class EmailNotificationJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.MergedJobDataMap;

            // Dictionary that holds list of eligible mailing events for each account
            Dictionary<Account, List<Event>> eventLists = new Dictionary<Account, List<Event>>();

            CalendarContext Context = (CalendarContext)dataMap["Context"];
            var accounts = Context.Accounts.ToList();

            foreach(var account in accounts)
            {
                eventLists.Add(account, new List<Event>());
            }

            // Add events to each account in dictionary
            var events = Context.Events.Where(e => !e.Finished).ToList();
            foreach(var @event in events)
            {
                if (IsEligibleForEmail(@event))
                    eventLists[accounts
                        .Where(a => a.AccountID == @event.AccountID).FirstOrDefault()]
                        .Add(@event);
            }

            // send the emails
            List<Task> tasks = new List<Task>();
            var settings = Context.Settings.FirstOrDefault();
            foreach(var pair in eventLists)
            {
                string subject = "iQCalendar daily breefing";
                string body = GenerateEmailMessage(pair.Value);

                tasks.Add(
                    Task.Run(async () => await
                        SendEmailMessage(subject, 
                                         body, 
                                         settings.HostEmailUsername, 
                                         settings.HostEmailPassword, 
                                         pair.Key.Recipients)
                        )
                    );
            }
        }

        private static async Task SendEmailMessage(string subject, string body, string hostEmail, string hostPassword, string recipientsString)
        {
            string[] recipients = recipientsString.Split(',');

            var smtpClient = new SmtpClient("smtp.gmail.com") //smtp.yandex.ru, smtp.gmail.com
            {
                Port = 587, // google=587, yandex=465
                Credentials = new NetworkCredential(hostEmail, hostPassword),
                EnableSsl = true
            };
            foreach(var recipient in recipients)
            {
                smtpClient.Send(hostEmail, recipient, subject, body);
            }
        }
        private static string GenerateEmailMessage(List<Event> events)
        {
            string s = string.Empty;

            s += "G'day! Here's a list of things you really don't want to miss - ";
            s += "\n\n";

            foreach(var e in events)
            {
                s += $"{e.Name}, due {e.Date:dd.MMM.yyyy.}\n";
                s += $"  - {e.Description}\n";
                s += $"\n";
            }

            return s;
        }

        private static bool IsEligibleForEmail(Event e)
        {
            DateTime now = DateTime.Now;
            DateTime eventDate = e.Date;
            int[] notifications = ConvertNotificationsToInteger(e.Notifications);

            if (eventDate == DateTime.MinValue)
                return false;

            // check if today is the day for email for notification value
            for (int i = 0; i < notifications.Length; i++)
            {
                DateTime checkDate = eventDate.AddDays(-notifications[i]).Date;
                if (now.Date == checkDate)
                    return true;
            }

            return false;
        }
        private static int[] ConvertNotificationsToInteger(string notifications)
        {
            DateTime now = DateTime.Now;
            int[] resultArray;
            string[] s = notifications.Split(',');
            resultArray = new int[s.Length];

            if (s.Length == 0)
            {   // if there are no notifications set, we will return one element, zero.
                // this means that the notification will be sent only the same day that it occurs.
                resultArray = new int[1];
                resultArray[0] = 0;
                return resultArray;
            }

            for(int i = 0; i < s.Length; i++)
            { // notifications can be "week", "twoweek", "month", "year" or simple integer representing number of days
                switch (s[i])
                {
                    case "year":
                        resultArray[i] = 365 + Convert.ToInt32(DateTime.IsLeapYear(now.Year));
                        break;
                    case "month":
                        resultArray[i] = DateTime.DaysInMonth(now.Year, now.Month); //provereno radi
                        break;
                    case "twoweek":
                        resultArray[i] = 14;
                        break;
                    case "week":
                        resultArray[i] = 7;
                        break;
                    default:
                        resultArray[i] = Convert.ToInt32(s[i]);
                        break;
                }
            }

            return resultArray;
        }
    }
}

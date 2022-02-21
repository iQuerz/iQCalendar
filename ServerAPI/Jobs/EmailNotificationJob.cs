using Quartz;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ServerAPI.Jobs
{
    public class EmailNotificationJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            // Dictionary that holds list of eligible mailing events for each account
            Dictionary<Account, List<Event>> eventLists = new Dictionary<Account, List<Event>>();
            List<Account> accounts = (List<Account>)dataMap["accounts"];
            foreach(var account in accounts)
            {
                eventLists.Add(account, new List<Event>());
            }

            // Add events to each account in dictionary
            var events = (List<Event>)dataMap["events"];
            foreach(var @event in events)
            {
                if (IsEligibleForEmail(@event))
                    eventLists[accounts
                        .Where(a => a.AccountID == @event.AccountID).FirstOrDefault()]
                        .Add(@event);
            }

            // send the emails
            List<Task> tasks = new List<Task>();
            var settings = (Settings)dataMap["settings"];
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

            var smtpClient = new SmtpClient("smtp.gmail.com")//smtp.yandex.ru //smtp.gmail.com
            {
                Port = 587, // google=587 yandex=465
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

            s += "Lorem Ipsum daily:";
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
            DateTime eventDate = GetNextOccurance(e);
            int[] notifications = ConvertNotificationsToInteger(e.Notifications);

            if (eventDate == DateTime.MinValue)
                return false;

            // za svaki notification proveri da li je danas dan da se oglasi event.
            // check if today is the day
            for(int i = 0; i < notifications.Length; i++)
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
                        resultArray[i] = DateTime.DaysInMonth(now.Year, now.Month);
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
        private static DateTime GetNextOccurance(Event e)
        { // goes through finished iterations and calculates the next time the event should "appear"

            // if it's finished and not recurring, no need for email.
            if (e.IterationsFinished > 0 && e.RecurringType == RecurringType.NonRecurring)
                return DateTime.MinValue;

            DateTime date = e.Date;
            int iterations = e.IterationsFinished;
            for(int i = 0; i < iterations; i++)
            {
                date = date.AddDays(GetDaysFromRecurringType(e.RecurringType, date));
            }

            return date;
        }
        private static int GetDaysFromRecurringType(RecurringType type, DateTime date)
        {
            switch (type)
            {
                case RecurringType.Daily:
                    return 1;
                case RecurringType.Weekly:
                    return 7;
                case RecurringType.Monthly:
                    return DateTime.DaysInMonth(date.Year, date.Month);
                case RecurringType.Yearly:
                    return 365 + Convert.ToInt32(DateTime.IsLeapYear(date.Year));
                default:
                    return 0;
            }
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;

using Quartz;

using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Models.Types;

namespace ServerAPI.Jobs
{
    public class EventsUpdateJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.MergedJobDataMap;

            CalendarContext Context = (CalendarContext)dataMap["Context"];

            var events = Context.Events.Where(e => e.Finished == true
                                                && e.RecurringType != RecurringType.NonRecurring);

            foreach (var e in events)
            {
                e.Date = getNextDate(e);
                e.Finished = false;
            }

            await Context.SaveChangesAsync();
        }

        static DateTime getNextDate(Event e)
        {
            switch (e.RecurringType)
            {
                case RecurringType.Daily:
                    return e.Date.AddDays(1);
                case RecurringType.Weekly:
                    return e.Date.AddDays(7);
                case RecurringType.Monthly:
                    return e.Date.AddMonths(1);
                case RecurringType.Yearly:
                    return e.Date.AddMonths(12);
                default:
                    return e.Date;
            }
        }
    }
}

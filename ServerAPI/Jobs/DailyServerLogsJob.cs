using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Jobs
{
    public class DailyServerLogsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // if we want to save logs in a dynamic path we will need context to grab the path from the db.
            // that's why i left context datamap

            //JobKey key = context.JobDetail.Key;
            //JobDataMap dataMap = context.MergedJobDataMap;

            string log = await File.ReadAllTextAsync("Logs/temp.txt");
            await File.WriteAllTextAsync($"Logs/{DateTime.Now.ToLongDateString()}.log", log);
            await File.WriteAllTextAsync("Logs/temp.txt", string.Empty);

        }
    }
}

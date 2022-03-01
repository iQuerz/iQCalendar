using System;
using System.IO;
using System.Threading.Tasks;

using Quartz;

namespace ServerAPI.Jobs
{
    public class ServerLogsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // if we want to save logs in a dynamic path we will need context to grab the path from the db.
            // that's why i left context datamap

            //JobKey key = context.JobDetail.Key;
            //JobDataMap dataMap = context.MergedJobDataMap;

            DateTime now = DateTime.Now.AddDays(-1); // the thing about 00:00:01... yeah same here

            string log = await File.ReadAllTextAsync("Logs/temp.txt");
            await File.WriteAllTextAsync($"Logs/{now.ToLongDateString()}.log", log);
            await File.WriteAllTextAsync("Logs/temp.txt", string.Empty);

        }
    }
}

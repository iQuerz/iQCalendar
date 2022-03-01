using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Quartz;

namespace ServerAPI.Jobs
{
    public class ServerBackupJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            DateTime now = DateTime.Now.AddDays(-1); // we call it at 00:00:01 which is technically the next day, so we AddDays(-1).

            DirectoryInfo dir = new DirectoryInfo("Backup");

            //get latest backup
            var lastBackup = dir.GetFiles().OrderByDescending(f => f.CreationTime).ElementAt(0);

            //get current database
            FileInfo currentDB = new FileInfo("Data/iQCalendarDB.db");

            if(!fileCompare(currentDB, lastBackup)) //if their contents are different, make a new backup.
                File.Copy("Data/iQCalendarDB.db", $"Backup/iQCalendarBackup_{now:yyyy-MM-dd_HH-mm}.db");

            //delete old backups older than 12 months
            deleteOldBackups(dir);
        }

        static bool fileCompare(FileInfo f1, FileInfo f2)
        {
            var fs1 = f1.OpenRead();
            var fs2 = f2.OpenRead();

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }

            int f1byte, f2byte;
            do
            {
                f1byte = fs1.ReadByte();
                f2byte = fs2.ReadByte();
            }
            while ((f1byte == f2byte) && (f1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((f1byte - f2byte) == 0);
        }

        static void deleteOldBackups(DirectoryInfo dir)
        {// find a good way of deleting backups
            DateTime now = DateTime.Now.AddDays(-1);

            foreach (var file in dir.GetFiles())
            {
                string name = file.Name;
                string[] dateString = name.Split('_')[1].Split('-');

                int year = Convert.ToInt32(dateString[0]);
                int month = Convert.ToInt32(dateString[1]);
                int day = Convert.ToInt32(dateString[2]);

                DateTime date = new DateTime(year, month, day);

                if (date.AddMonths(12).Date <= now.Date) // every backup older than 12 months will be deleted
                    file.Delete();
            }
        }
    }
}

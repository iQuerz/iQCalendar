using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

using ServerAPI.Jobs;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JobsSetup js = new JobsSetup();
            Task.Run(async () => await js.setupJobs());

            CreateHostBuilder(args).Build().Run();

            Task.Run(async () => await js.stopJobs());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

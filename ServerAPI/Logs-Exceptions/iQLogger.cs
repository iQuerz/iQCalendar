using System;
using System.IO;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ServerAPI.Logs
{
    public class iQLogger
    {
        public static string log_path = "Logs/";

        public static void addLog(HttpRequest httpRequest, object obj = null)
        {
            string s = string.Empty;
            var now = DateTime.Now;

            s += $"{now.ToLongTimeString()} - \n";
            s += $"HTTP{httpRequest.Method} at {httpRequest.Path}, \n";
            s += $"from {httpRequest.HttpContext.Connection.LocalIpAddress}: \n";
            if(obj != null)
            {
                s += JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            s += $"\n\n";

            File.AppendAllText(log_path + "temp.txt", s);
        }
    }
}

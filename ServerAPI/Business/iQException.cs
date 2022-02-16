using System;
using System.IO;

namespace ServerAPI.Business
{
    public class iQException : Exception
    {
        public int StatusCode;
        public iQError Error;
        public iQException(iQError error, int code)
            : base(error.Error)
        {
            Error = error;
            StatusCode = code;

            Directory.CreateDirectory("Logs");
            string log = DateTime.Now.ToString("dd-MMM-yyyy H:mm:ss");
            log += "\n";
            log += error.Error;
            log += "\n";
            log += error.Details;
            log += "\n";
            log += StackTrace;

            File.WriteAllText($"Logs/{DateTime.Now}-{StatusCode}.log", log);
        }
    }
}

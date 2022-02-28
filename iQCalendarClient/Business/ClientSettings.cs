using Newtonsoft.Json;
using System.IO;

namespace iQCalendarClient.Business
{
    class ClientSettings
    {
        public string DefaultLanguage { get; set; }
        public string DateTimeFormat { get; set; }
        public int? AccountID { get; set; }
        public string CachedPassword { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }

        public void loadSettings()
        {
            string json = File.ReadAllText("settings.json");
            var settings = JsonConvert.DeserializeObject<ClientSettings>(json);

            DefaultLanguage = settings.DefaultLanguage;
            DateTimeFormat = settings.DateTimeFormat;
            AccountID = settings.AccountID;
            CachedPassword = settings.CachedPassword;
            ServerIP = settings.ServerIP;
            ServerPort = settings.ServerPort;
        }
        public void saveSettings()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("settings.json", json);
        }
    }
}

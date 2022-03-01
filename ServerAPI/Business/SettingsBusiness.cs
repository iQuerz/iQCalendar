using System.Threading.Tasks;

using ServerAPI.Data.Models;
using ServerAPI.Exceptions;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<Settings> GetSettings(string name)
        {
            var settings = await _context.Settings.FindAsync(name);

            if(settings == null)
            {
                var err = new iQError
                {
                    Error = "Invalid server name.",
                    Details = "S02 - Wrong server name. Cannot access settings without it."
                };
                throw new iQException(err, 400);
            }

            return settings;
        }

        public async Task<Settings> UpdateSettings(Settings settings)
        {
            var s = await _context.Settings.FindAsync(settings.ServerName);

            if(s == null)
            {
                var err = new iQError
                {
                    Error = "Settings not found.",
                    Details = "S03 - Specified settings do not exist."
                };
                throw new iQException(err, 404);
            }

            s.ServerName = settings.ServerName;
            s.ServerPassword = settings.ServerPassword;
            s.HostEmailPassword = settings.HostEmailPassword;
            s.HostEmailUsername = settings.HostEmailUsername;
            s.NotificationTime = settings.NotificationTime;
            s.Port = settings.Port;

            return s;
        }
    }
}

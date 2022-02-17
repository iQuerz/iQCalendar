using System.Linq;
using System.Threading.Tasks;

using ServerAPI.Data.Models;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<Settings> GetSettings(string name)
        {
            var settings = _context.Settings.Where(s => s.ServerName == name);

            if(settings == null)
            {
                var err = new iQError
                {
                    Error = "Invalid server name.",
                    Details = "S02 - Wrong server name. Cannot access settings without it."
                };
                throw new iQException(err, 400);
            }

            return settings.FirstOrDefault();
        }
        public async Task<Settings> UpdateSettings(Settings settings)
        {
            var s = await _context.Settings.FindAsync(settings.revisionID);

            if(s == null)
            {
                var err = new iQError
                {
                    Error = "Settings not found.",
                    Details = "S03 - Specified settings do not exist."
                };
                throw new iQException(err, 404);
            }

            s.HostEmailPassword = settings.HostEmailPassword;
            s.HostEmailUsername = settings.HostEmailUsername;
            s.NotificationTime = settings.NotificationTime;
            s.ServerName = settings.ServerName;
            s.Port = settings.Port;
            s.revisionDate = System.DateTime.Now;

            return s;
        }
    }
}

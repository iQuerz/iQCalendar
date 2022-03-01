using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using ServerAPI.Data;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        CalendarContext _context;

        public Logic(CalendarContext context)
        {
            _context = context;
        }

        internal async Task<bool> Authenticate(HttpRequest request)
        {
            // extract auth header value
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            // its hashed as username:password
            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.ClientPassword)
                return true;
            if (password == account.AdminPassword) // admin should have access to normal stuff too
                return true;

            return false;
        }
        internal async Task<bool> AuthenticateAdmin(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.AdminPassword)
                return true;

            return false;
        }
        internal async Task<bool> AuthenticateServer(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (header == null || !header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var settings = _context.Settings.FirstOrDefault(s => s.ServerName == username);
            if (password == settings.ServerPassword)
                return true;

            return false;
        }

        private void DecodeAuth(out string username, out string password, string header)
        {
            string encodedCredentials = header.Substring("Basic ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usrAndPass = encoding.GetString(Convert.FromBase64String(encodedCredentials));

            string[] tempArray = usrAndPass.Split(':');

            username = tempArray[0];
            password = tempArray[1];
        }

    }
}

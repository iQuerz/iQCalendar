using Microsoft.AspNetCore.Http;
using ServerAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        CalendarContext _context;

        public Logic(CalendarContext context)
        {
            _context = context;
        }

        public async Task<bool> Authenticate(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (!header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.ClientPassword)
                return true;

            return false;
        }
        public async Task<bool> AuthenticateAdmin(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (!header.StartsWith("Basic"))
                return false;

            string username, password;
            DecodeAuth(out username, out password, header);

            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);
            if (password == account.AdminPassword)
                return true;

            return false;
        }


        public void DecodeAuth(out string username, out string password, string header)
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

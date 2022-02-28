using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ServerAPI.Data.Models;
using ServerAPI.Exceptions;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<Account> GetAccount(string username)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);

            if (account == null)
            {
                var err = new iQError
                {
                    Error = "Account not found.",
                    Details = "A01 - The specified account does not exist in our database. Try with a different one."
                };
                throw new iQException(err, 404);
            }

            return account;
        }

        public async Task<List<Account>> GetAccounts(string name)
        {
            return _context.Accounts.ToList();
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var a = _context.Accounts.FirstOrDefault(a => a.Name == account.Name);

            if (a != null)
            {
                var err = new iQError
                {
                    Error = $"Account '{account.Name}' already exists.",
                    Details = "A02 - The specified account name already exists in our database. Try with a different one."
                };
                throw new iQException(err, 400);
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            var a = await _context.Accounts.FindAsync(account.AccountID);

            if (a == null)
            {
                var err = new iQError
                {
                    Error = "Account not found.",
                    Details = "A01 - The specified account does not exist in our database. Try with a different one."
                };
                throw new iQException(err, 404);
            }

            a.Name = account.Name;
            a.AdminPassword = account.AdminPassword;
            a.ClientPassword = account.ClientPassword;
            a.Recipients = account.Recipients;

            await _context.SaveChangesAsync();

            return a;
        }

        public async Task DeleteAccount(int accountID)
        {
            var a = await _context.Accounts.FindAsync(accountID);

            if (a == null)
            {
                var err = new iQError
                {
                    Error = "Account not found.",
                    Details = "A01 - The specified account does not exist in our database. Try with a different one."
                };
                throw new iQException(err, 404);
            }

            foreach(var e in _context.Events)
            {
                if (e.AccountID == a.AccountID)
                    _context.Events.Remove(e);
            }

            _context.Remove(a);
            await _context.SaveChangesAsync();
        }
    }
}

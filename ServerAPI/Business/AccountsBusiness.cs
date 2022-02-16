using ServerAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<Account> GetAccount(string username)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Name == username);

            if (account == null)
                throw new iQException($"Account {username} not found.");

            return account;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var a = _context.Accounts.FirstOrDefault(a => a.Name == account.Name);

            if (a != null)
                throw new iQException($"Account {account.Name} already exists.");

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            var a = await _context.Accounts.FindAsync(account.AccountID);

            if (a == null)
                throw new iQException($"Account {account.Name} not found.");

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
                throw new iQException($"Account not found.");

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

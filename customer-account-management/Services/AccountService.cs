using customer_account_management.Models;
using Microsoft.EntityFrameworkCore;

namespace customer_account_management.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool OpenAccount(Account model)
        {
            if (_context.Accounts.Any(a => a.Email == model.Email))
            {
                return false; // Account already exists
            }

            model.AccountNumber = GenerateAccountNumber();
            _context.Accounts.Add(model);
            _context.SaveChanges();
            return true;
        }

        private long GenerateAccountNumber()
        {
            return _context.Accounts.Any() ? _context.Accounts.Max(a => a.AccountNumber) + 1 : 100;
        }
    }
}

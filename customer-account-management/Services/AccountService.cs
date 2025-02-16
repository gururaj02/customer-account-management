using customer_account_management.Models;
using Microsoft.AspNetCore.Identity;
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

        // Register a new user
        public async Task<bool> RegisterAsync(Account account)
        {
            // Check if email already exists
            var existingUser = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == account.Email);
            if (existingUser != null) return false;

            // Directly save user (without hashing password)
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Accounts.AnyAsync(u => u.Email == email);
        }


        // Login user
        public async Task<Account?> LoginAsync(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}

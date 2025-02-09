using customer_account_management.Models;

namespace customer_account_management.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(Account model)
        {
            // Checking if the user already exists
            if (_context.Accounts.Any(u => u.Email == model.Email))
            {
                return false;
            }

            // Add user to database
            _context.Accounts.Add(model);
            _context.SaveChanges();

            return true;
        }

        // Authenticate User Login
        public Account? AuthenticateUser(string email, string password)
        {
            return _context.Accounts.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}

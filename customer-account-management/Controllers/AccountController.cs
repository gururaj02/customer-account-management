using customer_account_management.Models;
using customer_account_management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Import for session

namespace customer_account_management.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // Show Register Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Register Form Submission
        [HttpPost]
        public async Task<IActionResult> Register(Account model)
        {
            if (!ModelState.IsValid) return View(model);

            bool success = await _accountService.RegisterAsync(model);
            if (success)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Email already exists!");
            return View(model);
        }

        // Show Login Page
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _accountService.LoginAsync(email, password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(); // Stay on login page and show error
            }

            // Store user session data (optional)
            HttpContext.Session.SetString("FullName", user.FullName);

            return RedirectToAction("Dashboard");
        }


        // Show Dashboard Page (After Login)
        public IActionResult Dashboard()
        {
            var fullName = HttpContext.Session.GetString("FullName");

            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login");
            }

            ViewBag.FullName = fullName; // Pass name to view
            return View();
        }

        // Logout action to clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}

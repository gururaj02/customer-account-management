using customer_account_management.Models;
using customer_account_management.Services;
using Microsoft.AspNetCore.Mvc;

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

        // Handle Login Form Submission
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _accountService.LoginAsync(email, password);
            if (user != null)
            {
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // Dummy Dashboard Page (after login)
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}

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

        [HttpGet]
        public IActionResult OpenAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpenAccount(Account model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Data. Please fill all fields correctly.";
                return View(model);
            }

            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if session is missing
            }

            model.Email = userEmail;
            bool result = _accountService.OpenAccount(model);

            if (!result)
            {
                ViewData["Message"] = "Account already exists!";
                return View();
            }

            ViewData["Message"] = "Account created successfully!";
            return RedirectToAction("Dashboard", "User");
        }
    }
}

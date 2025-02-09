using customer_account_management.Models;
using customer_account_management.Services;
using Microsoft.AspNetCore.Mvc;

namespace customer_account_management.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Show Register Page
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration Form Submission
        [HttpPost("Register")]
        public IActionResult Register([FromForm] Account model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = _userService.RegisterUser(model);

            if (!result)
            {
                ViewData["Message"] = "User Already Exists!";
                return View();
            }

            return RedirectToAction("Login"); // Redirect to login after success
        }

        // Show Login Page
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] Account model)
        {
            var user = _userService.AuthenticateUser(model.Email, model.Password);

            if (user == null)
            {
                ViewData["Message"] = "Invalid Email or Password!";
                return View();
            }

            // Store user info in session (after fixing session issue)
            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction("Dashboard"); // Redirect after successful login
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            ViewData["UserEmail"] = userEmail;
            return View();
        }

    }
}

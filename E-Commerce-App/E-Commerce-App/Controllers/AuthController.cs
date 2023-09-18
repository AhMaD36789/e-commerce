using E_Commerce_App.Models;
using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    /// <summary>
    /// Controller responsible for authentication and user-related actions.
    /// </summary>
    public class AuthController : Controller
    {
        private IUserService userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="service">User service.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign-in manager.</param>
        public AuthController(IUserService service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            userService = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Displays the default view for authentication.
        /// </summary>
        /// <returns>The default authentication view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the view for user registration.
        /// </summary>
        /// <returns>The registration view.</returns>
        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Handles user registration when the registration form is submitted.
        /// </summary>
        /// <param name="register">User registration data.</param>
        /// <returns>
        ///   <list type="bullet">
        ///     <item>
        ///       <description>If registration is successful, redirects to the "Home/Index" action.</description>
        ///     </item>
        ///     <item>
        ///       <description>If registration fails, returns the registration view with validation errors.</description>
        ///     </item>
        ///   </list>
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUp(RegisterUserDTO register)
        {
            register.Roles = new List<string>() { "Users" };

            var user = await userService.Register(register, this.ModelState);
            if (ModelState.IsValid)
            {
                await userService.Authenticate(register.UserName, register.Password);
                return Redirect("/Home/Index");
            }
            else
            {
                return View(register);
            }
        }

        /// <summary>
        /// Handles user authentication when the login form is submitted.
        /// </summary>
        /// <param name="loginData">User login data.</param>
        /// <returns>
        ///   <list type="bullet">
        ///     <item>
        ///       <description>If authentication is successful, sets a welcome message and redirects to the "Home/Index" action.</description>
        ///     </item>
        ///     <item>
        ///       <description>If authentication fails, returns the "Index" view with an error message.</description>
        ///     </item>
        ///   </list>
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Authenticate(LoginDTO loginData)
        {
            var user = await userService.Authenticate(loginData.UserName, loginData.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                return View("Index", loginData);
            }
            else
            {
                TempData["AlertMessage"] = $"Welcome {loginData.UserName} in Tech Pioneers Website :)";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Logs the user out and redirects to the "Home/Index" action.
        /// </summary>
        /// <returns>Redirects to the "Home/Index" action.</returns>
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

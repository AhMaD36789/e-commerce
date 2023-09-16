using E_Commerce_App.Models;
using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_App.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IUserService service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager  )
        {
            userService = service; 
            _userManager = userManager; 
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUp(RegisterUserDTO register)
        {
            register.Roles = new List<string>() { "Administrator" };
            //register.Roles = new List<string>() { "Editor" };

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
        // call LogOut service to LogOut then go to home



        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

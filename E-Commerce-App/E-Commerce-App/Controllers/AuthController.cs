using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_App.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;

        public AuthController(IUserService service)
        {
            userService = service; ;
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
                this.ModelState.AddModelError("InvalidLogin", "Invalid login attempt");

                return RedirectToAction("Index");
            }
            else
            {
                string jwtToken = user.Token;

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(60)
                };

                // Set the JWT token in the cookie
                Response.Cookies.Append("authToken", jwtToken, cookieOptions);

                TempData["AlertMessage"] = $"Welcome {loginData.UserName} in Tech Pioneers Website :)";
                return RedirectToAction("Index", "Home");
            }
        }
        // call LogOut service to LogOut then go to home


        public IActionResult LogOut()
        {
            Response.Cookies.Delete("authToken");
            return RedirectToAction("Index", "Home");
        }
    }
}

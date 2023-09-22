using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    public class Empty : Controller
    {
        private readonly IEmail _email;
        public Empty(IEmail email)
        {
            _email = email;
        }
        public async Task<IActionResult> Index()
        {
            await _email.SendEmail();
            return View();
        }
    }
}

using E_Commerce_App.Models;
using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_App.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IAddImageToCloud _addImageToCloud;
        private readonly ICategory _categoryService;
        public HomeController(ILogger<HomeController> logger, IAddImageToCloud add, ICategory category)
        {
            _logger = logger;
            _addImageToCloud = add;
            _categoryService = category;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories(); 

            if (User.Identity.IsAuthenticated)
            {
                var model = new LoginDTO { UserName = User.Identity.Name };
                ViewData["Categories"] = categories;
                return View(model);
            }
            else
            {
                ViewData["Categories"] = categories;
                return View();
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
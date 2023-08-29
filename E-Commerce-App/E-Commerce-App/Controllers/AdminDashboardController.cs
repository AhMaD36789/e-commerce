using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ICategory _category;
        private readonly IProduct _product;

        public AdminDashboardController(ICategory category, IProduct product)
        {
            _category = category;
            _product = product;

        }


        // GET: CategoryController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var categories = await _category.GetAllCategories();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> CategoryDetails(int categoryID)
        {
            var categories = await _category.GetCategoryById(categoryID);

            var products = await _product.GetProductsByCategory(categoryID);

            CategoryProductVW productsByCategory = new CategoryProductVW()
            {
                Category = categories,
                Products = products
            };


            return View(productsByCategory);
        }

        public async Task<IActionResult> AllProducts(int categoryID)
        {
            var products = await _product.GetProductsByCategory(categoryID);
            return View(products);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> ProductDetails(int productID)
        {
            var productDetails = await _product.GetProductById(productID);
            return View(productDetails);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StoreDbContext _context;
        private readonly IProduct _product;
        private readonly IAddImageToCloud _addImageToCloud;
        private readonly ICategory _category;

        public ProductsController(StoreDbContext context, IProduct product, IAddImageToCloud addImageToCloud, ICategory category)
        {
            _context = context;
            _product = product;
            _addImageToCloud = addImageToCloud;
            _category = category;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var returnList = await _product.GetAllProducts();
            return View(returnList);
        }

        public async Task<IActionResult> GetProducts(int categoryID)
        {
            var products = await _product.GetProductsByCategory(categoryID);
            return View(products);
        }
        public async Task<IActionResult> ProductDetails(int productID)
        {
            var productDetails = await _product.GetProductById(productID);
            return View(productDetails);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Products == null)

            {
                return NotFound();
            }

            var product = await _product.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("ProductId,CategoryId,Name,Description,Price,StockQuantity,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _addImageToCloud.UploadProductImage(file, product);
                await _product.AddNewProduct(file, product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            //var product = await _context.Products.FindAsync(id);
            var product = await _product.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _category.GetAllCategories(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile file)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            var oldProduct = await _product.GetProductById(id);

            if (file != null)
            {
                // If a new file is uploaded, update the ProductImage property
                oldProduct = await _addImageToCloud.UploadProductImage(file, oldProduct);
            }
            else
                ModelState.Remove("file");

            // Update the rest of the properties
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            oldProduct.StockQuantity = product.StockQuantity;

            if (ModelState.IsValid)
            {
                try
                {
                    // Detach the oldProduct entity
                    _context.Entry(oldProduct).State = EntityState.Detached;

                    // Update the product entity
                    await _product.UpdateProduct(id, oldProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _category.GetAllCategories(), "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _product.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StoreDbContext.Products'  is null.");
            }

            await _product.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}

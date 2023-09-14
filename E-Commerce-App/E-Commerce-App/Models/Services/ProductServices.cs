using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class ProductServices : IProduct
    {
        private readonly StoreDbContext _context;
        public ProductServices(StoreDbContext db)
        {
            _context = db;
        }

        public async Task<Product> AddNewProduct(IFormFile file, Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with id {Id} not found.");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var newProduct = await _context.Products.ToListAsync();

            return newProduct;
        }

        public async Task<Product> GetProductById(int productID)
        {
            var newProduct = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == productID);

            return newProduct;
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryID)
        {
            var newProduct = await _context.Products
                .Where(id => id.CategoryId == categoryID)
                .ToListAsync();

            return newProduct;
        }

        public async Task<Product> UpdateProduct(int Id, Product product)
        {
            var newProduct = await _context.Products.FindAsync(Id);
            newProduct.ProductId = product.ProductId;
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.StockQuantity = product.StockQuantity;
            newProduct.CategoryId = product.CategoryId;
            if (product.ProductImage != null)
                newProduct.ProductImage = product.ProductImage;

            _context.Update(newProduct);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}

using E_Commerce_App.Data;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class ProductServices : IProduct
    {
        private readonly StoreDbContext _product;
        public ProductServices(StoreDbContext db)
        {
            _product = db;
        }
        public async Task<Product> AddNewProduct(Product product)
        {
            _product.Add(product);
            await _product.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int Id)
        {
            var product = await _product.Products.FindAsync(Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with id {Id} not found.");
            _product.Products.Remove(product);
            await _product.SaveChangesAsync();

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var newProduct = await _product.Products.ToListAsync();

            return newProduct;
        }

        public async Task<Product> GetProductById(int productID)
        {
            var newProduct = await _product.Products.FirstOrDefaultAsync(c => c.ProductId == productID);

            return newProduct;
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryID)
        {
            var newProduct = await _product.Products
                .Where(id => id.CategoryId == categoryID)
                .ToListAsync();

            return newProduct;
        }

        public Task<Product> UpdateProduct(int Id, Product productDTO)
        {
            throw new NotImplementedException();
        }
    }
}

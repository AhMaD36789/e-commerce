﻿using E_Commerce_App.Data;
using E_Commerce_App.Models.DTOs;
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
        public Task<ProductDTO> AddNewProduct(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(int Id)
        {
            Product product = await _product.Products.FindAsync(Id);
            _product.Entry(product).State = EntityState.Deleted;
            await _product.SaveChangesAsync();
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var newProduct = await _product.Products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ProductImage = p.ProductImage,
            }).ToListAsync();

            return newProduct;
        }

        public async Task<ProductDTO> GetProductById(int Id)
        {
            var newProduct = await _product.Products.FirstOrDefaultAsync(c => c.ProductId == Id);
            var newProductDTO = new ProductDTO
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Description = newProduct.Description,
                ProductImage = newProduct.ProductImage,
            };
            return newProductDTO;
        }

        public Task<ProductDTO> UpdateProduct(int Id, ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }
    }
}

﻿namespace E_Commerce_App.Models.Interfaces
{
    public interface IProduct
    {
        Task<Product> AddNewProduct(Product productDTO);
        Task<Product> GetProductById(int productID);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductsByCategory(int categoryID);
        Task<Product> UpdateProduct(int Id, Product productDTO);
        Task DeleteProduct(int Id);
    }
}

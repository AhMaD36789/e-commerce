using E_Commerce_App.Models.DTOs;

namespace E_Commerce_App.Models.Interfaces
{
    public interface IProduct
    {
        Task<ProductDTO> AddNewProduct(ProductDTO productDTO);
        Task<ProductDTO> GetProductById(int Id);
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> UpdateProduct(int Id, ProductDTO productDTO);
        Task DeleteProduct(int Id);
    }
}

using E_Commerce_App.Models.DTOs;

namespace E_Commerce_App.Models.ViewModels
{
    public class CategoryProductVW
    {
        public CategoryDTO Category { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}

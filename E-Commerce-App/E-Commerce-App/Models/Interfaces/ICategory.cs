using E_Commerce_App.Models.DTOs;

namespace E_Commerce_App.Models.Interfaces
{
    public interface ICategory
    {
        Task<CategoryDTO> CreateNewCategory(CategoryDTO categoryDTO);
        Task<CategoryDTO> GetCategoryById(int Id);
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> UpdateCategory(int Id, CategoryDTO categoryDTO);
        Task DeleteCategory(int Id);
    }
}

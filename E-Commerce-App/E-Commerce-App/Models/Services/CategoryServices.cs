using E_Commerce_App.Data;
using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Models.Services
{
    public class CategoryServices : ICategory
    {
        private readonly StoreDbContext _category;
        public CategoryServices(StoreDbContext DB)
        {
            _category = DB;
        }
        public Task<CategoryDTO> CreateNewCategory(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCategory(int Id)
        {
            Category category = await _category.Categories.FindAsync(Id);
            _category.Entry(category).State = EntityState.Deleted;
            await _category.SaveChangesAsync();
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var newList = await _category.Categories.Select(c => new CategoryDTO
            {

                CategoryId = c.CategoryId,
                Name = c.Name,

            }).ToListAsync();
            return newList;
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryID)
        {
            var newCategory = await _category.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryID);
            var CategoryDTO = new CategoryDTO
            {
                CategoryId = categoryID,
                Name = newCategory.Name
            };
            return CategoryDTO;
        }

        public Task<CategoryDTO> UpdateCategory(int Id, CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}

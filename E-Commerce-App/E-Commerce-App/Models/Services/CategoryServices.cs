using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CategoryServices : ICategory
{
    private readonly StoreDbContext _context;

    public CategoryServices(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateNewCategory(Category category)
    {
        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        category.CategoryId = category.CategoryId;

        return category;
    }

    public async Task DeleteCategory(int Id)
    {
        var category = await _context.Categories.FindAsync(Id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();

        return categories;
    }

    public async Task<Category> GetCategoryById(int categoryID)
    {
        var category = await _context.Categories.FindAsync(categoryID);

        if (category == null)
        {
            return null;
        }

        return category;
    }

    public async Task<Category> UpdateCategory(int Id, Category categoryDTO)
    {
        var category = await _context.Categories.FindAsync(Id);

        if (category == null)
        {
            return null;
        }

        category.Name = categoryDTO.Name;
        if (categoryDTO.imgURL != null)
            category.imgURL = categoryDTO.imgURL;

        _context.Categories.Update(category);

        await _context.SaveChangesAsync();

        return categoryDTO;
    }
}

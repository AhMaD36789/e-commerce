﻿using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CategoriesController : Controller
{
    private readonly ICategory _category;
    private readonly IProduct _product;

    public CategoriesController(ICategory category, IProduct product)
    {
        _category = category;
        _product = product;
    }

    // GET: Categories1
    public async Task<IActionResult> Index()
    {
        var categories = await _category.GetAllCategories();
        return categories != null ? View(categories) : Problem("Entity set 'StoreDbContext.Categories'  is null.");
    }

    // GET: CategoryController/Details/5
    public async Task<IActionResult> CategoryDetails(int categoryID)
    {
        var category = await _category.GetCategoryById(categoryID);

        var products = await _product.GetProductsByCategory(categoryID);

        CategoryProductVM productsByCategory = new CategoryProductVM()
        {
            Category = category,
            Products = products
        };
        return View(productsByCategory);
    }

    // GET: Categories1/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _category.GetCategoryById(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Categories1/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories1/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            await _category.CreateNewCategory(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Categories1/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _category.GetCategoryById(id.Value);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
    {
        if (id != category.CategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _category.UpdateCategory(id, category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(category.CategoryId))
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
        return View(category);
    }

    // GET: Categories1/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _category.GetCategoryById(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories1/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _category.DeleteCategory(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CategoryExists(int id)
    {
        var category = await _category.GetCategoryById(id);
        return category != null;
    }
}

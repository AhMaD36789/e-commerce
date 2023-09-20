using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CartViewComponent : ViewComponent
{
    private readonly IProduct _product;

    public CartViewComponent(IProduct product)
    {
        _product = product;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var productIdsCookie = HttpContext.Request.Cookies["productIds"];

        List<int> productIds;
        if (productIdsCookie != null)
        {
            productIds = JsonConvert.DeserializeObject<List<int>>(productIdsCookie);
        }

        else
        {
            productIds = new List<int>();
        }

        var products = new List<Product>();
        foreach (var id in productIds)
        {
            var product = await _product.GetProductById(id);
            products.Add(product);
        }

        return View(products);
    }
}

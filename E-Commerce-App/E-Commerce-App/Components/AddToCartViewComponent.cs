using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce_App.Components
{
    public class AddToCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int productId)
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
            productIds.Add(productId);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // Set expiry to 30 days
                IsEssential = true
            };

            HttpContext.Response.Cookies.Append("productIds", JsonConvert.SerializeObject(productIds), options);

            return View(productIds);
        }
    }

}

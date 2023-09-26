using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace E_Commerce_App.Pages.OrderConfirmation
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrder _order;
        private readonly IProduct _product;

        public OrderConfirmationModel(UserManager<ApplicationUser> user, IOrder order, IProduct product)
        {
            _userManager = user;
            _order = order;
            _product = product;
        }
        [BindProperty]
        public Order OrderConfirmed { get; set; }

        public List<CartItem> formCartItems { get; set; }

        public async Task OnGetAsync(decimal totalPrice)
        {
            var productIdsCookie = HttpContext.Request.Cookies["productIds"];

            Dictionary<int, int> productQuantities = new Dictionary<int, int>();

            if (productIdsCookie != null)
            {
                try
                {
                    productQuantities = JsonConvert.DeserializeObject<Dictionary<int, int>>(productIdsCookie);
                }
                catch (JsonSerializationException)
                {
                    var productIds = JsonConvert.DeserializeObject<List<int>>(productIdsCookie);

                    foreach (var id in productIds)
                    {
                        if (!productQuantities.ContainsKey(id))
                        {
                            productQuantities[id] = 1;
                        }
                        else
                        {
                            productQuantities[id]++;
                        }
                    }
                }
            }

            formCartItems = new List<CartItem>();

            foreach (var item in productQuantities)
            {
                var product = await _product.GetProductById(item.Key);
                var cartItem = new CartItem
                {
                    Product = product,
                    Quantity = item.Value
                };
                formCartItems.Add(cartItem);
            }

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            OrderConfirmed = new Order()
            {
                OrderDate = DateTime.Now,
                UserId = userID,
                TotalPrice = totalPrice,
                CartItems = formCartItems
            };

        }

        public async Task OnPost(Order order)
        {


            await _order.Create(order);
            RedirectToAction("Index", "Home");
        }

    }
}

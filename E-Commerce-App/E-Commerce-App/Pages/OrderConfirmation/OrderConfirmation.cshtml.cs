using E_Commerce_App.Models;
using E_Commerce_App.Models.Interfaces;
using E_Commerce_App.Models.Services;
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
        private readonly IEmail _email;
        private readonly IPaymentService _paymentService;

        public OrderConfirmationModel(UserManager<ApplicationUser> user, IOrder order, IProduct product, IEmail email, IPaymentService payment)
        {
            _userManager = user;
            _order = order;
            _product = product;
            _email = email;
            _paymentService = payment;
        }
        [BindProperty]
        public Order OrderConfirmed { get; set; }

        public List<CartItem> formCartItems { get; set; }

        public async Task OnGetAsync()
        {
            var productIdsCookie = HttpContext.Request.Cookies["productIds"];

            Dictionary<int, int> productQuantities = new Dictionary<int, int>();

            if (productIdsCookie != null)
            {
                productQuantities = JsonConvert.DeserializeObject<Dictionary<int, int>>(productIdsCookie);
            }

            formCartItems = new List<CartItem>();
            decimal? totalPrice = 0;

            foreach (var item in productQuantities)
            {
                var product = await _product.GetProductById(item.Key);
                var cartItem = new CartItem
                {
                    Product = product,
                    Quantity = item.Value,
                };
                totalPrice += product.Price * item.Value;
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

            await _order.Create(OrderConfirmed);

        }

        public async Task<IActionResult> OnPost(Order order)
        {
            order = await _order.Update(order.ID, order);

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            await _email.SendEmailOrderSummery(userEmail, userName, order);

            var session = await _paymentService.PaymentProcess(order);

            Response.Headers.Add("Location", session.Url);

            //if (Request.Cookies["productIds"] != null)
            //{
            //    Response.Cookies.Delete("productIds");
            //}

            return new StatusCodeResult(303);
        }

    }
}

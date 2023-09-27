﻿using Stripe;
using Stripe.Checkout;

namespace E_Commerce_App.Models.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<Session> PaymentProcess(Order order)
        {
            StripeConfiguration.ApiKey = "sk_test_51Nu9apGTsxCj81xc4G7NzFGmwMldXKwNwRsxl2dQdabXZJ8VITVSQlpREi0j8qy8qQwdMKPo0FhOVvvUlst2Bi8900rdC6MHnn";

            string domain = "https://localhost:7198/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "OrderConfirmed/OrderConfirmation",
                CancelUrl = domain + "Cart/Index",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in order.CartItems)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            var session = service.Create(options);

            return session;
        }
    }
}
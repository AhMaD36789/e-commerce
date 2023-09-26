using Stripe;
using Stripe.Checkout;

namespace E_Commerce_App.Models.Services
{
    public class PaymentService
    {
        public async Task PaymentProcess()
        {
            StripeConfiguration.ApiKey = "sk_test_51Nu9apGTsxCj81xc4G7NzFGmwMldXKwNwRsxl2dQdabXZJ8VITVSQlpREi0j8qy8qQwdMKPo0FhOVvvUlst2Bi8900rdC6MHnn";

            var options = new SessionCreateOptions
            {
                SuccessUrl = "",
                CancelUrl = "",
                LineItems = new List<SessionLineItemOptions>
                { new SessionLineItemOptions
                {
                    Price = "",
                    Quantity = 2,
                },
            },
                Mode = "Payment",
            };

            var service = new SessionService();
            service.Create(options);

        }
    }
}

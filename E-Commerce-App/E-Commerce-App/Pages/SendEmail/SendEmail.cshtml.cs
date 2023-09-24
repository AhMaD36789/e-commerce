using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace E_Commerce_App.Pages.SendEmail
{
    public class SendEmailModel : PageModel
    {
        private readonly IEmail _email;

        public SendEmailModel(IEmail email)
        {
            _email = email;
        }

        public async Task<IActionResult> OnGet()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            await _email.SendEmail(userEmail);
            return RedirectToAction("Index", "Home");
        }
    }
}

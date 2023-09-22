using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Pages.EmailPage
{
    public class EmailPageModel : PageModel
    {
        [BindProperty]
        public Reciever Reciever { get; set; }
        private readonly IEmail _email;

        public EmailPageModel(IEmail email)
        {
            _email = email;
        }
        public void OnGet()
        {
        }

    }
    public class Reciever
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

    }
}

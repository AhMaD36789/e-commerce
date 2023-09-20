using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models.DTOs
{
    public class CartDTO
    {
        public int UserId { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}

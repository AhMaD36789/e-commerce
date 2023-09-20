using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }

       
    }
}

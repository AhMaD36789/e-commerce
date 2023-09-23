using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }

        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public List<Product>? Products { get; set; }
    }
}

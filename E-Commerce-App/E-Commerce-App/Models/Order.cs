using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models
{
    public class Order
    {
        [Display(Name = "Phone Numbe")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        public string? City { get; set; }


        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }


        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }


        [Key]
        public int OrderID { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Total Price")]
        public decimal? TotalPrice { get; set; }
        public List<Dictionary<int, int>>? CartItems { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_App.Models.DTOs
{
    public class ProductDTO
    {
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        //public int StockQuantity { get; set; }

        [Display(Name = "Image")]
        public string ProductImage { get; set; }
        //public byte[] ProductImage { get; set; }

        // Navigation property 
       // public Category? Category { get; set; }
    }
}

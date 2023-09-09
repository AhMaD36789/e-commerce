using E_Commerce_App.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryId = 1,
                    Name = "Laptops",
                },
                 new Category
                 {
                     CategoryId = 2,
                     Name = "Accessories",
                 },
                 new Category
                 {
                     CategoryId = 3,
                     Name = "Screens",
                 }
                );
            modelBuilder.Entity<Product>().HasData(
                new List<Product>
                    {
                    new Product
                     {
                         ProductId = 1,
                         Name = "MacBook Pro",
                         Description = "Powerful laptop for professionals",
                         Price = 1499,
                         StockQuantity = 50,
                         CategoryId = 1,
                         ProductImage = null
                     },
                     new Product
                     {
                         ProductId = 2,
                         Name = "Dell XPS 13",
                         Description = "Sleek and high-performance laptop",
                         Price = 1299,
                         StockQuantity = 40,
                         CategoryId = 1,
                         ProductImage = null
                     },
                     new Product
                     {
                         ProductId = 3,
                         Name = "Lenovo LEGION 5",
                         Description = "Gaming laptop",
                         Price = 999,
                         StockQuantity = 35,
                         CategoryId = 1,
                         ProductImage = null
                     },
                           new Product
                             {
                                 ProductId = 4,
                                 Name = "Laptop Bag",
                                 Description = "Stylish and durable laptop carrying bag",
                                 Price = 49,
                                 StockQuantity = 200,
                                 CategoryId = 2,
                                 ProductImage = null
                             },
                             new Product
                             {
                                 ProductId = 5,
                                 Name = "Wireless Mouse",
                                 Description = "Ergonomic wireless mouse",
                                 Price = 19,
                                 StockQuantity = 150,
                                 CategoryId = 2,
                                 ProductImage = null
                             },
                             new Product
                             {
                                 ProductId = 6,
                                 Name = "Laptop Stand",
                                 Description = "Adjustable laptop stand for better ergonomics",
                                 Price = 29,
                                 StockQuantity = 100,
                                 CategoryId = 2,
                                 ProductImage = null
                             },
                        new Product
                        {
                            ProductId = 7,
                            Name = "24-Inch Monitor",
                            Description = "Full HD monitor for crisp visuals",
                            Price = 199,
                            StockQuantity = 30,
                            CategoryId = 3,
                            ProductImage = null
                        },
                        new Product
                        {
                            ProductId = 8,
                            Name = "27-Inch 4K Monitor",
                            Description = "High-resolution 4K monitor with vibrant colors",
                            Price = 399,
                            StockQuantity = 20,
                            CategoryId = 3,
                            ProductImage = null
                        },
                        new Product
                        {
                            ProductId = 9,
                            Name = "Dual Monitor Stand",
                            Description = "Sturdy stand to hold two monitors for multitasking",
                            Price = 89,
                            StockQuantity = 10,
                            CategoryId = 3,
                            ProductImage = null
                        }
                        });
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}

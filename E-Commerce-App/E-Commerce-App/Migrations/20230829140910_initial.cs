using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce_App.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Laptops" },
                    { 2, "Accessories" },
                    { 3, "Screens" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "ProductImage", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Powerful laptop for professionals", "MacBook Pro", 1499m, null, 50 },
                    { 2, 1, "Sleek and high-performance laptop", "Dell XPS 13", 1299m, null, 40 },
                    { 3, 1, "Gaming laptop", "Lenovo LEGION 5", 999m, null, 35 },
                    { 4, 2, "Stylish and durable laptop carrying bag", "Laptop Bag", 49m, null, 200 },
                    { 5, 2, "Ergonomic wireless mouse", "Wireless Mouse", 19m, null, 150 },
                    { 6, 2, "Adjustable laptop stand for better ergonomics", "Laptop Stand", 29m, null, 100 },
                    { 7, 3, "Full HD monitor for crisp visuals", "24-Inch Monitor", 199m, null, 30 },
                    { 8, 3, "High-resolution 4K monitor with vibrant colors", "27-Inch 4K Monitor", 399m, null, 20 },
                    { 9, 3, "Sturdy stand to hold two monitors for multitasking", "Dual Monitor Stand", 89m, null, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

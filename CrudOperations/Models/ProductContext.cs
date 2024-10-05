using System.Data.Entity;

namespace CrudOperations.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=CategoryContext") // Ensure this matches your connection string name
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } // Add this line to access categories
    }
}

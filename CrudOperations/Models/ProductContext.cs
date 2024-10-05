using System.Data.Entity;

namespace CrudOperations.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=CategoryContext") 
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
    }
}

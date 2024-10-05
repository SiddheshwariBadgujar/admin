using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudOperations.Models
{
    public class CategoryContext : DbContext
    {
        public CategoryContext() : base("name=CategoryContext") 
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

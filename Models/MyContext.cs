using Microsoft.EntityFrameworkCore;

namespace ProductAndCategories_2.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> products {get;set;}
        public DbSet<Category> categories {get;set;}

        public DbSet<Association> associations{get;set;}
    }
}
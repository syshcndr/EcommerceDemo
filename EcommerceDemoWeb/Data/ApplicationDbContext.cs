using EcommerceDemoWeb.Areas.Admin.Models;
using EcommerceDemoWeb.Controllers;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemoWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}

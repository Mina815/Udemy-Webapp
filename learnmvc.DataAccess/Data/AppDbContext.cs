using learnmvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace learnmvc.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
                
        }
        public DbSet<Course> courses { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<CoverType> CoverTypes { get; set; } 
        public DbSet<Product> Products { get; set; } 
    }
}

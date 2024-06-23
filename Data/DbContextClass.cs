using Microsoft.EntityFrameworkCore;
using WebAPI_Using_SP.Models;

namespace WebAPI_Using_SP.Data
{
    public class DbContextClass:DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("conStr"));
        }
        public DbSet<Product> Products { get; set; }
    }
}

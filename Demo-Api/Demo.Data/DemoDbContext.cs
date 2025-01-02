using Microsoft.EntityFrameworkCore;
using Demo.Core.StoreProcedure;
using Demo.Core.Tables;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        }

        public DbSet<Banner> rntBanner { get; set; }
        public DbSet<City> rntCity { get; set; }
        public DbSet<CMS> rntCMS { get; set; } 
        public DbSet<ContactUs> rntContactUs { get; set; }
        public DbSet<Country> rntCountry { get; set; }
        public DbSet<Customer> rntCustomer { get; set; }
        public DbSet<State> rntState { get; set; }
        public DbSet<User> rntUser { get; set; }
        public DbSet<Faq> rntFaq { get; set; }
        public DbSet<SPData> SP { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using PostaAPI.Classes;

namespace PostaAPI.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserJwtToken> UserJwtToken { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Countries> Countries { get; set; } 
        public DbSet<Cities> Cities { get; set; }   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

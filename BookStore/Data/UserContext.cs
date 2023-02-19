using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class UserContext:DbContext
    {
        
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToContainer("Users").HasPartitionKey(u => u.id);
            modelBuilder.Entity<UserModel>().OwnsMany(b => b.Books);
        }
    }
}

using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Data
{
    public class BookContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }
        public string _endpoint { get; set; } = " ";
        public string _key { get; set; } = " ";
        public BookContext(string endpoint,string key)
        {
            _endpoint= endpoint;
            _key= key;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseCosmos(
            _endpoint,
            _key,
            "BookStore"
            );
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>().ToContainer("Books").HasPartitionKey(e => e.id);
        }
    }



}


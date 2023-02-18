using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookContext : DbContext
    {

        public DbSet<BookModel> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseCosmos(
            "https://roshanliu.documents.azure.com:443/",
            "1sg7cQ45BfiueaFKsCcsU8EP9eQ3km9p8ce9MaoGV7ZVfPXiQje95bhlzjB33IkLcflS1WhpgphlACDbMMuwXw==",
            "BookStore"
            );
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>().ToContainer("Books").HasPartitionKey(e => e.id);
        }
    }



}


using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class EventContext:DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }
        public DbSet<EventModel> Events { get; set; }
    }
}

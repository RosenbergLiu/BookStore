namespace BookStore.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string? BookId { get; set; }
        public bool Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public string? UserId { get; set; }
    }
}

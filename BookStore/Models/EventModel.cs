namespace BookStore.Models
{
    public class EventModel
    {
        public string? BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public string? UserId { get; set; }
    }
}

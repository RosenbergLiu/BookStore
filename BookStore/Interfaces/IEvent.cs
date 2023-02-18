using Microsoft.Extensions.Logging;

namespace BookStore.Interfaces
{
    public interface IEvent
    {
        public record Reserved(string BookId, int Quantity, DateTime DateTime, string UserId) : IEvent;
        public record Returned(string BookId, int Quantity, DateTime DateTime, string UserId) : IEvent;
    }
}

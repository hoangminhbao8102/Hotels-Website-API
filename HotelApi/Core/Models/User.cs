using HotelApi.Core.Contracts;

namespace HotelApi.Core.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Hotel>? Hotels { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}

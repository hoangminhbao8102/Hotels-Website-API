using HotelApi.Core.Contracts;

namespace HotelApi.Core.Models
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Status { get; set; } = null!;

        // Navigation properties
        public Hotel? Hotel { get; set; }
        public RoomType? RoomType { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}

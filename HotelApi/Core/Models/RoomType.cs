using HotelApi.Core.Contracts;

namespace HotelApi.Core.Models
{
    public class RoomType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        // Navigation properties
        public ICollection<Room>? Rooms { get; set; }
    }
}

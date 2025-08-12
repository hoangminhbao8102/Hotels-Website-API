using HotelManagement.Core.Contracts;

namespace HotelManagement.Core.Models
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Hotel? Hotel { get; set; }
        public User? User { get; set; }
    }
}

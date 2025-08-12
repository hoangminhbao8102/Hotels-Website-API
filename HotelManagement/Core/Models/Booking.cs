using HotelManagement.Core.Contracts;

namespace HotelManagement.Core.Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Room? Room { get; set; }
        public Payment? Payment { get; set; }
    }
}

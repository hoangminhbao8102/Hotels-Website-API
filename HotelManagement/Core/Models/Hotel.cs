using HotelManagement.Core.Contracts;

namespace HotelManagement.Core.Models
{
    public class Hotel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User? Owner { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<HotelService>? HotelServices { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}

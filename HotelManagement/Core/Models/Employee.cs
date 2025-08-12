using HotelManagement.Core.Contracts;

namespace HotelManagement.Core.Models
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // Navigation properties
        public Hotel? Hotel { get; set; }
    }
}

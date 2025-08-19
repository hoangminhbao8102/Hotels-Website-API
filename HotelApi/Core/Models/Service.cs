using HotelApi.Core.Contracts;

namespace HotelApi.Core.Models
{
    public class Service : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        // Navigation properties
        public ICollection<HotelService>? HotelServices { get; set; }
    }
}

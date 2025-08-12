namespace HotelManagement.Core.Models
{
    public class HotelService
    {
        public int HotelId { get; set; }
        public int ServiceId { get; set; }

        // Navigation properties
        public Hotel? Hotel { get; set; }
        public Service? Service { get; set; }
    }
}

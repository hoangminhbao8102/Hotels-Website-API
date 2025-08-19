using HotelApi.Core.Contracts;

namespace HotelApi.Core.Models
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;

        // Navigation properties
        public Booking? Booking { get; set; }
    }
}

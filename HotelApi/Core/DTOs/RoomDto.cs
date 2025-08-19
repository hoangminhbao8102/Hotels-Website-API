namespace HotelApi.Core.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}

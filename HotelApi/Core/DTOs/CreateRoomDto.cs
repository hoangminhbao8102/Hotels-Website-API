namespace HotelApi.Core.DTOs
{
    public class CreateRoomDto
    {
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}

namespace HotelApi.Core.DTOs
{
    public class CreateEmployeeDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}

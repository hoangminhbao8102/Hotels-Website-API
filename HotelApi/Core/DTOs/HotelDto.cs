﻿namespace HotelApi.Core.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OwnerId { get; set; }
    }
}

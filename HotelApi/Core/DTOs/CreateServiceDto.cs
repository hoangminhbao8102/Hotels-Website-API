﻿namespace HotelApi.Core.DTOs
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}

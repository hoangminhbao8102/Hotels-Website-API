using System.ComponentModel.DataAnnotations;

namespace HotelApi.Core.DTOs
{
    public class CreateReviewDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "HotelId phải > 0")]
        public int HotelId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UserId phải > 0")]
        public int UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}

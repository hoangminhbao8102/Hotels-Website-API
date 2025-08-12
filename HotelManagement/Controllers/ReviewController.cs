using HotelManagement.Core.DTOs;
using HotelManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            var reviews = await _reviewService.GetByHotelAsync(hotelId);
            return Ok(ResponseDto<IEnumerable<ReviewDto>>.SuccessResponse(reviews));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            var review = await _reviewService.CreateAsync(dto);
            return Ok(ResponseDto<ReviewDto>.SuccessResponse(review, "Đánh giá thành công"));
        }
    }
}

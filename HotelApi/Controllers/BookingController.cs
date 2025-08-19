using HotelApi.Core.DTOs;
using HotelApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            var booking = await _bookingService.CreateAsync(dto);
            if (booking == null)
                return BadRequest(ResponseDto<BookingDto>.FailureResponse("Không thể tạo booking", 400));

            return Ok(ResponseDto<BookingDto>.SuccessResponse(booking, "Tạo booking thành công"));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var bookings = await _bookingService.GetByUserAsync(userId);
            return Ok(ResponseDto<IEnumerable<BookingDto>>.SuccessResponse(bookings));
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var success = await _bookingService.CancelAsync(id);
            if (!success)
                return NotFound(ResponseDto<bool>.FailureResponse("Không tìm thấy booking", 404));

            return Ok(ResponseDto<bool>.SuccessResponse(true, "Hủy booking thành công"));
        }
    }
}

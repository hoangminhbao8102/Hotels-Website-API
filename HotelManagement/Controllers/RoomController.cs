using HotelManagement.Core.DTOs;
using HotelManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            var rooms = await _roomService.GetByHotelAsync(hotelId);
            return Ok(ResponseDto<IEnumerable<RoomDto>>.SuccessResponse(rooms));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var room = await _roomService.CreateAsync(dto);
            return Ok(ResponseDto<RoomDto>.SuccessResponse(room, "Tạo phòng thành công"));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            var success = await _roomService.UpdateStatusAsync(id, status);
            if (!success)
                return NotFound(ResponseDto<bool>.FailureResponse("Không tìm thấy phòng", 404));

            return Ok(ResponseDto<bool>.SuccessResponse(true, "Cập nhật trạng thái phòng thành công"));
        }
    }
}

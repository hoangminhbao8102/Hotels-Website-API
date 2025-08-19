using HotelApi.Core.DTOs;
using HotelApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelService.GetAllAsync();
            return Ok(ResponseDto<IEnumerable<HotelDto>>.SuccessResponse(hotels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(ResponseDto<HotelDto>.FailureResponse("Không tìm thấy khách sạn", 404));

            return Ok(ResponseDto<HotelDto>.SuccessResponse(hotel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHotelDto dto)
        {
            var hotel = await _hotelService.CreateAsync(dto);
            return Ok(ResponseDto<HotelDto>.SuccessResponse(hotel, "Tạo khách sạn thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hotelService.DeleteAsync(id);
            if (!result)
                return NotFound(ResponseDto<bool>.FailureResponse("Không tìm thấy khách sạn", 404));

            return Ok(ResponseDto<bool>.SuccessResponse(true, "Xóa khách sạn thành công"));
        }
    }
}

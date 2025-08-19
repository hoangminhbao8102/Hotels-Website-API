using HotelApi.Core.DTOs;
using HotelApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(ResponseDto<IEnumerable<ServiceDto>>.SuccessResponse(services));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
        {
            var service = await _serviceService.CreateAsync(dto);
            return Ok(ResponseDto<ServiceDto>.SuccessResponse(service, "Tạo dịch vụ thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.DeleteAsync(id);
            if (!result)
                return NotFound(ResponseDto<bool>.FailureResponse("Không tìm thấy dịch vụ", 404));

            return Ok(ResponseDto<bool>.SuccessResponse(true, "Xóa dịch vụ thành công"));
        }
    }
}

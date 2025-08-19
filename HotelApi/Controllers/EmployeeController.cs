using HotelApi.Core.DTOs;
using HotelApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            var employees = await _employeeService.GetByHotelAsync(hotelId);
            return Ok(ResponseDto<IEnumerable<EmployeeDto>>.SuccessResponse(employees));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);
            return Ok(ResponseDto<EmployeeDto>.SuccessResponse(employee, "Tạo nhân viên thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _employeeService.DeleteAsync(id);
            if (!success)
                return NotFound(ResponseDto<bool>.FailureResponse("Không tìm thấy nhân viên", 404));

            return Ok(ResponseDto<bool>.SuccessResponse(true, "Xóa nhân viên thành công"));
        }
    }
}

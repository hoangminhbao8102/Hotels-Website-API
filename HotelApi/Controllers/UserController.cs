using HotelApi.Core.DTOs;
using HotelApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterAsync(dto);
            return Ok(ResponseDto<UserDto>.SuccessResponse(user, "Đăng ký thành công"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null)
                return StatusCode(401, ResponseDto<UserDto>.FailureResponse("Email hoặc mật khẩu không đúng", 401));

            return Ok(ResponseDto<UserDto>.SuccessResponse(user, "Đăng nhập thành công"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(ResponseDto<IEnumerable<UserDto>>.SuccessResponse(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(ResponseDto<UserDto>.FailureResponse("Không tìm thấy người dùng", 404));

            return Ok(ResponseDto<UserDto>.SuccessResponse(user));
        }
    }
}

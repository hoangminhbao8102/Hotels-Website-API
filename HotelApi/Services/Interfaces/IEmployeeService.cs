using HotelApi.Core.DTOs;

namespace HotelApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetByHotelAsync(int hotelId);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

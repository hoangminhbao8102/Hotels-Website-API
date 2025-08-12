using HotelManagement.Core.DTOs;

namespace HotelManagement.Services.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllAsync();
        Task<HotelDto> GetByIdAsync(int id);
        Task<HotelDto> CreateAsync(CreateHotelDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

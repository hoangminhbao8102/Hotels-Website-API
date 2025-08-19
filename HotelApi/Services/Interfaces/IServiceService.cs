using HotelApi.Core.DTOs;

namespace HotelApi.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

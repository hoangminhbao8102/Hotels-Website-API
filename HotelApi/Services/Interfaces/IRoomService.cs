using HotelApi.Core.DTOs;

namespace HotelApi.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetByHotelAsync(int hotelId);
        Task<RoomDto> CreateAsync(CreateRoomDto dto);
        Task<bool> UpdateStatusAsync(int roomId, string status);
    }
}

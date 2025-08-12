using HotelManagement.Core.DTOs;

namespace HotelManagement.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetByHotelAsync(int hotelId);
        Task<RoomDto> CreateAsync(CreateRoomDto dto);
        Task<bool> UpdateStatusAsync(int roomId, string status);
    }
}

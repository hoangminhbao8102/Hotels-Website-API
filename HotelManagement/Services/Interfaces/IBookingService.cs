using HotelManagement.Core.DTOs;

namespace HotelManagement.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateAsync(CreateBookingDto dto);
        Task<IEnumerable<BookingDto>> GetByUserAsync(int userId);
        Task<bool> CancelAsync(int bookingId);
    }
}

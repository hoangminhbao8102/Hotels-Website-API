using HotelApi.Core.DTOs;

namespace HotelApi.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateAsync(CreateBookingDto dto);
        Task<IEnumerable<BookingDto>> GetByUserAsync(int userId);
        Task<bool> CancelAsync(int bookingId);
    }
}

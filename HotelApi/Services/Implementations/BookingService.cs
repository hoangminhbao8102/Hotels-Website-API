using HotelApi.Core.DTOs;
using HotelApi.Data.Contexts;
using HotelApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly HotelDbContext _context;

        public BookingService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
        {
            // Lấy giá phòng từ RoomType để tính tiền
            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == dto.RoomId);

            if (room == null) return null!;

            var days = (dto.CheckOutDate - dto.CheckInDate).Days;
            var total = room.RoomType!.Price * days;

            var booking = new Core.Models.Booking
            {
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalAmount = total,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status
            };
        }

        public async Task<IEnumerable<BookingDto>> GetByUserAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    RoomId = b.RoomId,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalAmount = b.TotalAmount,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<bool> CancelAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using HotelManagement.Core.DTOs;
using HotelManagement.Data.Contexts;
using HotelManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDto>> GetByHotelAsync(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    HotelId = r.HotelId,
                    RoomTypeId = r.RoomTypeId,
                    RoomNumber = r.RoomNumber,
                    Status = r.Status
                })
                .ToListAsync();
        }

        public async Task<RoomDto> CreateAsync(CreateRoomDto dto)
        {
            var room = new Core.Models.Room
            {
                HotelId = dto.HotelId,
                RoomTypeId = dto.RoomTypeId,
                RoomNumber = dto.RoomNumber,
                Status = dto.Status
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new RoomDto
            {
                Id = room.Id,
                HotelId = room.HotelId,
                RoomTypeId = room.RoomTypeId,
                RoomNumber = room.RoomNumber,
                Status = room.Status
            };
        }

        public async Task<bool> UpdateStatusAsync(int roomId, string status)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) return false;

            room.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

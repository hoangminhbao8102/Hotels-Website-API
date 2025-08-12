using HotelManagement.Core.DTOs;
using HotelManagement.Data.Contexts;
using HotelManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly HotelDbContext _context;

        public HotelService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            return await _context.Hotels
                .Select(h => new HotelDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Domain = h.Domain,
                    Address = h.Address,
                    Description = h.Description,
                    OwnerId = h.OwnerId
                }).ToListAsync();
        }

        public async Task<HotelDto> GetByIdAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return null!;

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Domain = hotel.Domain,
                Address = hotel.Address,
                Description = hotel.Description,
                OwnerId = hotel.OwnerId
            };
        }

        public async Task<HotelDto> CreateAsync(CreateHotelDto dto)
        {
            var hotel = new Core.Models.Hotel
            {
                Name = dto.Name,
                Domain = dto.Domain,
                Address = dto.Address,
                Description = dto.Description,
                OwnerId = dto.OwnerId,
                CreatedAt = DateTime.Now
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Domain = hotel.Domain,
                Address = hotel.Address,
                Description = hotel.Description,
                OwnerId = hotel.OwnerId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

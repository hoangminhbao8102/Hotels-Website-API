using HotelManagement.Core.DTOs;
using HotelManagement.Data.Contexts;
using HotelManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly HotelDbContext _context;

        public ServiceService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            return await _context.Services
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price
                }).ToListAsync();
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var service = new Core.Models.Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

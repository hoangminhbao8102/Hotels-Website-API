using HotelManagement.Core.DTOs;
using HotelManagement.Data.Contexts;
using HotelManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HotelDbContext _context;

        public EmployeeService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetByHotelAsync(int hotelId)
        {
            return await _context.Employees
                .Where(e => e.HotelId == hotelId)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    HotelId = e.HotelId,
                    Name = e.Name,
                    Position = e.Position,
                    Phone = e.Phone
                }).ToListAsync();
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Core.Models.Employee
            {
                HotelId = dto.HotelId,
                Name = dto.Name,
                Position = dto.Position,
                Phone = dto.Phone
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                HotelId = employee.HotelId,
                Name = employee.Name,
                Position = employee.Position,
                Phone = employee.Phone
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using HotelApi.Core.Models;
using HotelApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Data.Seeders
{
    public static class DataSeeder
    {
        public static void SeedData(HotelDbContext context)
        {
            context.Database.Migrate();

            // Seed User Admin
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    FullName = "Admin User",
                    Email = "admin@example.com",
                    Password = "admin123", // Plain text cho test
                    Role = "Admin",
                    CreatedAt = DateTime.Now
                };
                context.Users.Add(admin);
                context.SaveChanges();
            }

            var owner = context.Users.First(u => u.Role == "Admin");

            // Seed Hotel
            if (!context.Hotels.Any())
            {
                context.Hotels.Add(new Hotel
                {
                    Name = "Grand Hotel",
                    Domain = "grandhotel.com",
                    Address = "123 Main Street",
                    Description = "Khách sạn sang trọng 5 sao với tiện nghi hiện đại.",
                    OwnerId = owner.Id,
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }

            var hotel = context.Hotels.First();

            // Seed RoomTypes
            if (!context.RoomTypes.Any())
            {
                context.RoomTypes.AddRange(
                    new RoomType { Name = "Standard", Description = "Phòng tiêu chuẩn", Price = 500000 },
                    new RoomType { Name = "Deluxe", Description = "Phòng cao cấp", Price = 1000000 },
                    new RoomType { Name = "Suite", Description = "Phòng hạng sang", Price = 2000000 }
                );
                context.SaveChanges();
            }

            var standard = context.RoomTypes.First(rt => rt.Name == "Standard");
            var deluxe = context.RoomTypes.First(rt => rt.Name == "Deluxe");

            // Seed Rooms
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Room { HotelId = hotel.Id, RoomTypeId = standard.Id, RoomNumber = "101", Status = "Available" },
                    new Room { HotelId = hotel.Id, RoomTypeId = standard.Id, RoomNumber = "102", Status = "Available" },
                    new Room { HotelId = hotel.Id, RoomTypeId = deluxe.Id, RoomNumber = "201", Status = "Booked" }
                );
                context.SaveChanges();
            }

            // Seed Services
            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service { Name = "Spa", Description = "Dịch vụ spa thư giãn", Price = 300000 },
                    new Service { Name = "Breakfast", Description = "Bữa sáng buffet", Price = 100000 },
                    new Service { Name = "Airport Pickup", Description = "Đưa đón sân bay", Price = 200000 }
                );
                context.SaveChanges();
            }

            // Liên kết Hotel-Services
            if (!context.HotelServices.Any())
            {
                var services = context.Services.ToList();
                foreach (var service in services)
                {
                    context.HotelServices.Add(new HotelService
                    {
                        HotelId = hotel.Id,
                        ServiceId = service.Id
                    });
                }
                context.SaveChanges();
            }

            // Seed Booking mẫu
            if (!context.Bookings.Any())
            {
                var roomBooked = context.Rooms.First(r => r.RoomNumber == "201");

                context.Bookings.Add(new Booking
                {
                    UserId = owner.Id,
                    RoomId = roomBooked.Id,
                    CheckInDate = DateTime.Today.AddDays(1),
                    CheckOutDate = DateTime.Today.AddDays(3),
                    TotalAmount = deluxe.Price * 2,
                    Status = "Confirmed",
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
}

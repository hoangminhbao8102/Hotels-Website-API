using HotelApi.Core.DTOs;
using HotelApi.Data.Contexts;
using HotelApi.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly HotelDbContext _context;

        public ReviewService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDto>> GetByHotelAsync(int hotelId)
        {
            return await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    HotelId = r.HotelId,
                    UserId = r.UserId,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToListAsync();
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.HotelId <= 0) throw new ArgumentException("HotelId phải > 0.");
            if (dto.UserId <= 0) throw new ArgumentException("UserId phải > 0.");
            if (dto.Rating < 1 || dto.Rating > 5) throw new ArgumentException("Rating phải trong [1..5].");

            // 1) Kiểm tra khóa ngoại tồn tại
            var hotelExists = await _context.Hotels
                .AsNoTracking()
                .AnyAsync(h => h.Id == dto.HotelId);
            if (!hotelExists)
                throw new KeyNotFoundException($"Hotel #{dto.HotelId} không tồn tại.");

            var userExists = await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
                throw new KeyNotFoundException($"User #{dto.UserId} không tồn tại.");

            // (tuỳ chọn) chặn 1 user review 1 hotel nhiều lần
            // var duplicated = await _context.Reviews.AsNoTracking()
            //     .AnyAsync(r => r.HotelId == dto.HotelId && r.UserId == dto.UserId, ct);
            // if (duplicated) throw new InvalidOperationException("Bạn đã đánh giá khách sạn này rồi.");

            var review = new Core.Models.Review
            {
                HotelId = dto.HotelId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Comment = dto.Comment?.Trim() ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);

            try
            {
                await _context.SaveChangesAsync();
            }
            // Bắt lỗi FK (SQL 547) để báo rõ ràng
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && sql.Number == 547)
            {
                throw new InvalidOperationException(
                    "Không thể tạo review vì HotelId/UserId không hợp lệ (vi phạm khóa ngoại).",
                    ex
                );
            }

            return new ReviewDto
            {
                Id = review.Id,
                HotelId = review.HotelId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }
    }
}

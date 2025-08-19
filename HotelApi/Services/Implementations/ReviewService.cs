using HotelApi.Core.DTOs;
using HotelApi.Data.Contexts;
using HotelApi.Services.Interfaces;
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
            var review = new Core.Models.Review
            {
                HotelId = dto.HotelId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return new ReviewDto
            {
                Id = review.Id,
                HotelId = review.HotelId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment
            };
        }
    }
}

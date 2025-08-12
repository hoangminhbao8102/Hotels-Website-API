using HotelManagement.Core.DTOs;

namespace HotelManagement.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetByHotelAsync(int hotelId);
        Task<ReviewDto> CreateAsync(CreateReviewDto dto);
    }
}

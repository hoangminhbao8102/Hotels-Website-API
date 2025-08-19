using HotelApi.Core.DTOs;

namespace HotelApi.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetByHotelAsync(int hotelId);
        Task<ReviewDto> CreateAsync(CreateReviewDto dto);
    }
}

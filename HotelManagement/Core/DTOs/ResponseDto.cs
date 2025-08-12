namespace HotelManagement.Core.DTOs
{
    /// <summary>
    /// Chuẩn hóa response API
    /// </summary>
    public class ResponseDto<T>
    {
        /// <summary>
        /// Thành công hay thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mã trạng thái HTTP (200, 400, 401, 404, 500)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Thông báo cho client (OK, lỗi, v.v.)
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Dữ liệu trả về (có thể là object, list, v.v.)
        /// </summary>
        public T? Data { get; set; } 

        /// <summary>
        /// Trả về Response thành công
        /// </summary>
        public static ResponseDto<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
        {
            return new ResponseDto<T>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Trả về Response thất bại
        /// </summary>
        public static ResponseDto<T> FailureResponse(string message, int statusCode = 400)
        {
            return new ResponseDto<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Data = default!
            };
        }
    }
}

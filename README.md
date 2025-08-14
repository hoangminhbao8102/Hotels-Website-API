# Hotels-Website-API

> **Dự án Website khách sạn – Phân hệ ASP.NET Core Web API (C#)**  
> Cung cấp API cho hệ thống quản lý/đặt phòng khách sạn: khách sạn, phòng, dịch vụ, đặt phòng, đánh giá, nhân viên và xác thực người dùng (JWT).

## Mục lục
- [Tính năng](#tính-năng)
- [Kiến trúc & Công nghệ](#kiến-trúc--công-nghệ)
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Hướng dẫn cài đặt nhanh](#hướng-dẫn-cài-đặt-nhanh)
- [Cấu hình `appsettings.json`](#cấu-hình-appsettingsjson)
- [Migration & Seed dữ liệu](#migration--seed-dữ-liệu)
- [Chạy dự án](#chạy-dự-án)
- [Tài liệu API (Swagger)](#tài-liệu-api-swagger)
- [Bảng Endpoint](#bảng-endpoint)
- [Mô hình dữ liệu (khái quát)](#mô-hình-dữ-liệu-khái-quát)
- [Quy ước mã nguồn](#quy-ước-mã-nguồn)
- [Triển khai Production](#triển-khai-production)
- [Khắc phục sự cố](#khắc-phục-sự-cố)
- [Giấy phép](#giấy-phép)

---

## Tính năng
- Đăng ký/đăng nhập, cấp **JWT**; phân quyền **Admin/User**.
- Quản lý **Khách sạn, Phòng, Dịch vụ, Nhân viên** (CRUD).
- **Đặt phòng**: tạo, xem lịch sử đặt theo người dùng, hủy đặt.
- **Đánh giá**: thêm đánh giá theo khách sạn, xem danh sách đánh giá.
- Tìm kiếm/phân trang cơ bản (tùy endpoint).
- **Swagger UI** để thử API nhanh.
- Chuẩn bị sẵn luồng **seed data** mẫu để demo nhanh.
> Lưu ý: README này được viết cho cấu trúc repo hiện tại (thư mục `HotelManagement`) và giấy phép **MIT** của dự án.

---

## Kiến trúc & Công nghệ
- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server)
- **JWT** (xác thực/bảo mật)
- **AutoMapper** (map DTO ↔ entity)
- **FluentValidation** (validate DTO)
- **Swagger / Swashbuckle** (tài liệu API)

Cấu trúc (tham khảo):
```bash
HotelManagement/
 ├─ src/
 │   ├─ HotelManagement.Api/          # ASP.NET Core Web API
 │   ├─ HotelManagement.Core/         # Entities, DTOs, Contracts, Constants
 │   ├─ HotelManagement.Data/         # DbContext, Migrations, Repositories
 │   └─ HotelManagement.Services/     # Services (business logic)
 ├─ tests/                            # (tùy chọn) Unit/Integration tests
 └─ README.md
```

---

## Yêu cầu hệ thống
- **.NET SDK 8.0+**
- **SQL Server** 2019+ (LocalDB hoặc Developer đều được)
- **Node.js** *(chỉ nếu bạn muốn chạy client riêng; không bắt buộc cho API)*
- Hệ điều hành: Windows/Linux/macOS

---

## Hướng dẫn cài đặt nhanh
1. Clone repo
```bash
git clone https://github.com/hoangminhbao8102/Hotels-Website-API.git
cd Hotels-Website-API
```
2. Mở solution (nếu có) hoặc folder API trong `HotelManagement/src/HotelManagement.Api`.
3. Tạo database rỗng trên SQL Server (ví dụ: `HotelDb`).

---

## Cấu hình `appsettings.json`
Tạo/sửa `appsettings.json` trong project API:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=HotelDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=True"
  },
  "Jwt": {
    "Key": "REPLACE_WITH_A_LONG_RANDOM_SECRET_KEY",
    "Issuer": "HotelsWebsiteApi",
    "Audience": "HotelsWebsiteApiClient",
    "ExpireMinutes": 120
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> Nếu dùng **Windows Authentication**:
> ```json
> "DefaultConnection": "Server=localhost;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
> ```

---

## Migration & Seed dữ liệu
> Thực hiện trong thư mục project API (chứa `Program.cs`):
```bash
# Cài EF Tool nếu chưa có
dotnet tool install --global dotnet-ef

# Tạo migration (lần đầu)
dotnet ef migrations add InitialCreate -o ../HotelManagement.Data/Migrations

# Áp dụng migration
dotnet ef database update
```
**Seed dữ liệu**:
Trong `Program.cs` (hoặc `DataSeeder`), dự án sẽ seed dữ liệu mẫu khi chạy lần đầu (Users Admin/User, vài Hotels/Rooms/Services). Nếu bạn tắt seed, hãy thêm cờ cấu hình riêng (ví dụ `SeedData: true/false`) theo nhu cầu.

---

## Chạy dự án
```bash
dotnet restore
dotnet build
dotnet run
```
- Mặc định chạy ở `https://localhost:7039` hoặc `http://localhost:5114` (tuỳ `launchSettings.json`).
- Mở **Swagger**: `https://localhost:7039/swagger`

--- 

## Tài liệu API (Swagger)
- Tự động tạo bởi **Swashbuckle**.
- Bạn có thể thử **Authorize** với Bearer token (JWT) trực tiếp trên Swagger UI.

---

## Bảng Endpoint
> Quy ước: tiền tố mặc định là /api. Tên route có thể thay đổi nhẹ theo triển khai thực tế, nhưng cấu trúc sau đây là khuyến nghị thống nhất giữa BE/FE.

### Auth
| Method | Route                | Body/Params                     | Mô tả                         |
|--------|----------------------|---------------------------------|-------------------------------|
| POST   | `/api/auth/register` | `{ fullName, email, password }` | Đăng ký tài khoản             |
| POST   | `/api/auth/login`    | `{ email, password }`           | Đăng nhập → trả JWT           |
| GET    | `/api/auth/me`       | Header `Authorization: Bearer`  | Thông tin người dùng hiện tại |

### Hotel
| Method | Route             | Body/Params              | Mô tả               |
|--------|-------------------|--------------------------|---------------------|
| GET    | `/api/hotel`      | `?q=&page=&pageSize=`    | Danh sách khách sạn |
| GET    | `/api/hotel/{id}` |                          | Chi tiết khách sạn  |
| POST   | `/api/hotel`      | `{ name, address, ... }` | **Admin**: tạo      |
| PUT    | `/api/hotel/{id}` | `{ ... }`                | **Admin**: cập nhật |
| DELETE | `/api/hotel/{id}` |                          | **Admin**: xóa      |

### Room
| Method | Route                       | Body/Params                               | Mô tả                                      |
|--------|-----------------------------|-------------------------------------------|--------------------------------------------|
| GET    | `/api/room/hotel/{hotelId}` | `?from=&to=&guests=&page=&pageSize=`      | Danh sách phòng theo khách sạn, có thể lọc |
| POST   | `/api/room`                 | `{ hotelId, type, price, capacity, ... }` | **Admin**: tạo phòng                       |
| PUT    | `/api/room/{id}`            | `{ ... }`                                 | **Admin**: cập nhật phòng                  |
| DELETE | `/api/room/{id}`            |                                           | **Admin**: xóa phòng                       |

### Service (Dịch vụ)
| Method | Route               | Body/Params                 | Mô tả               |
|--------|---------------------|-----------------------------|---------------------|
| GET    | `/api/service`      | `?hotelId=&page=&pageSize=` | Danh sách dịch vụ   |
| POST   | `/api/service`      | `{ hotelId, name, price }`  | **Admin**: tạo      |
| PUT    | `/api/service/{id}` | `{ ... }`                   | **Admin**: cập nhật |
| DELETE | `/api/service/{id}` |                             | **Admin**: xóa      |

### Employee (Nhân viên)
| Method | Route                      | Body/Params                | Mô tả                  |
|--------|----------------------------|----------------------------|------------------------|
| GET    | `/api/employee/hotel/{id}` |                            | Danh sách nhân viên KS |
| POST   | `/api/employee`            | `{ hotelId, name, role }`  | **Admin**: tuyển dụng  |
| DELETE | `/api/employee/{id}`       |                            | **Admin**: xóa         |

### Booking (Đặt phòng)
| Method | Route                        | Body/Params                                     | Mô tả                                  |
|--------|------------------------------|-------------------------------------------------|----------------------------------------|
| GET    | `/api/booking`               | `{ userId, roomId, checkIn, checkOut, guests }` | Tạo đặt phòng                          |
| POST   | `/api/booking/user/{userId}` | Header `Authorization: Bearer` *(nên bắt buộc)* | Xem lịch sử đặt theo user              |
| PATCH  | `/api/booking/{id}/cancel`   |                                                 | Hủy đặt phòng (quy tắc thời gian/fee…) |

### Review (Đánh giá)
| Method | Route                         | Body/Params                    | Mô tả                       |
|--------|-------------------------------|--------------------------------|-----------------------------|
| GET    | `/api/review/hotel/{hotelId}` | `?page=&pageSize=`             | Danh sách đánh giá          |
| POST   | `/api/review`                 | `{ hotelId, rating, comment }` | Thêm đánh giá (yêu cầu JWT) |

> Backlog mở rộng (tùy chọn): Upload ảnh (S3/Local), thanh toán, coupon, báo cáo thống kê…

---

## Mô hình dữ liệu (khái quát)
```scss
User( Id, FullName, Email, PasswordHash, Role, CreatedAt )
Hotel( Id, Name, Address, City, Description, Stars, CreatedAt )
Room( Id, HotelId, Type, Price, Capacity, Status )
Service( Id, HotelId, Name, Price )
Employee( Id, HotelId, FullName, Title, Phone )
Booking( Id, UserId, RoomId, CheckIn, CheckOut, Guests, Status )
Review( Id, HotelId, UserId, Rating, Comment, CreatedAt )
```
- **Ràng buộc**:
  - `Booking` kiểm tra trùng lịch phòng; trạng thái: `Pending/Confirmed/Cancelled`.
  - `Review.Rating 1–5`; mỗi user có thể review nhiều KS (hoặc 1/KS tùy rule).
  - `User.Role` ∈ `{Admin,User}`.

---

## Quy ước mã nguồn
- **DTO** tách biệt Entity, dùng **AutoMapper**.
- **Service/Repository pattern** cho nghiệp vụ & truy vấn.
- **FluentValidation**: validate DTO nhập vào.
- **ProblemDetails** cho lỗi chuẩn REST.
- **Pagination**: `{ page, pageSize }` (mặc định `page=1`, `pageSize=10`).
- **Sorting/Filtering**: query string `q`, `from`, `to`, `guests`,…

---

## Triển khai Production
- **Windows**: Kestrel + **IIS** (reverse proxy).
- **Linux**: Kestrel + **Nginx** (reverse proxy).
- Biến môi trường:
  - `ASPNETCORE_ENVIRONMENT=Production`
  - `ConnectionStrings__DefaultConnection=...`
  - `Jwt__Key=...`
- **Logging**: Serilog/NLog (khuyến nghị).
- **Backup** DB định kỳ; bật `Migrations` có kiểm soát.

---

## Khắc phục sự cố
- **Không thấy Swagger**: kiểm tra `app.UseSwagger()` và `app.UseSwaggerUI`() trong `Program.cs` (chỉ Dev hay cả Prod).
- **Lỗi kết nối SQL**: kiểm tra `DefaultConnection`, mở port `1433`, `TrustServerCertificate=True` khi dùng self-signed.
- **`No DbContext was found`**: đảm bảo project start-up là API (chứa `Program.cs`), `DbContext` không abstract/generic và đã tham chiếu đúng.
- **JWT 401**: thêm header `Authorization: Bearer {token};` kiểm tra thời hạn token, `Issuer/Audience` khớp cấu hình.

---

## Giấy phép
Dự án cấp phép theo **MIT License**. Xem tệp [LICENSE](LICENSE) trong repo.

## Tác giả
- [@hoangminhbao8102](https://github.com/hoangminhbao8102/Hotels-Website-API) – Hotels-Website-API (C# / ASP.NET Core). Repo công khai với thư mục chính `HotelManagement`

---

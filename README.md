# Hướng dẫn chạy dự án

## Yêu cầu hệ thống

- **SQL Server**: Để chạy file script cơ sở dữ liệu.
- **.NET Core SDK**: .Net 8 .
## Tổng quan về hệ thống

Hệ thống bao gồm 2 dự án chính:  
1. **AuthorizationServer**:  
   - Mục đích: Cấp phát **JWT Token** để xác thực và ủy quyền người dùng.  
   - Chức năng chính:
     - Đăng nhập bằng email và mật khẩu.
     - Tạo và trả về **Bearer Token** để sử dụng trong các API khác.

2. **Resource2**:  
   - Mục đích: Quản lý tài nguyên (Quản lý đơn hàng).  
   - Chức năng chính:
     - Xác thực **Bearer Token**: Mọi yêu cầu phải kèm **Bearer Token**, nếu không sẽ trả về **401 Unauthorized**
     - Cung cấp các API để thêm, sửa, xóa, và truy xuất thông tin tài nguyên.

**Quy trình hoạt động tổng quan**:  
- Người dùng đăng nhập vào `AuthorizationServer` để nhận **JWT Token**.  
- Token này được gửi kèm trong các yêu cầu đến `Resource` để xác thực và thực hiện các thao tác API.
---
## Các bước thực hiện

### Bước 1: Cấu hình cơ sở dữ liệu
1. Mở **SQL Server Management Studio**.
2. Chạy file script SQL được cung cấp trong dự án để tạo cơ sở dữ liệu và bảng.

### Bước 2: Cập nhật chuỗi kết nối
1. Mở file `appsettings.json` trong hai dự án:
   - **AuthorizationServer**
   - **Resource2**
2. Tìm mục `"ConnectionStrings"` và chỉnh sửa chuỗi kết nối thành thông tin server SQL trên máy tính của bạn.  
   Ví dụ:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=`YOUR_SERVER_NAME`;Database=SOA1;Trusted_Connection=yes;MultipleActiveResultSets=true;TrustServerCertificate=Yes"
   }
### Bước 3: Lấy token
1. Chạy **AuthorizationServer** .
2. Đăng nhập bằng **email** và **password** để lấy **JWT token**.

### Bước 4: Sử dụng token để xác thực
1. Chạy **Resource2**
2. Thêm **Bearer Token** vào mục `Authorize`.
   ```text
   Bearer <token>
   ```
### Bước 5: Thực hiện thao tác với API

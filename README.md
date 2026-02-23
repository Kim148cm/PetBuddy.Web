PetBuddy – Hệ Thống Quản Lý & Dịch Vụ Thú Cưng
1. Giới thiệu

PetBuddy là hệ thống quản lý dịch vụ và bán sản phẩm dành cho thú cưng, bao gồm các chức năng:

Quản lý người dùng và phân quyền

Quản lý thú cưng

Hồ sơ sức khỏe

Đặt lịch dịch vụ

Quản lý sản phẩm và giỏ hàng

Đơn hàng và thanh toán

Đánh giá dịch vụ

Chat AI hỗ trợ

Thông báo và hỗ trợ khách hàng

Blog chia sẻ kiến thức

2. Công nghệ sử dụng
Backend

ASP.NET MVC

C#

ADO.NET / Entity Framework

SQL Server

Database

Microsoft SQL Server

Thiết kế theo mô hình quan hệ

Sử dụng:

Primary Key

Foreign Key

Identity

UNIQUE

CHECK Constraint

3. Cấu trúc Database

Database gồm các nhóm bảng chính:

3.1 Quản lý người dùng

VaiTro

NguoiDung

NhanVien

LichSuDangNhap

3.2 Quản lý thú cưng

ThuCung

HoSoSucKhoe

3.3 Dịch vụ

DanhMucDichVu

DichVu

KhungGio

LichLamViec

DonDatLich

ChiTietDonDatLich

DanhGia

3.4 Sản phẩm & bán hàng

DanhMucSanPham

NhaCungCap

SanPham

GioHang

ChiTietGioHang

DonHang

ChiTietDonHang

ThanhToan

3.5 Hệ thống & hỗ trợ

PhienChat

TinNhanChat

ThongBao

YeuCauHoTro

BaiViet

HinhAnh

4. Cách cài đặt và chạy dự án
Bước 1: Tạo Database

Mở SQL Server và chạy file script tạo database:

CREATE DATABASE PetBuddy

Sau đó chạy toàn bộ file SQL để tạo bảng.

Bước 2: Cấu hình chuỗi kết nối

Mở file Web.config và chỉnh sửa connection string:

<connectionStrings>
  <add name="PetBuddyConnection"
       connectionString="Data Source=.;Initial Catalog=PetBuddy;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
Bước 3: Chạy dự án

Mở project bằng Visual Studio

Build Solution

Chọn IIS Express

Run

5. Tài khoản mặc định
Email: admin@gmail.com
Password: 123456
6. Mục tiêu dự án

Xây dựng hệ thống quản lý dịch vụ thú cưng hoàn chỉnh

Áp dụng mô hình MVC

Thiết kế database chuẩn hóa

Kết nối và thao tác dữ liệu với SQL Server

Phát triển chức năng đặt lịch và bán hàng thực tế

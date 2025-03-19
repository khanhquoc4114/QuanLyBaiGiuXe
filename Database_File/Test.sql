-- Tạo CSDL
CREATE DATABASE BAIGIUXE COLLATE Vietnamese_CI_AS;
GO

USE BAIGIUXE;
GO

-- Bảng Vé Tháng
CREATE TABLE VeThang (
    MaVeThang VARCHAR(10) PRIMARY KEY,
    Nhom NVARCHAR(20) NOT NULL,
    MaThe VARCHAR(10),
    ChuXe NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(10),
    DiaChi NVARCHAR(200),
    Email VARCHAR(100),
    NgayKichHoat DATETIME NOT NULL,
    NgayHetHan DATETIME NOT NULL,
    BienSo VARCHAR(20) NOT NULL,
	NhanHieu VARCHAR(20),
    LoaiXe NVARCHAR(20) COLLATE Vietnamese_CI_AS CHECK (LoaiXe IN ('Xe máy', 'Ô tô', 'Chung')) NOT NULL,
    GiaVe DECIMAL(10, 2) NOT NULL,
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);
GO

CREATE TABLE VeLuot(
	MaVeLuot INT PRIMARY KEY,
    MaThe VARCHAR(10) NOT NULL,
    ThoiGianXuLy DATETIME NOT NULL,
    GiaVe DECIMAL(10,2) NOT NULL,
	BienSo VARCHAR(20) NOT NULL,
    GhiChu NVARCHAR(255),
	Nhom NVARCHAR(20) NOT NULL,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
); GO

CREATE TABLE Nhom(
	MaNhom VARCHAR(50) PRIMARY KEY,
	TenNhom NVARCHAR(50) NOT NULL,
	ThongTinKhac NVARCHAR(255)
);GO

-- Bảng Thẻ
CREATE TABLE The (
    MaThe VARCHAR(10) PRIMARY KEY,
    SoThuTu INT,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AS CHECK (LoaiThe IN ('Tháng', 'Lượt')) NOT NULL,
    NgayTaoThe DATETIME NOT NULL,
    NgayCapNhatThe DATETIME NOT NULL,
    TrangThaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AS CHECK (TrangThaiThe IN ('Còn hạn', 'Hết hạn', 'Không có', 'Sử dụng')) NOT NULL,
);
GO

-- Bảng Người Dùng
CREATE TABLE NguoiDung (
    MaNguoiDung VARCHAR(10) PRIMARY KEY,
    MaNhom VARCHAR(50),
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AS NOT NULL,
    TaiKhoan VARCHAR(20) UNIQUE NOT NULL,
    MatKhau VARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(500)
	FOREIGN KEY (MaNhom) REFERENCES MaNhom(MaNhom),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);
GO


INSERT INTO VeThang (
    MaVe, MaThe, ChuXe, DienThoai, Email, DiaChi, BienSo, LoaiXe, 
    NgayKichHoat, NgayHetHan, GiaVe, GhiChu, Nhom
) VALUES 
    (1, '0055085211', N'Nguyễn Văn A', '0938178731', 'nguyenvana@gmail.com', 
     N'123 Đường Láng, Hà Nội', '86B8-04254', 'Xe máy', 
     '2025-03-01 00:00:00', '2025-04-01 00:00:00', 150000.00, 
     N'Khách hàng thân thiết', 1),
    (2, '0055052082', N'Trần Thị B', '0909123456', 'tranthib@yahoo.com', 
     N'45 Nguyễn Huệ, TP.HCM', '51A-98765', 'Ô tô', 
     '2025-03-05 00:00:00', '2025-04-05 00:00:00', 500000.00, 
     N'Đăng ký qua website', 2);



-- Một vài câu lệnh truy vấn
Select * from VeThang;
DElete from The;
drop table The;
sp_help 'VeThang'

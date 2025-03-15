-- Tạo CSDL
CREATE DATABASE BAIGIUXE1 COLLATE Vietnamese_CI_AS;
GO

USE BAIGIUXE1;
GO

-- Bảng Người Dùng
CREATE TABLE NguoiDung (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AS NOT NULL,
    TenDangNhap NVARCHAR(20) UNIQUE NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL,
    QuyenHan NVARCHAR(50) COLLATE Vietnamese_CI_AS CHECK (QuyenHan IN ('Quản trị viên', 'Khách hàng')) NOT NULL
);
GO

-- Bảng Thẻ
CREATE TABLE The (
    MaThe NVARCHAR(10) PRIMARY KEY,
    MaNguoiDung NVARCHAR(10) NULL,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AS CHECK (LoaiThe IN ('Tháng', 'Lượt')) NOT NULL,
    BienSo NVARCHAR(15) NOT NULL,
    ThoiGianBatDau DATETIME NULL,
    ThoiGianKetThuc DATETIME NULL,
    TrangThaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AS CHECK (TrangThaiThe IN ('Còn hạn', 'Hết hạn', 'Không có', 'Đang dùng', 'Không')) NOT NULL,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO

-- Bảng Lịch Sử Vào/Ra Bãi Đỗ Xe
CREATE TABLE LichSuVaoRa (
    MaThe NVARCHAR(10),
    TinhTrangXe NVARCHAR(10) COLLATE Vietnamese_CI_AS CHECK (TinhTrangXe IN ('Đã rời', 'Đang đỗ')) NOT NULL,
    ThoiGianVao DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NULL,
    PhiPhaiTra MONEY NULL,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);
GO

-- Bảng Nhật Ký Hoạt Động Ứng Dụng
CREATE TABLE NhatKyHoatDong (
    MaNguoiDung NVARCHAR(10),
    ThoiGianVao DATETIME NOT NULL,
    HoatDong NVARCHAR(50) COLLATE Vietnamese_CI_AS NOT NULL,
    ChiTiet NVARCHAR(100) COLLATE Vietnamese_CI_AS NOT NULL,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO

-- Bảng Cấu Hình Hệ Thống
CREATE TABLE CauHinhHeThong (
    ThamSo NVARCHAR(50) NOT NULL,
    GiaTri MONEY NOT NULL
);
GO

-- Chèn dữ liệu vào bảng Cấu Hình Hệ Thống
INSERT INTO CauHinhHeThong (ThamSo, GiaTri) VALUES
('Tiền giữ ban ngày', 3000),
('Tiền giữ ban đêm', 4000),
('Tiền phạt mất thẻ', 50000),
('Số làn xe', 2);
GO

-- Chèn dữ liệu vào bảng Người Dùng
INSERT INTO NguoiDung (MaNguoiDung, HoTen, TenDangNhap, MatKhau, QuyenHan) VALUES
('AD001', 'Ngô Văn Nam', 'adnamnv', '123456', 'Quản trị viên'),
('US000001', 'Nguyễn Hoàng Nam', 'usnamnh', '123456', 'Khách hàng'),
('US000002', 'Trần Trung Hậu', 'ushautt', '123456', 'Khách hàng'),
('US000003', 'Lê Minh', 'usminhl', '123456', 'Khách hàng');
GO

-- Chèn dữ liệu vào bảng Thẻ
INSERT INTO The (MaThe, MaNguoiDung, LoaiThe, BienSo, ThoiGianBatDau, ThoiGianKetThuc, TrangThaiThe) VALUES
('qwertyuiop', 'US000001', 'Tháng', '59X1-123.45', '2025-02-01 15:00:00', '2025-03-04 23:59:59', 'Còn hạn'),
('asdfghjkl', 'US000002', 'Tháng', '59X1-123.44', '2025-01-01 15:00:00', '2025-01-31 23:59:59', 'Hết hạn'),
('zxcvbnm', NULL, 'Lượt', '59X1-123.46', NULL, NULL, 'Đang dùng');
GO

-- Chèn dữ liệu vào bảng Lịch Sử Vào/Ra
INSERT INTO LichSuVaoRa (MaThe, TinhTrangXe, ThoiGianVao, ThoiGianKetThuc, PhiPhaiTra) VALUES
('qwertyuiop', 'Đã rời', '2025-03-04 15:00:00', '2025-03-04 23:00:00', NULL),
('asdfghjkl', 'Đang đỗ', '2025-03-04 15:00:00', NULL, NULL),
('zxcvbnm', 'Đã rời', '2025-03-04 15:00:00', '2025-03-04 23:00:00', 7000),
('zxcvbnm', 'Đang đỗ', '2025-03-05 15:00:00', NULL, NULL);
GO

-- Chèn dữ liệu vào bảng Nhật Ký Hoạt Động
INSERT INTO NhatKyHoatDong (MaNguoiDung, ThoiGianVao, HoatDong, ChiTiet) VALUES
('AD001', '2025-03-04 16:00:00', 'Đăng nhập', 'Thành công'),
('US000001', '2025-03-04 17:00:00', 'Đăng ký vé tháng', 'Thành công');
GO

-- Xuất dữ liệu
SELECT * FROM NguoiDung;
SELECT * FROM The;
SELECT * FROM LichSuVaoRa;
SELECT * FROM NhatKyHoatDong;
SELECT * FROM CauHinhHeThong;
GO

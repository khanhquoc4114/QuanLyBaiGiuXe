-- Tạo CSDL
CREATE DATABASE BAIGIUXE COLLATE Vietnamese_CI_AI;
GO

USE BAIGIUXE;
GO

SET LANGUAGE British; -- Hoặc French
SET DATEFORMAT dmy;
DECLARE @datevar DATETIME2 = '31/12/2023 17:13:13';  
SELECT @datevar;
SELECT GETDATE();

-- Bảng Nhóm
CREATE TABLE Nhom (
    MaNhom INT IDENTITY(1,1) PRIMARY KEY,
    TenNhom NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);
GO

-- Bảng Thẻ
CREATE TABLE The (
    MaThe VARCHAR(20) PRIMARY KEY,
    SoThuTu INT,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AI CHECK (LoaiThe IN (N'Ô tô', N'Xe máy', N'Chung')) NOT NULL,
    NgayTaoThe DATETIME NOT NULL,
    NgayCapNhatThe DATETIME NOT NULL,
    TrangThaiThe NVARCHAR(20) COLLATE Vietnamese_CI_AI CHECK (TrangThaiThe IN (N'Sử dụng', N'Hết hạn')) NOT NULL
);
GO

-- Bảng Vé Tháng
CREATE TABLE VeThang (
    MaVeThang INT IDENTITY(1,1) PRIMARY KEY,
    MaNhom INT NOT NULL,
    MaThe VARCHAR(20) NOT NULL,
    ChuXe NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL ,
    DienThoai VARCHAR(15),
    DiaChi NVARCHAR(200) COLLATE Vietnamese_CI_AI,
    Email VARCHAR(100),
    NgayKichHoat DATETIME DEFAULT GETDATE(),
    NgayHetHan DATETIME DEFAULT GETDATE(),
    BienSo VARCHAR(20) NOT NULL,
	NhanHieu VARCHAR(20) COLLATE Vietnamese_CI_AI,
    LoaiXe NVARCHAR(20) COLLATE Vietnamese_CI_AI CHECK (LoaiXe IN (N'Xe máy', N'Ô tô', N'Chung')) NOT NULL,
    GiaVe DECIMAL(10, 2) NOT NULL,
    GhiChu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
);
GO


--Bảng Vé Lượt
CREATE TABLE VeLuot(
	MaVeLuot INT IDENTITY(1,1) PRIMARY KEY,
    MaThe VARCHAR(20) NOT NULL,
	MaNhom INT NOT NULL,
    ThoiGianXuLy DATETIME DEFAULT GETDATE(),
    GiaVe DECIMAL(10,2) NOT NULL,
	BienSo VARCHAR(20) NOT NULL,
    GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
);
GO

--Bảng Nhóm Nhân Viên
CREATE TABLE NhomNhanVien (
    MaNhomNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    TenNhomNhanVien NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
	SoLuongNhanVien INT NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);
GO

-- Bảng Người Dùng
CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL,
	TaiKhoan VARCHAR(20) UNIQUE NOT NULL,
    MatKhau VARCHAR(100) NOT NULL,
    MaThe VARCHAR(20) NOT NULL,
    MaNhomNhanVien INT NOT NULL,
    GhiChu NVARCHAR(500)
	FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);
GO

CREATE TRIGGER trg_TangSoLuongNhanVien
ON NhanVien
AFTER INSERT
AS
BEGIN
    UPDATE NhomNhanVien
    SET SoLuongNhanVien = SoLuongNhanVien + 1
    WHERE MaNhomNhanVien IN (SELECT MaNhomNhanVien FROM INSERTED);
END;

CREATE TRIGGER trg_GiamSoLuongNhanVien
ON NhanVien
AFTER DELETE
AS
BEGIN
    UPDATE NhomNhanVien
    SET SoLuongNhanVien = SoLuongNhanVien - 1
    WHERE MaNhomNhanVien IN (SELECT MaNhomNhanVien FROM DELETED);
END;

CREATE TRIGGER trg_CapNhatSoLuongNhanVien
ON NhanVien
AFTER UPDATE
AS
BEGIN
    IF UPDATE(MaNhomNhanVien)
    BEGIN
        UPDATE NhomNhanVien
        SET SoLuongNhanVien = SoLuongNhanVien - 1
        WHERE MaNhomNhanVien IN (SELECT MaNhomNhanVien FROM DELETED);

        UPDATE NhomNhanVien
        SET SoLuongNhanVien = SoLuongNhanVien + 1
        WHERE MaNhomNhanVien IN (SELECT MaNhomNhanVien FROM INSERTED);
    END
END;

--Bảng Nhật ký Vé Tháng
CREATE TABLE NhatKyVeThang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HanhDong NVARCHAR(50) COLLATE Vietnamese_CI_AI,
    MayTinhXuLy NVARCHAR(100),
    MaThe VARCHAR(20),
    ThoiGianXuLy DATETIME DEFAULT GETDATE(),
    MaNhom INT,
    ChuXe NVARCHAR(100) COLLATE Vietnamese_CI_AI,
    NhanVienXuLy NVARCHAR(100) COLLATE Vietnamese_CI_AI,
    NoiDungThayDoi NVARCHAR(MAX)
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);

CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    MaNhomNhanVien INT NOT NULL,
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL,
    MaThe VARCHAR(20) NOT NULL,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    GhiChu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);

-- Một vài câu lệnh truy vấn
Select * from VeThang;
DElete from VeLuot;
DElete from VeThang;
DElete from The;
DElete from Nhom;
drop table NhanVien;
drop table VeThang;
drop table VeLuot;
drop table The;
drop table Nhom;
drop table ThongTinCaNhan;
drop table NhomNhanVien;
SELECT name FROM sys.databases;
sp_help 'VeThang'

--Lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;

select * from NhanVien where TenNhomNhanVien = N'bảo vệ'
SELECT TOP 1 MaNhanVien FROM NhanVien WHERE TenDangNhap = 'tranvanc'
SELECT MaNhomNhanVien FROM NhomNhanVien WHERE TenNhomNhanVien = N'Bảo vệ'

-- Tạo mock data
INSERT INTO Nhom (TenNhom, ThongTinKhac) VALUES
(N'Khách VIP', N'Ưu đãi giảm giá 10%'),
(N'Nhân viên công ty', N'Giảm giá 20%'),
(N'Khách vãng lai', N'Không có ưu đãi');

INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThaiThe) VALUES
('THE001', 1, N'Ô tô', '2024-01-01 08:00:00', '2024-02-01 08:00:00', N'Sử dụng'),
('THE002', 2, N'Xe máy', '2024-01-05 09:30:00', '2024-02-10 10:00:00', N'Sử dụng'),
('THE003', 3, N'Chung', '2024-02-01 07:45:00', '2024-03-01 07:50:00', N'Hết hạn'),
('THE004', 4, N'Ô tô', '2024-03-10 10:15:00', '2024-04-10 10:15:00', N'Sử dụng'),
('THE005', 5, N'Xe máy', '2024-03-15 12:00:00', '2024-04-15 12:00:00', N'Hết hạn');

INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, NgayKichHoat, NgayHetHan, BienSo, NhanHieu, LoaiXe, GiaVe, GhiChu) VALUES
(1, 'THE001', N'Nguyễn Văn A', '0987654321', N'123 Đường ABC, Hà Nội', 'nguyenvana@example.com', '2024-01-10 08:00:00', '2024-07-10 08:00:00', '30A-12345', N'Toyota', N'Ô tô', 5000000, N'Khách VIP'),
(2, 'THE002', N'Trần Thị B', '0912345678', N'456 Đường XYZ, Hồ Chí Minh', 'tranthib@example.com', '2024-02-15 09:30:00', '2024-08-15 09:30:00', '59C1-67890', N'Honda', N'Xe máy', 1500000, N'Nhân viên công ty');

INSERT INTO VeLuot (MaThe, MaNhom, ThoiGianXuLy, GiaVe, BienSo, GhiChu) VALUES
('THE001', 1, '2024-03-01 10:15:00', 30000, '30A-12345', N'Ve vào bãi xe A'),
('THE002', 2, '2024-03-02 18:30:00', 10000, '59C1-67890', N'Ve vào bãi xe B');

INSERT INTO NhomNhanVien (TenNhomNhanVien, SoLuongNhanVien, ThongTinKhac)
VALUES 
(N'Quản trị hệ thống', 5, N'Nhóm này có quyền cao nhất, có thể chỉnh sửa toàn bộ hệ thống.'),
(N'Kế toán', 3, N'Chỉ được truy cập các chức năng liên quan đến hóa đơn và doanh thu.'),
(N'Bảo vệ', 10, N'Chỉ có quyền xem và xử lý vé xe.');

INSERT INTO NhanVien (MaNhomNhanVien, HoTen, MaThe, TenDangNhap, MatKhau, GhiChu) VALUES
(1, N'Nguyễn Văn A', 'THE001', 'nguyenvana', '123456', N'Nhân viên quản lý xe máy'),
(2, N'Lê Thị B', 'THE002', 'lethib', 'abcdef', N'Nhân viên quản lý ô tô'),
(3, N'Trần Văn C', 'THE003', 'tranvanc', 'qwerty', N'Nhân viên mới vào làm');


SELECT 
    nv.*,
    n.TenNhom
FROM NhanVien nv
JOIN Nhom n ON nv.MaNhom = n.MaNhom
WHERE nv.MaNhanVien = 1;

SELECT 
    vt.MaThe, 
    vt.BienSo, 
    vt.ChuXe, 
    n.TenNhom,  
    vt.NgayKichHoat, 
    vt.NgayHetHan, 
    vt.GiaVe
FROM VeThang vt
JOIN Nhom n ON vt.MaNhom = n.MaNhom
WHERE MaThe LIKE N'%Nguyễn%'
	OR ChuXe LIKE N'%Nguyễn%'
   OR BienSo LIKE N'%Nguyễn%'
   OR DienThoai LIKE N'%Nguyễn%'
   OR DiaChi LIKE N'%Nguyễn%'
   OR Email LIKE N'%Nguyễn%'
   OR NhanHieu LIKE N'%Nguyễn%'
   OR GhiChu LIKE N'%Nguyễn%';

-- Kiểm tra điều kiện
SELECT name, definition 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('dbo.The') AND name = 'CK__The__TrangThaiTh__4F47C5E3';

SELECT MaVeThang FROM VeThang WHERE BienSo 

SELECT 
    vt.MaThe, 
    vt.BienSo, 
    vt.ChuXe, 
    n.TenNhom, 
    t.LoaiThe AS LoaiVe, 
    vt.NgayKichHoat, 
    vt.NgayHetHan, 
    vt.GiaVe
FROM VeThang vt
JOIN Nhom n ON vt.MaNhom = n.MaNhom
JOIN The t ON vt.MaThe = t.MaThe;

--  Toàn bộ khoá ngoại
SELECT 
    fk.name AS ForeignKeyName, 
    tp.name AS ParentTable, 
    cp.name AS ParentColumn, 
    tr.name AS ReferencedTable, 
    cr.name AS ReferencedColumn
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.tables AS tp ON fkc.parent_object_id = tp.object_id
INNER JOIN sys.columns AS cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.tables AS tr ON fkc.referenced_object_id = tr.object_id
INNER JOIN sys.columns AS cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id;


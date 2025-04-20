-- Tạo CSDL
CREATE DATABASE testdoxe COLLATE Vietnamese_CI_AI;
GO

USE testdoxe;
GO

SET LANGUAGE British;
SET DATEFORMAT dmy;
DECLARE @datevar DATETIME2 = '31/12/2023 17:13:13';  
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
	MayTinhXuLy NVARCHAR(50),
	MaNhanVienXuLy INT NOT NULL,
	BienSo VARCHAR(20) NOT NULL,
    ThoiGianVao DATETIME DEFAULT GETDATE(),
	ThoiGianRa DATETIME NULL,
    GiaVe DECIMAL(10,2) NOT NULL,
	LoaiXe NVARCHAR(20),
    GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaNhanVienXuLy) REFERENCES NhomNhanVien(MaNhomNhanVien)
);
GO

CREATE TABLE TinhTienCongVan (
    LoaiXe NVARCHAR(20),
    GiaNgay DECIMAL(10,2),
    GiaDem DECIMAL(10,2),
    GiaNgayDem DECIMAL(10,2),
    GiaPhuThu DECIMAL(10,2),
    PhuThuTu TIME,
    PhuThuDen TIME,
    DemTu TIME,
    DemDen TIME,
    GiaoNgayDem TIME
);
GO

CREATE TABLE TinhTienLuyTien (
    LoaiXe NVARCHAR(20),
    Moc1 INT, -- phút
    GiaMoc1 DECIMAL(10,2),
    Moc2 INT, -- phút
    GiaMoc2 DECIMAL(10,2),
    ChuKy INT, -- phút
    GiaChuKy DECIMAL(10,2),
    CongMoc TINYINT -- 0: không, 1: cộng mốc 1, 2: cộng cả 2
);

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
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    MaNhomNhanVien INT NOT NULL,
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL,
    MaThe VARCHAR(20),
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    GhiChu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
	CONSTRAINT UQ_NhanVien_MaThe UNIQUE(MaThe)
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
GO

CREATE TRIGGER trg_GiamSoLuongNhanVien
ON NhanVien
AFTER DELETE
AS
BEGIN
    UPDATE NhomNhanVien
    SET SoLuongNhanVien = SoLuongNhanVien - 1
    WHERE MaNhomNhanVien IN (SELECT MaNhomNhanVien FROM DELETED);
END;
GO

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
GO

CREATE TABLE ChucNang (
    MaChucNang INT PRIMARY KEY,
    TenChucNang NVARCHAR(100)
);
GO

CREATE TABLE PhanQuyen (
    MaNhomNhanVien INT,
    MaChucNang INT,
    CoQuyen BIT,
    PRIMARY KEY (MaNhomNhanVien, MaChucNang),
    FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
);
GO

CREATE TABLE NhatKyDangNhap (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNhanVien INT NOT NULL,
    ThoiGianDangNhap DATETIME NOT NULL DEFAULT GETDATE(),
    ThoiGianDangXuat DATETIME NULL,
    TongThoiGian AS 
        DATEDIFF(SECOND, ThoiGianDangNhap, ISNULL(ThoiGianDangXuat, GETDATE())),
	FOREIGN KEY (MaVeThang) REFERENCES VeThang(MaVeThang),
);

SELECT * FROM NhatKyXuLyVeThang
drop table NhatKyXuLyVeThang

SELECT 
    nk.ID,
    nk.HanhDong,
    nk.MayTinhXuLy,
    nk.ThoiGianXuLy,
--    n.TenNhom,
--    nnv.TenNhomNhanVien,
	vt.*
FROM NhatKyXuLyVeThang nk
INNER JOIN VeThang vt ON nk.MaVeThang = vt.MaVeThang
INNER JOIN Nhom n ON vt.MaNhom = n.MaNhom
LEFT JOIN NhomNhanVien nnv ON nk.MaNhomNhanVien = nnv.MaNhomNhanVien;


--Bảng Nhật ký Vé Tháng
CREATE TABLE NhatKyXuLyVeThang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HanhDong NVARCHAR(50) COLLATE Vietnamese_CI_AI,
    MayTinhXuLy NVARCHAR(100),
    MaThe VARCHAR(20),
    ThoiGianXuLy DATETIME DEFAULT GETDATE(),
    MaVeThang INT NOT NULL,
	MaNhomNhanVien INT NOT NULL,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
	FOREIGN KEY (MaVeThang) REFERENCES VeThang(MaVeThang),
	FOREIGN Key (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien)
);

SELECT name FROM sys.databases;
sp_help 'VeThang'

--Lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;

-- Tạo mock data
-- 1. Nhóm vé (cho VeThang, VeLuot)
INSERT INTO Nhom (TenNhom, ThongTinKhac)
VALUES 
(N'Khách hàng VIP', N'Ưu đãi giảm giá'),
(N'Khách hàng thường', N'Không ưu đãi');

-- 2. Nhóm Nhân Viên
INSERT INTO NhomNhanVien (TenNhomNhanVien, SoLuongNhanVien, ThongTinKhac)
VALUES
(N'Quản trị viên', 0, N'Quản lý toàn bộ hệ thống'),
(N'Nhân viên giữ xe', 0, N'Chỉ truy cập chức năng quét thẻ');

-- 3. Thẻ
INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThaiThe)
VALUES
('THE001', 1, N'Ô tô', GETDATE(), GETDATE(), N'Sử dụng'),
('THE002', 2, N'Xe máy', GETDATE(), GETDATE(), N'Sử dụng'),
('THE003', 3, N'Chung', GETDATE(), GETDATE(), N'Hết hạn'),
('THE004', 4, N'Ô tô', GETDATE(), GETDATE(), N'Sử dụng'),
('THE005', 5, N'Xe máy', GETDATE(), GETDATE(), N'Sử dụng');

-- 4. Nhân viên (triggers sẽ tự cập nhật SoLuongNhanVien)
INSERT INTO NhanVien (HoTen, TenDangNhap, MatKhau, MaThe, MaNhomNhanVien, GhiChu)
VALUES
(N'Nguyễn Văn A', 'admin', '123456', 'THE001', 1, N'Quản trị cấp cao'),
(N'Trần Thị B', 'nhanvien1', '123456', 'THE002', 2, N'Ca sáng'),
(N'Lê Văn C', 'nhanvien2', '123456', 'THE003', 2, N'Ca tối');

-- 5. Vé Tháng
INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, NgayKichHoat, NgayHetHan, BienSo, NhanHieu, LoaiXe, GiaVe, GhiChu)
VALUES
(1, 'THE004', N'Nguyễn Văn D', '0912345678', N'123 Trần Hưng Đạo', 'vand@example.com', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '51A-12345', 'Toyota', N'Ô tô', 1500000, N'Khách VIP'),
(2, 'THE005', N'Phạm Thị E', '0987654321', N'456 Lê Lợi', 'epham@example.com', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '59X1-67890', 'Honda', N'Xe máy', 300000, NULL);

-- 6. Vé Lượt
INSERT INTO VeLuot (MaNhom, MaThe, ThoiGianXuLy, GiaVe, BienSo, GhiChu)
VALUES
(1, 'THE004', GETDATE(), 10000, '51A-12345', N'Vào lúc 7h sáng'),
(2, 'THE005', GETDATE(), 5000, '59X1-67890', N'Vào lúc 8h tối');

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


-- Xoá toàn bộ thông tin, giữ lại bảng
DECLARE @sql NVARCHAR(MAX) = '';
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
SELECT @sql = @sql + 'DELETE FROM [' + TABLE_SCHEMA + '].[' + TABLE_NAME + '];' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

-- Drop toàn bộ bảng (cẩn thận)
DECLARE @dropSQL NVARCHAR(MAX) = '';
SELECT @dropSQL = @dropSQL + 'DROP TABLE [' + TABLE_SCHEMA + '].[' + TABLE_NAME + '];' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @dropSQL;

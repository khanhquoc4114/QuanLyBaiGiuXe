-- Tạo CSDL
CREATE DATABASE BAIGIUXE COLLATE Vietnamese_CI_AI;
GO

USE BAIGIUXE;
GO


-- Bảng Vé Tháng
CREATE TABLE VeThang (
    MaVeThang VARCHAR(10) PRIMARY KEY,
    MaNhom VARCHAR(10) NOT NULL,
    MaThe VARCHAR(10) NOT NULL,
    ChuXe NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL ,
    DienThoai VARCHAR(10),
    DiaChi NVARCHAR(200) COLLATE Vietnamese_CI_AI,
    Email VARCHAR(100),
    NgayKichHoat DATETIME NOT NULL,
    NgayHetHan DATETIME NOT NULL,
    BienSo VARCHAR(20) NOT NULL,
	NhanHieu VARCHAR(20) COLLATE Vietnamese_CI_AI,
    LoaiXe NVARCHAR(20) COLLATE Vietnamese_CI_AI CHECK (LoaiXe IN (N'Xe máy', N'Ô tô', N'Chung')) NOT NULL,
    GiaVe DECIMAL(10, 2) NOT NULL,
    GhiChu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
);
GO

CREATE TABLE VeLuot(
	MaVeLuot INT PRIMARY KEY,
    MaThe VARCHAR(10) NOT NULL,
	MaNhom VARCHAR(10) NOT NULL,
    ThoiGianXuLy DATETIME NOT NULL,
    GiaVe DECIMAL(10,2) NOT NULL,
	BienSo VARCHAR(20) NOT NULL,
    GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
);
GO

CREATE TABLE Nhom(
	MaNhom VARCHAR(10) PRIMARY KEY,
	TenNhom NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
	ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);
GO

-- Bảng Thẻ
CREATE TABLE The (
    MaThe VARCHAR(10) PRIMARY KEY,
    SoThuTu INT,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AI CHECK (LoaiThe IN (N'Tháng', N'Lượt')) NOT NULL,
    NgayTaoThe DATETIME NOT NULL,
    NgayCapNhatThe DATETIME NOT NULL,
    TrangThaiThe NVARCHAR(20) COLLATE Vietnamese_CI_AI CHECK (TrangThaiThe IN ('Sử dụng', 'Hết hạn'))
);
GO

-- Bảng Người Dùng
CREATE TABLE NguoiDung (
    MaNguoiDung VARCHAR(10) PRIMARY KEY,
    MaNhom VARCHAR(50),
    HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL,
    TaiKhoan VARCHAR(20) UNIQUE NOT NULL,
    MatKhau VARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(500)
	FOREIGN KEY (MaNhom) REFERENCES MaNhom(MaNhom),
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);
GO

-- Một vài câu lệnh truy vấn
Select * from VeThang;
DElete from The;
DElete from Nhom;
DElete from VeLuot;
DElete from VeThang;
drop table VeThang;
drop table VeLuot;
drop table The;
drop table Nhom;
drop table ThongTinCaNhan;
SELECT name FROM sys.databases;
sp_help 'VeThang'

--Lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;

-- Dữ liệu mẫu cho bảng Nhom
INSERT INTO Nhom (MaNhom, TenNhom, ThongTinKhac) VALUES 
('N001', N'Nhóm A', N'Thông tin nhóm A'),
('N002', N'Nhóm B', N'Thông tin nhóm B');

-- Dữ liệu mẫu cho bảng The
INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThaiThe) VALUES 
('T001', 1, N'Tháng', '2025-03-01', '2025-03-10', N'Yes'),
('T002', 2, N'Lượt', '2025-03-05', '2025-03-10', N'No');

-- Dữ liệu mẫu cho bảng VeThang
INSERT INTO VeThang (MaVeThang, MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, NgayKichHoat, NgayHetHan, BienSo, NhanHieu, LoaiXe, GiaVe, GhiChu) VALUES 
('VT001', 'N001', 'T001', N'Nguyễn Văn A', '0901234567', N'Hà Nội', 'a@example.com', '2025-03-01', '2025-06-01', '30A-12345', N'Toyota', N'Ô tô', 500000, N'Không có'),
('VT002', 'N002', 'T002', N'Trần Văn B', '0912345678', N'Sài Gòn', 'b@example.com', '2025-03-02', '2025-06-02', '59X2-6789', N'Honda', N'Xe máy', 150000, N'Không có');

-- Dữ liệu mẫu cho bảng VeLuot
INSERT INTO VeLuot (MaVeLuot, MaThe, MaNhom, ThoiGianXuLy, GiaVe, BienSo, GhiChu) VALUES 
(1, 'T001', 'N001', '2025-03-15 08:30:00', 20000, '30A-12345', N'Vào bãi xe'),
(2, 'T002', 'N002', '2025-03-15 09:00:00', 5000, '59X2-6789', N'Ra bãi xe');


CREATE TABLE DocumentIndex (id INT PRIMARY KEY,
word NVARCHAR(MAX) COLLATE Vietnamese_CI_AI,
document_offset INT);

INSERT INTO DocumentIndex (id, word, document_offset) VALUES
(1,N'Có cái con cá ă ắ ớ ờ ố ở',502)

drop table DocumentIndex;

SELECT name, definition 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('dbo.The') AND name = 'CK__The__LoaiThe__40058253';


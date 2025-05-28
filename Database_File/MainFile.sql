-- Tạo CSDL
CREATE DATABASE testdoxe COLLATE Vietnamese_CI_AI;
USE testdoxe;
SET LANGUAGE British;
SET DATEFORMAT dmy;
SET Dateformat ymd;
DECLARE @datevar DATETIME2 = '31/12/2023 17:13:13';  
SELECT GETDATE();
GO

--Lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;

CREATE TABLE LoaiXe (
    MaLoaiXe INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiXe NVARCHAR(50) COLLATE Vietnamese_CI_AI, 
    TrangThai NVARCHAR(50) COLLATE Vietnamese_CI_AI Default N'Sử dụng'
);
INSERT INTO LoaiXe (TenLoaiXe, TrangThai)
VALUES
(N'Xe máy', N'Sử dụng'),
(N'Xe oto', N'Sử dụng');
select * from LoaiXe
GO

-- Bảng Nhóm Vé Tháng
CREATE TABLE Nhom (
    MaNhom INT IDENTITY(1,1) PRIMARY KEY,
    TenNhom NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);

INSERT INTO Nhom (TenNhom, ThongTinKhac)
VALUES
(N'Nhóm Thành Long', N'Nhóm cho xe máy'),
(N'Nhóm Lý Hải', N'Nhóm cho ô tô'),
(N'Nhóm Trấn Thành', N'Nhóm chung'),
(N'Nhóm Lê Lợi', N'Khách VIP'),
(N'Nhóm Bình Dương', N'Nhóm ưu đãi tháng này');
GO

-- Bảng Thẻ 
CREATE TABLE The (
    MaThe VARCHAR(20) PRIMARY KEY,
    SoThuTu INT,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AI DEFAULT N'Chung', 
    NgayTaoThe DATETIME NOT NULL,
    NgayCapNhatThe DATETIME NOT NULL,
    TrangThaiThe NVARCHAR(20) COLLATE Vietnamese_CI_AI CHECK (TrangThaiThe IN (N'Sử dụng', N'Hết hạn'))
);

INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThaiThe)
VALUES
('T001', 1, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T002', 2, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T003', 3, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T004', 4, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T005', 5, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T006', 6, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T007', 7, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T008', 8, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T009', 9, N'Chung',  GETDATE(), GETDATE(), N'Sử dụng'),
('T010', 10, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T011', 11, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T012', 12, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T013', 13, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T014', 14, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T015', 15, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T016', 16, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T017', 17, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T018', 18, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T019', 19, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T020', 20, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T021', 21, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T022', 22, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T023', 23, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T024', 24, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T025', 25, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T026', 26, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T027', 27, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T028', 28, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T029', 29, N'Chung', GETDATE(), GETDATE(), N'Sử dụng'),
('T030', 30, N'Chung', GETDATE(), GETDATE(), N'Sử dụng');
GO

--Bảng Nhóm Nhân Viên - tạo trước - done
CREATE TABLE NhomNhanVien (
    MaNhomNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    TenNhomNhanVien NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
	SoLuongNhanVien INT NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);

INSERT INTO NhomNhanVien (TenNhomNhanVien, SoLuongNhanVien, ThongTinKhac)
VALUES
(N'Bảo vệ', 5, N'Nhóm nhân viên bảo vệ khu vực'),
(N'Kỹ thuật', 10, N'Nhóm kỹ thuật và sửa chữa'),
(N'Kế toán', 3, N'Nhóm quản lý thu chi'),
(N'Quản trị viên', 4, N'Nhóm đón tiếp khách hàng'),
(N'Quản lý', 2, N'Nhóm quản lý cấp cao');
GO

-- Bảng Vé Tháng - done(thiếu thêm ảnh) - thiếu trạng thái ra, chưa ra - 
	CREATE TABLE VeThang (
		MaVeThang INT IDENTITY(1,1) PRIMARY KEY,
		MaNhom INT,
		MaThe VARCHAR(20),
		ChuXe NVARCHAR(100) COLLATE Vietnamese_CI_AI,
		DienThoai VARCHAR(15),
		DiaChi NVARCHAR(200) COLLATE Vietnamese_CI_AI,
		Email VARCHAR(100),
		NgayKichHoat DATETIME DEFAULT GETDATE() NOT NULL,
		NgayHetHan DATETIME DEFAULT DATEADD(MONTH, 1, GETDATE()),
		BienSo VARCHAR(20),
		NhanHieu VARCHAR(20) COLLATE Vietnamese_CI_AI,
		MaLoaiXe INT, -- Xét theo MaLoaiXe (dễ dàng mở rộng)
		GiaVe INT,
		GhiChu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
		MaNhanVienXuLy INT,
		MayTinhXuLy NVARCHAR(50),
		LoaiVe NVARCHAR(20) COLLATE Vietnamese_CI_AI DEFAULT N'Vé tháng',
		FOREIGN KEY (MaNhanVienXuLy) REFERENCES NhomNhanVien(MaNhomNhanVien),
		FOREIGN KEY (MaThe) REFERENCES The(MaThe),
		FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe),
		FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
	);
INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, NgayKichHoat, NgayHetHan, BienSo, NhanHieu, MaLoaiXe, GiaVe, GhiChu, MaNhanVienXuLy, MayTinhXuLy, LoaiVe)
VALUES
(1, 'T001', N'Nguyễn Văn A', '0909123456', N'123 Lê Lợi, Q1', 'a@example.com', '2025-05-01', '2025-06-01', '59A1-23456', N'Honda', 1, 120000, NULL, 2, 'PC01', N'Vé tháng'),
(2, 'T002', N'Trần Thị B', '0911222333', N'456 Hai Bà Trưng, Q3', 'b@example.com', '2025-05-02', '2025-06-02', '51B2-34567', N'Toyota', 2, 800000, NULL, 3, 'PC02', N'Vé tháng'),
(1, 'T003', N'Lê Văn C', '0933444555', N'789 Nguyễn Huệ, Q1', 'c@example.com', '2025-05-03', '2025-06-03', '60C3-11223', N'Yamaha', 1, 120000, N'Khách quen', 1, 'PC01', N'Vé tháng'),
(3, 'T004', N'Phạm Thị D', '0988777666', N'321 Điện Biên Phủ, Q10', 'd@example.com', '2025-05-04', '2025-06-04', '62D4-33445', N'Ford', 2, 900000, NULL, 4, 'PC03', N'Vé tháng'),
(2, 'T005', N'Ngô Văn E', '0966888999', N'654 Lý Tự Trọng, Q1', 'e@example.com', '2025-05-05', '2025-06-05', '59E5-55667', N'Honda', 1, 120000, NULL, 3, 'PC02', N'Vé tháng'),
(1, 'T006', N'Võ Thị F', '0911666777', N'777 Lê Văn Sỹ, Q3', 'f@example.com', '2025-05-06', '2025-06-06', '51F6-77889', N'Yamaha', 1, 120000, NULL, 2, 'PC01', N'Vé tháng'),
(3, 'T007', N'Phan Văn G', '0944555666', N'888 Nguyễn Trãi, Q5', 'g@example.com', '2025-05-07', '2025-06-07', '60G7-99001', N'Ford', 2, 900000, NULL, 4, 'PC03', N'Vé tháng'),
(1, 'T008', N'Nguyễn Thị H', '0909777888', N'111 Nguyễn Thị Minh Khai, Q1', 'h@example.com', '2025-05-08', '2025-06-08', '59H8-11123', N'Honda', 1, 120000, NULL, 1, 'PC01', N'Vé tháng'),
(2, 'T009', N'Lê Văn I', '0977555444', N'222 Lý Thường Kiệt, Q10', 'i@example.com', '2025-05-09', '2025-06-09', '51I9-23456', N'Toyota', 2, 800000, NULL, 3, 'PC02', N'Vé tháng'),
(1, 'T010', N'Trần Thị J', '0922333444', N'333 Nguyễn Văn Cừ, Q5', 'j@example.com', '2025-05-10', '2025-06-10', '60J0-34567', N'Yamaha', 1, 120000, NULL, 1, 'PC01', N'Vé tháng'),
(3, 'T011', N'Phạm Văn K', '0911999888', N'444 Điện Biên Phủ, Q3', 'k@example.com', '2025-05-11', '2025-06-11', '62K1-45678', N'Ford', 2, 900000, NULL, 4, 'PC03', N'Vé tháng'),
(2, 'T012', N'Nguyễn Văn L', '0933222111', N'555 Lê Lai, Q1', 'l@example.com', '2025-05-12', '2025-06-12', '59L2-56789', N'Honda', 1, 120000, NULL, 3, 'PC02', N'Vé tháng'),
(1, 'T013', N'Lê Thị M', '0909988777', N'666 Võ Văn Kiệt, Q5', 'm@example.com', '2025-05-13', '2025-06-13', '51M3-67890', N'Yamaha', 1, 120000, NULL, 2, 'PC01', N'Vé tháng'),
(3, 'T014', N'Phan Văn N', '0988111222', N'777 Trần Hưng Đạo, Q1', 'n@example.com', '2025-05-14', '2025-06-14', '60N4-78901', N'Ford', 2, 900000, NULL, 4, 'PC03', N'Vé tháng'),
(1, 'T015', N'Võ Thị O', '0912777333', N'888 Cách Mạng Tháng 8, Q10', 'o@example.com', '2025-05-15', '2025-06-15', '59O5-89012', N'Honda', 1, 120000, NULL, 1, 'PC01', N'Vé tháng');
select * from veThang
GO

-- Hàm chứa thông tin vé tháng cho nhật ký - done 
CREATE FUNCTION dbo.fn_GenThongTinVeThang (
    @MaVeThang INT,
    @MaNhom INT,
    @MaThe VARCHAR(20),
    @ChuXe NVARCHAR(100),
    @DienThoai VARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email VARCHAR(100),
    @NgayKichHoat DATETIME,
    @NgayHetHan DATETIME,
    @BienSo VARCHAR(20),
    @NhanHieu VARCHAR(20),
    @MaLoaiXe INT,
    @GiaVe INT,
    @GhiChu NVARCHAR(500),
    @MaNhanVienXuLy INT,
    @MayTinhXuLy NVARCHAR(50)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    RETURN CONCAT(
        N'Mã vé: ', @MaVeThang, N', Mã nhóm: ', @MaNhom, N', Mã thẻ: ', @MaThe,
        N', Chủ xe: ', @ChuXe, N', Điện thoại: ', @DienThoai, N', Địa chỉ: ', @DiaChi,
        N', Email: ', @Email, N', Ngày kích hoạt: ', FORMAT(@NgayKichHoat, 'yyyy-MM-dd'),
        N', Ngày hết hạn: ', FORMAT(@NgayHetHan, 'yyyy-MM-dd'), N', Biển số: ', @BienSo,
        N', Nhãn hiệu: ', @NhanHieu, N', Loại xe: ', @MaLoaiXe, N', Giá vé: ', FORMAT(@GiaVe, 'N2'),
        N', Ghi chú: ', @GhiChu, N', Máy xử lý: ', @MayTinhXuLy, N', Nhân viên xử lý: ', @MaNhanVienXuLy
    );
END

-- Bảng Nhật Ký xử Lý Vé Tháng - done(thiếu khoá ngoại với nhân viên và nhóm nhân viên)
CREATE TABLE NhatKyXuLyVeThang (
    MaNhatKyXuLyVeThang INT IDENTITY(1,1) PRIMARY KEY,
    MaVeThang INT NOT NULL,
    HanhDong NVARCHAR(20) NOT NULL,
    MaNhanVienXuLy INT,
    MayTinhXuLy NVARCHAR(50) COLLATE Vietnamese_CI_AI,
    NoiDungCu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    NoiDungMoi NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    ThoiGianXuLy DATETIME DEFAULT GETDATE(),
);
select * from NhatKyXuLyVeThang

-- Trigger Thêm Vé Tháng - done
CREATE TRIGGER trg_Insert_VeThang
ON VeThang
AFTER INSERT
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));
    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVienXuLy, MayTinhXuLy, NoiDungMoi
    )
    SELECT 
        i.MaVeThang,
        N'Thêm vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            i.MaVeThang, i.MaNhom, i.MaThe, i.ChuXe, i.DienThoai, i.DiaChi, i.Email,
            i.NgayKichHoat, i.NgayHetHan, i.BienSo, i.NhanHieu, i.LoaiXe, i.GiaVe,
            i.GhiChu, i.MaNhanVienXuLy, i.MayTinhXuLy
        )
    FROM inserted i;
END

-- Trigger Sửa Vé Tháng - done
CREATE TRIGGER trg_Update_VeThang
ON VeThang
AFTER UPDATE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVienXuLy, MayTinhXuLy, NoiDungCu, NoiDungMoi
    )
    SELECT 
        i.MaVeThang,
        N'Sửa vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            d.MaVeThang, d.MaNhom, d.MaThe, d.ChuXe, d.DienThoai, d.DiaChi, d.Email,
            d.NgayKichHoat, d.NgayHetHan, d.BienSo, d.NhanHieu, d.LoaiXe, d.GiaVe,
            d.GhiChu, d.MaNhanVienXuLy, d.MayTinhXuLy
        ),
        dbo.fn_GenThongTinVeThang(
            i.MaVeThang, i.MaNhom, i.MaThe, i.ChuXe, i.DienThoai, i.DiaChi, i.Email,
            i.NgayKichHoat, i.NgayHetHan, i.BienSo, i.NhanHieu, i.LoaiXe, i.GiaVe,
            i.GhiChu, i.MaNhanVienXuLy, i.MayTinhXuLy
        )
    FROM inserted i
    JOIN deleted d ON i.MaVeThang = d.MaVeThang;
END

-- Trigger xoá vé tháng - done
CREATE OR ALTER TRIGGER trg_Delete_VeThang
ON VeThang
AFTER DELETE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVienXuLy, MayTinhXuLy, NoiDungCu
    )
    SELECT 
        d.MaVeThang,
        N'Xoá vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            d.MaVeThang, d.MaNhom, d.MaThe, d.ChuXe, d.DienThoai, d.DiaChi, d.Email,
            d.NgayKichHoat, d.NgayHetHan, d.BienSo, d.NhanHieu, d.LoaiXe, d.GiaVe,
            d.GhiChu, d.MaNhanVienXuLy, d.MayTinhXuLy
        )
    FROM deleted d;
END

--Xoá
drop trigger trg_Delete_VeThang
drop trigger trg_Update_VeThang
drop trigger trg_Insert_VeThang
delete from NhatKyXuLyVeThang
drop table NhatKyXuLyVeThang
GO

--Bảng Vé Lượt
delete from VeLuot
drop table VeLuot
CREATE TABLE VeLuot(
	MaVeLuot INT IDENTITY(1,1) PRIMARY KEY,
    MaThe VARCHAR(20), -- temp NULL
	MaNhom INT, --temp NULL
    ThoiGianVao DATETIME DEFAULT GETDATE() NOT NULL,
	ThoiGianRa DATETIME NULL,
	TrangThai NVARCHAR(20), -- TrangThai IN (N'Chưa ra', N'Đã ra', N'Xe mất thẻ')
	MaNhanVienXuLy INT, -- temp
	MayTinhXuLy NVARCHAR(50),
	CachTinhTien BIT Default 0, -- 0 - Công văn, 1 - Luỹ tiến
    TongTien INT NOT NULL,
	BienSo VARCHAR(20) NOT NULL,
	MaLoaiXe INT, -- Xét theo MaLoaiXe (dễ dàng mở rộng)
    GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
	LoaiVe NVARCHAR(20) COLLATE Vietnamese_CI_AI DEFAULT N'Vé tháng',
    FOREIGN KEY (MaThe) REFERENCES The(MaThe),
    FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom),
    FOREIGN KEY (MaNhanVienXuLy) REFERENCES NhomNhanVien(MaNhomNhanVien)
);
INSERT INTO VeLuot (MaThe, MaNhom, ThoiGianVao, ThoiGianRa, TrangThai, MaNhanVienXuLy, MayTinhXuLy, CachTinhTien, TongTien, BienSo, MaLoaiXe, GhiChu, LoaiVe)
VALUES
('T016', 1, '2025-05-01 08:00:00', '2025-05-01 09:00:00', N'Đã ra', 2, 'PC01', 0, 10000, '59A1-11111', 1, NULL, N'Vé lượt'),
('T017', 2, '2025-05-01 09:15:00', '2025-05-01 10:15:00', N'Đã ra', 3, 'PC02', 0, 15000, '51B2-22222', 2, NULL, N'Vé lượt'),
('T018', 1, '2025-05-01 10:30:00', NULL, N'Chưa ra', 1, 'PC03', 0, 0, '60C3-33333', 1, NULL, N'Vé lượt'),
('T019', 3, '2025-05-01 11:00:00', '2025-05-01 12:00:00', N'Đã ra', 4, 'PC04', 1, 20000, '62D4-44444', 2, NULL, N'Vé lượt'),
('T020', 2, '2025-05-01 12:30:00', NULL, N'Chưa ra', 3, 'PC02', 1, 0, '63E5-55555', 1, NULL, N'Vé lượt'),
('T021', 1, '2025-05-02 08:00:00', '2025-05-02 09:00:00', N'Đã ra', 2, 'PC01', 0, 10000, '64F6-66666', 1, N'Khách thường xuyên', N'Vé lượt'),
('T022', 2, '2025-05-02 09:30:00', NULL, N'Chưa ra', 3, 'PC02', 0, 0, '65G7-77777', 2, NULL, N'Vé lượt'),
('T023', 1, '2025-05-02 10:00:00', '2025-05-02 11:00:00', N'Đã ra', 1, 'PC03', 0, 12000, '66H8-88888', 1, NULL, N'Vé lượt'),
('T024', 3, '2025-05-02 11:30:00', NULL, N'Chưa ra', 4, 'PC04', 1, 0, '67I9-99999', 2, NULL, N'Vé lượt'),
('T025', 2, '2025-05-02 12:00:00', '2025-05-02 13:00:00', N'Đã ra', 3, 'PC02', 1, 18000, '68J0-00000', 1, NULL, N'Vé lượt'),
('T026', 1, '2025-05-03 08:00:00', NULL, N'Chưa ra', 2, 'PC01', 0, 0, '69K1-11111', 1, NULL, N'Vé lượt'),
('T027', 2, '2025-05-03 09:00:00', '2025-05-03 10:00:00', N'Đã ra', 3, 'PC02', 0, 12000, '70L2-22222', 2, NULL, N'Vé lượt'),
('T028', 1, '2025-05-03 10:30:00', NULL, N'Chưa ra', 1, 'PC03', 1, 0, '71M3-33333', 1, NULL, N'Vé lượt'),
('T029', 3, '2025-05-03 11:00:00', '2025-05-03 12:00:00', N'Đã ra', 4, 'PC04', 1, 15000, '72N4-44444', 2, NULL, N'Vé lượt'),
('T030', 2, '2025-05-03 12:30:00', '2025-05-03 13:30:00', N'Đã ra', 3, 'PC02', 0, 10000, '73O5-55555', 1, N'Xe công ty', N'Vé lượt');
select * from VeLuot
GO
-- Thống kê theo máy tinh - done
drop view vw_ThongKeTheoMayTinh
GO

CREATE VIEW vw_ThongKeTheoMayTinh AS
SELECT
    MayTinhXuLy,
	vl.MaLoaiXe,
	lx.TenLoaiXe,
    LoaiVe,
	ThoiGianVao,
	ThoiGianRa,
    1 AS SoLuotVao,
    CASE WHEN ThoiGianRa IS NOT NULL THEN 1 ELSE 0 END AS SoLuotRa,
    CASE WHEN ThoiGianRa IS NOT NULL THEN TongTien ELSE 0 END AS TongTien
FROM VeLuot vl JOIN LoaiXe lx 
ON vl.MaLoaiXe = lx.MaLoaiXe

UNION ALL

SELECT
    MayTinhXuLy,
	vt.MaLoaiXe,
	lx.TenLoaiXe,
    LoaiVe,
	NgayKichHoat,
	NgayHetHan,
    1 AS SoLuotVao,
    1 AS SoLuotRa,
    GiaVe AS TongTien
FROM VeThang vt JOIN LoaiXe lx 
ON vt.MaLoaiXe = lx.MaLoaiXe;
select * from vw_ThongKeTheoMayTinh
GO

CREATE VIEW vw_ChiTietThongKe AS
SELECT
    vl.MaThe,
    vl.BienSo,
    ISNULL(vt.ChuXe, N'Vé lượt') AS ChuXe,
    ISNULL(lx.TenLoaiXe, N'') AS TenLoaiXe,
    ISNULL(n.TenNhom, N'') AS TenNhom,
    vl.LoaiVe,
    vl.TrangThai,
    vl.ThoiGianVao,
    vl.ThoiGianRa,
    vl.TongTien
FROM VeLuot vl
LEFT JOIN LoaiXe lx ON vl.MaLoaiXe = lx.MaLoaiXe
LEFT JOIN Nhom n ON vl.MaNhom = n.MaNhom
LEFT JOIN VeThang vt ON vl.MaThe = vt.MaThe

UNION ALL

SELECT
    vt.MaThe,
    vt.BienSo,
    ISNULL(vt.ChuXe, N'') AS ChuXe,
    ISNULL(lx.TenLoaiXe, N'') AS TenLoaiXe,
    ISNULL(n.TenNhom, N'') AS TenNhom,
    vt.LoaiVe,
    N'Đã ra' AS TrangThai,
    vt.NgayKichHoat AS ThoiGianVao,
    vt.NgayHetHan AS ThoiGianRa,
    ISNULL(vt.GiaVe, 0) AS TongTien
FROM VeThang vt
LEFT JOIN LoaiXe lx ON vt.MaLoaiXe = lx.MaLoaiXe
LEFT JOIN Nhom n ON vt.MaNhom = n.MaNhom;

select * from vw_ChiTietThongKe

-- Chi tiết từng loại vé và loại xe
CREATE VIEW vw_DienGiaiThongKe AS
SELECT
    v.LoaiVe,
    CASE WHEN v.TenLoaiXe = '' THEN N'- Khác' ELSE N'- ' + v.TenLoaiXe END AS DienGiai,
    COUNT(*) AS SoLuong,
    SUM(v.TongTien) AS TongTien
FROM vw_ChiTietThongKe v
GROUP BY v.LoaiVe, v.TenLoaiXe

UNION ALL

-- Tổng cộng theo loại vé
SELECT
    v.LoaiVe,
    N'Tổng:' AS DienGiai,
    COUNT(*) AS SoLuong,
    SUM(v.TongTien) AS TongTien
FROM vw_ChiTietThongKe v
GROUP BY v.LoaiVe

UNION ALL

-- Tổng cộng tất cả
SELECT
    N'Tổng:' AS LoaiVe,
    N'Tổng:' AS DienGiai,
    COUNT(*) AS SoLuong,
    SUM(v.TongTien) AS TongTien
FROM vw_ChiTietThongKe v;
select * from vw_DienGiaiThongKe

--------------------------------------------------------------------------
select * from vw_BangDienGiai
CREATE VIEW vw_BangDienGiai AS
-- Dòng tiêu đề: Vé lượt
SELECT
    N'Vé lượt' AS DienGiai,
    NULL AS SoLuong,
    NULL AS TongTien
UNION ALL
-- Dữ liệu Vé lượt - Xe oto
SELECT
    N'- Xe oto',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé lượt' AND v.TenLoaiXe = N'Xe oto'
UNION ALL
-- Dữ liệu Vé lượt - Xe máy
SELECT
    N'- Xe máy',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé lượt' AND v.TenLoaiXe = N'Xe máy'
UNION ALL
-- Dòng Tổng Vé lượt
SELECT
    N'Tổng:',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé lượt'
UNION ALL
-- Dòng tiêu đề: Vé tháng
SELECT
    N'Vé tháng',
    NULL,
    NULL
UNION ALL
-- Dữ liệu Vé tháng - Xe oto
SELECT
    N'- Xe oto',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé tháng' AND v.TenLoaiXe = N'Xe oto'
UNION ALL
-- Dữ liệu Vé tháng - Xe máy
SELECT
    N'- Xe máy',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé tháng' AND v.TenLoaiXe = N'Xe máy'
UNION ALL
-- Dòng Tổng Vé tháng
SELECT
    N'Tổng:',
    COUNT(*),
    SUM(v.TongTien)
FROM vw_ChiTietThongKe v
WHERE v.LoaiVe = N'Vé tháng';
--------------------------------------------------------------------------


-- Bảng tính tiền
drop table TinhTienThang
Create table TinhTienThang(
	MaTinhTienThang INT PRIMARY KEY IDENTITY,
	GiaVeThang INT NOT NULL,
	MaLoaiXe INT,
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
INSERT INTO TinhTienThang (GiaVeThang, MaLoaiXe)
VALUES
(200000, 2),  -- Xe máy
(1000000, 1); -- Ô tô
Go

drop table TinhTienCongVan
CREATE TABLE TinhTienCongVan (
	MaCongVan INT PRIMARY KEY IDENTITY,
	ThuTienTruoc BIT Default 0, --1 là có, 0 là không 
    DemTu TINYINT,
    DemDen TINYINT,
    GioGiaoNgayDem TINYINT,
    GiaThuong INT,
    GiaDem INT,
    GiaNgayDem INT,
    GiaPhuThu INT,
    PhuThuTu TINYINT,
    PhuThuDen TINYINT,
	MaLoaiXe INT,
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
INSERT INTO TinhTienCongVan (
    ThuTienTruoc, DemTu, DemDen, GioGiaoNgayDem,
    GiaThuong, GiaDem, GiaNgayDem, GiaPhuThu,
    PhuThuTu, PhuThuDen, MaLoaiXe
)
VALUES
(0, 23, 6, 10, 3000, 4000, 5000, 1000, 22, 6, 2),
(1, 23, 6, 10, 10000, 12000, 15000, 5000, 22, 6, 1);
GO

drop table TinhTienLuyTien
CREATE TABLE TinhTienLuyTien (
	MaLuyTien INT PRIMARY KEY IDENTITY,
    Moc1 TINYINT, 
    GiaMoc1 INT,
    Moc2 TINYINT, 
    GiaMoc2 INT,
    GiaVuotMoc INT,
    ChuKy TINYINT, 
    CongMoc TINYINT, -- 0: không cộng, 1: cộng mốc 1, 2: cộng cả 2
	MaLoaiXe INT,
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
INSERT INTO TinhTienLuyTien (
    Moc1, GiaMoc1, Moc2, GiaMoc2, GiaVuotMoc,
    ChuKy, CongMoc, MaLoaiXe
)
VALUES
(1, 3000, 2, 6000, 2000, 1, 1, 2),
(1, 10000, 3, 25000, 8000, 1, 2, 1);
GO

select * from TinhTienThang
select * from TinhTienCongVan
select * from TinhTienLuyTien


-- Bảng Nhân Viên - done
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

-- Tăng Giảm Cap Nhat So lượng nhân viên - done
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

SET IDENTITY_INSERT NhanVien ON;
INSERT INTO NhanVien (MaNhanVien, MaNhomNhanVien, HoTen, MaThe, TenDangNhap, MatKhau, GhiChu)
VALUES
(1, 1, N'Nguyễn Văn A', 'T006', 'admin', 'admin', N'Nhân viên phòng kỹ thuật'),
(2, 2, N'Trần Thị B', 'T007', 'tranthib', 'matkhau123', N'Nhân viên phòng kinh doanh'),
(3, 1, N'Lê Văn C', 'T008', 'levanc', 'matkhau123', N'Nhân viên phòng kỹ thuật'),
(4, 3, N'Phạm Thị D', 'T009', 'phamthid', 'matkhau123', N'Nhân viên phòng kế toán'),
(5, 2, N'Hoàng Văn E', 'T010', 'hoangvane', 'matkhau123', N'Nhân viên phòng kinh doanh');
SET IDENTITY_INSERT NhanVien OFF;

GO

CREATE TABLE ChucNang (
    MaChucNang INT PRIMARY KEY,
    TenChucNang NVARCHAR(100)
);

CREATE TABLE PhanQuyen (
    MaNhomNhanVien INT,
    MaChucNang INT,
    CoQuyen BIT,
    PRIMARY KEY (MaNhomNhanVien, MaChucNang),
    FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
);
GO

-- Nhập ký đăng nhập - done
CREATE TABLE NhatKyDangNhap (
    ID INT IDENTITY(1,1) PRIMARY KEY, -- nên chỉnh lại MaNhatKyDangNhap
    MaNhanVien INT NOT NULL,
    ThoiGianDangNhap DATETIME NOT NULL DEFAULT GETDATE(),
    ThoiGianDangXuat DATETIME NULL,
    TongThoiGian AS 
        DATEDIFF(SECOND, ThoiGianDangNhap, ISNULL(ThoiGianDangXuat, GETDATE())),
	FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
);
select * from NhatKyDangNhap
GO

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

SELECT name FROM sys.databases;
sp_help 'VeThang'

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

-- Toàn bộ trigger
SELECT 
    name AS TriggerName,
    OBJECT_NAME(parent_id) AS TableName,
    type_desc AS TriggerType,
    create_date,
    modify_date,
    OBJECT_DEFINITION(object_id) AS TriggerDefinition
FROM sys.triggers;

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

-- Xoá toàn bộ bảng (cẩn thận)
DECLARE @dropSQL NVARCHAR(MAX) = '';
SELECT @dropSQL = @dropSQL + 'DROP TABLE [' + TABLE_SCHEMA + '].[' + TABLE_NAME + '];' + CHAR(13)
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @dropSQL;

-- lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;

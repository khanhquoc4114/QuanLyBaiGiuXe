-- Tạo CSDL
--CREATE DATABASE testdoxe2 COLLATE Vietnamese_CI_AI;
--USE testdoxe2;
--set dateformat ymd;
--GO

--Lấy toàn bộ database
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'SELECT * FROM ' + TABLE_NAME + '; ' 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';
EXEC sp_executesql @sql;
GO 
-- ============================================================== Bảng Loại Xe 
CREATE TABLE LoaiXe (
	MaLoaiXe INT IDENTITY(1,1) PRIMARY KEY,
	TenLoaiXe NVARCHAR(50) COLLATE Vietnamese_CI_AI, 
	TrangThai NVARCHAR(50) COLLATE Vietnamese_CI_AI Default N'Sử dụng'
); 
Go
-- sp bảng loai xe
Create or alter procedure sp_bangloaixe
AS
BEGIN
	Select MaLoaiXe,
	TenLoaiXe AS N'Tên loại xe',
	TrangThai AS N'Trạng thái'
	from Loaixe 
END 
GO
exec sp_bangloaixe
GO

-- ============================================================== Bảng Nhóm Vé Tháng
CREATE TABLE Nhom (
    MaNhom INT IDENTITY(1,1) PRIMARY KEY,
    TenNhom NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);
Go

-- ============================================================== Bảng Thẻ 
CREATE TABLE The (
    MaThe VARCHAR(20) PRIMARY KEY,
    SoThuTu INT,
    LoaiThe NVARCHAR(10) COLLATE Vietnamese_CI_AI DEFAULT N'Chung', 
    NgayTaoThe DATETIME2 NOT NULL,
    NgayCapNhatThe DATETIME2 NOT NULL,
    TrangThaiThe NVARCHAR(20) COLLATE Vietnamese_CI_AI default N'Sử dụng' CHECK (TrangThaiThe IN (N'Sử dụng', N'Không sử dụng', N'Mất thẻ'))-- Sử dụng, Không sử dụng, Mất thẻ
);
GO
-- sp hiện bảng thẻ
Create or alter procedure sp_banghienthe
AS
BEGIN
	select 
	ROW_NUMBER() OVER (ORDER BY NgayTaoThe) AS STT,
	MaThe AS N'Mã thẻ', 
	SoThuTu AS N'Số thứ tự', 
	LoaiThe as N'Loại thẻ', 
	NgayTaoThe as N'Ngày tạo thẻ', 
	NgayCapNhatThe as N'Ngày cập nhật', 
	TrangThaiThe as N'Trạng thái thẻ'
	from The
END
GO

--sp đếm thẻ
Create or Alter procedure sp_bangdemthe
AS
BEGIN
	SET NOCOUNT ON;
	SELECT N'Tất cả' AS N'Thống kê', COUNT(*) AS N'Số lượng'
	FROM The
	UNION ALL
	SELECT N'Sử dụng', COUNT(*) FROM The WHERE TrangThaiThe = N'Sử dụng'
	UNION ALL
	SELECT N'Không sử dụng', COUNT(*) FROM The WHERE TrangThaiThe = N'Không sử dụng'
	UNION ALL
	SELECT N'Mất thẻ', COUNT(*) FROM The WHERE TrangThaiThe = N'Mất thẻ';
END
Go
exec sp_bangdemthe
GO

-- ============================================================== Bảng Nhóm Nhân Viên - done
CREATE TABLE NhomNhanVien (
    MaNhomNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    TenNhomNhanVien NVARCHAR(50) COLLATE Vietnamese_CI_AI NOT NULL,
	SoLuongNhanVien INT DEFAULT 0 NOT NULL,
    ThongTinKhac NVARCHAR(255) COLLATE Vietnamese_CI_AI
);
GO
-- sp bảng đếm số nhân viên theo nhóm
Create or Alter procedure sp_bangnhomnhanvien
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 0 As N'MaNhomNhanVien',N'Tổng cộng' AS N'Tên nhóm', SUM(SoLuongNhanVien) AS N'Số lượng nhân viên', '' AS N'Thông tin khác'
	FROM NhomNhanVien
	UNION ALL
	SELECT MaNhomNhanVien, TenNhomNhanVien, SoLuongNhanVien, ThongTinKhac
	FROM NhomNhanVien;
END
GO
exec sp_bangnhomnhanvien
GO

-- ============================================================== Bảng Nhân Viên - done
select * from nhanvien
CREATE TABLE NhanVien (
	MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
	MaNhomNhanVien INT NOT NULL,
	HoTen NVARCHAR(100) COLLATE Vietnamese_CI_AI NOT NULL,
	MaThe VARCHAR(20),
	TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
	MatKhau VARCHAR(255) NOT NULL,
	GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
	TrangThai int COLLATE Vietnamese_CI_AI DEFAULT 1, -- 1 là sử dụng, 0 là khoá
	FOREIGN KEY (MaNhomNhanVien) REFERENCES NhomNhanVien(MaNhomNhanVien),
	FOREIGN KEY (MaThe) REFERENCES The(MaThe),
	CONSTRAINT UQ_NhanVien_MaThe UNIQUE(MaThe)
);
GO
-- Tăng So lượng nhân viên - done
CREATE TRIGGER trg_TangSoLuongNhanVien
ON NhanVien
AFTER INSERT
AS
BEGIN
    UPDATE NhomNhanVien
    SET SoLuongNhanVien = SoLuongNhanVien + cnt.c
    FROM (
        SELECT MaNhomNhanVien, COUNT(*) AS c
        FROM INSERTED
        GROUP BY MaNhomNhanVien
    ) AS cnt
    WHERE NhomNhanVien.MaNhomNhanVien = cnt.MaNhomNhanVien;
END;
GO
-- Giảm số lượng nhân viên
CREATE TRIGGER trg_GiamSoLuongNhanVien
ON NhanVien
AFTER DELETE
AS
BEGIN
    UPDATE NhomNhanVien
    SET SoLuongNhanVien = SoLuongNhanVien - cnt.c
    FROM (
        SELECT MaNhomNhanVien, COUNT(*) AS c
        FROM DELETED
        GROUP BY MaNhomNhanVien
    ) AS cnt
    WHERE NhomNhanVien.MaNhomNhanVien = cnt.MaNhomNhanVien;
END;
GO
-- Cập nhật số lượng nhân viên
CREATE TRIGGER trg_CapNhatSoLuongNhanVien
ON NhanVien
AFTER UPDATE
AS
BEGIN
    IF UPDATE(MaNhomNhanVien)
    BEGIN
        -- Giảm nhóm cũ
        UPDATE NhomNhanVien
        SET SoLuongNhanVien = SoLuongNhanVien - cnt.c
        FROM (
            SELECT MaNhomNhanVien, COUNT(*) AS c
            FROM DELETED
            GROUP BY MaNhomNhanVien
        ) AS cnt
        WHERE NhomNhanVien.MaNhomNhanVien = cnt.MaNhomNhanVien;

        -- Tăng nhóm mới
        UPDATE NhomNhanVien
        SET SoLuongNhanVien = SoLuongNhanVien + cnt.c
        FROM (
            SELECT MaNhomNhanVien, COUNT(*) AS c
            FROM INSERTED
            GROUP BY MaNhomNhanVien
        ) AS cnt
        WHERE NhomNhanVien.MaNhomNhanVien = cnt.MaNhomNhanVien;
    END
END;
GO
-- Thêm Nhân Viên
SET IDENTITY_INSERT NhanVien ON;
SET IDENTITY_INSERT NhanVien OFF;
select * from NhanVien
Go
-- sp bảng nhân viên
CREATE OR ALTER PROCEDURE sp_bangnhanvien
    @TimKiem NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        nv.MaNhanVien,
        nv.HoTen AS N'Họ tên',
        nv.TenDangNhap AS N'Tài khoản',
        nv.MatKhau AS N'Mật khẩu',
        nv.MaThe AS N'Mã thẻ',
		nv.TrangThai As N'Trạng thái',
        nv.GhiChu AS N'Ghi chú',
        nhom.TenNhomNhanVien AS N'Nhóm'
    FROM
        dbo.NhanVien nv
    LEFT JOIN
        dbo.NhomNhanVien nhom ON nv.MaNhomNhanVien = nhom.MaNhomNhanVien
    WHERE
        nv.HoTen LIKE N'%' + @TimKiem + N'%' OR @TimKiem IS NULL;
END
GO
EXEC sp_bangnhanvien
GO
-- ============================================================== Nhập ký đăng nhập - done
CREATE TABLE NhatKyDangNhap (
    MaNhatKyDangNhap INT IDENTITY(1,1) PRIMARY KEY, 
    MaNhanVien INT NOT NULL,
    ThoiGianDangNhap DATETIME2 NOT NULL DEFAULT GETDATE(),
    ThoiGianDangXuat DATETIME2 NULL,
    TongThoiGian AS 
        DATEDIFF(SECOND, ThoiGianDangNhap, ISNULL(ThoiGianDangXuat, GETDATE())),
	FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
GO
CREATE OR ALTER PROCEDURE sp_nhatkydangnhap
    @tgTu DATETIME = NULL,
	@tgDen DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

	SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    SELECT
		ROW_NUMBER() OVER (ORDER BY MaNhatKyDangNhap DESC) AS STT,
        nv.HoTen as N'Tên nhân viên',
		nv.TenDangNhap as N'Tên đăng nhập',
		nk.ThoiGianDangNhap as N'Đăng nhập',
		nk.ThoiGianDangXuat as N'Đăng xuất',
		nk.TongThoiGian as N'Tổng thời gian đăng nhập'
    FROM
        dbo.NhatKyDangNhap nk
    LEFT JOIN
        dbo.NhanVien nv ON nk.MaNhanVien = nv.MaNhanVien
    WHERE
        nk.ThoiGianDangNhap Between @tgTu and @tgDen
END
GO
exec sp_nhatkydangnhap
GO

-- ============================================================== Bảng Vé Tháng - done
CREATE TABLE VeThang (
	MaVeThang INT IDENTITY(1,1) PRIMARY KEY,
	MaNhom INT,
	MaThe VARCHAR(20),
	ChuXe NVARCHAR(100) COLLATE Vietnamese_CI_AI,
	DienThoai VARCHAR(15),
	DiaChi NVARCHAR(200) COLLATE Vietnamese_CI_AI,
	Email VARCHAR(100),
	NgayKichHoat DATETIME2 DEFAULT GETDATE() NOT NULL,
	NgayHetHan DATETIME2 DEFAULT DATEADD(MONTH, 1, GETDATE()),
	BienSo VARCHAR(20),
	NhanHieu VARCHAR(20) COLLATE Vietnamese_CI_AI,
	MaLoaiXe INT,
	GiaVe INT,
	GhiChu NVARCHAR(255) COLLATE Vietnamese_CI_AI,
	MaNhanVien INT,
	MayTinhXuLy NVARCHAR(50),
	LoaiVe NVARCHAR(20) COLLATE Vietnamese_CI_AI DEFAULT N'Vé tháng',
	TrangThai NVarchar(20) COLLATE Vietnamese_CI_AI Default N'Sử dụng' CHECK (TrangThai IN (N'Sử dụng', N'Hết hạn', N'Mất thẻ'))
	FOREIGN KEY (MaThe) REFERENCES The(MaThe),
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe),
	FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
); 
GO

-- Hàm chứa thông tin vé tháng cho nhật ký - done 
IF OBJECT_ID(N'dbo.fn_GenThongTinVeThang', N'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_GenThongTinVeThang;
GO
CREATE FUNCTION dbo.fn_GenThongTinVeThang (
    @MaVeThang INT,
    @MaNhom INT,
    @MaThe VARCHAR(20),
    @ChuXe NVARCHAR(100),
    @DienThoai VARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email VARCHAR(100),
    @NgayKichHoat DATETIME2,
    @NgayHetHan DATETIME2,
    @BienSo VARCHAR(20),
    @NhanHieu VARCHAR(20),
    @MaLoaiXe INT,
    @GiaVe INT,
    @GhiChu NVARCHAR(255),
    @MaNhanVien INT,
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
        N', Ghi chú: ', @GhiChu, N', Máy xử lý: ', @MayTinhXuLy, N', Nhân viên xử lý: ', @MaNhanVien
    );
END
GO

-- Bảng Nhật Ký xử Lý Vé Tháng - done(thiếu khoá ngoại với nhân viên và nhóm nhân viên)
CREATE TABLE NhatKyXuLyVeThang (
    MaNhatKyXuLyVeThang INT IDENTITY(1,1) PRIMARY KEY,
    MaVeThang INT NOT NULL,
    HanhDong NVARCHAR(20) NOT NULL,
    MaNhanVien INT,
	FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    MayTinhXuLy NVARCHAR(50) COLLATE Vietnamese_CI_AI,
    NoiDungCu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    NoiDungMoi NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    ThoiGianXuLy DATETIME2 DEFAULT GETDATE(),
);
Go
create or alter procedure sp_bangnhatkyvethang
	@tgTu DATETIME2 = NULL,
	@tgDen DATETIME2 = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');
	select HanhDong as N'Hành động',
	nv.HoTen N'Nhân viên xử lý',
	ThoiGianXuLy as N'Thời gian xử lý',
	MayTinhXuLy as N'Máy tính xử lý',
	NoiDungCu as N'Nội dung cũ',
	NoiDungMoi as N'Nội dung mới'
	from NhatKyXuLyVeThang nkvt
	Left Join Nhanvien nv on nv.MaNhanVien = nkvt.MaNhanVien
	where ThoiGianXuLy between @tgTu and @tgDen;
END
GO
exec sp_bangnhatkyvethang
GO

-- Trigger Thêm Vé Tháng - done
CREATE OR ALTER TRIGGER trg_Insert_VeThang
ON VeThang
AFTER INSERT
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));
    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVien, MayTinhXuLy, NoiDungMoi
    )
    SELECT 
        i.MaVeThang,
        N'Thêm vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            i.MaVeThang, i.MaNhom, i.MaThe, i.ChuXe, i.DienThoai, i.DiaChi, i.Email,
            i.NgayKichHoat, i.NgayHetHan, i.BienSo, i.NhanHieu, i.MaLoaiXe, i.GiaVe,
            i.GhiChu, i.MaNhanVien, i.MayTinhXuLy
        )
    FROM inserted i;
END
GO
-- Trigger Sửa Vé Tháng - done
CREATE OR ALTER TRIGGER trg_Update_VeThang
ON VeThang
AFTER UPDATE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVien, MayTinhXuLy, NoiDungCu, NoiDungMoi
    )
    SELECT 
        i.MaVeThang,
        N'Sửa vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            d.MaVeThang, d.MaNhom, d.MaThe, d.ChuXe, d.DienThoai, d.DiaChi, d.Email,
            d.NgayKichHoat, d.NgayHetHan, d.BienSo, d.NhanHieu, d.MaLoaiXe, d.GiaVe,
            d.GhiChu, d.MaNhanVien, d.MayTinhXuLy
        ),
        dbo.fn_GenThongTinVeThang(
            i.MaVeThang, i.MaNhom, i.MaThe, i.ChuXe, i.DienThoai, i.DiaChi, i.Email,
            i.NgayKichHoat, i.NgayHetHan, i.BienSo, i.NhanHieu, i.MaLoaiXe, i.GiaVe,
            i.GhiChu, i.MaNhanVien, i.MayTinhXuLy
        )
    FROM inserted i
    JOIN deleted d ON i.MaVeThang = d.MaVeThang;
END
GO
-- Trigger xoá vé tháng - done
CREATE OR ALTER TRIGGER trg_Delete_VeThang
ON VeThang
AFTER DELETE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeThang (
        MaVeThang, HanhDong, MaNhanVien, MayTinhXuLy, NoiDungCu
    )
    SELECT 
        d.MaVeThang,
        N'Xoá vé tháng',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeThang(
            d.MaVeThang, d.MaNhom, d.MaThe, d.ChuXe, d.DienThoai, d.DiaChi, d.Email,
            d.NgayKichHoat, d.NgayHetHan, d.BienSo, d.NhanHieu, d.MaLoaiXe, d.GiaVe,
            d.GhiChu, d.MaNhanVien, d.MayTinhXuLy
        )
    FROM deleted d;
END
GO

-- sp bảng vé tháng
CREATE OR ALTER PROCEDURE sp_ve_thang
	@TimKiem NVarChar(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
		ROW_NUMBER() OVER (ORDER BY vt.NgayHetHan) AS STT,
        MaVeThang,
        vt.MaThe AS N'Mã thẻ',
        BienSo AS N'Biển số',
        ChuXe AS N'Chủ xe',
		nvt.TenNhom as N'Nhóm',
        lx.TenLoaiXe AS N'Loại vé',
        NgayKichHoat AS N'Ngày kích hoạt',
        NgayHetHan AS N'Ngày hết hạn',
        DienThoai AS N'Điện thoại',
        GiaVe AS N'Giá vé',
        nv.HoTen AS N'Nhân viên xử lý',
        DiaChi AS N'Địa chỉ',
        Email AS N'Email',
        NhanHieu AS N'Nhãn hiệu',
        vt.GhiChu AS N'Ghi chú',
        MayTinhXuLy AS N'Máy tính xử lý'
    FROM VeThang vt
	LEFT JOIN Nhom nvt ON vt.MaNhom = nvt.MaNhom
	LEFT JOIN Loaixe lx ON vt.MaLoaiXe = lx.MaLoaiXe
	LEFT JOIN Nhanvien nv on vt.MaNhanVien = nv.MaNhanVien
	where vt.MaThe LIKE N'%' + @TimKiem + N'%' OR @TimKiem IS NULL
	OR vt.BienSo LIKE N'%' + @TimKiem + N'%' OR @TimKiem IS NULL
	OR vt.ChuXe LIKE N'%' + @TimKiem + N'%' OR @TimKiem IS NULL
	OR vt.Email LIKE N'%' + @TimKiem + N'%' OR @TimKiem IS NULL
	;
END
GO
EXEC sp_ve_thang @TimKiem = N'Nguyễn';
GO

-- sp bảng nhóm vé tháng
CREATE OR ALTER PROCEDURE sp_nhomvethang
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        0 AS MaNhom, 
        N'Tổng cộng' AS N'Tên nhóm', 
        COUNT(*) AS N'Số lượng xe', 
        '' AS N'Thông tin khác'
    FROM VeThang
	UNION ALL
    SELECT 
        n.MaNhom, 
        n.TenNhom AS N'Tên nhóm', 
        COUNT(*) AS N'Số lượng xe', 
        n.ThongTinKhac AS N'Thông tin khác'
    FROM Nhom n
    LEFT JOIN VeThang vt ON n.MaNhom = vt.MaNhom
    GROUP BY n.MaNhom, n.TenNhom, n.ThongTinKhac;
END
GO
EXEC sp_nhomvethang;
GO

-- ============================================================== Bảng Vé Lượt
CREATE TABLE VeLuot(
	MaVeLuot INT IDENTITY(1,1) PRIMARY KEY,
    MaThe VARCHAR(20), -- temp NULL
	MaNhom INT default null, 
    ThoiGianVao DATETIME2 DEFAULT GETDATE() NOT NULL,
	ThoiGianRa DATETIME2 DEFAULT NULL,
	TrangThai NVARCHAR(20) COLLATE Vietnamese_CI_AI Default N'Chưa ra', 
	MaNhanVien INT, -- temp
	MayTinhXuLy NVARCHAR(50),
	CachTinhTien int Default 0, -- -1 - Miễn Phí, 0 - Công văn, 1 - Luỹ tiến
    TongTien INT CHECK (TongTien >= 0),
	BienSo VARCHAR(20),
	MaLoaiXe INT default 1,
	LoaiVe NVARCHAR(20) COLLATE Vietnamese_CI_AI DEFAULT N'Vé lượt' CHECK (LoaiVe IN (N'Vé lượt', N'Vé tháng')),
	LaVeThang BIT Default 0,
	MaVeThang INT,
	AnhVaoPath NVARCHAR(255),
	AnhRaPath NVARCHAR(255)
    FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe),
    FOREIGN KEY (MaNhom) REFERENCES Nhom(MaNhom)
);
Go

-- Hiện bảng tra cứu ra vào
CREATE or alter PROCEDURE sp_TraCuuRaVao
    @LoaiTruyVan NVARCHAR(100) = N'Tất cả xe',
    @LoaiVe NVARCHAR(20) = N'Tất cả loại vé',
    @MaLoaiXe INT = -1,
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL,
    @SoThe NVARCHAR(50) = NULL,
    @BienSo NVARCHAR(50) = NULL,
    @MaThe NVARCHAR(50) = NULL,
    @MaNhanVienVao INT = -1,
    @MaNhanVienRa INT = -1
AS
BEGIN
    SET NOCOUNT ON;
	SET @tgTu = ISNULL(@tgTu, '2000-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    SELECT 
        v.MaVeLuot,
        v.MaThe AS N'Mã thẻ',
        v.BienSo AS N'Biển số',
        lx.TenLoaiXe AS N'Loại xe',
        ISNULL(vt.ChuXe, N'Vé lượt') AS N'Chủ xe',
        ISNULL(nvt.TenNhom, N'Vé lượt') AS N'Nhóm',
        v.LoaiVe AS N'Loại vé',
        v.ThoiGianVao AS N'Thời gian vào',
        v.ThoiGianRa AS N'Thời gian ra',
        v.TrangThai AS N'Trạng thái',
        nv.HoTen AS N'Nhân viên xử lý',
        v.MayTinhXuLy AS N'Máy xử lý',
        CASE v.CachTinhTien 
            WHEN 0 THEN N'Công văn' 
            WHEN 1 THEN N'Lũy tiến' 
            ELSE N'Khác' 
        END AS N'Cách tính tiền',
        ISNULL(v.TongTien, 0) AS N'Tổng tiền',
        v.AnhVaoPath AS N'Ảnh vào',
        v.AnhRaPath AS N'Ảnh ra'
    FROM VeLuot v
    LEFT JOIN VeThang vt ON v.MaVeThang = vt.MaVeThang
    LEFT JOIN Nhom nvt ON vt.MaNhom = nvt.MaNhom
    LEFT JOIN LoaiXe lx ON v.MaLoaiXe = lx.MaLoaiXe
    LEFT JOIN NhanVien nv ON v.MaNhanVien = nv.MaNhanVien
    WHERE
        (v.ThoiGianVao >= @tgTu AND v.ThoiGianVao <= @tgDen)
        AND (@LoaiTruyVan = N'Tất cả xe' OR v.TrangThai = @LoaiTruyVan)
        AND (@LoaiVe = N'Tất cả loại vé' OR v.LoaiVe = @LoaiVe)
        AND (@MaLoaiXe = -1 OR v.MaLoaiXe = @MaLoaiXe)
        AND (@SoThe IS NULL OR v.MaThe LIKE '%' + @SoThe + '%')
        AND (@BienSo IS NULL OR v.BienSo LIKE '%' + @BienSo + '%')
        AND (@MaThe IS NULL OR v.MaThe LIKE '%' + @MaThe + '%')
        AND (@MaNhanVienVao = -1 OR v.MaNhanVien = @MaNhanVienVao)
        AND (@MaNhanVienRa = -1 OR v.MaNhanVien = @MaNhanVienRa)
    ORDER BY v.ThoiGianVao DESC;
END
GO
EXEC sp_TraCuuRaVao
    @LoaiTruyVan = N'Tất cả xe',
    @LoaiVe = N'Tất cả loại vé',
    @MaLoaiXe = -1,
    @tgTu = '2025-06-12',
    @tgDen = '2025-06-13',
    @SoThe = NULL,
    @BienSo = NULL,
    @MaThe = NULL,
    @MaNhanVienVao = -1,
    @MaNhanVienRa = -1;
GO

-- Tạo bảng Nhật ký xử lý Vé lượt
CREATE TABLE NhatKyXuLyVeLuot (
    MaNhatKyXuLyVeLuot INT IDENTITY(1,1) PRIMARY KEY,
    HanhDong NVARCHAR(20) NOT NULL,
    MaNhanVien INT,
    MayTinhXuLy NVARCHAR(50) COLLATE Vietnamese_CI_AI,
    NoiDungCu NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    NoiDungMoi NVARCHAR(500) COLLATE Vietnamese_CI_AI,
    ThoiGianXuLy DATETIME2 DEFAULT GETDATE()
);
GO

-- procedure hiển thị table mất thẻ
create or alter procedure sp_xulymatthe
@maveluot int = NULL
as
begin
	select vl.BienSo,
	vl.MaThe,
	lx.TenLoaiXe,
	ThoiGianVao,
	nv.HoTen,
	vl.MayTinhXuLy,
	CachTinhTien,
	vl.MaLoaiXe,
	vl.TongTien,
	vl.AnhVaoPath,
	vl.AnhRaPath
	from veluot vl
	left join LoaiXe lx on lx.MaLoaiXe = vl.MaLoaiXe
	left join nhanvien nv on nv.MaNhanVien = vl.MaNhanVien
	where MaVeLuot = @maveluot
end
go

-- procedure hiện nhật ký vé lượt
create or alter procedure sp_bangnhatkyveluot
	@tgTu DATETIME2 = NULL,
	@tgDen DATETIME2 = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

	select 
	ROW_NUMBER() OVER (ORDER BY nkvl.ThoiGianXuLy DESC) AS STT,
	HanhDong as N'Hành động',
	nv.HoTen N'Nhân viên xử lý',
	ThoiGianXuLy as N'Thời gian xử lý',
	MayTinhXuLy as N'Máy tính xử lý',
	NoiDungCu as N'Nội dung cũ',
	NoiDungMoi as N'Nội dung mới'
	from NhatKyXuLyVeLuot nkvl
	Left Join Nhanvien nv on nv.MaNhanVien = nkvl.MaNhanVien
	where ThoiGianXuLy between @tgTu and @tgDen;
END
GO
exec sp_bangnhatkyveluot
GO

--Tạo bảng nhật ký điều chỉnh giá vé
CREATE TABLE NhatKyDieuChinhVeLuot (
    MaDieuChinh INT IDENTITY(1,1) PRIMARY KEY,
    MaVeLuot INT NOT NULL,
    GiaVeCu INT NOT NULL,
    GiaVeMoi INT NOT NULL,
    MaNhanVien INT NOT NULL,
    MayTinhXuLy NVARCHAR(50),
    ThoiGianXuLy DATETIME2 DEFAULT GETDATE(),
    MaThe VARCHAR(20),
    BienSo VARCHAR(20),
    LoaiXe NVARCHAR(20),
    LoaiVe NVARCHAR(20),
    FOREIGN KEY (MaVeLuot) REFERENCES VeLuot(MaVeLuot),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
Go
-- tạo trigger giá vé
CREATE or alter TRIGGER trg_NhatKyDieuChinh
ON VeLuot
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO NhatKyDieuChinhVeLuot (
        MaVeLuot, GiaVeCu, GiaVeMoi, MaNhanVien, MayTinhXuLy, MaThe, BienSo, LoaiXe, LoaiVe
    )
    SELECT 
        i.MaVeLuot,
        d.TongTien AS GiaVeCu,
        i.TongTien AS GiaVeMoi,
        i.MaNhanVien,
        i.MayTinhXuLy,
        i.MaThe,
        i.BienSo,
        lx.TenLoaiXe,
        i.LoaiVe
    FROM inserted i
    JOIN deleted d ON i.MaVeLuot = d.MaVeLuot
    LEFT JOIN LoaiXe lx ON i.MaLoaiXe = lx.MaLoaiXe
    WHERE i.TongTien <> d.TongTien;
END;
GO
--procedure hiện bảng nhật ký điều chỉnh
CREATE OR ALTER PROCEDURE sp_nhatkydieuchinh
    @tgTu DATETIME2 = NULL,
	@tgDen DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

	SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');
    SELECT 
        ROW_NUMBER() OVER (ORDER BY nk.ThoiGianXuLy DESC) AS STT,
        GiaVeCu AS N'Giá vé cũ',
        GiaVeMoi AS N'Giá vé mới',
        nv.HoTen AS N'Nhân viên xử lý',
        nk.MayTinhXuLy AS N'Máy tính xử lý',
        nk.ThoiGianXuLy AS N'Thời gian xử lý',
        nk.MaThe AS N'Mã thẻ',
        nk.BienSo AS N'Biển số',
        nk.LoaiXe AS N'Loại xe',
        nk.LoaiVe AS N'Loại vé'
    FROM 
        NhatKyDieuChinhVeLuot nk
    LEFT JOIN 
        NhanVien nv ON nk.MaNhanVien = nv.MaNhanVien
	    WHERE
        nk.ThoiGianXuLy Between @tgTu and @tgDen
    ORDER BY 
        nk.ThoiGianXuLy DESC;
END;
GO
exec sp_nhatkydieuchinh
Go

-- Tạo dữ liệu riêng thông tin vé lượt
DROP FUNCTION IF EXISTS dbo.fn_GenThongTinVeLuot;
GO
CREATE FUNCTION dbo.fn_GenThongTinVeLuot (
    @MaVeLuot INT,
    @MaNhom INT,
    @MaThe VARCHAR(20),
    @ChuXe NVARCHAR(100),
    @DienThoai VARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email VARCHAR(100),
    @ThoiGianVao DATETIME2,
    @ThoiGianRa DATETIME2,
    @BienSo VARCHAR(20),
    @NhanHieu NVARCHAR(50),
    @MaLoaiXe INT,
    @TongTien INT,
    @TrangThai NVARCHAR(20),
    @MaNhanVien INT,
    @MayTinhXuLy NVARCHAR(50),
    @MaVeThang INT,
    @AnhVaoPath NVARCHAR(255),
    @AnhRaPath NVARCHAR(255)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    RETURN CONCAT(
        N'[Vé lượt] Mã vé: ', @MaVeLuot,
        N' | Mã nhóm: ', @MaNhom,
        N' | Mã thẻ: ', @MaThe,
        N' | Chủ xe: ', @ChuXe,
        N' | Điện thoại: ', @DienThoai,
        N' | Địa chỉ: ', @DiaChi,
        N' | Email: ', @Email,
        N' | Biển số: ', @BienSo,
        N' | Nhãn hiệu: ', @NhanHieu,
        N' | Mã loại xe: ', @MaLoaiXe,
        N' | Thời gian vào: ', FORMAT(@ThoiGianVao, 'yyyy-MM-dd HH:mm:ss'),
        N' | Thời gian ra: ', ISNULL(FORMAT(@ThoiGianRa, 'yyyy-MM-dd HH:mm:ss'), N'Chưa ra'),
        N' | Tổng tiền: ', FORMAT(@TongTien, '#,##0 VNĐ'),
        N' | Trạng thái: ', @TrangThai,
        N' | Máy tính xử lý: ', @MayTinhXuLy,
        N' | Mã nhân viên xử lý: ', @MaNhanVien,
        N' | Mã vé tháng: ', @MaVeThang,
        N' | Ảnh vào: ', @AnhVaoPath,
        N' | Ảnh ra: ', @AnhRaPath
    );
END;
GO

-- Trigger Thêm Vé lượt
CREATE OR ALTER TRIGGER trg_Insert_VeLuot
ON VeLuot
AFTER INSERT
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeLuot (
        HanhDong, MaNhanVien, MayTinhXuLy, NoiDungMoi
    )
    SELECT 
        N'Thêm vé lượt',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeLuot(
            i.MaVeLuot, i.MaNhom, i.MaThe, NULL, NULL, NULL, NULL,
            i.ThoiGianVao, i.ThoiGianRa, i.BienSo, NULL, i.MaLoaiXe, i.TongTien,
            i.TrangThai, i.MaNhanVien, i.MayTinhXuLy, i.MaVeThang, i.AnhVaoPath, i.AnhRaPath
        )
    FROM inserted i;
END
GO

-- Trigger Sửa Vé lượt
CREATE OR ALTER TRIGGER trg_Update_VeLuot
ON VeLuot
AFTER UPDATE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeLuot (
        HanhDong, MaNhanVien, MayTinhXuLy, NoiDungCu, NoiDungMoi
    )
    SELECT 
        N'Sửa vé lượt',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeLuot(
            d.MaVeLuot, d.MaNhom, d.MaThe, NULL, NULL, NULL, NULL,
            NULL, NULL, d.BienSo, NULL, d.MaLoaiXe, d.TongTien,
            NULL, d.MaNhanVien, d.MayTinhXuLy, d.MaVeThang, d.AnhVaoPath, d.AnhRaPath
        ),
        dbo.fn_GenThongTinVeLuot(
            i.MaVeLuot, i.MaNhom, i.MaThe, NULL, NULL, NULL, NULL,
            NULL, NULL, i.BienSo, NULL, i.MaLoaiXe, i.TongTien,
            NULL, i.MaNhanVien, i.MayTinhXuLy, i.MaVeThang, i.AnhVaoPath, i.AnhRaPath
        )
    FROM inserted i
    JOIN deleted d ON i.MaVeLuot = d.MaVeLuot;
END
GO

-- Trigger Xoá Vé lượt
CREATE OR ALTER TRIGGER trg_Delete_VeLuot
ON VeLuot
AFTER DELETE
AS
BEGIN
    DECLARE @NguoiThucHien INT = CAST(SESSION_CONTEXT(N'NguoiThucHien') AS INT);
    DECLARE @MayTinhXuLy NVARCHAR(100) = CAST(SESSION_CONTEXT(N'MayTinhXuLy') AS NVARCHAR(100));

    INSERT INTO NhatKyXuLyVeLuot (
        HanhDong, MaNhanVien, MayTinhXuLy, NoiDungCu
    )
    SELECT 
        N'Xoá vé lượt',
        @NguoiThucHien,
        @MayTinhXuLy,
        dbo.fn_GenThongTinVeLuot(
            d.MaVeLuot, d.MaNhom, d.MaThe, NULL, NULL, NULL, NULL,
            NULL, NULL, d.BienSo, NULL, d.MaLoaiXe, d.TongTien,
            NULL, d.MaNhanVien, d.MayTinhXuLy, d.MaVeThang, d.AnhVaoPath, d.AnhRaPath
        )
    FROM deleted d;
END
GO

-- Trigger cập nhật giá trị cho vé lượt 
CREATE OR ALTER TRIGGER trg_VeLuot_SetLoaiVe_TongTien
ON VeLuot
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	ALTER TABLE VeLuot DISABLE TRIGGER trg_Update_VeLuot;
    UPDATE vl
    SET 
        LoaiVe = CASE WHEN vl.LaVeThang = 1 THEN N'Vé tháng' ELSE N'Vé lượt' END,
        LaVeThang = CASE WHEN vl.MaVeThang is NULL THEN 0 ELSE 1 END,
        TrangThai = CASE WHEN vl.ThoiGianRa is NULL THEN N'Chưa Ra' ELSE N'Đã ra' END
    FROM VeLuot vl
    INNER JOIN inserted i ON vl.MaVeLuot = i.MaVeLuot;
	ALTER TABLE VeLuot ENABLE TRIGGER trg_Update_VeLuot;
END;
Go

CREATE OR ALTER PROCEDURE sp_hienbangveluot
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    SELECT 
        ROW_NUMBER() OVER (ORDER BY vl.ThoiGianVao DESC) AS STT,
        vl.ThoiGianVao AS N'Thời gian vào',
        vl.ThoiGianRa AS N'Thời gian ra',
        vl.MayTinhXuLy AS N'Máy tính xử lý',
        nv.HoTen AS N'Nhân viên xử lý',
        vl.TongTien AS N'Tổng tiền',
        vl.MaThe AS N'Mã thẻ',
        vl.BienSo AS N'Biển số',
        vl.LoaiVe AS N'Loại vé',
        vl.TrangThai AS N'Trạng thái',
        CASE vl.CachTinhTien
            WHEN -1 THEN N'Miễn phí'
            WHEN 0 THEN N'Công văn'
            WHEN 1 THEN N'Lũy tiến'
            ELSE N'Khác'
        END AS N'Cách tính tiền',
        vl.AnhVaoPath AS N'Ảnh vào',
        vl.AnhRaPath AS N'Ảnh ra'
    FROM VeLuot vl
    LEFT JOIN NhanVien nv ON vl.MaNhanVien = nv.MaNhanVien
    WHERE vl.ThoiGianVao BETWEEN @tgTu AND @tgDen;
END;

GO
CREATE OR ALTER PROCEDURE sp_tongveluot
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    SELECT 
		N'Tổng' as N'Diễn giải',
        COUNT(*) AS N'Số lượng',
        SUM(TongTien) AS N'Tổng tiền'
    FROM VeLuot
    WHERE ThoiGianVao BETWEEN @tgTu AND @tgDen;
END
Go
exec sp_tongveluot
Go
-- ============================================================== F. Bảng thống kê theo máy tính
CREATE Or Alter procedure sp_ThongKeTheoMayTinh 
	@LoaiVe NVARCHAR(20) = NULL,
	@LoaiXe NVARCHAR(20) = NULL,
	@tgTu DATETIME2 = NULL,
	@tgDen DATETIME2 = NULL
AS
BEGIN
	SET NOCOUNT ON;
    SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

	SELECT
		vl.MayTinhXuLy as N'Máy tính',
		lx.TenLoaiXe as N'Loại xe',
		vl.LoaiVe as N'Loại vé',
		COUNT(*) AS N'Số lượt vào',
		SUM(CASE WHEN vl.ThoiGianRa IS NOT NULL THEN 1 ELSE 0 END) AS N'Số lượt ra',
		SUM(vl.TongTien) AS N'Tổng tiền'
	FROM VeLuot vl
	LEFT JOIN LoaiXe lx ON vl.MaLoaiXe = lx.MaLoaiXe
	Where (@LoaiXe IS NULL OR lx.TenLoaiXe = @LoaiXe)
	AND (@LoaiVe IS NULL OR vl.LoaiVe = @LoaiVe)
	AND vl.ThoiGianVao Between @tgTu And @tgDen
	GROUP BY vl.MayTinhXuLy, lx.TenLoaiXe, vl.LoaiVe;
END
Go

-- ============================================================== G. Bảng thống kê tổng quát
Create or alter procedure sp_ThongKeTongQuat 
AS 
Begin
select * from Veluot
END
GO
-- ============================================================== H.1. Bảng thống kê chi tiết
CREATE Or Alter VIEW vw_ThongKeChiTiet AS
SELECT
    vl.MaThe,
    vl.BienSo,
    ISNULL(vt.ChuXe, N'Vé Lượt') AS ChuXe,
    lx.TenLoaiXe AS TenLoaiXe,
    ISNULL(n.TenNhom, N'Vé lượt') AS TenNhom,
    vl.LoaiVe,
    vl.TrangThai,
    vl.ThoiGianVao,
    vl.ThoiGianRa,
    vl.TongTien
FROM VeLuot vl
LEFT JOIN LoaiXe lx ON vl.MaLoaiXe = lx.MaLoaiXe
LEFT JOIN Nhom n ON vl.MaNhom = n.MaNhom
LEFT JOIN VeThang vt ON vl.MaThe = vt.MaThe
GO
select * from vw_ThongKeChiTiet
Go
-- ============================================================== H.2. Bảng diễn giải thống kê
CREATE OR ALTER PROCEDURE sp_BangDienGiai
    @LoaiXe NVARCHAR(50) = NULL,
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL,
	@TrangThai NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    -- CTE cho Vé lượt
    ;WITH VeLuotFiltered AS (
        SELECT vl.*, lx.TenLoaiXe
        FROM VeLuot vl
        JOIN LoaiXe lx ON vl.MaLoaiXe = lx.MaLoaiXe
        WHERE vl.ThoiGianVao BETWEEN @tgTu AND @tgDen
          AND (@LoaiXe IS NULL OR lx.TenLoaiXe = @LoaiXe)
		  AND (@TrangThai IS NULL OR vl.TrangThai = @TrangThai)
    ),
    -- CTE cho Vé tháng
    VeThangFiltered AS (
        SELECT vt.*, lx.TenLoaiXe
        FROM VeThang vt
        JOIN LoaiXe lx ON vt.MaLoaiXe = lx.MaLoaiXe
        WHERE vt.NgayKichHoat BETWEEN @tgTu AND @tgDen
          AND (@LoaiXe IS NULL OR lx.TenLoaiXe = @LoaiXe)
    )

    -- Gộp kết quả ra 1 bảng duy nhất
    SELECT  [Diễn giải], [Số lượng], [Tổng tiền]FROM (
        SELECT N'Vé lượt' AS [Diễn giải], NULL AS [Số lượng], NULL AS [Tổng tiền], 1 AS ThuTu
        UNION ALL
        SELECT N'- ' + TenLoaiXe, COUNT(*), SUM(TongTien), 2
        FROM VeLuotFiltered
        GROUP BY TenLoaiXe
        UNION ALL
        SELECT N'Tổng:', COUNT(*), SUM(TongTien), 3
        FROM VeLuotFiltered
        UNION ALL
        SELECT N'Vé tháng', NULL, NULL, 4
        UNION ALL
        SELECT N'- ' + TenLoaiXe, COUNT(*), SUM(GiaVe), 5
        FROM VeThangFiltered
        GROUP BY TenLoaiXe
        UNION ALL
        SELECT N'Tổng:', COUNT(*), SUM(GiaVe), 6
        FROM VeThangFiltered
    ) AS TongHop
    ORDER BY ThuTu;
END;
GO
EXEC sp_BangDienGiai @LoaiXe = NULL, @tgTu='2025-04-03', @tgDen = '2025-5-3', @TrangThai = N'Đã ra';
EXEC sp_BangDienGiai @LoaiXe = NULL, @tgTu='2025-04-03', @tgDen = '2025-5-3', @TrangThai = NULL;
GO

-- ============================================================== I. Thống kê theo thời gian 
CREATE OR ALTER PROCEDURE sp_ThongKeTheoThoiGian
    @KieuThongKe NVARCHAR(10),   -- 'ngày', 'tuan', 'thang', 'nam'
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL,
    @LoaiVe NVARCHAR(10) = NULL,
    @LoaiXe NVARCHAR(50) = NULL,
    @MaNhanVien NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalize thời gian
    SET @tgTu = ISNULL(@tgTu, '2000-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');

    ;WITH ThongKeCTE AS (
        SELECT
            CASE 
                WHEN @KieuThongKe = N'ngày'  THEN FORMAT(Ve.ThoiGian, 'dd/MM/yyyy')
                WHEN @KieuThongKe = N'tuần'  THEN CAST(DATEPART(WEEK, Ve.ThoiGian) AS NVARCHAR) + N' /' + CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
                WHEN @KieuThongKe = N'tháng' THEN CAST(MONTH(Ve.ThoiGian) AS NVARCHAR) + N' /' + CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
                WHEN @KieuThongKe = N'năm'   THEN CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
            END AS N'ThoigianGroup',
            MIN(Ve.ThoiGian) AS ThoiGianThuc,
            COUNT(CASE WHEN Ve.IsVao = 1 THEN 1 END) AS SoLuotVao,
            COUNT(CASE WHEN Ve.IsVao = 0 THEN 1 END) AS SoLuotRa,
            SUM(Ve.TongTien) AS TongTien
        FROM (
            -- Vé lượt
            SELECT ThoiGianVao AS ThoiGian, 1 AS IsVao, TongTien, MaLoaiXe, MaNhanVien
            FROM VeLuot
            WHERE @LoaiVe IS NULL OR LoaiVe = @LoaiVe

            UNION ALL

            SELECT ThoiGianRa AS ThoiGian, 0 AS IsVao, TongTien, MaLoaiXe, MaNhanVien
            FROM VeLuot
            WHERE @LoaiVe IS NULL OR LoaiVe = @LoaiVe

            UNION ALL

            SELECT NgayKichHoat AS ThoiGian, 1 AS IsVao, GiaVe AS TongTien, MaLoaiXe, MaNhanVien
            FROM VeThang
            WHERE @LoaiVe IS NULL OR @LoaiVe = N'Vé tháng'
        ) Ve
        JOIN LoaiXe lx ON Ve.MaLoaiXe = lx.MaLoaiXe
        WHERE 
            Ve.ThoiGian BETWEEN @tgTu AND @tgDen
            AND (@LoaiXe IS NULL OR lx.TenLoaiXe = @LoaiXe)
            AND (@MaNhanVien IS NULL OR Ve.MaNhanVien = @MaNhanVien)
        GROUP BY 
            CASE 
                WHEN @KieuThongKe = N'ngày'  THEN FORMAT(Ve.ThoiGian, 'dd/MM/yyyy')
                WHEN @KieuThongKe = N'tuần'  THEN CAST(DATEPART(WEEK, Ve.ThoiGian) AS NVARCHAR) + N' /' + CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
                WHEN @KieuThongKe = N'tháng' THEN CAST(MONTH(Ve.ThoiGian) AS NVARCHAR) + N' /' + CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
                WHEN @KieuThongKe = N'năm'   THEN CAST(YEAR(Ve.ThoiGian) AS NVARCHAR)
            END
    )

    SELECT 
        ROW_NUMBER() OVER (ORDER BY ThoiGianThuc DESC) AS STT,
        ThoigianGroup AS N'Thời gian',
        SoLuotVao AS N'Số lượt vào',
        SoLuotRa AS N'Số lượt ra',
        TongTien
    FROM ThongKeCTE
    ORDER BY ThoiGianThuc DESC;
END;

Go
EXEC sp_ThongKeTheoThoiGian 
    @KieuThongKe = 'ngày',
    @tgTu = '2025-04-01',
    @tgDen = '2025-06-30',
    @LoaiVe = 'Vé tháng',
    @LoaiXe = 'Xe máy',
    @MaNhanVien = NULL;
GO

-- ============================================================== J. Bảng thống kê theo nhân viên
CREATE OR ALTER PROCEDURE sp_ThongKeTheoNhanVien
    @LoaiVe NVARCHAR(50) = N'Tất cả loại vé',
    @MaLoaiXe INT = -1,
    @tgTu DATETIME2 = NULL,
    @tgDen DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET @tgTu = ISNULL(@tgTu, '1900-01-01');
    SET @tgDen = ISNULL(@tgDen, '3000-01-01');
    SELECT
		ROW_NUMBER() OVER (ORDER BY nv.HoTen) AS N'STT',
        nv.HoTen AS N'Tên Nhân Viên',
        lx.TenLoaiXe AS N'Loại Xe',
        vl.LoaiVe AS N'Loại vé',
        COUNT(*) AS N'Số lượt vào',
        SUM(CASE WHEN vl.ThoiGianRa IS NOT NULL THEN 1 ELSE 0 END) AS N'Số lượt ra',
        SUM(ISNULL(vl.TongTien, 0)) AS N'Tổng tiền'
    FROM VeLuot vl
    LEFT JOIN NhanVien nv ON vl.MaNhanVien = nv.MaNhanVien
    LEFT JOIN LoaiXe lx ON vl.MaLoaiXe = lx.MaLoaiXe
    WHERE
        (@LoaiVe = N'Tất cả loại vé' OR vl.LoaiVe = @LoaiVe)
        AND (@MaLoaiXe = -1 OR vl.MaLoaiXe = @MaLoaiXe)
        AND (@tgTu IS NULL OR vl.ThoiGianVao >= @tgTu)
        AND (@tgDen IS NULL OR vl.ThoiGianVao <= @tgDen)
    GROUP BY nv.Hoten, lx.TenLoaiXe, vl.LoaiVe
END
GO
EXEC sp_ThongKeTheoNhanVien 
GO

-- ============================================================== Bảng tính tiền tháng
CREATE TABLE TinhTienThang (
    MaTinhTienThang INT PRIMARY KEY IDENTITY,
    GiaVeThang INT NOT NULL,
    MaLoaiXe INT UNIQUE,
	PhutMienPhi int,
    FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
Go
CREATE OR ALTER PROCEDURE sp_upsertTinhTienThang
    @MaLoaiXe INT,
    @GiaVeThang INT,
    @PhutMienPhi INT
AS
BEGIN
    MERGE TinhTienThang AS target
    USING (
        SELECT @MaLoaiXe AS MaLoaiXe, @GiaVeThang AS GiaVeThang, @PhutMienPhi AS PhutMienPhi
    ) AS source
    ON (target.MaLoaiXe = source.MaLoaiXe)
    WHEN MATCHED THEN
        UPDATE SET 
            GiaVeThang = source.GiaVeThang,
            PhutMienPhi = source.PhutMienPhi
    WHEN NOT MATCHED THEN
        INSERT (GiaVeThang, MaLoaiXe, PhutMienPhi)
        VALUES (source.GiaVeThang, source.MaLoaiXe, source.PhutMienPhi);
END
GO

-- ============================================================== Bảng tính tiền công văn
CREATE TABLE TinhTienCongVan (
	MaCongVan INT PRIMARY KEY IDENTITY,
	ThuTienTruoc BIT Default 0,
    DemTu TINYINT,
    DemDen TINYINT,
    GioGiaoNgayDem TINYINT,
    GiaThuong INT,
    GiaDem INT,
    GiaNgayDem INT,
    GiaPhuThu INT,
    PhuThuTu TINYINT,
    PhuThuDen TINYINT,
	MaLoaiXe INT Unique,
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
Go
-- insert và update TinhTienCongVan
CREATE or alter PROCEDURE sp_UpsertTinhTienCongVan
    @MaLoaiXe INT,
    @ThuTienTruoc BIT,
    @DemTu TINYINT,
    @DemDen TINYINT,
    @GioGiaoNgayDem TINYINT,
    @GiaThuong INT,
    @GiaDem INT,
    @GiaNgayDem INT,
    @GiaPhuThu INT,
    @PhuThuTu TINYINT,
    @PhuThuDen TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    MERGE TinhTienCongVan AS target
    USING (
        SELECT 
            @MaLoaiXe AS MaLoaiXe,
            @ThuTienTruoc AS ThuTienTruoc,
            @DemTu AS DemTu,
            @DemDen AS DemDen,
            @GioGiaoNgayDem AS GioGiaoNgayDem,
            @GiaThuong AS GiaThuong,
            @GiaDem AS GiaDem,
            @GiaNgayDem AS GiaNgayDem,
            @GiaPhuThu AS GiaPhuThu,
            @PhuThuTu AS PhuThuTu,
            @PhuThuDen AS PhuThuDen
    ) AS source
    ON target.MaLoaiXe = source.MaLoaiXe
    WHEN MATCHED THEN 
        UPDATE SET 
            ThuTienTruoc = source.ThuTienTruoc,
            DemTu = source.DemTu,
            DemDen = source.DemDen,
            GioGiaoNgayDem = source.GioGiaoNgayDem,
            GiaThuong = source.GiaThuong,
            GiaDem = source.GiaDem,
            GiaNgayDem = source.GiaNgayDem,
            GiaPhuThu = source.GiaPhuThu,
            PhuThuTu = source.PhuThuTu,
            PhuThuDen = source.PhuThuDen
    WHEN NOT MATCHED THEN 
        INSERT (
            MaLoaiXe, ThuTienTruoc, DemTu, DemDen, GioGiaoNgayDem,
            GiaThuong, GiaDem, GiaNgayDem, GiaPhuThu, PhuThuTu, PhuThuDen
        )
        VALUES (
            source.MaLoaiXe, source.ThuTienTruoc, source.DemTu, source.DemDen, source.GioGiaoNgayDem,
            source.GiaThuong, source.GiaDem, source.GiaNgayDem, source.GiaPhuThu, source.PhuThuTu, source.PhuThuDen
        );
END;
GO
select * from TinhTienCongVan
Go

-- ============================================================== Bảng tính tiền luỹ tiến - done
CREATE TABLE TinhTienLuyTien (
	MaLuyTien INT PRIMARY KEY IDENTITY,
    Moc1 TINYINT, 
    GiaMoc1 INT,
    Moc2 TINYINT, 
    GiaMoc2 INT,
    GiaVuotMoc INT,
    ChuKy TINYINT, 
    CongMoc TINYINT, -- 0: không cộng, 1: cộng mốc 1, 2: cộng cả 2
	MaLoaiXe INT unique,
	FOREIGN KEY (MaLoaiXe) REFERENCES LoaiXe(MaLoaiXe)
);
Go
-- insert và update Tinh tiền luỹ tiến
CREATE or alter PROCEDURE sp_UpsertTinhTienLuyTien
    @MaLoaiXe INT,
    @Moc1 TINYINT,
    @GiaMoc1 INT,
    @Moc2 TINYINT,
    @GiaMoc2 INT,
    @GiaVuotMoc INT,
    @ChuKy TINYINT,
    @CongMoc TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    MERGE TinhTienLuyTien AS target
    USING (SELECT 
                @MaLoaiXe AS MaLoaiXe,
                @Moc1 AS Moc1,
                @GiaMoc1 AS GiaMoc1,
                @Moc2 AS Moc2,
                @GiaMoc2 AS GiaMoc2,
                @GiaVuotMoc AS GiaVuotMoc,
                @ChuKy AS ChuKy,
                @CongMoc AS CongMoc
           ) AS source
    ON (target.MaLoaiXe = source.MaLoaiXe)
    WHEN MATCHED THEN
        UPDATE SET 
            Moc1 = source.Moc1,
            GiaMoc1 = source.GiaMoc1,
            Moc2 = source.Moc2,
            GiaMoc2 = source.GiaMoc2,
            GiaVuotMoc = source.GiaVuotMoc,
            ChuKy = source.ChuKy,
            CongMoc = source.CongMoc
    WHEN NOT MATCHED THEN
        INSERT (MaLoaiXe, Moc1, GiaMoc1, Moc2, GiaMoc2, GiaVuotMoc, ChuKy, CongMoc)
        VALUES (source.MaLoaiXe, source.Moc1, source.GiaMoc1, source.Moc2, source.GiaMoc2, source.GiaVuotMoc, source.ChuKy, source.CongMoc);
END

GO

-- ============================================================== Phân quyền - not done
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

-- ============================================================== Phân quyền - not done
CREATE TABLE CauHinhHeThong (
    TenCongTy NVARCHAR(255),
    DiaChi NVARCHAR(255),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(20), -- 
    TienPhatMatThe INT, -- đồng 
    SoLuongXeToiDa INT, -- chiếc
    HanMucVeThang INT,	-- ngày
    HinhThucThuPhi INT,      -- -1 - miễn phí, 0 công văn, 1 luỹ tiến
    XuLyVeThangHetHan INT        -- -1 - không cho vào, 0 cảnh cáo, 1 như vé lượt
);
GO

-- Thêm data cần thiết để chạy chương trình
select * from TinhTienCongVan
Go
insert into The (MaThe, NgayTaoThe, NgayCapNhatThe) values ('T001', GETDATE(), GETDATE());
Go
INSERT INTO NhomNhanVien (TenNhomNhanVien) VALUES (N'Quản trị viên');
Go
select * from NhanVien
Insert into NhanVien (MaNhomNhanVien, HoTen, MaThe, TenDangNhap, MatKhau)
values (1,N'Quốc','T001','admin','admin');

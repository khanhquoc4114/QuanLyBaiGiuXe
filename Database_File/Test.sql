CREATE TABLE VeThang (
    MaVe INT PRIMARY KEY,
    MaThe VARCHAR(50),
    ChuXe NVARCHAR(100),
    DienThoai VARCHAR(10),
    Email VARCHAR(100),
    DiaChi NVARCHAR(200),
    BienSo VARCHAR(20),
    LoaiXe VARCHAR(20),
    NgayKichHoat DATETIME,
    NgayHetHan DATETIME,
    GiaVe DECIMAL(15, 2),
    GhiChu NVARCHAR(500),
    Nhom INT,
    FOREIGN KEY (MaThe) REFERENCES The(MaThe)
);

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

CREATE TABLE The (
    MaThe VARCHAR(50) PRIMARY KEY,
    LoaiThe VARCHAR(20),
    NgayTaoThe DATETIME,
    NgayCapNhatThe DATETIME,
    TrangThai VARCHAR(20)
);

INSERT INTO The (MaThe, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThai)
VALUES 
    ('0055085211', 'Monthly', '2025-03-10 09:30:00', '2025-03-10 09:30:00', 'Active'),
    ('0055052082', 'PayPerUse', '2025-03-12 14:15:00', '2025-03-12 14:15:00', 'Active');

Select * from VeThang


# Hệ Thống Quản Lý Bãi Giữ Xe (Parking Management System)

Đây là đồ án chuyên ngành xây dựng hệ thống quản lý bãi giữ xe sử dụng giao diện WinForms (C#), cơ sở dữ liệu SQL Server và mô hình YOLOv5 để nhận diện biển số xe.

## Thư mục chính

- `QuanLyBaiGiuXe/` – Source code WinForms (C#).
- `Database_File/` – File script khởi tạo cơ sở dữ liệu SQL Server.
- `python-server/` – Server Python dùng để chạy mô hình nhận diện biển số xe (YOLOv5).

---

## Hướng dẫn cài đặt và chạy hệ thống

### 1. Khởi tạo cơ sở dữ liệu (SQL Server)

1. Cài đặt **SQL Server** và **SQL Server Management Studio (SSMS)**.
2. Mở file `Filewithoutinsert.sql` trong thư mục `Database_File/` bằng SSMS.
3. Chạy toàn bộ script để tạo các bảng và dữ liệu mẫu.
4. Đảm bảo chuỗi kết nối (connection string) trong project WinForms trỏ đúng đến SQL Server của bạn.

### 2. Build ứng dụng WinForms (Visual Studio)

1. Mở thư mục `QuanLyBaiGiuXe/` bằng **Visual Studio 2022**.
2. Chọn `Build > Build Solution` hoặc bấm `Ctrl+Shift+B`.
3. Sau khi build, thư mục `bin/Debug/` sẽ được tạo ra.
4. Copy thư mục `python-server/` vào bên trong `bin/Debug/`:

```bash
# Từ thư mục gốc của repo
cp -r python-server QuanLyBaiGiuXe/bin/Debug/
```

### 3. Chạy server Python (mô hình nhận diện)

1. Đảm bảo máy đã cài Python 3.8+ và pip.
2. Cài đặt các thư viện cần thiết:

```bash
cd QuanLyBaiGiuXe/bin/Debug/python-server
pip install -r requirements.txt
```

3. Server python sẽ tự động khởi chạy chung với ứng dụng

Server Flask sẽ chạy ở cổng 5000 và chờ nhận ảnh từ ứng dụng WinForms để nhận diện biển số.

---

## Thông tin thêm

- YOLOv5 dùng để nhận diện biển số xe từ ảnh chụp bởi camera.
- Ứng dụng kết nối thiết bị RFID và camera theo thời gian thực.
- Giao tiếp giữa WinForms và Python server bằng TCP/IP.

---

## Thành viên thực hiện

- Nguyễn Đặng Khánh Quốc – 22521212
- Nguyễn Thiên Phú – 22521103

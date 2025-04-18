using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe.Models
{
    class Manager
    {
        Connector db = new Connector();
        public Manager() { }

        #region Vé Lượt
        public DataTable GetAllVeLuot()
        {
            DataTable dtbVeLuot = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM VeLuot", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbVeLuot);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé lượt: " + ex.Message);
            }
            return dtbVeLuot;
        }

        public DataTable GetTong()
        {
            DataTable dtbTong = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                            COUNT(*) AS SoLuong,
                            SUM(GiaVe) AS TongTien
                            FROM VeLuot;", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbTong);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách tổng: " + ex.Message);
            }
            return dtbTong;
        }
        #endregion

        #region Vé Tháng
        public DataTable GetAllVeThang()
        {
            DataTable dtbVeThang = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
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
                    JOIN The t ON vt.MaThe = t.MaThe;", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbVeThang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé tháng: " + ex.Message);
            }
            return dtbVeThang;
        }
        public int GetMaVeThangByBienSo(string BienSo)
        {
            int maVeThang = -1; 

            try
            {
                db.OpenConnection(); // Mở kết nối CSDL

                using (SqlCommand cmd = new SqlCommand("SELECT MaVeThang FROM VeThang WHERE BienSo = @BienSo", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@BienSo", BienSo);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        maVeThang = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy MaVeThang: " + ex.Message);
            }

            return maVeThang;
        }
        public int GetMaNhomByTenNhom(string TenNhom)
        {
            int maNhom = -1;
            try
            {
                db.OpenConnection(); // Mở kết nối CSDL
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhom FROM Nhom WHERE TenNhom = @TenNhom", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TenNhom", TenNhom);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        maNhom = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy MaNhom: " + ex.Message);
            }
            return maNhom;
        }
        public bool SuaVeThang(int MaVeThang, string tenNhom, string maThe, string chuXe, string dienThoai,
                          string diaChi, string email, DateTime ngayKichHoat,
                          DateTime ngayHetHan, string bienSo, string nhanHieu,
                          string loaiXe, decimal giaVe, string ghiChu)
        {
            try
            {
                db.OpenConnection();

                // Lấy MaNhom từ TenNhom
                int maNhom = GetMaNhomByTenNhom(tenNhom);

                using (SqlCommand updateCmd = new SqlCommand(@"
                    UPDATE VeThang 
                    SET MaNhom = @MaNhom, ChuXe = @ChuXe, DienThoai = @DienThoai, 
                        DiaChi = @DiaChi, Email = @Email, NgayKichHoat = @NgayKichHoat, 
                        NgayHetHan = @NgayHetHan, BienSo = @BienSo, NhanHieu = @NhanHieu, 
                        LoaiXe = @LoaiXe, GiaVe = @GiaVe, GhiChu = @GhiChu
                    WHERE MaVeThang = @MaVeThang", db.GetConnection()))
                {
                    updateCmd.Parameters.AddWithValue("@MaNhom", maNhom);
                    updateCmd.Parameters.AddWithValue("@ChuXe", chuXe);
                    updateCmd.Parameters.AddWithValue("@DienThoai", dienThoai);
                    updateCmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.Parameters.AddWithValue("@NgayKichHoat", ngayKichHoat);
                    updateCmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHan);
                    updateCmd.Parameters.AddWithValue("@BienSo", bienSo);
                    updateCmd.Parameters.AddWithValue("@NhanHieu", nhanHieu);
                    updateCmd.Parameters.AddWithValue("@LoaiXe", loaiXe);
                    updateCmd.Parameters.AddWithValue("@GiaVe", giaVe);
                    updateCmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                    updateCmd.Parameters.AddWithValue("@MaVeThang", MaVeThang);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật Vé Tháng: " + ex.Message);
                return false;
            }
        }
        public bool ThemVeThang(string tenNhom, string maThe, string chuXe, string dienThoai,
                          string diaChi, string email, DateTime ngayKichHoat,
                          DateTime ngayHetHan, string bienSo, string nhanHieu,
                          string loaiXe, decimal giaVe, string ghiChu)
        {
            int DaTonTai = GetMaVeThangByBienSo(bienSo);
            if (DaTonTai != -1)
            {
                MessageBox.Show("Biển số đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                db.OpenConnection();

                // Lấy MaNhom từ TenNhom
                int maNhom = GetMaNhomByTenNhom(tenNhom);
                if (maNhom == -1)
                {
                    MessageBox.Show("Nhóm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SqlCommand insertCmd = new SqlCommand(@"
            INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, 
                                 NgayKichHoat, NgayHetHan, BienSo, NhanHieu, 
                                 LoaiXe, GiaVe, GhiChu)
            VALUES (@MaNhom, @MaThe, @ChuXe, @DienThoai, @DiaChi, @Email, 
                    @NgayKichHoat, @NgayHetHan, @BienSo, @NhanHieu, 
                    @LoaiXe, @GiaVe, @GhiChu)", db.GetConnection()))
                {
                    insertCmd.Parameters.AddWithValue("@MaNhom", maNhom);
                    insertCmd.Parameters.AddWithValue("@MaThe", maThe);
                    insertCmd.Parameters.AddWithValue("@ChuXe", chuXe);
                    insertCmd.Parameters.AddWithValue("@DienThoai", dienThoai);
                    insertCmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@NgayKichHoat", ngayKichHoat);
                    insertCmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHan);
                    insertCmd.Parameters.AddWithValue("@BienSo", bienSo);
                    insertCmd.Parameters.AddWithValue("@NhanHieu", nhanHieu);
                    insertCmd.Parameters.AddWithValue("@LoaiXe", loaiXe);
                    insertCmd.Parameters.AddWithValue("@GiaVe", giaVe);
                    insertCmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                    int rowsInserted = insertCmd.ExecuteNonQuery();
                    return rowsInserted > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm vé tháng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public DataTable GetVeThangByMaVeThang(int MaVeThang)
        {
            DataTable dt = new DataTable();

            try
            {
                db.OpenConnection(); // Mở kết nối CSDL

                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        n.TenNhom, 
                        vt.MaThe, 
                        vt.ChuXe,
                        vt.DienThoai,
                        vt.DiaChi,
                        vt.Email,
                        vt.NgayKichHoat, 
                        vt.NgayHetHan, 
                        vt.BienSo,
                        vt.NhanHieu,
                        vt.LoaiXe,
                        vt.GiaVe,
                        vt.GhiChu
                    FROM VeThang vt
                    JOIN Nhom n ON vt.MaNhom = n.MaNhom
                    WHERE vt.MaVeThang = @MaVeThang", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaVeThang", MaVeThang);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin vé tháng: " + ex.Message);
            }

            return dt;
        }
        public List<string> GetDanhSachNhom()
        {
            List<string> danhSachTenNhom = new List<string>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TenNhom FROM Nhom", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachTenNhom.Add(reader["TenNhom"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhóm: " + ex.Message);
            }

            return danhSachTenNhom;
        }

        public bool XoaVeThang(string BienSo)
        {
            int MaVeThang = -1;
            MaVeThang = GetMaVeThangByBienSo(BienSo);
            if (MaVeThang == -1)
            {
                MessageBox.Show("Không tìm thấy vé tháng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM VeThang WHERE MaVeThang = @mavethang", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@mavethang", MaVeThang);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool GiaHanVeThang(int MaVeThang, DateTime NgayHetHan)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("UPDATE VeThang SET NgayHetHan = @NgayHetHan WHERE MaVeThang = @MaVeThang", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@NgayHetHan", NgayHetHan);
                    cmd.Parameters.AddWithValue("@MaVeThang", MaVeThang);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gia hạn vé tháng: " + ex.Message);
                return false;
            }
        }

        public bool DoiTheThang(int MaVeThang, string MaThe)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("UPDATE VeThang SET MaThe = @MaThe WHERE MaVeThang = @MaVeThang", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", MaThe);
                    cmd.Parameters.AddWithValue("@MaVeThang", MaVeThang);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gia hạn vé tháng: " + ex.Message);
                return false;
            }
        }

        public DataTable TimKiemVeThang(string content)
        {
            DataTable dtbVeThang = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
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
            WHERE MaThe LIKE @content
                OR ChuXe LIKE @content
                OR BienSo LIKE @content
                OR DienThoai LIKE @content
                OR DiaChi LIKE @content
                OR Email LIKE @content
                OR NhanHieu LIKE @content
                OR GhiChu LIKE @content", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@content", "%" + content + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbVeThang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé tháng: " + ex.Message);
            }

            return dtbVeThang;
        }

        #endregion

        #region Nhóm
        public DataTable GetAllNhom()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select TenNhom, ThongTinKhac from Nhom", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public bool ThemNhom(string TenNhom, string ThongTinKhac)
        {
            db.OpenConnection();

            // Kiểm tra xem Mã Thẻ đã tồn tại chưa
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Nhom WHERE TenNhom = @tennhom", db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@tennhom", TenNhom);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên nhóm đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Nếu Mã Thẻ chưa tồn tại, thêm vào bảng The
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Nhom (TenNhom, ThongTinKhac)" +
                                                   $" VALUES (@tennhom, @thongtinkhac)", db.GetConnection()))
            {
                //cmd.Parameters.AddWithValue("@mathe", 0);
                cmd.Parameters.AddWithValue("@tennhom", TenNhom);
                cmd.Parameters.AddWithValue("@thongtinkhac", ThongTinKhac);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool XoaNhom(string TenNhom)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Nhom WHERE TenNhom = @tennhom", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@tennhom", TenNhom);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool CapNhatNhom(string TenNhomHienTai, string TenNhomMoi, string ThongTinKhacMoi)
        {
            db.OpenConnection();

            // Tìm IdNhom dựa trên TenNhom
            int MaNhom = -1;
            using (SqlCommand cmd = new SqlCommand("SELECT MaNhom FROM Nhom WHERE TenNhom = @tennhom", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@tennhom", TenNhomHienTai);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MaNhom = Convert.ToInt32(result);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhóm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Cập nhật thông tin nhóm theo IdNhom
            using (SqlCommand updateCmd = new SqlCommand("UPDATE Nhom SET TenNhom = @tennhommoi, ThongTinKhac = @thongtinkhacmoi WHERE MaNhom = @manhom", db.GetConnection()))
            {
                updateCmd.Parameters.AddWithValue("@manhom", MaNhom);
                updateCmd.Parameters.AddWithValue("@tennhommoi", TenNhomMoi);
                updateCmd.Parameters.AddWithValue("@thongtinkhacmoi", ThongTinKhacMoi);

                int rowsAffected = updateCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Thẻ
        public DataTable GetAllThe()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from The", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        public bool XoaThe(string MaThe)
        {
            db.OpenConnection();

            using (SqlCommand cmd = new SqlCommand("DELETE FROM The WHERE MaThe = @mathe", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@mathe", MaThe);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool ThemThe(string MaThe, string TenThe, DateTime NgayTao)
        {
            db.OpenConnection();

            // Kiểm tra xem Mã Thẻ đã tồn tại chưa
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM The WHERE MaThe = @mathe", db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@mathe", MaThe);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Mã Thẻ đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Nếu Mã Thẻ chưa tồn tại, thêm vào bảng The
            using (SqlCommand cmd = new SqlCommand("INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe, TrangThaiThe)" +
                                                   $" VALUES (@mathe, 0, @loaithe, @ngaytao, @ngaycapnhat, @trangthai)", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@mathe", MaThe);
                cmd.Parameters.AddWithValue("@loaithe", TenThe);
                cmd.Parameters.AddWithValue("@ngaytao", NgayTao);
                cmd.Parameters.AddWithValue("@ngaycapnhat", NgayTao);
                cmd.Parameters.AddWithValue("@trangthai", "Sử dụng");

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool KiemTraThe(string maThe)
        {
            try
            {
                db.OpenConnection(); // Mở kết nối CSDL

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM The WHERE MaThe = @MaThe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", maThe);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Trả về true nếu mã thẻ tồn tại, ngược lại false
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra thẻ: " + ex.Message);
                return false;
            }
        }


        #endregion

        #region Nhóm Nhân Viên
        public DataTable GetAllNhomNhanVien()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from NhomNhanVien", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable GetNhomNhanVienByID(string MaNhomNhanVien)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from NhomNhanVien where MaNhomNhanVien = @manhom", db.GetConnection());
            cmd.Parameters.AddWithValue("@manhom", MaNhomNhanVien);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemNhomNhanVien(string TenNhom, string ThongTinKhac)
        {
            db.OpenConnection();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM NhomNhanVien WHERE TenNhomNhanVien = @tennhom", db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@tennhom", TenNhom);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên nhóm nhân viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            using (SqlCommand cmd = new SqlCommand("INSERT INTO NhomNhanVien (TenNhomNhanVien, SoLuongNhanVien, ThongTinKhac) VALUES (@tennhom,0, @thongtinkhac)", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@tennhom", TenNhom);
                cmd.Parameters.AddWithValue("@thongtinkhac", ThongTinKhac);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool CapNhatNhomNhanVien(string MaNhomNhanVien, string TenNhomNhanVien, string ThongTinKhac)
        {
            db.OpenConnection();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM NhomNhanVien WHERE TenNhomNhanVien = @tennhom", db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@tennhom", TenNhomNhanVien);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên nhóm nhân viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            using (SqlCommand updateCmd = new SqlCommand("UPDATE NhomNhanVien SET TenNhomNhanVien = @tennhomnhanvien, ThongTinKhac = @thongtinkhac WHERE MaNhomNhanVien = @MaNhomNhanVien", db.GetConnection()))
            {
                updateCmd.Parameters.AddWithValue("@MaNhomNhanVien", MaNhomNhanVien);
                updateCmd.Parameters.AddWithValue("@tennhomnhanvien", TenNhomNhanVien);
                updateCmd.Parameters.AddWithValue("@thongtinkhac", ThongTinKhac);

                try
                {
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật nhóm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public bool XoaNhomNhanVien(string MaNhomNhanVien)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM NhomNhanVien WHERE MaNhomNhanVien = @manhomnhanvien", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@manhomnhanvien", MaNhomNhanVien);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public List<string> GetDanhSachNhomNhanVien()
        {
            List<string> danhSachTenNhomNhanVien = new List<string>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TenNhomNhanVien FROM NhomNhanVien", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachTenNhomNhanVien.Add(reader["TenNhomNhanVien"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhóm: " + ex.Message);
            }

            return danhSachTenNhomNhanVien;
        }
        #endregion

        #region Nhân Viên
        public DataTable GetAllNhanVien()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@"
                                                SELECT 
                                                    nv.*,
                                                    n.TenNhomNhanVien
                                                FROM NhanVien nv
                                                JOIN NhomNhanVien n ON nv.MaNhomNhanVien = n.MaNhomNhanVien", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable GetNhanVienByID(string MaNhanVien)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand(@"
                                                SELECT 
                                                    nv.*,
                                                    n.TenNhomNhanVien
                                                FROM NhanVien nv
                                                JOIN NhomNhanVien n ON nv.MaNhomNhanVien = n.MaNhomNhanVien
                                                WHERE nv.MaNhanVien = @manhanvien", db.GetConnection());

                cmd.Parameters.AddWithValue("@manhanvien", MaNhanVien);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin nhân viên: " + ex.Message);
            }

            return dt;
        }
        public bool XoaNhanVien(string MaNhanVien)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNhanVien = @manhanvien", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@manhanvien", MaNhanVien);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public int GetMaNhomNhanVienByTen(string tenNhomNhanVien)
        {
            int maNhom = -1;
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhomNhanVien FROM NhomNhanVien WHERE TenNhomNhanVien = @TenNhom", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TenNhom", tenNhomNhanVien);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        maNhom = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã nhóm nhân viên: " + ex.Message);
            }

            return maNhom;
        }
        public int GetMaNhanVienByTen(string tenDangNhap)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaNhanVien FROM NhanVien WHERE TenDangNhap = @TenDangNhap", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool ThemNhanVien(string tenNhom, string hoTen, string maThe, string tenDangNhap, string matKhau, string ghiChu)
        {
            try
            {
                if (GetMaNhanVienByTen(tenDangNhap) != -1)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    return false;
                }

                int maNhom = GetMaNhomNhanVienByTen(tenNhom);
                if (maNhom == -1)
                {
                    MessageBox.Show("Không tìm thấy mã nhóm phù hợp!");
                    return false;
                }

                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NhanVien (MaNhomNhanVien, HoTen, MaThe, TenDangNhap, MatKhau, GhiChu) VALUES (@MaNhom, @HoTen, @MaThe, @TenDangNhap, @MatKhau, @GhiChu)", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaNhom", maNhom);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@MaThe", maThe);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                return false;
            }
        }
        public bool CapNhatNhanVien(string maNhanVien, string tenNhom, string hoTen, string maThe, string tenDangNhap, string matKhau, string ghiChu)
        {
            try
            {
                int maNhom = GetMaNhomNhanVienByTen(tenNhom);
                if (maNhom == -1)
                {
                    MessageBox.Show("Không tìm thấy mã nhóm phù hợp!");
                    return false;
                }

                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                                                UPDATE NhanVien 
                                                SET 
                                                    MaNhomNhanVien = @MaNhom,
                                                    HoTen = @HoTen,
                                                    MaThe = @MaThe,
                                                    TenDangNhap = @TenDangNhap,
                                                    MatKhau = @MatKhau,
                                                    GhiChu = @GhiChu
                                                WHERE MaNhanVien = @MaNhanVien", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaNhom", maNhom);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@MaThe", maThe);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien); // << bạn cần truyền thêm cái này

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                return false;
            }
        }
        public DataTable TimKiemNhanVien(string content)
        {
            DataTable dtbNhanVien = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                        SELECT 
                            nv.*,
                            n.TenNhomNhanVien
                        FROM NhanVien nv
                        JOIN NhomNhanVien n ON nv.MaNhomNhanVien = n.MaNhomNhanVien
                        WHERE MaThe LIKE @content OR
                            nv.HoTen LIKE @content OR 
                            nv.TenDangNhap LIKE @content OR
                            nv.GhiChu LIKE @content", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@content", "%" + content + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbNhanVien);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
            }

            return dtbNhanVien;
        }
        #endregion
    }
}

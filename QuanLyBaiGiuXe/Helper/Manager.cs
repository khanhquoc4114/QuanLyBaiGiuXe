using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Text;
using QuanLyBaiGiuXe.Helper;
using Aqua.EnumerableExtensions;
using System.Security.Policy;

namespace QuanLyBaiGiuXe.Models
{
    class Manager
    {
        Connector db = new Connector();
        public Manager() { }

        #region Gửi thông tin session
        public void GuiSession()
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("EXEC sp_set_session_context @key, @value", db.GetConnection()))
                {
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar, 128);
                    cmd.Parameters.Add("@value", SqlDbType.NVarChar, 256);

                    // Set NguoiThucHien
                    cmd.Parameters["@key"].Value = "NguoiThucHien";
                    cmd.Parameters["@value"].Value = Session.MaNhanVien;
                    cmd.ExecuteNonQuery();

                    // Set MayTinhXuLy
                    cmd.Parameters["@key"].Value = "MayTinhXuLy";
                    cmd.Parameters["@value"].Value = Session.MayTinhXuLy;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi thông tin session: " + ex.Message);
            }
        }
        #endregion

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
        public DataTable GetTongVeLuot()
        {
            DataTable dtbTong = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                            COUNT(*) AS SoLuong,
                            SUM(TongTien) AS TongTien
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
        public DataRow GetVeLuotDangGuiByID(string maThe)
        {
            DataTable dtbVeLuot = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP 1 * 
                        FROM VeLuot 
                        WHERE MaThe = @MaThe AND ThoiGianRa IS NULL 
                        ORDER BY ThoiGianVao DESC", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", maThe);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbVeLuot);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy vé lượt đang gửi: " + ex.Message);
            }

            return (dtbVeLuot.Rows.Count > 0) ? dtbVeLuot.Rows[0] : null;
        }
        public bool CapNhatRaVeLuot(int maVeLuot, DateTime thoiGianRa, decimal giaVe)
        {
            db.OpenConnection();
            using (SqlCommand updateCmd = new SqlCommand("UPDATE VeLuot SET ThoiGianRa = @ThoiGianRa, GiaVe = @GiaVe WHERE MaVeLuot = @MaVeLuot", db.GetConnection()))
            {
                updateCmd.Parameters.AddWithValue("@MaVeLuot", maVeLuot);
                updateCmd.Parameters.AddWithValue("@ThoiGianRa", thoiGianRa);
                updateCmd.Parameters.AddWithValue("@GiaVe", giaVe);

                int rowsAffected = updateCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        #endregion

        #region Vé Tháng
        public DataTable GetAllVeThang(string content = null)
        {
            DataTable table = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_ve_thang", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TimKiem", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(content) ? (object)DBNull.Value : content
                    });

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
            }
            return table;
        }

        public bool KiemTraVeThang(string mathe)
        {
            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM VeThang vt " +
                    "WHERE MaThe = @MaThe ", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", mathe);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Thẻ đã tồn tại trong 1 nhóm vé rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra vé tháng: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
        public bool SuaVeThang(string MaVeThang, string tenNhom, string maThe, string chuXe, string dienThoai,
                          string diaChi, string email, DateTime ngayKichHoat,
                          DateTime ngayHetHan, string bienSo, string nhanHieu,
                          string maloaiXe, decimal giaVe, string ghiChu)
        {
            try
            {
                db.OpenConnection();

                GuiSession(); // Gửi thông tin session

                // Lấy MaNhom từ TenNhom
                int maNhom = GetMaNhomByTenNhom(tenNhom);

                using (SqlCommand updateCmd = new SqlCommand(@"
                    UPDATE VeThang 
                    SET MaNhom = @MaNhom, ChuXe = @ChuXe, DienThoai = @DienThoai, 
                        DiaChi = @DiaChi, Email = @Email, NgayKichHoat = @NgayKichHoat, 
                        NgayHetHan = @NgayHetHan, BienSo = @BienSo, NhanHieu = @NhanHieu, 
                        MaLoaiXe = @MaLoaiXe, GiaVe = @GiaVe, GhiChu = @GhiChu
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
                    updateCmd.Parameters.AddWithValue("@MaLoaiXe", maloaiXe);
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
            try
            {
                db.OpenConnection();
                GuiSession();
                int maNhom = GetMaNhomByTenNhom(tenNhom);
                if (maNhom == -1)
                {
                    MessageBox.Show("Nhóm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, 
                                         NgayKichHoat, NgayHetHan, BienSo, NhanHieu, 
                                         MaLoaiXe, GiaVe, GhiChu, MaNhanVien, MayTinhXuLy)
                    VALUES (@MaNhom, @MaThe, @ChuXe, @DienThoai, @DiaChi, @Email, 
                            @NgayKichHoat, @NgayHetHan, @BienSo, @NhanHieu, 
                            @MaLoaiXe, @GiaVe, @GhiChu, @MaNhanVienXuLy, @MayTinhXuLy)", db.GetConnection()))
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
                    insertCmd.Parameters.AddWithValue("@MaLoaiXe", loaiXe);
                    insertCmd.Parameters.AddWithValue("@GiaVe", giaVe);
                    insertCmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                    insertCmd.Parameters.AddWithValue("@MaNhanVienXuLy", Session.MaNhanVien);
                    insertCmd.Parameters.AddWithValue("@MayTinhXuLy", Session.VaiTro);

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
        public DataTable GetVeThangByMaVeThang(string MaVeThang)
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
                        lx.MaLoaiXe,
                        vt.GiaVe,
                        vt.GhiChu
                    FROM VeThang vt
                    LEFT JOIN Nhom n ON vt.MaNhom = n.MaNhom
                    LEFT JOIN LoaiXe lx ON vt.MaLoaiXe = lx.MaLoaiXe
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

        public bool XoaVeThang(string MaVeThang)
        {
            db.OpenConnection();
            GuiSession();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM VeThang WHERE MaVeThang = @mavethang", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@mavethang", MaVeThang);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool GiaHanVeThang(string MaVeThang, DateTime NgayHetHan)
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
        public bool DoiTheThang(string MaVeThang, string MaThe)
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

        public int GetGiaTienTheoLoaiXe(string maLoaiXe)
        {
            int giaTien = 0;
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT GiaVeThang FROM TinhTienThang WHERE MaLoaiXe = @MaLoaiXe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiXe", maLoaiXe);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        giaTien = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy giá tiền theo loại xe: " + ex.Message);
            }
            return giaTien;
        }
        #endregion

        #region Nhóm Vé Tháng
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
        public DataTable GetAllNhom()
        {
            DataTable table = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("Exec sp_nhomvethang", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
            }
            return table;
        }
        public DataTable GetNhomByID(string MaNhom)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from Nhom where MaNhom = @manhom ", db.GetConnection());
            cmd.Parameters.AddWithValue("@manhom", MaNhom);
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
        public bool XoaNhomByID(string MaNhom)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Nhom WHERE MaNhom = @manhom", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@manhom", MaNhom);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool CapNhatNhom(string MaNhom, string TenNhomMoi, string ThongTinKhacMoi)
        {
            db.OpenConnection();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Nhom WHERE TenNhom = @tennhom", db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@tennhom", TenNhomMoi);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên nhóm vé tháng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

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
        public DataTable GetCountThe()
        {
            DataTable table = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("Exec sp_bangdemthe", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
            }
            return table;
        }
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
            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("delete from The WHERE MaThe = @mathe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", MaThe);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa thẻ: " + ex, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool ThemThe(string MaThe, string TenThe, DateTime NgayTao)
        {
            try
            {
                db.OpenConnection();

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
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thẻ thất bại, lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool KhoiPhucThe(string MaThe)
        {
            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("Update The Set TrangThaiThe = N'Sử dụng' WHERE MaThe = @mathe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", MaThe);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra thẻ: " + ex.Message);
                return false;
            }
        }
        public bool KiemTraTonTaiThe(string maThe)
        {
            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM The WHERE MaThe = @MaThe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", maThe);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra thẻ: " + ex.Message);
                return false;
            }
        }

        public bool KiemTraTrangThaiTheConSuDung(string maThe)
        {
            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM The WHERE MaThe = @MaThe AND (TrangThaiThe = N'Không sử dụng' OR TrangThaiThe = N'Mất thẻ')", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", maThe);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra thẻ: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public bool SetTrangThaiSuDungThe(string MaThe, bool TrangThai)
        {
            try
            {
                db.OpenConnection();
                string Trangthai = (TrangThai) ? "Sử dụng" : "Không sử dụng";

                using (SqlCommand cmd = new SqlCommand($"Update table The The Set TrangThai = '{Trangthai}' WHERE MaThe = @mathe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", MaThe);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa thẻ: " + ex, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Nhóm Nhân Viên
        public DataTable GetAllNhomNhanVien()
        {
            DataTable table = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("Exec sp_bangnhomnhanvien", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy bảng nhóm nhân viên: " + ex.Message);
            }
            return table;
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
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNhanVien = @manhanvien", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@manhanvien", MaNhanVien);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
        public DataTable GetAllNhanVien(string content=null)
        {
            DataTable dtbNhanVien = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_bangnhanvien", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TimKiem", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(content) ? (object)DBNull.Value : content
                    });

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
        public List<string> GetDanhSachNhanVien()
        {
            List<string> danhSachTenNhom = new List<string>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TenNhanVien FROM NhanVien", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachTenNhom.Add(reader["TenNhanVien"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách xe: " + ex.Message);
            }

            return danhSachTenNhom;
        }

        #endregion

        #region Nhật Ký
        public DataTable GetNhatKyDangNhap()
        {
            DataTable dtbNhatKyDangNhap = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"exec sp_nhatkydangnhap", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbNhatKyDangNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy nhật ký: " + ex.Message);
            }
            return dtbNhatKyDangNhap;
        }

        public DataTable TimKiemNhatKyDangNhap(DateTime dtTu, DateTime dtDen)
        {
            DataTable dtbNhatKyDangNhap = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM NhatKyDangNhap WHERE ThoiGianDangNhap >= @Tu AND ThoiGianDangNhap <= @Den", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Tu", dtTu);
                    cmd.Parameters.AddWithValue("@Den", dtDen);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbNhatKyDangNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé tháng: " + ex.Message);
            }

            return dtbNhatKyDangNhap;
        }

        public DataTable TimKiemNhatKyXuLyVeThang(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbNhatKyDangNhap = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_bangnhatkyvethang", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@tgTu", SqlDbType.DateTime)
                    {
                        Value = tgTu.HasValue ? (object)tgTu.Value : DBNull.Value
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgDen", SqlDbType.DateTime)
                    {
                        Value = tgDen.HasValue ? (object)tgDen.Value : DBNull.Value
                    });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbNhatKyDangNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé tháng: " + ex.Message);
            }

            return dtbNhatKyDangNhap;
        }

        public DataTable GetAllXuLyVeThang()
        {
            DataTable dtbXuLyVeThang = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"exec sp_bangnhatkyvethang", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbXuLyVeThang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách xử lý vé tháng: " + ex.Message);
            }
            return dtbXuLyVeThang;
        }

        public DataTable GetXuLyVeLuot(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbNhatKyDangNhap = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_bangnhatkyveluot", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@tgTu", SqlDbType.DateTime)
                    {
                        Value = tgTu.HasValue ? (object)tgTu.Value : DBNull.Value
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgDen", SqlDbType.DateTime)
                    {
                        Value = tgDen.HasValue ? (object)tgDen.Value : DBNull.Value
                    });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbNhatKyDangNhap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách vé tháng: " + ex.Message);
            }

            return dtbNhatKyDangNhap;
        }
        #endregion

        #region Thống Kê
        public DataTable GetThongKeTheoMayTinh()
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"exec sp_ThongKeTheoMayTinh", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dtbThongKe.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách thống kê theo máy tính: " + ex.Message);
            }
            return dtbThongKe;
        }

        public DataTable GetThongKeTheoMayTinhByTimKiem(string LoaiVe, string LoaiXe, DateTime? tgTu, DateTime? tgDen)
        {
            DataTable dtbXuLyVeThang = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_ThongKeTheoMayTinh", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@LoaiVe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(LoaiVe) ? (object)DBNull.Value : LoaiVe
                    });

                    cmd.Parameters.Add(new SqlParameter("@LoaiXe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(LoaiXe) ? (object)DBNull.Value : LoaiXe
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgTu", SqlDbType.DateTime)
                    {
                        Value = tgTu.HasValue ? (object)tgTu.Value : DBNull.Value
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgDen", SqlDbType.DateTime)
                    {
                        Value = tgDen.HasValue ? (object)tgDen.Value : DBNull.Value
                    });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbXuLyVeThang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thống kê: " + ex.Message);
            }
            return dtbXuLyVeThang;
        }

        public List<LoaiXeItem> GetDanhSachXe()
        {
            var list = new List<LoaiXeItem>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaLoaiXe, TenLoaiXe FROM LoaiXe", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LoaiXeItem
                            {
                                MaLoaiXe = Convert.ToInt32(reader["MaLoaiXe"]),
                                TenLoaiXe = reader["TenLoaiXe"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách xe: " + ex.Message);
            }

            return list;
        }

        public DataTable GetThongKeChiTiet()
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"select * from vw_ThongKeChiTiet", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbThongKe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy bảng thống kê chi tiết: " + ex.Message);
            }
            return dtbThongKe;
        }

        public DataTable GetDienGiaiThongKeChiTiet(string TrangThai = null, string loaixe = null, DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_BangDienGiai", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(TrangThai) ? (object)DBNull.Value : TrangThai
                    });

                    cmd.Parameters.Add(new SqlParameter("@LoaiXe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(loaixe) ? (object)DBNull.Value : loaixe
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgTu", SqlDbType.DateTime)
                    {
                        Value = tgTu.HasValue ? (object)tgTu.Value : DBNull.Value
                    });

                    cmd.Parameters.Add(new SqlParameter("@tgDen", SqlDbType.DateTime)
                    {
                        Value = tgDen.HasValue ? (object)tgDen.Value : DBNull.Value
                    });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbThongKe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy bảng diễn giải: " + ex.Message);
            }
            return dtbThongKe;
        }

        public DataTable GetThongKeChiTietByTimKiem(string TrangThai, string LoaiVe, string LoaiXe, DateTime tgTu, DateTime tgDen)
        {
            DataTable dtbXuLyVeThang = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"Select * from vw_ThongKeChiTiet 
                                WHERE TrangThai LIKE @TrangThai
                                AND LoaiVe LIKE @LoaiVe
                                AND TenLoaiXe LIKE @LoaiXe
                                AND ThoiGianVao >= @tgTu
                                AND ThoiGianVao <= @tgDen", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", "%" + TrangThai + "%");
                        cmd.Parameters.AddWithValue("@LoaiVe", "%" + LoaiVe + "%");
                        cmd.Parameters.AddWithValue("@LoaiXe", "%" + LoaiXe + "%");
                        cmd.Parameters.AddWithValue("@tgTu", tgTu);
                        cmd.Parameters.AddWithValue("@tgDen", tgDen);
                        adapter.Fill(dtbXuLyVeThang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách xử lý vé tháng: " + ex.Message);
            }
            return dtbXuLyVeThang;
        }

        public DataTable GetThongKeTheoKhoangThoiGian(string KieuThongKe = "ngày", DateTime? tgTu = null, DateTime? tgDen = null, string loaixe = null, string loaive = null, string nhanvien = null)
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_ThongKeTheoThoiGian", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@KieuThongKe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(KieuThongKe) ? (object)DBNull.Value : KieuThongKe
                    });

                    // @LoaiXe
                    cmd.Parameters.Add(new SqlParameter("@LoaiXe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(loaixe) ? (object)DBNull.Value : loaixe
                    });

                    cmd.Parameters.Add(new SqlParameter("@MaNhanVien", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(nhanvien) ? (object)DBNull.Value : nhanvien
                    });

                    // @tgTu
                    cmd.Parameters.Add(new SqlParameter("@tgTu", SqlDbType.DateTime)
                    {
                        Value = tgTu.HasValue ? (object)tgTu.Value : DBNull.Value
                    });

                    // @tgDen
                    cmd.Parameters.Add(new SqlParameter("@tgDen", SqlDbType.DateTime)
                    {
                        Value = tgDen.HasValue ? (object)tgDen.Value : DBNull.Value
                    });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbThongKe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy bảng diễn giải: " + ex.Message);
            }
            return dtbThongKe;
        }

        #endregion

        #region Tính Tiền
        public DataTable GetAllLoaiXe()
        {
            DataTable dtbLoaiXe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("exec sp_bangloaixe", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbLoaiXe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
            }
            return dtbLoaiXe;
        }

        public DataTable GetTinhTienCongVanByID(int MaLoaiXe)
        {
            DataTable dt = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM TinhTienCongVan where MaLoaiXe = @MaLoaiXe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiXe", MaLoaiXe);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy TinhTienCongVan: " + ex.Message);
            }
            return dt;
        }

        public DataTable GetTinhTienLuyTienByID(int MaLoaiXe)
        {
            DataTable dt = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM TinhTienLuyTien where MaLoaiXe = @MaLoaiXe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiXe", MaLoaiXe);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy TinhTienLuyTien: " + ex.Message);
            }
            return dt;
        }

        public int GetGiaVeThangById(int MaLoaiXe)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 GiaVeThang FROM TinhTienThang WHERE MaLoaiXe = @MaLoaiXe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiXe", MaLoaiXe);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int GetPhutMienPhiById(int MaLoaiXe)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 PhutMienPhi FROM TinhTienThang WHERE MaLoaiXe = @MaLoaiXe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiXe", MaLoaiXe);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public bool UpsertGiaVeThang(string maLoaiXe, string giaVeThang, string PhutMienPhi)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_upsertTinhTienThang", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@MaLoaiXe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(maLoaiXe) ? (object)DBNull.Value : maLoaiXe
                    });

                    cmd.Parameters.Add(new SqlParameter("@GiaVeThang", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(giaVeThang) ? (object)DBNull.Value : giaVeThang
                    });
                    cmd.Parameters.Add(new SqlParameter("@PhutMienPhi", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(PhutMienPhi) ? (object)DBNull.Value : PhutMienPhi
                    });
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpsertTinhTienCongVan(string maLoaiXe, bool thuTienTruoc, byte demTu, byte demDen, byte gioGiaoNgayDem,
            int giaThuong, int giaDem, int giaNgayDem, int giaPhuThu, byte phuThuTu, byte phuThuDen)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_UpsertTinhTienCongVan", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaLoaiXe", maLoaiXe);
                    cmd.Parameters.AddWithValue("@ThuTienTruoc", thuTienTruoc);
                    cmd.Parameters.AddWithValue("@DemTu", demTu);
                    cmd.Parameters.AddWithValue("@DemDen", demDen);
                    cmd.Parameters.AddWithValue("@GioGiaoNgayDem", gioGiaoNgayDem);
                    cmd.Parameters.AddWithValue("@GiaThuong", giaThuong);
                    cmd.Parameters.AddWithValue("@GiaDem", giaDem);
                    cmd.Parameters.AddWithValue("@GiaNgayDem", giaNgayDem);
                    cmd.Parameters.AddWithValue("@GiaPhuThu", giaPhuThu);
                    cmd.Parameters.AddWithValue("@PhuThuTu", phuThuTu);
                    cmd.Parameters.AddWithValue("@PhuThuDen", phuThuDen);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpsertTinhTienLuyTien(string maLoaiXe, byte moc1, int giaMoc1, byte moc2, int giaMoc2, byte chuKy, int giaVuotMoc, int congMoc)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_UpsertTinhTienLuyTien", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaLoaiXe", maLoaiXe);
                    cmd.Parameters.AddWithValue("@Moc1", moc1);
                    cmd.Parameters.AddWithValue("@GiaMoc1", giaMoc1);
                    cmd.Parameters.AddWithValue("@Moc2", moc2);
                    cmd.Parameters.AddWithValue("@GiaMoc2", giaMoc2);
                    cmd.Parameters.AddWithValue("@ChuKy", chuKy);
                    cmd.Parameters.AddWithValue("@GiaVuotMoc", giaVuotMoc);
                    cmd.Parameters.AddWithValue("@CongMoc", congMoc);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        public bool ThemLoaiXe(string Tenloaixe)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO LoaiXe (TenLoaiXe) VALUES (@TenLoaiXe)", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TenLoaiXe", Tenloaixe);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm loại xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool XoaLoaiXe(string maloaixe)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("delete from LoaiXe where MaLoaiXe = @maloaixe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@maloaixe", maloaixe);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xoá xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
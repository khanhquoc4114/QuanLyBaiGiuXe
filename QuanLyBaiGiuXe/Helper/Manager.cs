using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Helper;
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

                    cmd.Parameters["@key"].Value = "NguoiThucHien";
                    cmd.Parameters["@value"].Value = Session.MaNhanVien;
                    cmd.ExecuteNonQuery();

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
        public DataTable GetAllVeLuot(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbVeLuot = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_hienbangveluot", db.GetConnection()))
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
        public DataTable GetTongVeLuot(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbVeLuot = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_tongveluot", db.GetConnection()))
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
        public bool SetGiaVeLuotByID(string maveluot, int sotien = 0)
        {
            try
            {
                db.OpenConnection();
                GuiSession();
                using (SqlCommand cmd = new SqlCommand("Update VeLuot SET TongTien = @sotien WHERE MaVeLuot= @maveluot ", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@maveluot", maveluot);
                    cmd.Parameters.AddWithValue("@sotien", sotien);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật giá vé lượt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật vé lượt khi mất thẻ, cập nhật thời gian ra, ảnh ra và trạng thái vé là "Mất thẻ"
        /// </summary>
        /// <param name="maveluot"></param>
        /// <param name="AnhRaPath"></param>
        /// <param name="sotien"></param>
        /// <returns></returns>
        public bool CapNhatMatTheVeLuot(string maveluot, string AnhRaPath ,int sotien = 0)
        {
            try
            {
                db.OpenConnection();
                GuiSession();

                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE VeLuot 
                    SET 
                        TongTien = @sotien, 
                        ThoiGianRa = @tgRa, 
                        AnhRaPath = @anhrapath,
                        TrangThai = 'Mất thẻ'
                    WHERE MaVeLuot = @maveluot", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@maveluot", maveluot);
                    cmd.Parameters.AddWithValue("@sotien", sotien);
                    cmd.Parameters.AddWithValue("@tgRa", DateTime.Now);
                    cmd.Parameters.AddWithValue("@anhrapath", AnhRaPath ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật giá vé lượt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
        /// <summary>
        /// Kiểm tra thẻ đã tồn tại trong vé tháng chưa
        /// </summary>
        /// <param name="mathe"></param>
        /// <returns></returns>
        public bool KiemTraTheTrongVeThang(string mathe)
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

        public bool SuaVeThang(string MaVeThang, string maNhom, string maThe, string chuXe, string dienThoai,
                          string diaChi, string email, DateTime ngayKichHoat,
                          DateTime ngayHetHan, string bienSo, string nhanHieu,
                          string maloaiXe, decimal giaVe, string ghiChu)
        {
            try
            {
                db.OpenConnection();

                GuiSession();

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
        public bool ThemVeThang(string maNhom, string maThe, string chuXe, string dienThoai,
                          string diaChi, string email, DateTime ngayKichHoat,
                          DateTime ngayHetHan, string bienSo, string nhanHieu,
                          string loaiXe, decimal giaVe, string ghiChu)
        {
            try
            {
                db.OpenConnection();
                GuiSession();

                using (SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO VeThang (MaNhom, MaThe, ChuXe, DienThoai, DiaChi, Email, 
                                         NgayKichHoat, NgayHetHan, BienSo, NhanHieu, 
                                         MaLoaiXe, GiaVe, GhiChu, MaNhanVien, MayTinhXuLy)
                    VALUES (@MaNhom, @MaThe, @ChuXe, @DienThoai, @DiaChi, @Email, 
                            @NgayKichHoat, @NgayHetHan, @BienSo, @NhanHieu, 
                            @MaLoaiXe, @GiaVe, @GhiChu, @MaNhanVien, @MayTinhXuLy)", db.GetConnection()))
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
                    insertCmd.Parameters.AddWithValue("@MaNhanVien", Session.MaNhanVien);
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

        public bool KiemTraHanVeThang(string maThe, out DateTime? ngayHetHan)
        {
            ngayHetHan = null;
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 NgayHetHan " +
                    "FROM VeThang " +
                    "WHERE MaThe = @MaThe " +
                    "AND TrangThai = N'Sử dụng' " +
                    "ORDER BY NgayKichHoat DESC", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", maThe);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        ngayHetHan = Convert.ToDateTime(result);
                        return ngayHetHan >= DateTime.Now;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra hạn vé tháng: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Nhóm Vé Tháng
        public List<ComboBoxItem> GetDanhSachNhom()
        {
            List<ComboBoxItem> danhSach = new List<ComboBoxItem>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhom, TenNhom FROM Nhom", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new ComboBoxItem
                            {
                                Value = Convert.ToInt32(reader["MaNhom"]),
                                Text = reader["TenNhom"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhóm: " + ex.Message);
            }

            return danhSach;
        }
        public DataTable GetAllNhomVeThang()
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
        public DataTable GetNhomVeThangByID(string MaNhom)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from Nhom where MaNhom = @manhom ", db.GetConnection());
            cmd.Parameters.AddWithValue("@manhom", MaNhom);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemNhomVeThang(string TenNhom, string ThongTinKhac)
        {
            db.OpenConnection();

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
        public bool XoaNhomVeThangByID(string MaNhom)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Nhom WHERE MaNhom = @manhom", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@manhom", MaNhom);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool CapNhatNhomVeThang(string MaNhom, string TenNhomMoi, string ThongTinKhacMoi)
        {
            db.OpenConnection();

            string query = @"
                    SELECT COUNT(*) 
                    FROM Nhom 
                    WHERE TenNhom = @TenNhom 
                      AND (@MaNhom IS NULL OR MaNhom != @MaNhom)";
            using (SqlCommand checkCmd = new SqlCommand(query, db.GetConnection()))
            {
                checkCmd.Parameters.AddWithValue("@TenNhom", TenNhomMoi);
                checkCmd.Parameters.AddWithValue("@MaNhom", MaNhom);

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
                MessageBox.Show("Lỗi khi lấy bảng đếm thẻ: " + ex.Message);
            }
            return table;
        }
        public DataTable GetAllThe()
        {
            DataTable table = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("Exec sp_banghienthe", db.GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy bảng danh sách thẻ: " + ex.Message);
            }
            return table;
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

                using (SqlCommand cmd = new SqlCommand("INSERT INTO The (MaThe, SoThuTu, LoaiThe, NgayTaoThe, NgayCapNhatThe)" +
                                                       $" VALUES (@mathe, 0, @loaithe, @ngaytao, @ngaycapnhat)", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", MaThe);
                    cmd.Parameters.AddWithValue("@loaithe", TenThe);
                    cmd.Parameters.AddWithValue("@ngaytao", NgayTao);
                    cmd.Parameters.AddWithValue("@ngaycapnhat", NgayTao);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thẻ thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM The WHERE MaThe = @MaThe AND TrangThaiThe = N'Sử dụng'", db.GetConnection()))
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

        /// <summary>
        /// -1 là mất thẻ, 0 là không sử dụng, 1 là sử dụng
        /// </summary>
        /// <param name="MaThe"></param>
        /// <param name="TrangThai"></param>
        /// <returns></returns>
        public bool SetTrangThaiSuDungThe(string MaThe, int trangthai)
        {
            try
            {
                db.OpenConnection();
                string Trangthai = "Không sử dụng";
                if (trangthai == -1)
                {
                    Trangthai = "Mất thẻ";
                }
                else if (trangthai == 1)
                {
                    Trangthai = "Sử dụng";
                }

                using (SqlCommand cmd = new SqlCommand($"Update The Set TrangThaiThe = @TrangThai WHERE MaThe = @mathe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", MaThe);
                    cmd.Parameters.AddWithValue("@TrangThai", Trangthai);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái sử dụng thẻ: " + ex, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public List<ComboBoxItem> GetDanhSachNhomNhanVien()
        {
            List<ComboBoxItem> danhSach = new List<ComboBoxItem>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhomNhanVien, TenNhomNhanVien FROM NhomNhanVien", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new ComboBoxItem
                            {
                                Value = Convert.ToInt32(reader["MaNhomNhanVien"]),
                                Text = reader["TenNhomNhanVien"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhóm: " + ex.Message);
            }

            return danhSach;
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
        public int GetMaNhanVienByThe(string MaThe, out bool isTonTai)
        {
            isTonTai = false;
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaNhanVien FROM NhanVien WHERE MaThe = @MaThe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", MaThe);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        isTonTai = true;
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã nhân viên theo thẻ: " + ex.Message);
            }

            return -1;
        }

        public bool ThemNhanVien(string tenNhom, string hoTen, string maThe, string tenDangNhap, string matKhau, string ghiChu, out bool isTonTaiThe, out bool isTonTaiTheChoNhanVien)
        {
            isTonTaiThe = false;
            isTonTaiTheChoNhanVien = false;
            try
            {
                if (!KiemTraTonTaiThe(maThe))
                {
                    isTonTaiThe = false;
                    return false;
                } else isTonTaiThe = true;

                int nv = GetMaNhanVienByThe(maThe, out isTonTaiTheChoNhanVien);

                if (isTonTaiTheChoNhanVien)
                {
                    isTonTaiTheChoNhanVien = true;
                    return false;
                } else isTonTaiTheChoNhanVien = false;

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
        public bool CapNhatNhanVien(string maNhanVien, string tenNhom, string hoTen, string maThe, string tenDangNhap, string matKhau, string ghiChu, out bool isTonTaiThe, out bool isTonTaiTheChoNhanVien)
        {
            isTonTaiThe = false;
            isTonTaiTheChoNhanVien = false;
            try
            {
                if (!KiemTraTonTaiThe(maThe))
                {
                    isTonTaiThe = false;
                    return false;
                }
                else isTonTaiThe = true;

                int nv = GetMaNhanVienByThe(maThe, out isTonTaiTheChoNhanVien);

                if (isTonTaiTheChoNhanVien && nv.ToString().Trim() != maNhanVien.Trim())
                {
                    isTonTaiTheChoNhanVien = true;
                    return false;
                }
                else isTonTaiTheChoNhanVien = false;
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
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message);
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
        public List<ComboBoxItem> GetDanhSachNhanVien()
        {
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhanVien, HoTen FROM NhanVien", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ComboBoxItem
                            {
                                Value = Convert.ToInt32(reader["MaNhanVien"]),
                                Text = reader["HoTen"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
            }

            return items;
        }
        public bool CapNhatTrangThaiNhanVien(string manhanvien, string trangthai)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("UPdate NhanVien Set TrangThai = @TrangThai where MaNhanVien = @manhanvien", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@manhanvien", manhanvien);
                    cmd.Parameters.AddWithValue("@TrangThai", trangthai);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
                MessageBox.Show("Lỗi khi lấy nhật ký đăng nhập: " + ex.Message);
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

        public DataTable GetAllXuLyVeThang(DateTime? tgTu = null, DateTime? tgDen = null)
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
                MessageBox.Show("Lỗi khi lấy nhật ký xử lý vé tháng: " + ex.Message);
            }

            return dtbNhatKyDangNhap;
        }

        public DataTable GetXuLyVeLuot(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtgNhatKy = new DataTable();

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
                        adapter.Fill(dtgNhatKy);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy nhật ký xử lý vé lượt: " + ex.Message);
            }

            return dtgNhatKy;
        }

        public DataTable GetNhatKyDieuChinhGiaVe(DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtgNhatKy = new DataTable();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_nhatkydieuchinh", db.GetConnection()))
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
                        adapter.Fill(dtgNhatKy);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy nhật ký điều chỉnh giá vé: " + ex.Message);
            }

            return dtgNhatKy;
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
                MessageBox.Show("Lỗi khi lấy thống kê theo máy tính: " + ex.Message);
            }
            return dtbXuLyVeThang;
        }

        public List<ComboBoxItem> GetDanhSachXe()
        {
            var list = new List<ComboBoxItem>();

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaLoaiXe, TenLoaiXe FROM LoaiXe", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ComboBoxItem
                            {
                                Value = Convert.ToInt32(reader["MaLoaiXe"]),
                                Text = reader["TenLoaiXe"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
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
                MessageBox.Show("Lỗi khi lấy thống kê chi tiết: " + ex.Message);
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
                MessageBox.Show("Lỗi khi lấy bảng thống kê theo khoảng thời gian: " + ex.Message);
            }
            return dtbThongKe;
        }

        public DataTable GetThongKeTheoNhanVien(string loaive = null, string maloaixe = null, DateTime? tgTu = null, DateTime? tgDen = null)
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_ThongKeTheoNhanVien", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@LoaiVe", SqlDbType.NVarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(loaive) ? (object)DBNull.Value : loaive
                    });

                    cmd.Parameters.Add(new SqlParameter("@MaLoaiXe", SqlDbType.Int)
                    {
                        Value = string.IsNullOrEmpty(maloaixe) ? (object)DBNull.Value : int.Parse(maloaixe)
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
                MessageBox.Show("Lỗi khi lấy thống kê theo nhân viên: " + ex.Message);
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
                MessageBox.Show("Lỗi khi lấy dữ liệu tính tiền công văn: " + ex.Message);
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
                MessageBox.Show("Lỗi khi lấy dữ liệu tính tiền luỹ tiến: " + ex.Message);
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
                MessageBox.Show("Lỗi khi cập nhật giá vé tháng và số phút miễn phí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi cập nhật tính tiền công văn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi cập nhật tính tiền luỹ tiến: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Loại Xe
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
        #endregion

        public DataTable TraCuuRaVao(string loaiTruyVan=null, string loaiVe = null, string maLoaiXe = null, DateTime? tgTu=null,
            DateTime? tgDen=null, string soThe = null, string bienSo = null,string maThe = null, string maNhanVienVao = null, string maNhanVienRa = null)
        {
            DataTable dtbThongKe = new DataTable();
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_TraCuuRaVao", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoaiTruyVan", loaiTruyVan);
                    cmd.Parameters.AddWithValue("@LoaiVe", loaiVe);
                    cmd.Parameters.AddWithValue("@MaLoaiXe", string.IsNullOrEmpty(maLoaiXe) ? -1 : int.Parse(maLoaiXe));
                    cmd.Parameters.AddWithValue("@tgTu", tgTu);
                    cmd.Parameters.AddWithValue("@tgden", tgDen);
                    cmd.Parameters.AddWithValue("@SoThe", string.IsNullOrEmpty(soThe) ? DBNull.Value : (object)soThe);
                    cmd.Parameters.AddWithValue("@BienSo", string.IsNullOrEmpty(bienSo) ? DBNull.Value : (object)bienSo);
                    cmd.Parameters.AddWithValue("@MaThe", string.IsNullOrEmpty(maThe) ? DBNull.Value : (object)maThe);
                    cmd.Parameters.AddWithValue("@MaNhanVienVao", string.IsNullOrEmpty(maNhanVienVao) ? -1 : int.Parse(maNhanVienVao));
                    cmd.Parameters.AddWithValue("@MaNhanVienRa", string.IsNullOrEmpty(maNhanVienRa) ? -1 : int.Parse(maNhanVienRa));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtbThongKe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tra cứu xe ra vào: " + ex.Message);
            }
            return dtbThongKe;
        }

    }
}
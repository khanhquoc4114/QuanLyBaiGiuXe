using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Models;
using Remote.Linq.SimpleQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe.Helper
{
    class VeManager
    {
        Connector db = new Connector();
        TinhTienManager TinhTienManager = new TinhTienManager();
        Manager manager = new Manager();

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
        public bool ThemVeLuot(string mathe, string bienso, DateTime tgVao, string maloaixe, string pathVao)
        {
            if (manager.KiemTraTrangThaiTheConSuDung(mathe))
            {
                MessageBox.Show("Thẻ đã hết hạn sử dụng hoặc đã mất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int maVeThang = -1;
            maVeThang = GetMaVeThangByInfo(mathe);
            try
            {
                GuiSession();
                db.OpenConnection();
                using (SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO VeLuot (MaThe, ThoiGianVao, MaNhanVien, MayTinhXuLy, CachTinhTien, BienSo, MaLoaiXe, MaVeThang, AnhVaoPath)
                    VALUES (@MaThe, @ThoiGianVao, @MaNhanVien, @MayTinhXuLy, @CachTinhTien, @BienSo, @MaLoaiXe, @MaVeThang, @AnhVaoPath);
                ", db.GetConnection()))
                {
                    insertCmd.Parameters.AddWithValue("@MaThe", mathe);
                    insertCmd.Parameters.AddWithValue("@ThoiGianVao", tgVao);
                    insertCmd.Parameters.AddWithValue("@MaNhanVien", Session.MaNhanVien);
                    insertCmd.Parameters.AddWithValue("@MayTinhXuLy", Session.VaiTro);
                    insertCmd.Parameters.AddWithValue("@CachTinhTien", Session.cachtinhtien);
                    insertCmd.Parameters.AddWithValue("@BienSo", bienso);
                    insertCmd.Parameters.AddWithValue("@MaLoaiXe", maloaixe);
                    insertCmd.Parameters.AddWithValue("@AnhVaoPath", pathVao);
                    if (maVeThang == -1)
                    {
                        insertCmd.Parameters.AddWithValue("@MaVeThang", DBNull.Value);
                    } else
                    {
                        insertCmd.Parameters.AddWithValue("@MaVeThang", maVeThang);
                    }


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

        public int GetMaVeThangByInfo(string mathe)
        {
            int maVeThang = -1;

            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT Top 1 MaVeThang FROM VeThang WHERE MaThe = @MaThe", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", mathe);

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

        public VeLuotInfo CapNhatVeLuot(string mathe, string bienso, DateTime tgRa, string pathRa)
        {
            int tongtien = 0;
            DateTime gioVao = DateTime.MinValue;
            int maLoaiXe = 0;
            string CachTinhTien = "0";
            int maVeThang = -1;
            maVeThang = GetMaVeThangByInfo(mathe);

            VeLuotInfo veCapNhat = null;

            try
            {
                db.OpenConnection();

                using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP 1 CachTinhTien, ThoiGianVao, MaLoaiXe
                        FROM VeLuot
                        WHERE MaThe = @mathe
                        AND TrangThai = N'Chưa Ra'
                        AND BienSo = @bienso
                        ORDER BY ThoiGianVao DESC; ", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@mathe", mathe);
                    cmd.Parameters.AddWithValue("@bienso", bienso);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            CachTinhTien = row["CachTinhTien"].ToString();
                            gioVao = Convert.ToDateTime(row["ThoiGianVao"]);
                            maLoaiXe = Convert.ToInt32(row["MaLoaiXe"]);

                            tongtien = TinhTienManager.TinhTien(CachTinhTien, gioVao, tgRa, maLoaiXe);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thẻ với biến số xe");
                            return null;
                        }
                    }
                }

                GuiSession();

                using (SqlCommand updateCmd = new SqlCommand(@"
                        UPDATE VeLuot 
                        SET ThoiGianRa = @ThoiGianRa, TongTien = @tongtien, AnhRaPath = @AnhRaPath
                        WHERE MaThe = @MaThe
                        AND ThoiGianRa IS NULL;
                    ", db.GetConnection()))
                {
                    updateCmd.Parameters.AddWithValue("@MaThe", mathe);
                    updateCmd.Parameters.AddWithValue("@ThoiGianRa", tgRa);
                    updateCmd.Parameters.AddWithValue("@tongtien", tongtien);
                    updateCmd.Parameters.AddWithValue("@AnhRaPath", pathRa);

                    int rowsUpdated = updateCmd.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        veCapNhat = new VeLuotInfo
                        {
                            MaThe = mathe,
                            BienSo = bienso,
                            ThoiGianVao = gioVao,
                            ThoiGianRa = tgRa,
                            TongTien = tongtien,
                            MaLoaiXe = maLoaiXe,
                            LaVeThang = maVeThang != -1 ? true : false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                db.CloseConnection();
            }

            return veCapNhat;
        }

        public bool KiemTraTrongBai(string mathe, string bienso)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP 1 1
                        FROM VeLuot
                        WHERE MaThe = @MaThe
                          AND TrangThai = N'Chưa Ra'
                        ORDER BY ThoiGianVao DESC;", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaThe", mathe);

                    object result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra vé lượt trong bãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}

using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe.Models
{
    class Manager
    {
        Connector db = new Connector();
        public Manager() { }
        public DataTable GetAllVeThang()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from VeThang", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public List<string> LayDanhSachTenNhom()
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

        #region The
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

        #endregion
    }
}

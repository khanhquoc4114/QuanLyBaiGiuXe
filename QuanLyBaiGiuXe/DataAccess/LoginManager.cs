using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBaiGiuXe.DataAccess
{
    public class LoginManager
    {
        Connector db = new Connector();

        public LoginManager(){}

        public string GetTen(string username, string password)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("SELECT HoTen FROM NhanVien WHERE TenDangNhap = @username AND MatKhau = @password", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        public string GetMaNhanVien(string username, string password, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT MaNhanVien, TrangThai 
                    FROM NhanVien 
                    WHERE TenDangNhap = @username AND MatKhau = @password", db.GetConnection()))
                {
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string trangThai = reader["TrangThai"].ToString();
                            if (trangThai == "Khoá")
                            {
                                errorMessage = "Tài khoản này đã bị khóa.";
                                return "";
                            }

                            return reader["MaNhanVien"].ToString();
                        }
                        else
                        {
                            errorMessage = "Sai tên đăng nhập hoặc mật khẩu.";
                            return "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra đăng nhập hệ thống: " + ex.Message;
                return "";
            }
        }

        public string GetVaiTro(string username, string password)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand(@"
                    SELECT n.TenNhomNhanVien 
                    FROM NhanVien nv
                    JOIN NhomNhanVien n ON nv.MaNhomNhanVien = n.MaNhomNhanVien
                    WHERE nv.TenDangNhap = @username AND nv.MatKhau = @password", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        public bool CheckOut(string maNhanVien, DateTime checkout)
        {
            try
            {
                db.OpenConnection();
                using (SqlConnection conn = db.GetConnection())
                {
                    string query = @"
                        UPDATE NhatKyDangNhap 
                        SET ThoiGianDangXuat = @TgRa
                        WHERE MaNhanVien = @MaNV 
                          AND ThoiGianDangXuat IS NULL
                          AND ThoiGianDangNhap = (
                              SELECT TOP 1 ThoiGianDangNhap
                              FROM NhatKyDangNhap
                              WHERE MaNhanVien = @MaNV AND ThoiGianDangXuat IS NULL
                              ORDER BY ThoiGianDangNhap DESC
                          )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@TgRa", SqlDbType.DateTime).Value = checkout;
                        cmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = maNhanVien;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Checkout Error: " + ex.Message);
                return false;
            }
        }

        public bool CheckIn(string MaNhanVien, DateTime checkIn)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO NhatKyDangNhap (MaNhanVien, ThoiGianDangNhap)
                    VALUES (@MaNV, @TgVao)", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@MaNV", MaNhanVien);
                    cmd.Parameters.AddWithValue("@TgVao", checkIn);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetHoTenByMaNhanVien(string maNhanVien)
        {
            db.OpenConnection();
            using (SqlCommand cmd = new SqlCommand("SELECT HoTen FROM NhanVien WHERE MaNhanVien = @MaNV", db.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNhanVien);
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }
    }
}

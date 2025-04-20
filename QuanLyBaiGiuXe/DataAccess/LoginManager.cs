using System;
using System.Data.SqlClient;
using QuanLyBaiGiuXe.Models;

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

        public string GetMaNhanVien(string username, string password)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNhanVien FROM NhanVien WHERE TenDangNhap = @username AND MatKhau = @password", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            } catch
            {
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

        public bool CheckOut(string MaNhanVien, DateTime checkout)
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE NhatKyDangNhap 
                        SET ThoiGianDangXuat = @TgRa
                        WHERE MaNhanVien = @MaNV AND ThoiGianDangXuat IS NULL", db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TgRa", checkout);
                    cmd.Parameters.AddWithValue("@MaNV", MaNhanVien);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch
            {
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
    }
}

using System;
using System.Data.SqlClient;

namespace QuanLyBaiGiuXe.Models
{
    public static class AppConfig
    {
        public static string TenCongTy;
        public static string DiaChi;
        public static string Email;
        public static string SoDienThoai;
        public static int TienPhatMatThe;
        public static int SoLuongXeToiDa;
        public static int HanMucVeThang;
        public static int HinhThucThuPhi;
        public static int XuLyVeThangHetHan;

        public static void Load(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM CauHinhHeThong", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    TenCongTy = reader["TenCongTy"]?.ToString();
                    DiaChi = reader["DiaChi"]?.ToString();
                    Email = reader["Email"]?.ToString();
                    SoDienThoai = reader["SoDienThoai"]?.ToString();
                    TienPhatMatThe = Convert.ToInt32(reader["TienPhatMatThe"]);
                    SoLuongXeToiDa = Convert.ToInt32(reader["SoLuongXeToiDa"]);
                    HanMucVeThang = Convert.ToInt32(reader["HanMucVeThang"]);
                    HinhThucThuPhi = Convert.ToInt32(reader["HinhThucThuPhi"]);
                    XuLyVeThangHetHan = Convert.ToInt32(reader["XuLyVeThangHetHan"]);
                }
            }
        }
    }
}

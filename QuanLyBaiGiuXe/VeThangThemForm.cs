using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangThemForm: Form
    {
        public VeThangThemForm()
        {
            InitializeComponent();
        }

        private readonly Connector db = new Connector();

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT MaVe, ChuXe, DienThoai, Email, DiaChi, BienSo, LoaiXe, NgayKichHoat, NgayHetHan, GiaVe, GhiChu, Nhom " +
                           "FROM VeThang " +
                           "WHERE Mathe = @mathe";
            using (SqlConnection connection = db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@mathe", tbMaThe.Text);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader result = cmd.ExecuteReader())
                        {
                            if (result.Read())
                            {
                                cbNhom.Text = result["Nhom"].ToString();
                                tbChuXe.Text = result["ChuXe"].ToString();
                                tbDiaChi.Text = result["DiaChi"].ToString();
                                tbDienThoai.Text = result["DienThoai"].ToString();
                                tbEmail.Text = result["Email"].ToString();
                                dataPickerNgayKichHoat.Text = result["NgayKichHoat"].ToString();
                                datePickerNgayHetHan.Text = result["NgayHetHan"].ToString();
                                tbBienSo.Text = result["Email"].ToString();
                                tbNhanHieu.Text = result["Email"].ToString();
                                cbLoaiXe.Text = result["Email"].ToString();
                                updGiaVe.Text = result["Email"].ToString();
                                rtbGhiChu.Text = result["Email"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy bản ghi nào với mã thẻ này.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
            }
        }
    }
}

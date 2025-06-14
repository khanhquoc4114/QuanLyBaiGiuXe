using QuanLyBaiGiuXe.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class CauHinhMayChu: Form
    {
        public CauHinhMayChu()
        {
            InitializeComponent();
        }

        private void CauHinhMayChu_Load(object sender, EventArgs e)
        {
            LoadUI();
        }
        private void LoadUI() { 
            List<string> strings = new List<string> { "Chứng thực Windows","Chứng thực SQL Server"};
            cbAuth.DataSource = strings;
        }

        private void cbAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string content = cbAuth.SelectedItem.ToString();
            if (content == "Chứng thực Windows")
            {
                tbUsername.Enabled = false;
                tbPass.Enabled = false;
            } else if (content == "Chứng thực SQL Server"){
                tbPass.Enabled = true;
                tbUsername.Enabled = true;
            }
        }

        private void btnThuKetNoi_Click(object sender, EventArgs e)
        {
            string server = tbServer.Text.Trim();
            string user = tbUsername.Text.Trim();
            string pass = tbPass.Text;
            string authType = cbAuth.SelectedItem.ToString();

            string connStr;
            if (authType == "Chứng thực Windows")
            {
                connStr = $"Data Source={server};Initial Catalog=testdoxe;Integrated Security=True;";
            }
            else
            {
                connStr = $"Data Source={server};Initial Catalog=testdoxe;User ID={user};Password={pass};";
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Session.connectionString = connStr;
                    MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kết nối thất bại.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuuCauHinh_Click(object sender, EventArgs e)
        {

        }

        private void btnTCPIP_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

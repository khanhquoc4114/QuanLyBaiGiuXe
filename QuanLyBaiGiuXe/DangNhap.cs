using System;
using System.Windows.Forms;
using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Helper;

namespace QuanLyBaiGiuXe
{
    public partial class DangNhap : Form
    {
        string MaNhanVien = "";
        LoginManager loginManager = new LoginManager();

        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text.Trim() == string.Empty || tbPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin đăng nhập!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                MaNhanVien = loginManager.GetMaNhanVien(tbUsername.Text, tbPassword.Text);
                if (!string.IsNullOrEmpty(MaNhanVien))
                {
                    Session.MaNhanVien = MaNhanVien;
                    string name = loginManager.GetTen(tbUsername.Text, tbPassword.Text);
                    string role = Session.VaiTro = loginManager.GetVaiTro(tbUsername.Text, tbPassword.Text);
                    loginManager.CheckIn(MaNhanVien.ToString(),DateTime.Now);
                    MessageBox.Show($"Xin chào {name} - {role}", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    MenuForm menu = new MenuForm(role);
                    menu.ShowDialog();
                    this.Show();
                } else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            string tenMayTinh = Environment.MachineName;
            Session.MayTinhXuLy = tenMayTinh;
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginManager.CheckOut(MaNhanVien.ToString(), DateTime.Now);
        }
    }
}

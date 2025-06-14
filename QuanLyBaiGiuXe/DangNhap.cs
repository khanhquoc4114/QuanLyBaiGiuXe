using System;
using System.Windows.Forms;
using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Helper;

namespace QuanLyBaiGiuXe
{
    public partial class DangNhap : Form
    {
        string MaNhanVien = null;
        LoginManager loginManager = new LoginManager();
        bool isLoggedIn = false;

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
                MaNhanVien = loginManager.GetMaNhanVien(tbUsername.Text, tbPassword.Text, out string ErrorMessage);
                if (!string.IsNullOrEmpty(MaNhanVien))
                {
                    Session.MaNhanVien = MaNhanVien;
                    string name = loginManager.GetTen(tbUsername.Text, tbPassword.Text);
                    string role = Session.VaiTro = loginManager.GetVaiTro(tbUsername.Text, tbPassword.Text);

                    isLoggedIn = true;
                    if (isLoggedIn)
                    {
                        loginManager.CheckIn(MaNhanVien.ToString(), DateTime.Now);
                        if (cbRememberMe.Checked)
                        {
                            Properties.Settings.Default.SavedUsername = tbUsername.Text.Trim();
                            Properties.Settings.Default.SavedPassword = tbPassword.Text.Trim();
                            Properties.Settings.Default.RememberMe = true;
                        }
                        else
                        {
                            Properties.Settings.Default.SavedUsername = "";
                            Properties.Settings.Default.SavedPassword = "";
                            Properties.Settings.Default.RememberMe = false;
                        }
                        Properties.Settings.Default.Save();
                    }
                    this.Hide();
                    MenuForm menu = new MenuForm(name);
                    menu.ShowDialog();
                    this.Show();
                }
                else
                {
                    ToastService.Show(ErrorMessage, this);
                }
            }
            catch (Exception ex)
            {
                ToastService.Show("Lỗi đăng nhập: " + ex.Message, this);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            Session.MayTinhXuLy = Environment.MachineName;
            if (Properties.Settings.Default.RememberMe)
            {
                tbUsername.Text = Properties.Settings.Default.SavedUsername;
                tbPassword.Text = Properties.Settings.Default.SavedPassword;
                MessageBox.Show(Properties.Settings.Default.SavedUsername, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRememberMe.Checked = true;
            }
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            isLoggedIn = false;
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap.PerformClick();
                e.Handled = true;
            }
        }
    }
}

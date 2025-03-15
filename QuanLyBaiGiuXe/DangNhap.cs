using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBaiGiuXe.DataAccess;

namespace QuanLyBaiGiuXe
{
    public partial class DangNhap : Form
    {
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
                object dt = loginManager.UserCheck(tbUsername.Text, tbPassword.Text);
                MessageBox.Show(dt.ToString(), "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dt.ToString() == "quanli")
                {
                    this.Hide();
                    //formMain form = new formMain();
                    //form.Show();
                }
                else if (dt.ToString() == "nguoidung")
                {
                    string id = loginManager.GetID(tbUsername.Text, tbPassword.Text).ToString().Trim();
                    this.Hide();
                    //formCustomerMain form = new formCustomerMain(id);
                    //form.Show();
                }
                else if (dt.ToString() == "nhanvien")
                {
                    string id = loginManager.GetID(tbUsername.Text, tbPassword.Text).ToString().Trim();
                    this.Hide();
                    //formAttendantMain form = new formAttendantMain(id);
                    //form.Show();
                }
                else
                {
                    MessageBox.Show("Thông tin đăng nhập không chính xác. Mời nhập lại!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbUsername.Clear();
                    tbPassword.Clear();
                    tbUsername.Focus();
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
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Helper;

namespace QuanLyBaiGiuXe
{
    public partial class DangNhap : Form
    {
        string MaNhanVien = "";
        LoginManager loginManager = new LoginManager();
        bool isLoggedIn = false;
        Process process = new Process();

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

                    this.Hide();
                    MenuForm menu = new MenuForm(name);
                    isLoggedIn = true;
                    if (isLoggedIn)
                    {
                        loginManager.CheckIn(MaNhanVien.ToString(),DateTime.Now);
                    }
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
            Session.MayTinhXuLy = Environment.MachineName;
            LoadModel();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (process != null)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                }
                catch { }

                process.Dispose();
                MessageBox.Show("Đã dừng quá trình nhận diện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (isLoggedIn == true)
            { 
                loginManager.CheckOut(Session.MaNhanVien, DateTime.Now);
            }
        }

        private void LoadModel()
        {
            process.StartInfo.FileName = "python";
            process.StartInfo.Arguments = "-u main_tcp.py";
            process.StartInfo.WorkingDirectory = Path.Combine(Application.StartupPath, "python-server");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += (sender, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[Python STDOUT] " + e.Data);
            };

            process.ErrorDataReceived += (sender, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[Python STDERR] " + e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
    }
}

using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Helper;

namespace QuanLyBaiGiuXe
{
    public partial class MenuForm: Form
    {
        LoginManager loginManager = new LoginManager();
        public MenuForm(string name = "Just Wuoc")
        {
            InitializeComponent();
            name = loginManager.GetHoTenByMaNhanVien(Session.MaNhanVien);
            lbXinChao.Text = "Xin chào, " + name;
        }
        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        #region Button region

        private void btnVeThang_Click(object sender, EventArgs e)
        {
            VeThangMainForm veThangMainForm = new VeThangMainForm();
            veThangMainForm.Show();
        }


        private void btnThe_Click(object sender, EventArgs e)
        {
            TheMainForm theMainForm = new TheMainForm();
            theMainForm.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            NhanVienMainForm nhanVienMainForm = new NhanVienMainForm();
            nhanVienMainForm.Show();
        }

        private void btnVeLuot_Click(object sender, EventArgs e)
        {
            VeLuotMainForm veLuotMainForm = new VeLuotMainForm();
            veLuotMainForm.Show();
        }

        private void btnThongKeTheoMayTinh_Click(object sender, EventArgs e)
        {
            var form = new ThongKeTheoMayTinhForm();
            form.Show();
        }

        private void btnThongKeTongQuat_Click(object sender, EventArgs e)
        {
            var form = new MainForm();
            form.Show();
        }

        private void btnNhatKyVeLuot_Click(object sender, EventArgs e)
        {
            var form = new NhatKyVeLuotForm();
            form.Show();
        }

        private void btnThongKeChiTiet_Click(object sender, EventArgs e)
        {
            var form = new ThongKeChiTietForm();
            form.Show();
        }

        private void btnNhatKyXuLyVeThang_Click(object sender, EventArgs e)
        {
            var form = new NhatKyXuLyVeThangForm();
            form.Show();
        }

        private void btnTraCuuXeVaoRa_Click(object sender, EventArgs e)
        {
            var form = new TraCuuXeVaoRaForm();
            form.Show();
        }

        private void btnThongKeTheoKhoangThoiGian_Click(object sender, EventArgs e)
        {
            var form = new ThongKeTheoKhoangThoiGianForm();
            form.Show();
        }

        private void btnNhatKyDangNhap_Click(object sender, EventArgs e)
        {
            var form = new NhatKyDangNhapForm();
            form.Show();
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            var form = new CauHinhNguoiDungForm();
            form.Show();
        }

        private void btnThongKeTheoNhanVien_Click(object sender, EventArgs e)
        {
            var form = new ThongKeTheoNhanVienForm();
            form.Show();
        }

        private void btnNhatKyDieuChinhGiaVe_Click(object sender, EventArgs e)
        {
            var form = new NhatKyDieuChinhGiaVeForm();
            form.Show();
        }
        #endregion

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.RememberMe == false)
            {
                loginManager.CheckOut(Session.MaNhanVien, DateTime.Now);
            }
            Program.KillModelProcess();
            Application.Exit();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }            
            loginManager.CheckOut(Session.MaNhanVien, DateTime.Now);

            Properties.Settings.Default.SavedUsername = "";
            Properties.Settings.Default.SavedPassword = "";
            Properties.Settings.Default.Save();

            Application.Restart();
        }
    }
}

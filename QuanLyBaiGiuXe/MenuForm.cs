using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class MenuForm: Form
    {
        public MenuForm(string name)
        {
            InitializeComponent();
            lbXinChao.Text = "Xin chào, " + name;
        }

        private void btnVeThang_Click(object sender, EventArgs e)
        {
            VeThangMainForm veThangMainForm = new VeThangMainForm();
            veThangMainForm.Show();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

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

        private void btnNhatKyMatThe_Click(object sender, EventArgs e)
        {
            var form = new NhatKyXuLyMatTheForm();
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
            var form = new CauHinhHeThongForm();
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
    }
}

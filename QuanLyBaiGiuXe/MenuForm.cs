using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class MenuForm: Form
    {
        string role;
        public MenuForm(string role)
        {
            InitializeComponent();
            this.role = role;
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

        }
    }
}

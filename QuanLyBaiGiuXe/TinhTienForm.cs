using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Models;

namespace QuanLyBaiGiuXe
{
    public partial class TinhTienForm: Form
    {
        Manager manager = new Manager();
        public TinhTienForm()
        {
            InitializeComponent();
        }

        private void trbTu_Scroll(object sender, EventArgs e)
        {
            tbTu.Text = trbTu.Value.ToString();
        }

        private void trbDen_Scroll(object sender, EventArgs e)
        {
            tbDen.Text = trbDen.Value.ToString();
        }

        private void trbKhoangGiao_Scroll(object sender, EventArgs e)
        {
            tbKhoangGiao.Text = trbKhoangGiao.Value.ToString();
        }

        private void trbPhuThuTu_Scroll(object sender, EventArgs e)
        {
            tbPhuThuTu.Text = trbPhuThuTu.Value.ToString();
        }

        private void trbPhuThuDen_Scroll(object sender, EventArgs e)
        {
            tbPhuThuDen.Text = trbPhuThuDen.Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void trbMoc1_Scroll(object sender, EventArgs e)
        {
            tbMoc1.Text = trbMoc1.Value.ToString();
        }

        private void trbMoc2_Scroll(object sender, EventArgs e)
        {
            tbMoc2.Text = trbMoc2.Value.ToString();
        }

        private void trbChuKy_Scroll(object sender, EventArgs e)
        {
            tbChuKy.Text = trbChuKy.Value.ToString();
        }

        private void TinhTienForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dtgLoaiXe.DataSource = manager.GetAllLoaiXe();

        }

        private void dtgTien_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dtgLoaiXe_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dtgLoaiXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không phải tiêu đề
            {
                int maLoaiXe = Convert.ToInt32(dtgLoaiXe.Rows[e.RowIndex].Cells["MaLoaiXe"].Value);

                DataTable dt = manager.GetTinhTienCongVanByID(maLoaiXe.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cbThuTienTruoc.Checked = row.Field<bool>("ThuTienTruoc");
                    trbTu.Value = row.Field<byte>("DemTu");
                    trbDen.Value = row.Field<byte>("DemDen");
                    trbKhoangGiao.Value = row.Field<byte>("GioGiaoNgayDem");
                    nupGiaThuong.Value = row.Field<int>("GiaThuong");
                    nupGiaDem.Value = row.Field<int>("GiaDem");
                    nupGiaNgayDem.Value = row.Field<int>("GiaNgayDem");
                    nupGiaPhuThu.Value = row.Field<int>("GiaPhuThu");
                    trbPhuThuTu.Value = row.Field<byte>("PhuThuTu");
                    trbPhuThuDen.Value = row.Field<byte>("PhuThuDen");
                }
                DataTable dt2 = manager.GetTinhTienLuyTienByID(maLoaiXe.ToString());
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataRow row = dt2.Rows[0];
                    trbMoc1.Value = row.Field<byte>("Moc1");
                    nupGiaMoc1.Value = row.Field<int>("GiaMoc1");
                    trbMoc2.Value = row.Field<byte>("Moc2");
                    nupGiaMoc2.Value = row.Field<int>("GiaMoc2");
                    trbChuKy.Value = row.Field<byte>("ChuKy");
                    nupGiaVuotMoc.Value = row.Field<int>("GiaVuotMoc");
                }
            }
        }
    }
}

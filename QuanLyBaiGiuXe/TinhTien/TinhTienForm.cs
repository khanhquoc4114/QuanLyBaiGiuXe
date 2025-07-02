using System;
using System.Data;
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

        private void TinhTienForm_Load(object sender, EventArgs e)
        {
            LoadData();
            if (dtgLoaiXe.Rows.Count > 0)
            {
                dtgLoaiXe.ClearSelection();
                dtgLoaiXe.Rows[0].Selected = true;
                dtgLoaiXe_CellClick(dtgLoaiXe, new DataGridViewCellEventArgs(0, 0));
            }
        }
        private void LoadData()
        {
            dtgLoaiXe.DataSource = manager.GetAllLoaiXe();
            dtgLoaiXe.Columns["MaLoaiXe"].Visible = false;
            if (dtgLoaiXe.Rows.Count > 0)
            {
                dtgLoaiXe.CurrentCell = dtgLoaiXe.Rows[0].Cells[1];
                dtgLoaiXe.Rows[0].Selected = true;
                dtgLoaiXe_CellClick(dtgLoaiXe, new DataGridViewCellEventArgs(0, 0));
            }
        }
        private void dtgLoaiXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)    
            {
                int maLoaiXe = Convert.ToInt32(dtgLoaiXe.Rows[e.RowIndex].Cells["MaLoaiXe"].Value);
                string TenLoaiXe = Convert.ToString(dtgLoaiXe.Rows[e.RowIndex].Cells["Tên loại xe"].Value);
                tbTen.Text = TenLoaiXe.Trim();

                nupGiaVeThang.Value = manager.GetGiaVeThangById(maLoaiXe);
                nupGiaMienPhi.Value = manager.GetPhutMienPhiById(maLoaiXe);

                DataTable dt = manager.GetTinhTienCongVanByID(maLoaiXe);
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
                DataTable dt2 = manager.GetTinhTienLuyTienByID(maLoaiXe);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataRow row = dt2.Rows[0];
                    trbMoc1.Value = row.Field<byte>("Moc1");
                    nupGiaMoc1.Value = row.Field<int>("GiaMoc1");
                    trbMoc2.Value = row.Field<byte>("Moc2");
                    nupGiaMoc2.Value = row.Field<int>("GiaMoc2");
                    trbChuKy.Value = row.Field<byte>("ChuKy");
                    nupGiaVuotMoc.Value = row.Field<int>("GiaVuotMoc");
                    int congMoc = row.Field<byte>("CongMoc");
                    if (congMoc == 1)
                    {
                        rdb1.Checked = true;
                    }
                    else if (congMoc == 2)
                    {
                        rdb2.Checked = true;
                    }
                    else
                    {
                        rdb0.Checked = true;
                    }
                }
            }
        }

        #region UI Session
        private void trbTu_ValueChanged(object sender, EventArgs e)
        {
            tbTu.Text = trbTu.Value.ToString();
        }

        private void trbDen_ValueChanged(object sender, EventArgs e)
        {
            tbDen.Text = trbDen.Value.ToString();
        }

        private void trbKhoangGiao_ValueChanged(object sender, EventArgs e)
        {
            tbKhoangGiao.Text = trbKhoangGiao.Value.ToString();
        }

        private void trbPhuThuTu_ValueChanged(object sender, EventArgs e)
        {
            tbPhuThuTu.Text = trbPhuThuTu.Value.ToString();
        }

        private void trbPhuThuDen_ValueChanged(object sender, EventArgs e)
        {
            tbPhuThuDen.Text = trbPhuThuDen.Value.ToString();
        }

        private void trbMoc1_ValueChanged(object sender, EventArgs e)
        {
            tbMoc1.Text = trbMoc1.Value.ToString();
        }

        private void trbMoc2_ValueChanged(object sender, EventArgs e)
        {
            tbMoc2.Text = trbMoc2.Value.ToString();
        }

        private void trbChuKy_ValueChanged(object sender, EventArgs e)
        {
            tbChuKy.Text = trbChuKy.Value.ToString();
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
        #endregion

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgLoaiXe.CurrentRow != null && dtgLoaiXe.CurrentRow.Index >= 0)
            {
                var row = dtgLoaiXe.CurrentRow;

                if (row.Cells["MaLoaiXe"].Value != null && row.Cells["Tên loại xe"].Value != null)
                {
                    int maLoaiXe = Convert.ToInt32(row.Cells["MaLoaiXe"].Value);
                    string TenLoaiXe = row.Cells["Tên loại xe"].Value.ToString().Trim();

                    MessageBox.Show($"Bạn có chắc chắn muốn xóa loại xe '{TenLoaiXe}' (Mã: {maLoaiXe})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                    if (manager.XoaLoaiXe(maLoaiXe.ToString()))
                    {
                        new ToastForm("Xóa loại xe thành công", this).Show();
                        LoadData();
                    } else
                    {
                        new ToastForm("Xóa loại xe thất bại", this).Show();
                    }
                }
                else
                {
                    MessageBox.Show("Dữ liệu dòng không hợp lệ!");
                }
            }
            else
            {
                MessageBox.Show("Không có dòng nào được chọn!");
            }
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDongYSua_Click(object sender, EventArgs e)
        {
            string maLoaiXe = dtgLoaiXe.Rows[dtgLoaiXe.CurrentCell.RowIndex].Cells["MaLoaiXe"].Value.ToString();
            string giaVeThang = nupGiaVeThang.Value.ToString();
            string PhutMienPhi = nupGiaMienPhi.Value.ToString();
            bool result = manager.UpsertGiaVeThang(maLoaiXe, giaVeThang, PhutMienPhi);
            if (result)
            {
                MessageBox.Show("Cập nhật giá vé tháng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật giá vé tháng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bool thuTienTruoc = cbThuTienTruoc.Checked;
            byte demTu = (byte)trbTu.Value;
            byte demDen = (byte)trbDen.Value;
            byte gioGiaoNgayDem = (byte)trbKhoangGiao.Value;
            int giaThuong = (int)nupGiaThuong.Value;
            int giaDem = (int)nupGiaDem.Value;
            int giaNgayDem = (int)nupGiaNgayDem.Value;
            int giaPhuThu = (int)nupGiaPhuThu.Value;
            byte phuThuTu = (byte)trbPhuThuTu.Value;
            byte phuThuDen = (byte)trbPhuThuDen.Value;

            manager.UpsertTinhTienCongVan(
                maLoaiXe,
                thuTienTruoc,
                demTu,
                demDen,
                gioGiaoNgayDem,
                giaThuong,
                giaDem,
                giaNgayDem,
                giaPhuThu,
                phuThuTu,
                phuThuDen
            );

            byte moc1 = (byte)trbMoc1.Value;
            int giaMoc1 = (int)nupGiaMoc1.Value;
            byte moc2 = (byte)trbMoc2.Value;
            int giaMoc2 = (int)nupGiaMoc2.Value;
            byte chuKy = (byte)trbChuKy.Value;
            int giaVuotMoc = (int)nupGiaVuotMoc.Value;
            byte congMoc = 0;
            if (rdb1.Checked)
                congMoc = 1;
            else if (rdb2.Checked)
                congMoc = 2;

            manager.UpsertTinhTienLuyTien(
                maLoaiXe,
                moc1,
                giaMoc1,
                moc2,
                giaMoc2,
                chuKy,
                giaVuotMoc,
                congMoc
            );
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var form = new ThemXeForm();
            form.ShowDialog();
            LoadData();
            if (form.isSuccess)
            {
                Clear();
                new ToastForm("Thêm loại xe thành công", this).Show();
            }

            else
            {
                new ToastForm("Thêm loại xe thất bại", this).Show();
            }
        }

        private void Clear()
        {
            tbTen.Text = string.Empty;
            nupGiaVeThang.Value = 0;
            nupGiaMienPhi.Value = 0;
            cbThuTienTruoc.Checked = false;
            trbTu.Value = 0;
            trbDen.Value = 0;
            trbKhoangGiao.Value = 0;
            nupGiaThuong.Value = 0;
            nupGiaDem.Value = 0;
            nupGiaNgayDem.Value = 0;
            nupGiaPhuThu.Value = 0;
            trbPhuThuTu.Value = 0;
            trbPhuThuDen.Value = 0;
            trbMoc1.Value = 0;
            nupGiaMoc1.Value = 0;
            trbMoc2.Value = 0;
            nupGiaMoc2.Value = 0;
            trbChuKy.Value = 0;
            nupGiaVuotMoc.Value = 0;
        }
    }
}

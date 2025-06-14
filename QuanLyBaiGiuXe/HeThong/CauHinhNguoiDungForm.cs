using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Models;
using QuanLyBaiGiuXe.Properties;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class CauHinhNguoiDungForm: Form
    {
        Connector db = new Connector();

        public CauHinhNguoiDungForm()
        {
            InitializeComponent();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveOrInsertData();
            AppConfig.Load(db.GetConnection());
            Properties.Settings.Default.ImagePath = tbPath.Text.Trim();
            Properties.Settings.Default.Save();
        }
        private void CauHinhNguoiDungForm_Load(object sender, EventArgs e)
        {
            LoadData();
            tbPath.Text = Properties.Settings.Default.ImagePath ?? string.Empty;
        }
        public void LoadData()
        {
            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM CauHinhHeThong", db.GetConnection()))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbTenCongTy.Text = reader["TenCongTy"]?.ToString();
                            tbDiaChiCongTy.Text = reader["DiaChi"]?.ToString();
                            tbEmailCongTy.Text = reader["Email"]?.ToString();
                            tbPath.Text = reader["SoDienThoai"]?.ToString();

                            nupPhatThe.Value = Convert.ToDecimal(reader["TienPhatMatThe"]);
                            nupXeToiDa.Value = Convert.ToDecimal(reader["SoLuongXeToiDa"]);
                            nupHanMuc.Value = Convert.ToDecimal(reader["HanMucVeThang"]);

                            int hinhThuc = Convert.ToInt32(reader["HinhThucThuPhi"]);
                            rbMienPhi.Checked = hinhThuc == -1;
                            rbCongVan.Checked = hinhThuc == 0;
                            rbLuyTien.Checked = hinhThuc == 1;

                            int hetHan = Convert.ToInt32(reader["XuLyVeThangHetHan"]);
                            rbKhongChoVao.Checked = hetHan == -1;
                            rbCanhBao.Checked = hetHan == 0;
                            rbPhiVeLuot.Checked = hetHan == 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu config: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveOrInsertData()
        {
            try
            {
                db.OpenConnection();

                string sql = @"
                    IF EXISTS (SELECT 1 FROM CauHinhHeThong)
                    BEGIN
                        UPDATE CauHinhHeThong SET
                            TenCongTy = @TenCongTy,
                            DiaChi = @DiaChi,
                            Email = @Email,
                            SoDienThoai = @SoDienThoai,
                            TienPhatMatThe = @TienPhatMatThe,
                            SoLuongXeToiDa = @SoLuongXeToiDa,
                            HanMucVeThang = @HanMucVeThang,
                            HinhThucThuPhi = @HinhThucThuPhi,
                            XuLyVeThangHetHan = @XuLyVeThangHetHan
                    END
                    ELSE
                    BEGIN
                        INSERT INTO CauHinhHeThong (
                            TenCongTy, DiaChi, Email, SoDienThoai,
                            TienPhatMatThe, SoLuongXeToiDa, HanMucVeThang,
                            HinhThucThuPhi, XuLyVeThangHetHan
                        )
                        VALUES (
                            @TenCongTy, @DiaChi, @Email, @SoDienThoai,
                            @TienPhatMatThe, @SoLuongXeToiDa, @HanMucVeThang,
                            @HinhThucThuPhi, @XuLyVeThangHetHan
                        )
                    END
                ";

                using (SqlCommand cmd = new SqlCommand(sql, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TenCongTy", tbTenCongTy.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", tbDiaChiCongTy.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", tbEmailCongTy.Text.Trim());
                    cmd.Parameters.AddWithValue("@SoDienThoai", tbPath.Text.Trim());

                    cmd.Parameters.AddWithValue("@TienPhatMatThe", Convert.ToInt32(nupPhatThe.Value));
                    cmd.Parameters.AddWithValue("@SoLuongXeToiDa", Convert.ToInt32(nupXeToiDa.Value));
                    cmd.Parameters.AddWithValue("@HanMucVeThang", Convert.ToInt32(nupHanMuc.Value));

                    int hinhThucThuPhi = rbMienPhi.Checked ? -1 :
                                         rbCongVan.Checked ? 0 :
                                         rbLuyTien.Checked ? 1 : 0;

                    int khiHetHan = rbKhongChoVao.Checked ? -1 :
                                    rbCanhBao.Checked ? 0 :
                                    rbPhiVeLuot.Checked ? 1 : 0;

                    cmd.Parameters.AddWithValue("@HinhThucThuPhi", hinhThucThuPhi);
                    cmd.Parameters.AddWithValue("@XuLyVeThangHetHan", khiHetHan);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Lưu cấu hình thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục lưu hình ảnh";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnTinhTienForm_Click(object sender, EventArgs e)
        {
            var form = new TinhTienForm();
            form.ShowDialog();
        }
    }
}

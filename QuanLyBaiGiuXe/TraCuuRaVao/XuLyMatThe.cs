using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using QuanLyBaiGiuXe.Properties;

namespace QuanLyBaiGiuXe
{
    public partial class XuLyMatThe: Form
    {
        Manager manager = new Manager();
        string maveluot = string.Empty;
        Connector db = new Connector();
        TinhTienManager TinhTienManager = new TinhTienManager();
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public bool isThanhCong = false;

        public XuLyMatThe(string maveluot)
        {
            InitializeComponent();
            this.maveluot = maveluot;
        }

        #region Button Logic
        private void btnChoRaKoTinhPhi_Click(object sender, EventArgs e)
        {
            bool raAnh = XuLyPathAnh("ra_", out string path);
            pbRa.Image = Image.FromFile(path);
            if (cbKhoaThe.Checked == true)
            {
                bool isKhoaThe = manager.SetTrangThaiSuDungThe(tbMaThe.Text, -1);
                if (!isKhoaThe)
                {
                    ToastService.Show("Không thể khóa thẻ!", this);
                    return;
                } else
                {
                    ToastService.Show("Khóa thẻ thành công!", this);
                }
            }
            bool result = manager.CapNhatMatTheVeLuot(maveluot,path, 0, cbKhoaThe.Checked);
            if (result)
            {
                ToastService.Show("Cập nhật vé thành công!, 0 đ", this);
                isThanhCong = true;
                LoadData();
                StopCamera();
            }
            else
            {
                ToastService.Show("Cập nhật vé thất bại!", this);
            }
        }

        private void btnChoRaTinhPhi_Click(object sender, EventArgs e)
        {
            bool raAnh = XuLyPathAnh("ra_", out string path);
            pbRa.Image = Image.FromFile(path);
            if (cbKhoaThe.Checked == true)
            {
                bool isKhoaThe = manager.SetTrangThaiSuDungThe(tbMaThe.Text, -1);
                if (!isKhoaThe)
                {
                    ToastService.Show("Không thể khóa thẻ!", this);
                    return;
                }
                else
                {
                    ToastService.Show("Khóa thẻ thành công!", this);
                }
            }

            int tien = Convert.ToInt32(tbGiaVe.Text) + AppConfig.TienPhatMatThe;
            bool result = manager.CapNhatMatTheVeLuot(maveluot, path, tien , cbKhoaThe.Checked);
            if (result)
            {
                ToastService.Show($"Cập nhật vé thành công!, {tien.ToString("N0")} đ", this);
                isThanhCong = true;
                StopCamera();
                LoadData();
            }
            else
            {
                ToastService.Show("Cập nhật vé thất bại!", this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void XuLyMatThe_Load(object sender, EventArgs e)
        {
            LoadCamera();
            LoadData();
        }
        private void LoadData()
        {
            lbTienPhat.Text = "(+ " + AppConfig.TienPhatMatThe.ToString("N0") + " VNĐ)";

            try
            {
                db.OpenConnection();
                using (SqlCommand cmd = new SqlCommand(@"sp_xulymatthe", db.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@maveluot", maveluot);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbMaThe.Text = reader["MaThe"].ToString();
                            tbBienSo.Text = reader["BienSo"].ToString();
                            tbLoaiXe.Text = reader["TenLoaiXe"].ToString();
                            tbThoiGianVao.Text = reader["ThoiGianVao"].ToString();
                            DateTime tgVao = Convert.ToDateTime(reader["ThoiGianVao"]);
                            DateTime tgRa = DateTime.Now;
                            tbNhanVien.Text = reader["HoTen"].ToString();
                            tbMayTinh.Text = reader["MayTinhXuLy"].ToString();
                            string CachTinhTien = reader["CachTinhTien"].ToString();
                            int maLoaiXe = Convert.ToInt32(reader["MaLoaiXe"]);
                            string TongTienReal = reader["TongTien"].ToString();
                            string AnhVaoPath = reader["AnhVaoPath"].ToString();
                            string AnhRaPath = reader["AnhRaPath"].ToString();
                            string TrangThai = reader["TrangThai"].ToString();
                            if (TrangThai == "Chưa ra")
                            {
                                int tongtiengiasu = TinhTienManager.TinhTien(CachTinhTien, tgVao, tgRa, maLoaiXe);
                                tbGiaVe.Text = tongtiengiasu.ToString();
                            }
                            else if (TrangThai == "Đã ra")
                            {
                                tbGiaVe.Text = TongTienReal;
                                lbTienPhat.Text = "";
                                btnChoRaKoTinhPhi.Enabled = false;
                                btnChoRaTinhPhi.Enabled = false;
                                cbKhoaThe.Enabled = false;
                                StopCamera();
                            }

                            LoadImageToPictureBox(pbVao, AnhVaoPath);
                            if (!string.IsNullOrEmpty(AnhRaPath))
                            {
                                LoadImageToPictureBox(pbRa, AnhRaPath);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu với mã vé lượt đã cho.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách loại xe: " + ex.Message);
            }
        }
        private void tbMaThe_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void LoadImageToPictureBox(PictureBox picBox, string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picBox.SizeMode = PictureBoxSizeMode.Zoom;
                    picBox.Image?.Dispose();

                    using (var ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                    {
                        picBox.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                picBox.Image = null;
            }
        }

        private bool XuLyPathAnh(string prefix, out string imagePath)
        {
            imagePath = string.Empty;
            string path = Settings.Default.ImagePath;
            try
            {
                string imageDir = Path.Combine($@"{path}\XeImages", DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(imageDir))
                    Directory.CreateDirectory(imageDir);

                string fileName = prefix + "_" + Guid.NewGuid().ToString("N").Substring(0, 9) + ".jpg";
                imagePath = Path.Combine(imageDir, fileName);

                using (Bitmap img = new Bitmap(pbRa.Image))
                {
                    img.Save(imagePath, ImageFormat.Jpeg);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu ảnh: " + ex.Message);
                return false;
            }
        }

        private void XuLyMatThe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                if (pbRa.InvokeRequired)
                {
                    pbRa.BeginInvoke(new Action(() =>
                    {
                        pbRa.Image?.Dispose();
                        pbRa.Image = bitmap;
                        pbRa.SizeMode = PictureBoxSizeMode.Zoom;
                    }));
                }
                else
                {
                    pbRa.Image?.Dispose();
                    pbRa.Image = bitmap;
                    pbRa.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch { }
        }
        private void LoadCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Không tìm thấy camera!");
                return;
            }
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }
        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop(); // Gửi tín hiệu dừng
                videoSource.WaitForStop();  // Chờ đến khi camera thật sự dừng
                videoSource = null;
            }
        }
    }
}

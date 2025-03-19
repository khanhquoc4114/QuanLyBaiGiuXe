using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.IO;
using Emgu.CV.Util;

namespace QuanLyBaiGiuXe
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;

                    // Hiển thị ảnh lên PictureBox
                    pbVao2.Image = Image.FromFile(imagePath);

                    // Xử lý ảnh và trích xuất biển số
                    string licensePlate = ExtractLicensePlate(imagePath);
                    tbResult.Text = licensePlate;
                }
            }
        }
        private string ExtractLicensePlate(string imagePath)
        {
            try
            {
                // Đọc ảnh bằng Emgu CV
                using (Mat image = CvInvoke.Imread(imagePath, Emgu.CV.CvEnum.ImreadModes.Color))
                {
                    // Chuyển ảnh sang grayscale để xử lý
                    Mat grayImage = new Mat();
                    CvInvoke.CvtColor(image, grayImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                    // Làm mịn ảnh để giảm nhiễu
                    CvInvoke.GaussianBlur(grayImage, grayImage, new Size(5, 5), 0);

                    // Phát hiện cạnh (Canny Edge Detection)
                    Mat edges = new Mat();
                    CvInvoke.Canny(grayImage, edges, 100, 200);

                    // Tìm contours (vùng có thể là biển số)
                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    CvInvoke.FindContours(edges, contours, null, Emgu.CV.CvEnum.RetrType.List,
                        Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                    // Lọc contour có thể là biển số (dựa trên tỷ lệ chiều dài/rộng)
                    Rectangle? licensePlateRegion = null;
                    for (int i = 0; i < contours.Size; i++)
                    {
                        Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                        float aspectRatio = (float)rect.Width / rect.Height;

                        // Giả định biển số có tỷ lệ chiều dài/rộng từ 2 đến 5
                        if (aspectRatio > 2 && aspectRatio < 5 && rect.Width > 100 && rect.Height > 20)
                        {
                            licensePlateRegion = rect;
                            break;
                        }
                    }

                    if (licensePlateRegion.HasValue)
                    {
                        // Cắt vùng biển số từ ảnh gốc
                        Mat licensePlateImage = new Mat(image, licensePlateRegion.Value);
                        string tempFilePath = Path.Combine(Path.GetTempPath(), "license_plate.jpg");
                        CvInvoke.Imwrite(tempFilePath, licensePlateImage);

                        // Gọi OpenALPR CLI để nhận diện biển số
                        string result = RunOpenALPR(tempFilePath);
                        return result;
                    }
                    else
                    {
                        return "Không tìm thấy biển số xe.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        private string RunOpenALPR(string imagePath)
        {
            // Giả định bạn đã cài OpenALPR CLI và cấu hình đường dẫn
            string openAlprPath = @"C:\path\to\alpr.exe"; // Đường dẫn đến OpenALPR CLI
            string configPath = @"C:\path\to\openalpr.conf"; // Đường dẫn đến file config
            string runtimeDataPath = @"C:\path\to\runtime_data"; // Đường dẫn đến runtime data

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = openAlprPath,
                Arguments = $"-c vn --config \"{configPath}\" --runtime_data \"{runtimeDataPath}\" \"{imagePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Phân tích kết quả từ OpenALPR
                if (!string.IsNullOrEmpty(output))
                {
                    string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        if (line.Contains("plate"))
                        {
                            string[] parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length > 1)
                            {
                                return parts[1].Trim(); // Trả về biển số
                            }
                        }
                    }
                }
                return "Không nhận diện được biển số.";
            }
        }
    }
}

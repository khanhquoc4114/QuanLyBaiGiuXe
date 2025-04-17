using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace QuanLyBaiGiuXe
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "\"D:\\UIT\\Year3\\semester2\\DACN\\AA_Main\\archive\\biensoxemayhon100bien\\anh\\0000_02187_b.jpg\"";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    try
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);
                        pbVao1.Image = image;
                    } catch
                    {

                    }
                }
            }
        }

        //Handle process for image
        private Bitmap PreProcessImage(Bitmap image)
        {
            // 1. Convert to Grayscale
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721); // Standard weights for RGB to grayscale
            Bitmap grayscaleImage = grayscaleFilter.Apply(image);

            // 2. Apply Gaussian Blur (Noise Reduction)
            GaussianBlur blurFilter = new GaussianBlur(2, 5); // Radius, Sigma
            blurFilter.ApplyInPlace(grayscaleImage); // Applies the filter directly to the image

            // 3. Contrast Enhancement (Histogram Equalization)
            HistogramEqualization equalizationFilter = new HistogramEqualization();
            equalizationFilter.ApplyInPlace(grayscaleImage);

            // 4. Sharpening (Optional, but often helpful)
            Sharpen sharpenFilter = new Sharpen();
            sharpenFilter.ApplyInPlace(grayscaleImage);

            return grayscaleImage;
        }

    }
}
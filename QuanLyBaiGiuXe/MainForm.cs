using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Linq;
using Tesseract;
using TGMT;

namespace QuanLyBaiGiuXe
{
    public partial class MainForm : Form
    {
        PlateReader reader;
        public MainForm()
        {
            InitializeComponent();
            //pbVao1.SizeMode = PictureBoxSizeMode.Zoom;
            //pbVao2.SizeMode = PictureBoxSizeMode.Zoom;
            //originalImage = new Bitmap(@"D:\UIT\Year3\semester2\DACN\AA_Main\archive\biensoxemayhon100bien\anh\0000_02187_b.jpg");
            //pbVao1.Image = originalImage; // Display in a PictureBox

            //// Perform some basic pre-processing (example)
            //Bitmap processedImage = PreProcessImage(originalImage);
            //pbVao2.Image = processedImage; // Display processed image

            //// You would then call your LPR functions here, passing in the processedImage
            //string licensePlateText = DetectLicensePlate(processedImage);
            //tbResult.Text = licensePlateText;
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
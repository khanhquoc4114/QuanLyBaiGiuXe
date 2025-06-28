namespace QuanLyBaiGiuXe
{
    partial class DieuChinhGiaVeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DieuChinhGiaVeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nupCu = new System.Windows.Forms.NumericUpDown();
            this.nupMoi = new System.Windows.Forms.NumericUpDown();
            this.btnDongY = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupCu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMoi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Điều chỉnh thành:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giá vé cũ:";
            // 
            // nupCu
            // 
            this.nupCu.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupCu.Location = new System.Drawing.Point(210, 20);
            this.nupCu.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nupCu.Name = "nupCu";
            this.nupCu.Size = new System.Drawing.Size(120, 30);
            this.nupCu.TabIndex = 1;
            this.nupCu.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // nupMoi
            // 
            this.nupMoi.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupMoi.Location = new System.Drawing.Point(210, 74);
            this.nupMoi.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nupMoi.Name = "nupMoi";
            this.nupMoi.Size = new System.Drawing.Size(120, 30);
            this.nupMoi.TabIndex = 1;
            this.nupMoi.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(127, 130);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(109, 34);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.UseVisualStyleBackColor = true;
            this.btnDongY.Click += new System.EventHandler(this.DongY_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(255, 130);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 34);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // DieuChinhGiaVeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 185);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.nupMoi);
            this.Controls.Add(this.nupCu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DieuChinhGiaVeForm";
            this.Text = "Điều chỉnh giá vé";
            this.Load += new System.EventHandler(this.DieuChinhGiaVeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupCu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nupCu;
        private System.Windows.Forms.NumericUpDown nupMoi;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.Button btnDong;
    }
}
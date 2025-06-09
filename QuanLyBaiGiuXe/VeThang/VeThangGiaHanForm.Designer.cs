namespace QuanLyBaiGiuXe
{
    partial class VeThangGiaHanForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.btnDongY = new System.Windows.Forms.Button();
            this.btnGiaHan1Thang = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gia hạn đến:";
            // 
            // dtp
            // 
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(163, 25);
            this.dtp.Margin = new System.Windows.Forms.Padding(4);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(129, 25);
            this.dtp.TabIndex = 1;
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(30, 80);
            this.btnDongY.Margin = new System.Windows.Forms.Padding(4);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(88, 38);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.UseVisualStyleBackColor = true;
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnGiaHan1Thang
            // 
            this.btnGiaHan1Thang.Location = new System.Drawing.Point(140, 80);
            this.btnGiaHan1Thang.Margin = new System.Windows.Forms.Padding(4);
            this.btnGiaHan1Thang.Name = "btnGiaHan1Thang";
            this.btnGiaHan1Thang.Size = new System.Drawing.Size(143, 38);
            this.btnGiaHan1Thang.TabIndex = 3;
            this.btnGiaHan1Thang.Text = "Gia hạn thêm 1 tháng";
            this.btnGiaHan1Thang.UseVisualStyleBackColor = true;
            this.btnGiaHan1Thang.Click += new System.EventHandler(this.btnGiaHan1Thang_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(308, 80);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(88, 38);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // VeThangGiaHanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 142);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnGiaHan1Thang);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.dtp);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VeThangGiaHanForm";
            this.Text = "VeThangGiaHanForm";
            this.Load += new System.EventHandler(this.VeThangGiaHanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.Button btnGiaHan1Thang;
        private System.Windows.Forms.Button btnDong;
    }
}
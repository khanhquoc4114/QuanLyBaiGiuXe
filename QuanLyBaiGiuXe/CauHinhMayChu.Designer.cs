namespace QuanLyBaiGiuXe
{
    partial class CauHinhMayChu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.cbAuth = new System.Windows.Forms.ComboBox();
            this.btnLuuCauHinh = new System.Windows.Forms.Button();
            this.btnThuKetNoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnTCPIP = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnTCPIP);
            this.panel2.Controls.Add(this.btnDong);
            this.panel2.Controls.Add(this.btnThuKetNoi);
            this.panel2.Controls.Add(this.btnLuuCauHinh);
            this.panel2.Controls.Add(this.cbAuth);
            this.panel2.Controls.Add(this.tbPass);
            this.panel2.Controls.Add(this.tbUsername);
            this.panel2.Controls.Add(this.tbServer);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(509, 258);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cấu hình máy chủ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(91, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Máy chủ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(53, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kiểu xác thực:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(44, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tên đăng nhập:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(86, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Mật khẩu:";
            // 
            // tbServer
            // 
            this.tbServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbServer.Location = new System.Drawing.Point(182, 33);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(289, 26);
            this.tbServer.TabIndex = 4;
            this.tbServer.Text = "WUOC";
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbUsername.Location = new System.Drawing.Point(182, 111);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(289, 26);
            this.tbUsername.TabIndex = 5;
            // 
            // tbPass
            // 
            this.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbPass.Location = new System.Drawing.Point(182, 149);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(289, 26);
            this.tbPass.TabIndex = 6;
            // 
            // cbAuth
            // 
            this.cbAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbAuth.FormattingEnabled = true;
            this.cbAuth.Location = new System.Drawing.Point(182, 71);
            this.cbAuth.Name = "cbAuth";
            this.cbAuth.Size = new System.Drawing.Size(289, 28);
            this.cbAuth.TabIndex = 7;
            this.cbAuth.SelectedIndexChanged += new System.EventHandler(this.cbAuth_SelectedIndexChanged);
            // 
            // btnLuuCauHinh
            // 
            this.btnLuuCauHinh.Location = new System.Drawing.Point(53, 200);
            this.btnLuuCauHinh.Name = "btnLuuCauHinh";
            this.btnLuuCauHinh.Size = new System.Drawing.Size(97, 30);
            this.btnLuuCauHinh.TabIndex = 8;
            this.btnLuuCauHinh.Text = "Lưu cấu hình";
            this.btnLuuCauHinh.UseVisualStyleBackColor = true;
            this.btnLuuCauHinh.Click += new System.EventHandler(this.btnLuuCauHinh_Click);
            // 
            // btnThuKetNoi
            // 
            this.btnThuKetNoi.Location = new System.Drawing.Point(160, 200);
            this.btnThuKetNoi.Name = "btnThuKetNoi";
            this.btnThuKetNoi.Size = new System.Drawing.Size(97, 30);
            this.btnThuKetNoi.TabIndex = 8;
            this.btnThuKetNoi.Text = "Thử kết nối";
            this.btnThuKetNoi.UseVisualStyleBackColor = true;
            this.btnThuKetNoi.Click += new System.EventHandler(this.btnThuKetNoi_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(374, 200);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(97, 30);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnTCPIP
            // 
            this.btnTCPIP.Location = new System.Drawing.Point(267, 200);
            this.btnTCPIP.Name = "btnTCPIP";
            this.btnTCPIP.Size = new System.Drawing.Size(97, 30);
            this.btnTCPIP.TabIndex = 8;
            this.btnTCPIP.Text = "Bật TCP/IP";
            this.btnTCPIP.UseVisualStyleBackColor = true;
            this.btnTCPIP.Click += new System.EventHandler(this.btnTCPIP_Click);
            // 
            // CauHinhMayChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 358);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CauHinhMayChu";
            this.Text = "Cấu hình máy chủ";
            this.Load += new System.EventHandler(this.CauHinhMayChu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbAuth;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTCPIP;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnThuKetNoi;
        private System.Windows.Forms.Button btnLuuCauHinh;
    }
}
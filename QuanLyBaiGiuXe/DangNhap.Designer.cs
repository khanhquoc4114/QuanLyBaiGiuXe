namespace QuanLyBaiGiuXe
{
    partial class DangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.cbRememberMe = new System.Windows.Forms.CheckBox();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.panelPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật khẩu:";
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Location = new System.Drawing.Point(172, 61);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(365, 26);
            this.tbUsername.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword.Location = new System.Drawing.Point(1, 1);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(329, 19);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            this.tbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPassword_KeyPress);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Location = new System.Drawing.Point(425, 185);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(112, 35);
            this.btnDangNhap.TabIndex = 5;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnDong
            // 
            this.btnDong.ForeColor = System.Drawing.Color.Black;
            this.btnDong.Location = new System.Drawing.Point(298, 185);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(112, 35);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // cbRememberMe
            // 
            this.cbRememberMe.AutoSize = true;
            this.cbRememberMe.Location = new System.Drawing.Point(41, 191);
            this.cbRememberMe.Name = "cbRememberMe";
            this.cbRememberMe.Size = new System.Drawing.Size(140, 24);
            this.cbRememberMe.TabIndex = 3;
            this.cbRememberMe.Text = "Giữ đăng nhập";
            this.cbRememberMe.UseVisualStyleBackColor = true;
            // 
            // panelPassword
            // 
            this.panelPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPassword.Controls.Add(this.tbPassword);
            this.panelPassword.Controls.Add(this.btnShowPassword);
            this.panelPassword.Location = new System.Drawing.Point(172, 130);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(365, 26);
            this.panelPassword.TabIndex = 4;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.FlatAppearance.BorderSize = 0;
            this.btnShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPassword.Location = new System.Drawing.Point(334, 0);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(30, 26);
            this.btnShowPassword.TabIndex = 2;
            this.btnShowPassword.Text = "👁";
            this.btnShowPassword.UseVisualStyleBackColor = true;
            // 
            // DangNhap
            // 
            this.ClientSize = new System.Drawing.Size(591, 268);
            this.Controls.Add(this.panelPassword);
            this.Controls.Add(this.cbRememberMe);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DangNhap";
            this.Text = "Đăng Nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DangNhap_FormClosing);
            this.Load += new System.EventHandler(this.DangNhap_Load);
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.CheckBox cbRememberMe;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Button btnShowPassword;
        private bool passwordVisible = false;
    }
}


namespace QuanLyBaiGiuXe
{
    partial class VeThangMainForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtgVeThang = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnDoiThe = new System.Windows.Forms.Button();
            this.btnKhoiPhucVeThang = new System.Windows.Forms.Button();
            this.btnGiaHanVeThang = new System.Windows.Forms.Button();
            this.btnSuaVeThang = new System.Windows.Forms.Button();
            this.btnXoaVeThang = new System.Windows.Forms.Button();
            this.btnThemVeThang = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgNhom = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSuaNhom = new System.Windows.Forms.Button();
            this.btnXoaNhom = new System.Windows.Forms.Button();
            this.btnThemNhom = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVeThang)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhom)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 618);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtgVeThang);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(208, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(770, 618);
            this.panel3.TabIndex = 1;
            // 
            // dtgVeThang
            // 
            this.dtgVeThang.AllowUserToAddRows = false;
            this.dtgVeThang.AllowUserToDeleteRows = false;
            this.dtgVeThang.AllowUserToResizeRows = false;
            this.dtgVeThang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVeThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgVeThang.Location = new System.Drawing.Point(0, 100);
            this.dtgVeThang.MultiSelect = false;
            this.dtgVeThang.Name = "dtgVeThang";
            this.dtgVeThang.ReadOnly = true;
            this.dtgVeThang.RowHeadersVisible = false;
            this.dtgVeThang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgVeThang.Size = new System.Drawing.Size(770, 518);
            this.dtgVeThang.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbContent);
            this.panel5.Controls.Add(this.btnXuatExcel);
            this.panel5.Controls.Add(this.btnTimKiem);
            this.panel5.Controls.Add(this.btnDoiThe);
            this.panel5.Controls.Add(this.btnKhoiPhucVeThang);
            this.panel5.Controls.Add(this.btnGiaHanVeThang);
            this.panel5.Controls.Add(this.btnSuaVeThang);
            this.panel5.Controls.Add(this.btnXoaVeThang);
            this.panel5.Controls.Add(this.btnThemVeThang);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(770, 100);
            this.panel5.TabIndex = 0;
            // 
            // tbContent
            // 
            this.tbContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbContent.Location = new System.Drawing.Point(423, 16);
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(100, 20);
            this.tbContent.TabIndex = 5;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(610, 16);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(75, 23);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "Xuất excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(529, 16);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnDoiThe
            // 
            this.btnDoiThe.Location = new System.Drawing.Point(100, 42);
            this.btnDoiThe.Name = "btnDoiThe";
            this.btnDoiThe.Size = new System.Drawing.Size(75, 23);
            this.btnDoiThe.TabIndex = 4;
            this.btnDoiThe.Text = "Đổi thẻ";
            this.btnDoiThe.UseVisualStyleBackColor = true;
            this.btnDoiThe.Click += new System.EventHandler(this.btnDoiThe_Click);
            // 
            // btnKhoiPhucVeThang
            // 
            this.btnKhoiPhucVeThang.Location = new System.Drawing.Point(18, 42);
            this.btnKhoiPhucVeThang.Name = "btnKhoiPhucVeThang";
            this.btnKhoiPhucVeThang.Size = new System.Drawing.Size(75, 23);
            this.btnKhoiPhucVeThang.TabIndex = 4;
            this.btnKhoiPhucVeThang.Text = "Khôi phục";
            this.btnKhoiPhucVeThang.UseVisualStyleBackColor = true;
            // 
            // btnGiaHanVeThang
            // 
            this.btnGiaHanVeThang.Location = new System.Drawing.Point(264, 13);
            this.btnGiaHanVeThang.Name = "btnGiaHanVeThang";
            this.btnGiaHanVeThang.Size = new System.Drawing.Size(75, 23);
            this.btnGiaHanVeThang.TabIndex = 3;
            this.btnGiaHanVeThang.Text = "Gia hạn";
            this.btnGiaHanVeThang.UseVisualStyleBackColor = true;
            this.btnGiaHanVeThang.Click += new System.EventHandler(this.btnGiaHanVeThang_Click);
            // 
            // btnSuaVeThang
            // 
            this.btnSuaVeThang.Location = new System.Drawing.Point(182, 12);
            this.btnSuaVeThang.Name = "btnSuaVeThang";
            this.btnSuaVeThang.Size = new System.Drawing.Size(75, 23);
            this.btnSuaVeThang.TabIndex = 2;
            this.btnSuaVeThang.Text = "Sửa";
            this.btnSuaVeThang.UseVisualStyleBackColor = true;
            this.btnSuaVeThang.Click += new System.EventHandler(this.btnSuaVeThang_Click);
            // 
            // btnXoaVeThang
            // 
            this.btnXoaVeThang.Location = new System.Drawing.Point(100, 12);
            this.btnXoaVeThang.Name = "btnXoaVeThang";
            this.btnXoaVeThang.Size = new System.Drawing.Size(75, 23);
            this.btnXoaVeThang.TabIndex = 1;
            this.btnXoaVeThang.Text = "Xoá";
            this.btnXoaVeThang.UseVisualStyleBackColor = true;
            this.btnXoaVeThang.Click += new System.EventHandler(this.btnXoaVeThang_Click);
            // 
            // btnThemVeThang
            // 
            this.btnThemVeThang.Location = new System.Drawing.Point(18, 13);
            this.btnThemVeThang.Name = "btnThemVeThang";
            this.btnThemVeThang.Size = new System.Drawing.Size(75, 23);
            this.btnThemVeThang.TabIndex = 0;
            this.btnThemVeThang.Text = "Thêm";
            this.btnThemVeThang.UseVisualStyleBackColor = true;
            this.btnThemVeThang.Click += new System.EventHandler(this.btnThemVeThang_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgNhom);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 618);
            this.panel2.TabIndex = 0;
            // 
            // dtgNhom
            // 
            this.dtgNhom.AllowUserToAddRows = false;
            this.dtgNhom.AllowUserToDeleteRows = false;
            this.dtgNhom.AllowUserToResizeRows = false;
            this.dtgNhom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgNhom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgNhom.Location = new System.Drawing.Point(0, 100);
            this.dtgNhom.MultiSelect = false;
            this.dtgNhom.Name = "dtgNhom";
            this.dtgNhom.ReadOnly = true;
            this.dtgNhom.RowHeadersVisible = false;
            this.dtgNhom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgNhom.Size = new System.Drawing.Size(208, 518);
            this.dtgNhom.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSuaNhom);
            this.panel4.Controls.Add(this.btnXoaNhom);
            this.panel4.Controls.Add(this.btnThemNhom);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(208, 100);
            this.panel4.TabIndex = 0;
            // 
            // btnSuaNhom
            // 
            this.btnSuaNhom.Location = new System.Drawing.Point(102, 42);
            this.btnSuaNhom.Name = "btnSuaNhom";
            this.btnSuaNhom.Size = new System.Drawing.Size(75, 23);
            this.btnSuaNhom.TabIndex = 0;
            this.btnSuaNhom.Text = "Sửa nhóm";
            this.btnSuaNhom.UseVisualStyleBackColor = true;
            this.btnSuaNhom.Click += new System.EventHandler(this.btnSuaNhom_Click);
            // 
            // btnXoaNhom
            // 
            this.btnXoaNhom.Location = new System.Drawing.Point(12, 42);
            this.btnXoaNhom.Name = "btnXoaNhom";
            this.btnXoaNhom.Size = new System.Drawing.Size(75, 23);
            this.btnXoaNhom.TabIndex = 0;
            this.btnXoaNhom.Text = "Xoá nhóm";
            this.btnXoaNhom.UseVisualStyleBackColor = true;
            this.btnXoaNhom.Click += new System.EventHandler(this.btnXoaNhom_Click);
            // 
            // btnThemNhom
            // 
            this.btnThemNhom.Location = new System.Drawing.Point(12, 16);
            this.btnThemNhom.Name = "btnThemNhom";
            this.btnThemNhom.Size = new System.Drawing.Size(75, 23);
            this.btnThemNhom.TabIndex = 0;
            this.btnThemNhom.Text = "Thêm nhóm";
            this.btnThemNhom.UseVisualStyleBackColor = true;
            this.btnThemNhom.Click += new System.EventHandler(this.btnThemNhom_Click);
            // 
            // VeThangMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 618);
            this.Controls.Add(this.panel1);
            this.Name = "VeThangMainForm";
            this.Text = "VeThangMainForm";
            this.Load += new System.EventHandler(this.VeThangMainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgVeThang)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhom)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dtgVeThang;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnGiaHanVeThang;
        private System.Windows.Forms.Button btnSuaVeThang;
        private System.Windows.Forms.Button btnXoaVeThang;
        private System.Windows.Forms.Button btnThemVeThang;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnKhoiPhucVeThang;
        private System.Windows.Forms.DataGridView dtgNhom;
        private System.Windows.Forms.Button btnSuaNhom;
        private System.Windows.Forms.Button btnXoaNhom;
        private System.Windows.Forms.Button btnThemNhom;
        private System.Windows.Forms.Button btnDoiThe;
    }
}
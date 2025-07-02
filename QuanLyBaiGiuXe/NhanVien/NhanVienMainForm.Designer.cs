namespace QuanLyBaiGiuXe
{
    partial class NhanVienMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NhanVienMainForm));
            this.btnSuaNhom = new System.Windows.Forms.Button();
            this.btnThemNhom = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnXoaNhom = new System.Windows.Forms.Button();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgNhomNhanVien = new System.Windows.Forms.DataGridView();
            this.btnXoaNhanVien = new System.Windows.Forms.Button();
            this.btnThemNhanVien = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSuaNhanVien = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtgNhanVien = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhomNhanVien)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhanVien)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSuaNhom
            // 
            this.btnSuaNhom.Location = new System.Drawing.Point(183, 13);
            this.btnSuaNhom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSuaNhom.Name = "btnSuaNhom";
            this.btnSuaNhom.Size = new System.Drawing.Size(100, 28);
            this.btnSuaNhom.TabIndex = 0;
            this.btnSuaNhom.Text = "Sửa nhóm";
            this.btnSuaNhom.UseVisualStyleBackColor = true;
            this.btnSuaNhom.Click += new System.EventHandler(this.btnSuaNhom_Click);
            // 
            // btnThemNhom
            // 
            this.btnThemNhom.Location = new System.Drawing.Point(13, 13);
            this.btnThemNhom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThemNhom.Name = "btnThemNhom";
            this.btnThemNhom.Size = new System.Drawing.Size(100, 28);
            this.btnThemNhom.TabIndex = 0;
            this.btnThemNhom.Text = "Thêm nhóm";
            this.btnThemNhom.UseVisualStyleBackColor = true;
            this.btnThemNhom.Click += new System.EventHandler(this.btnThemNhom_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSuaNhom);
            this.panel4.Controls.Add(this.btnXoaNhom);
            this.panel4.Controls.Add(this.btnThemNhom);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(463, 55);
            this.panel4.TabIndex = 0;
            // 
            // btnXoaNhom
            // 
            this.btnXoaNhom.Location = new System.Drawing.Point(353, 13);
            this.btnXoaNhom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnXoaNhom.Name = "btnXoaNhom";
            this.btnXoaNhom.Size = new System.Drawing.Size(100, 28);
            this.btnXoaNhom.TabIndex = 0;
            this.btnXoaNhom.Text = "Xoá nhóm";
            this.btnXoaNhom.UseVisualStyleBackColor = true;
            this.btnXoaNhom.Click += new System.EventHandler(this.btnXoaNhom_Click);
            // 
            // tbContent
            // 
            this.tbContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbContent.Location = new System.Drawing.Point(524, 17);
            this.tbContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(133, 22);
            this.tbContent.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(677, 13);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 28);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnKhoiPhuc
            // 
            this.btnKhoiPhuc.Location = new System.Drawing.Point(395, 13);
            this.btnKhoiPhuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(100, 28);
            this.btnKhoiPhuc.TabIndex = 4;
            this.btnKhoiPhuc.Text = "Khôi phục";
            this.btnKhoiPhuc.UseVisualStyleBackColor = true;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgNhomNhanVien);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 688);
            this.panel2.TabIndex = 0;
            // 
            // dtgNhomNhanVien
            // 
            this.dtgNhomNhanVien.AllowUserToAddRows = false;
            this.dtgNhomNhanVien.AllowUserToDeleteRows = false;
            this.dtgNhomNhanVien.AllowUserToResizeRows = false;
            this.dtgNhomNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgNhomNhanVien.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgNhomNhanVien.ColumnHeadersHeight = 29;
            this.dtgNhomNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgNhomNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgNhomNhanVien.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgNhomNhanVien.Location = new System.Drawing.Point(0, 55);
            this.dtgNhomNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtgNhomNhanVien.MultiSelect = false;
            this.dtgNhomNhanVien.Name = "dtgNhomNhanVien";
            this.dtgNhomNhanVien.ReadOnly = true;
            this.dtgNhomNhanVien.RowHeadersVisible = false;
            this.dtgNhomNhanVien.RowHeadersWidth = 51;
            this.dtgNhomNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgNhomNhanVien.Size = new System.Drawing.Size(463, 633);
            this.dtgNhomNhanVien.TabIndex = 4;
            // 
            // btnXoaNhanVien
            // 
            this.btnXoaNhanVien.Location = new System.Drawing.Point(137, 13);
            this.btnXoaNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnXoaNhanVien.Name = "btnXoaNhanVien";
            this.btnXoaNhanVien.Size = new System.Drawing.Size(100, 28);
            this.btnXoaNhanVien.TabIndex = 1;
            this.btnXoaNhanVien.Text = "Xoá";
            this.btnXoaNhanVien.UseVisualStyleBackColor = true;
            this.btnXoaNhanVien.Click += new System.EventHandler(this.btnXoaNhanVien_Click);
            // 
            // btnThemNhanVien
            // 
            this.btnThemNhanVien.Location = new System.Drawing.Point(8, 13);
            this.btnThemNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThemNhanVien.Name = "btnThemNhanVien";
            this.btnThemNhanVien.Size = new System.Drawing.Size(100, 28);
            this.btnThemNhanVien.TabIndex = 0;
            this.btnThemNhanVien.Text = "Thêm";
            this.btnThemNhanVien.UseVisualStyleBackColor = true;
            this.btnThemNhanVien.Click += new System.EventHandler(this.btnThemNhanVien_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tbContent);
            this.panel5.Controls.Add(this.btnTimKiem);
            this.panel5.Controls.Add(this.btnKhoiPhuc);
            this.panel5.Controls.Add(this.btnSuaNhanVien);
            this.panel5.Controls.Add(this.btnXoaNhanVien);
            this.panel5.Controls.Add(this.btnThemNhanVien);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1045, 55);
            this.panel5.TabIndex = 0;
            // 
            // btnSuaNhanVien
            // 
            this.btnSuaNhanVien.Location = new System.Drawing.Point(266, 13);
            this.btnSuaNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSuaNhanVien.Name = "btnSuaNhanVien";
            this.btnSuaNhanVien.Size = new System.Drawing.Size(100, 28);
            this.btnSuaNhanVien.TabIndex = 2;
            this.btnSuaNhanVien.Text = "Sửa";
            this.btnSuaNhanVien.UseVisualStyleBackColor = true;
            this.btnSuaNhanVien.Click += new System.EventHandler(this.btnSuaNhanVien_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtgNhanVien);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(463, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1045, 688);
            this.panel3.TabIndex = 1;
            // 
            // dtgNhanVien
            // 
            this.dtgNhanVien.AllowUserToAddRows = false;
            this.dtgNhanVien.AllowUserToDeleteRows = false;
            this.dtgNhanVien.AllowUserToResizeRows = false;
            this.dtgNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgNhanVien.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgNhanVien.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgNhanVien.Location = new System.Drawing.Point(0, 55);
            this.dtgNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtgNhanVien.MultiSelect = false;
            this.dtgNhanVien.Name = "dtgNhanVien";
            this.dtgNhanVien.ReadOnly = true;
            this.dtgNhanVien.RowHeadersVisible = false;
            this.dtgNhanVien.RowHeadersWidth = 51;
            this.dtgNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgNhanVien.Size = new System.Drawing.Size(1045, 633);
            this.dtgNhanVien.TabIndex = 4;
            this.dtgNhanVien.SelectionChanged += new System.EventHandler(this.dtgNhanVien_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1508, 688);
            this.panel1.TabIndex = 1;
            // 
            // NhanVienMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1508, 688);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NhanVienMainForm";
            this.Text = "Quản Lý Nhân Viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NhanVienMainForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhomNhanVien)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNhanVien)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSuaNhom;
        private System.Windows.Forms.Button btnThemNhom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnXoaNhom;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnKhoiPhuc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXoaNhanVien;
        private System.Windows.Forms.Button btnThemNhanVien;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSuaNhanVien;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgNhomNhanVien;
        private System.Windows.Forms.DataGridView dtgNhanVien;
    }
}
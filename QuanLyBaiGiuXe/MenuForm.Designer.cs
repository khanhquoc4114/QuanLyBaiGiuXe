using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    partial class MenuForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.btnThe = new System.Windows.Forms.Button();
            this.btnVeThang = new System.Windows.Forms.Button();
            this.btnVeLuot = new System.Windows.Forms.Button();
            this.btnTraCuuXeVaoRa = new System.Windows.Forms.Button();
            this.btnThongKeTheoKhoangThoiGian = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnThongKeTheoMayTinh = new System.Windows.Forms.Button();
            this.btnHeThong = new System.Windows.Forms.Button();
            this.btnNhatKyDieuChinhGiaVe = new System.Windows.Forms.Button();
            this.btnThongKeTheoNhanVien = new System.Windows.Forms.Button();
            this.btnNhatKyDangNhap = new System.Windows.Forms.Button();
            this.btnNhatKyXuLyVeThang = new System.Windows.Forms.Button();
            this.btnThongKeChiTiet = new System.Windows.Forms.Button();
            this.btnMainForm = new System.Windows.Forms.Button();
            this.btnCauHinhTinhTien = new System.Windows.Forms.Button();
            this.btnNhatKyVeLuot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbXinChao = new System.Windows.Forms.Label();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnThe
            // 
            this.btnThe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThe.Image = ((System.Drawing.Image)(resources.GetObject("btnThe.Image")));
            this.btnThe.Location = new System.Drawing.Point(4, 5);
            this.btnThe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThe.Name = "btnThe";
            this.btnThe.Size = new System.Drawing.Size(290, 168);
            this.btnThe.TabIndex = 0;
            this.btnThe.Text = "Thẻ";
            this.btnThe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tip.SetToolTip(this.btnThe, "Xem danh sách thẻ, đăng ký thẻ mới, cập nhật loại thẻ, xóa thẻ.");
            this.btnThe.UseVisualStyleBackColor = true;
            this.btnThe.Click += new System.EventHandler(this.btnThe_Click);
            // 
            // btnVeThang
            // 
            this.btnVeThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVeThang.Image = ((System.Drawing.Image)(resources.GetObject("btnVeThang.Image")));
            this.btnVeThang.Location = new System.Drawing.Point(600, 5);
            this.btnVeThang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVeThang.Name = "btnVeThang";
            this.btnVeThang.Size = new System.Drawing.Size(290, 168);
            this.btnVeThang.TabIndex = 0;
            this.btnVeThang.Text = "Vé tháng";
            this.btnVeThang.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVeThang.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tip.SetToolTip(this.btnVeThang, "Thêm, xóa, sửa vé tháng, gia hạn vé tháng, tạo nhóm vé tháng.");
            this.btnVeThang.UseVisualStyleBackColor = true;
            this.btnVeThang.Click += new System.EventHandler(this.btnVeThang_Click);
            // 
            // btnVeLuot
            // 
            this.btnVeLuot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVeLuot.Image = ((System.Drawing.Image)(resources.GetObject("btnVeLuot.Image")));
            this.btnVeLuot.Location = new System.Drawing.Point(302, 5);
            this.btnVeLuot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVeLuot.Name = "btnVeLuot";
            this.btnVeLuot.Size = new System.Drawing.Size(290, 168);
            this.btnVeLuot.TabIndex = 0;
            this.btnVeLuot.Text = "Vé lượt";
            this.btnVeLuot.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVeLuot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tip.SetToolTip(this.btnVeLuot, "Xem danh sách vé lượt, xuất ra file excel.");
            this.btnVeLuot.UseVisualStyleBackColor = true;
            this.btnVeLuot.Click += new System.EventHandler(this.btnVeLuot_Click);
            // 
            // btnTraCuuXeVaoRa
            // 
            this.btnTraCuuXeVaoRa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTraCuuXeVaoRa.Image = ((System.Drawing.Image)(resources.GetObject("btnTraCuuXeVaoRa.Image")));
            this.btnTraCuuXeVaoRa.Location = new System.Drawing.Point(600, 539);
            this.btnTraCuuXeVaoRa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTraCuuXeVaoRa.Name = "btnTraCuuXeVaoRa";
            this.btnTraCuuXeVaoRa.Size = new System.Drawing.Size(290, 170);
            this.btnTraCuuXeVaoRa.TabIndex = 0;
            this.btnTraCuuXeVaoRa.Text = "Tra cứu xe vào, ra";
            this.btnTraCuuXeVaoRa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTraCuuXeVaoRa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTraCuuXeVaoRa.UseVisualStyleBackColor = true;
            this.btnTraCuuXeVaoRa.Click += new System.EventHandler(this.btnTraCuuXeVaoRa_Click);
            // 
            // btnThongKeTheoKhoangThoiGian
            // 
            this.btnThongKeTheoKhoangThoiGian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKeTheoKhoangThoiGian.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKeTheoKhoangThoiGian.Image")));
            this.btnThongKeTheoKhoangThoiGian.Location = new System.Drawing.Point(302, 361);
            this.btnThongKeTheoKhoangThoiGian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThongKeTheoKhoangThoiGian.Name = "btnThongKeTheoKhoangThoiGian";
            this.btnThongKeTheoKhoangThoiGian.Size = new System.Drawing.Size(290, 168);
            this.btnThongKeTheoKhoangThoiGian.TabIndex = 0;
            this.btnThongKeTheoKhoangThoiGian.Text = "Thống kê theo khoảng thời gian";
            this.btnThongKeTheoKhoangThoiGian.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThongKeTheoKhoangThoiGian.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThongKeTheoKhoangThoiGian.UseVisualStyleBackColor = true;
            this.btnThongKeTheoKhoangThoiGian.Click += new System.EventHandler(this.btnThongKeTheoKhoangThoiGian_Click);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("btnNhanVien.Image")));
            this.btnNhanVien.Location = new System.Drawing.Point(898, 539);
            this.btnNhanVien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(290, 170);
            this.btnNhanVien.TabIndex = 0;
            this.btnNhanVien.Text = "Nhân viên";
            this.btnNhanVien.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNhanVien.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnThongKeTheoMayTinh
            // 
            this.btnThongKeTheoMayTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKeTheoMayTinh.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKeTheoMayTinh.Image")));
            this.btnThongKeTheoMayTinh.Location = new System.Drawing.Point(600, 361);
            this.btnThongKeTheoMayTinh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThongKeTheoMayTinh.Name = "btnThongKeTheoMayTinh";
            this.btnThongKeTheoMayTinh.Size = new System.Drawing.Size(290, 168);
            this.btnThongKeTheoMayTinh.TabIndex = 0;
            this.btnThongKeTheoMayTinh.Text = "Thống kê theo máy tính";
            this.btnThongKeTheoMayTinh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThongKeTheoMayTinh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThongKeTheoMayTinh.UseVisualStyleBackColor = true;
            this.btnThongKeTheoMayTinh.Click += new System.EventHandler(this.btnThongKeTheoMayTinh_Click);
            // 
            // btnHeThong
            // 
            this.btnHeThong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHeThong.Image = ((System.Drawing.Image)(resources.GetObject("btnHeThong.Image")));
            this.btnHeThong.Location = new System.Drawing.Point(302, 539);
            this.btnHeThong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHeThong.Name = "btnHeThong";
            this.btnHeThong.Size = new System.Drawing.Size(290, 170);
            this.btnHeThong.TabIndex = 0;
            this.btnHeThong.Text = "Hệ thống";
            this.btnHeThong.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHeThong.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHeThong.UseVisualStyleBackColor = true;
            this.btnHeThong.Click += new System.EventHandler(this.btnHeThong_Click);
            // 
            // btnNhatKyDieuChinhGiaVe
            // 
            this.btnNhatKyDieuChinhGiaVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhatKyDieuChinhGiaVe.Image = ((System.Drawing.Image)(resources.GetObject("btnNhatKyDieuChinhGiaVe.Image")));
            this.btnNhatKyDieuChinhGiaVe.Location = new System.Drawing.Point(898, 183);
            this.btnNhatKyDieuChinhGiaVe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNhatKyDieuChinhGiaVe.Name = "btnNhatKyDieuChinhGiaVe";
            this.btnNhatKyDieuChinhGiaVe.Size = new System.Drawing.Size(290, 168);
            this.btnNhatKyDieuChinhGiaVe.TabIndex = 0;
            this.btnNhatKyDieuChinhGiaVe.Text = "Nhật ký điều chỉnh giá vé";
            this.btnNhatKyDieuChinhGiaVe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNhatKyDieuChinhGiaVe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhatKyDieuChinhGiaVe.UseVisualStyleBackColor = true;
            this.btnNhatKyDieuChinhGiaVe.Click += new System.EventHandler(this.btnNhatKyDieuChinhGiaVe_Click);
            // 
            // btnThongKeTheoNhanVien
            // 
            this.btnThongKeTheoNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKeTheoNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKeTheoNhanVien.Image")));
            this.btnThongKeTheoNhanVien.Location = new System.Drawing.Point(898, 361);
            this.btnThongKeTheoNhanVien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThongKeTheoNhanVien.Name = "btnThongKeTheoNhanVien";
            this.btnThongKeTheoNhanVien.Size = new System.Drawing.Size(290, 168);
            this.btnThongKeTheoNhanVien.TabIndex = 0;
            this.btnThongKeTheoNhanVien.Text = "Thống kê theo nhân viên";
            this.btnThongKeTheoNhanVien.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThongKeTheoNhanVien.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThongKeTheoNhanVien.UseVisualStyleBackColor = true;
            this.btnThongKeTheoNhanVien.Click += new System.EventHandler(this.btnThongKeTheoNhanVien_Click);
            // 
            // btnNhatKyDangNhap
            // 
            this.btnNhatKyDangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhatKyDangNhap.Image = ((System.Drawing.Image)(resources.GetObject("btnNhatKyDangNhap.Image")));
            this.btnNhatKyDangNhap.Location = new System.Drawing.Point(302, 183);
            this.btnNhatKyDangNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNhatKyDangNhap.Name = "btnNhatKyDangNhap";
            this.btnNhatKyDangNhap.Size = new System.Drawing.Size(290, 168);
            this.btnNhatKyDangNhap.TabIndex = 0;
            this.btnNhatKyDangNhap.Text = "Nhật ký đăng nhập";
            this.btnNhatKyDangNhap.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNhatKyDangNhap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhatKyDangNhap.UseVisualStyleBackColor = true;
            this.btnNhatKyDangNhap.Click += new System.EventHandler(this.btnNhatKyDangNhap_Click);
            // 
            // btnNhatKyXuLyVeThang
            // 
            this.btnNhatKyXuLyVeThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhatKyXuLyVeThang.Image = ((System.Drawing.Image)(resources.GetObject("btnNhatKyXuLyVeThang.Image")));
            this.btnNhatKyXuLyVeThang.Location = new System.Drawing.Point(600, 183);
            this.btnNhatKyXuLyVeThang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNhatKyXuLyVeThang.Name = "btnNhatKyXuLyVeThang";
            this.btnNhatKyXuLyVeThang.Size = new System.Drawing.Size(290, 168);
            this.btnNhatKyXuLyVeThang.TabIndex = 0;
            this.btnNhatKyXuLyVeThang.Text = "Nhật ký xử lý vé tháng";
            this.btnNhatKyXuLyVeThang.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNhatKyXuLyVeThang.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhatKyXuLyVeThang.UseVisualStyleBackColor = true;
            this.btnNhatKyXuLyVeThang.Click += new System.EventHandler(this.btnNhatKyXuLyVeThang_Click);
            // 
            // btnThongKeChiTiet
            // 
            this.btnThongKeChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKeChiTiet.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKeChiTiet.Image")));
            this.btnThongKeChiTiet.Location = new System.Drawing.Point(4, 361);
            this.btnThongKeChiTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThongKeChiTiet.Name = "btnThongKeChiTiet";
            this.btnThongKeChiTiet.Size = new System.Drawing.Size(290, 168);
            this.btnThongKeChiTiet.TabIndex = 0;
            this.btnThongKeChiTiet.Text = "Thống kê chi tiết";
            this.btnThongKeChiTiet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThongKeChiTiet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThongKeChiTiet.UseVisualStyleBackColor = true;
            this.btnThongKeChiTiet.Click += new System.EventHandler(this.btnThongKeChiTiet_Click);
            // 
            // btnMainForm
            // 
            this.btnMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMainForm.Image = ((System.Drawing.Image)(resources.GetObject("btnMainForm.Image")));
            this.btnMainForm.Location = new System.Drawing.Point(898, 5);
            this.btnMainForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMainForm.Name = "btnMainForm";
            this.btnMainForm.Size = new System.Drawing.Size(290, 168);
            this.btnMainForm.TabIndex = 0;
            this.btnMainForm.Text = "Form chính";
            this.btnMainForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMainForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMainForm.UseVisualStyleBackColor = true;
            this.btnMainForm.Click += new System.EventHandler(this.btnMainForm_Click);
            // 
            // btnCauHinhTinhTien
            // 
            this.btnCauHinhTinhTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCauHinhTinhTien.Image = ((System.Drawing.Image)(resources.GetObject("btnCauHinhTinhTien.Image")));
            this.btnCauHinhTinhTien.Location = new System.Drawing.Point(4, 539);
            this.btnCauHinhTinhTien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCauHinhTinhTien.Name = "btnCauHinhTinhTien";
            this.btnCauHinhTinhTien.Size = new System.Drawing.Size(290, 170);
            this.btnCauHinhTinhTien.TabIndex = 0;
            this.btnCauHinhTinhTien.Text = "Cấu hình tính tiền";
            this.btnCauHinhTinhTien.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCauHinhTinhTien.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCauHinhTinhTien.UseVisualStyleBackColor = true;
            this.btnCauHinhTinhTien.Click += new System.EventHandler(this.btnCauHinhTinhTien_Click);
            // 
            // btnNhatKyVeLuot
            // 
            this.btnNhatKyVeLuot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNhatKyVeLuot.Image = ((System.Drawing.Image)(resources.GetObject("btnNhatKyVeLuot.Image")));
            this.btnNhatKyVeLuot.Location = new System.Drawing.Point(4, 183);
            this.btnNhatKyVeLuot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNhatKyVeLuot.Name = "btnNhatKyVeLuot";
            this.btnNhatKyVeLuot.Size = new System.Drawing.Size(290, 168);
            this.btnNhatKyVeLuot.TabIndex = 0;
            this.btnNhatKyVeLuot.Text = "Nhật ký vé lượt";
            this.btnNhatKyVeLuot.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNhatKyVeLuot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhatKyVeLuot.UseVisualStyleBackColor = true;
            this.btnNhatKyVeLuot.Click += new System.EventHandler(this.btnNhatKyVeLuot_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ColumnCount = 2;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panel1.Controls.Add(this.lbXinChao, 0, 0);
            this.panel1.Controls.Add(this.btnDangXuat, 1, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 1;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Size = new System.Drawing.Size(1194, 70);
            this.panel1.TabIndex = 1;
            // 
            // lbXinChao
            // 
            this.lbXinChao.AutoSize = true;
            this.lbXinChao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbXinChao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbXinChao.Location = new System.Drawing.Point(4, 0);
            this.lbXinChao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbXinChao.Name = "lbXinChao";
            this.lbXinChao.Size = new System.Drawing.Size(1124, 68);
            this.lbXinChao.TabIndex = 0;
            this.lbXinChao.Text = "Xin chào,";
            this.lbXinChao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.Red;
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDangXuat.Image = ((System.Drawing.Image)(resources.GetObject("btnDangXuat.Image")));
            this.btnDangXuat.Location = new System.Drawing.Point(1135, 5);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(54, 58);
            this.btnDangXuat.TabIndex = 1;
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.ColumnCount = 4;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.Controls.Add(this.btnMainForm, 4, 0);
            this.panel2.Controls.Add(this.btnNhanVien, 3, 3);
            this.panel2.Controls.Add(this.btnThe, 0, 0);
            this.panel2.Controls.Add(this.btnHeThong, 1, 3);
            this.panel2.Controls.Add(this.btnVeLuot, 1, 0);
            this.panel2.Controls.Add(this.btnVeThang, 2, 0);
            this.panel2.Controls.Add(this.btnNhatKyVeLuot, 0, 1);
            this.panel2.Controls.Add(this.btnNhatKyXuLyVeThang, 2, 1);
            this.panel2.Controls.Add(this.btnTraCuuXeVaoRa, 2, 3);
            this.panel2.Controls.Add(this.btnCauHinhTinhTien, 0, 3);
            this.panel2.Controls.Add(this.btnNhatKyDieuChinhGiaVe, 3, 1);
            this.panel2.Controls.Add(this.btnNhatKyDangNhap, 1, 1);
            this.panel2.Controls.Add(this.btnThongKeChiTiet, 0, 2);
            this.panel2.Controls.Add(this.btnThongKeTheoNhanVien, 3, 2);
            this.panel2.Controls.Add(this.btnThongKeTheoKhoangThoiGian, 1, 2);
            this.panel2.Controls.Add(this.btnThongKeTheoMayTinh, 2, 2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.RowCount = 4;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel2.Size = new System.Drawing.Size(1194, 716);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.ColumnCount = 1;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.RowCount = 2;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 0;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1194, 786);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MenuForm";
            this.Text = "MENU";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuForm_FormClosing);
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnThe;
        private System.Windows.Forms.Button btnVeThang;
        private System.Windows.Forms.Button btnTraCuuXeVaoRa;
        private System.Windows.Forms.Button btnVeLuot;
        private System.Windows.Forms.Button btnThongKeTheoKhoangThoiGian;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnThongKeTheoMayTinh;
        private System.Windows.Forms.Button btnHeThong;
        private System.Windows.Forms.Button btnNhatKyDieuChinhGiaVe;
        private System.Windows.Forms.Button btnThongKeTheoNhanVien;
        private System.Windows.Forms.Button btnNhatKyDangNhap;
        private System.Windows.Forms.Button btnNhatKyXuLyVeThang;
        private System.Windows.Forms.Button btnThongKeChiTiet;
        private System.Windows.Forms.Button btnMainForm;
        private System.Windows.Forms.Button btnCauHinhTinhTien;
        private System.Windows.Forms.Button btnNhatKyVeLuot;
        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label lbXinChao;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private ToolTip tip;
    }
}
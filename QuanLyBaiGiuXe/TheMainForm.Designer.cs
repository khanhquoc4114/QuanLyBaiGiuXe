namespace QuanLyBaiGiuXe
{
    partial class TheMainForm
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.dtgThe = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbSoThe = new System.Windows.Forms.CheckBox();
            this.tbMaThe = new System.Windows.Forms.TextBox();
            this.btnKhoiPhucThe = new System.Windows.Forms.Button();
            this.btnXoaThe = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgCountThe = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgThe)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCountThe)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 519);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(165, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(768, 519);
            this.panel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dtgThe);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 49);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(768, 470);
            this.panel5.TabIndex = 1;
            // 
            // dtgThe
            // 
            this.dtgThe.AllowUserToAddRows = false;
            this.dtgThe.AllowUserToDeleteRows = false;
            this.dtgThe.AllowUserToResizeRows = false;
            this.dtgThe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgThe.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgThe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgThe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgThe.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgThe.Location = new System.Drawing.Point(0, 0);
            this.dtgThe.Margin = new System.Windows.Forms.Padding(4);
            this.dtgThe.MultiSelect = false;
            this.dtgThe.Name = "dtgThe";
            this.dtgThe.ReadOnly = true;
            this.dtgThe.RowHeadersVisible = false;
            this.dtgThe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgThe.Size = new System.Drawing.Size(768, 470);
            this.dtgThe.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbSoThe);
            this.panel4.Controls.Add(this.tbMaThe);
            this.panel4.Controls.Add(this.btnKhoiPhucThe);
            this.panel4.Controls.Add(this.btnXoaThe);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(768, 49);
            this.panel4.TabIndex = 0;
            // 
            // cbSoThe
            // 
            this.cbSoThe.AutoSize = true;
            this.cbSoThe.Location = new System.Drawing.Point(221, 16);
            this.cbSoThe.Name = "cbSoThe";
            this.cbSoThe.Size = new System.Drawing.Size(15, 14);
            this.cbSoThe.TabIndex = 4;
            this.cbSoThe.UseVisualStyleBackColor = true;
            this.cbSoThe.CheckedChanged += new System.EventHandler(this.cbSoThe_CheckedChanged);
            // 
            // tbMaThe
            // 
            this.tbMaThe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMaThe.Location = new System.Drawing.Point(302, 11);
            this.tbMaThe.Name = "tbMaThe";
            this.tbMaThe.Size = new System.Drawing.Size(100, 23);
            this.tbMaThe.TabIndex = 3;
            this.tbMaThe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMaThe_KeyDown);
            // 
            // btnKhoiPhucThe
            // 
            this.btnKhoiPhucThe.Location = new System.Drawing.Point(114, 9);
            this.btnKhoiPhucThe.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnKhoiPhucThe.Name = "btnKhoiPhucThe";
            this.btnKhoiPhucThe.Size = new System.Drawing.Size(88, 27);
            this.btnKhoiPhucThe.TabIndex = 2;
            this.btnKhoiPhucThe.Text = "Khôi phục";
            this.btnKhoiPhucThe.UseVisualStyleBackColor = true;
            this.btnKhoiPhucThe.Click += new System.EventHandler(this.btnKhoiPhucThe_Click);
            // 
            // btnXoaThe
            // 
            this.btnXoaThe.Location = new System.Drawing.Point(13, 9);
            this.btnXoaThe.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXoaThe.Name = "btnXoaThe";
            this.btnXoaThe.Size = new System.Drawing.Size(88, 27);
            this.btnXoaThe.TabIndex = 1;
            this.btnXoaThe.Text = "Xoá";
            this.btnXoaThe.UseVisualStyleBackColor = true;
            this.btnXoaThe.Click += new System.EventHandler(this.btnXoaThe_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(243, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số thẻ: ";
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(545, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(223, 49);
            this.panel6.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgCountThe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 519);
            this.panel2.TabIndex = 0;
            // 
            // dtgCountThe
            // 
            this.dtgCountThe.AllowUserToAddRows = false;
            this.dtgCountThe.AllowUserToDeleteRows = false;
            this.dtgCountThe.AllowUserToResizeRows = false;
            this.dtgCountThe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgCountThe.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgCountThe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCountThe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgCountThe.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgCountThe.Location = new System.Drawing.Point(0, 0);
            this.dtgCountThe.Margin = new System.Windows.Forms.Padding(4);
            this.dtgCountThe.MultiSelect = false;
            this.dtgCountThe.Name = "dtgCountThe";
            this.dtgCountThe.ReadOnly = true;
            this.dtgCountThe.RowHeadersVisible = false;
            this.dtgCountThe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgCountThe.Size = new System.Drawing.Size(165, 519);
            this.dtgCountThe.TabIndex = 3;
            // 
            // TheMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TheMainForm";
            this.Text = "TheMainForm";
            this.Load += new System.EventHandler(this.TheMainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgThe)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCountThe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnKhoiPhucThe;
        private System.Windows.Forms.Button btnXoaThe;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbMaThe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbSoThe;
        private System.Windows.Forms.DataGridView dtgCountThe;
        private System.Windows.Forms.DataGridView dtgThe;
    }
}
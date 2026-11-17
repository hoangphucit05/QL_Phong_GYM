namespace He_Thong_GYM.Forms;

partial class FrmNhanVien
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelTieuDe = new Panel();
        lblMoTa = new Label();
        lblTieuDe = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        lblNgaySinh = new Label();
        dtpNgaySinh = new DateTimePicker();
        lblGioiTinh = new Label();
        cboGioiTinh = new ComboBox();
        lblSDT = new Label();
        txtSDT = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblDiaChi = new Label();
        txtDiaChi = new TextBox();
        lblChucVu = new Label();
        txtChucVu = new TextBox();
        lblLuong = new Label();
        nudLuong = new NumericUpDown();
        lblTrangThaiForm = new Label();
        cboTrangThai = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnLuu = new Button();
        btnNghiViec = new Button();
        panelTimKiem = new Panel();
        btnTimKiem = new Button();
        txtTimKiem = new TextBox();
        lblTimKiem = new Label();
        dgvNhanVien = new DataGridView();
        panelTrangThai = new Panel();
        lblTrangThaiTimKiem = new Label();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudLuong).BeginInit();
        flowThaoTac.SuspendLayout();
        panelTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
        panelTrangThai.SuspendLayout();
        SuspendLayout();
        // 
        // panelTieuDe
        // 
        panelTieuDe.BackColor = Color.White;
        panelTieuDe.Controls.Add(lblMoTa);
        panelTieuDe.Controls.Add(lblTieuDe);
        panelTieuDe.Dock = DockStyle.Top;
        panelTieuDe.Location = new Point(0, 0);
        panelTieuDe.Name = "panelTieuDe";
        panelTieuDe.Padding = new Padding(24, 16, 24, 16);
        panelTieuDe.Size = new Size(1200, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(339, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Quản lý hồ sơ nhân viên, chức vụ, lương và trạng thái làm việc.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(236, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ NHÂN VIÊN";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThongTin.ForeColor = Color.FromArgb(30, 41, 59);
        groupThongTin.Location = new Point(24, 96);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(16, 12, 16, 12);
        groupThongTin.Size = new Size(1152, 195);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin nhân viên";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblHoTen, 0, 0);
        tableThongTin.Controls.Add(txtHoTen, 1, 0);
        tableThongTin.Controls.Add(lblNgaySinh, 2, 0);
        tableThongTin.Controls.Add(dtpNgaySinh, 3, 0);
        tableThongTin.Controls.Add(lblGioiTinh, 0, 1);
        tableThongTin.Controls.Add(cboGioiTinh, 1, 1);
        tableThongTin.Controls.Add(lblSDT, 2, 1);
        tableThongTin.Controls.Add(txtSDT, 3, 1);
        tableThongTin.Controls.Add(lblEmail, 0, 2);
        tableThongTin.Controls.Add(txtEmail, 1, 2);
        tableThongTin.Controls.Add(lblDiaChi, 2, 2);
        tableThongTin.Controls.Add(txtDiaChi, 3, 2);
        tableThongTin.Controls.Add(lblChucVu, 0, 3);
        tableThongTin.Controls.Add(txtChucVu, 1, 3);
        tableThongTin.Controls.Add(lblLuong, 2, 3);
        tableThongTin.Controls.Add(nudLuong, 3, 3);
        tableThongTin.Controls.Add(lblTrangThaiForm, 0, 4);
        tableThongTin.Controls.Add(cboTrangThai, 1, 4);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 30);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 5;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableThongTin.Size = new Size(1120, 153);
        tableThongTin.TabIndex = 0;
        // 
        // lblHoTen
        // 
        lblHoTen.AutoSize = true;
        lblHoTen.Dock = DockStyle.Fill;
        lblHoTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHoTen.Location = new Point(3, 0);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(104, 30);
        lblHoTen.TabIndex = 0;
        lblHoTen.Text = "Họ và tên:";
        lblHoTen.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtHoTen
        // 
        txtHoTen.Dock = DockStyle.Fill;
        txtHoTen.Location = new Point(113, 3);
        txtHoTen.Margin = new Padding(3, 3, 12, 3);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(435, 23);
        txtHoTen.TabIndex = 1;
        // 
        // lblNgaySinh
        // 
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Dock = DockStyle.Fill;
        lblNgaySinh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNgaySinh.Location = new Point(563, 0);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Size = new Size(104, 30);
        lblNgaySinh.TabIndex = 2;
        lblNgaySinh.Text = "Ngày sinh:";
        lblNgaySinh.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpNgaySinh
        // 
        dtpNgaySinh.Dock = DockStyle.Fill;
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        dtpNgaySinh.Location = new Point(673, 3);
        dtpNgaySinh.Margin = new Padding(3, 3, 12, 3);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.Size = new Size(435, 23);
        dtpNgaySinh.TabIndex = 3;
        // 
        // lblGioiTinh
        // 
        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Dock = DockStyle.Fill;
        lblGioiTinh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGioiTinh.Location = new Point(3, 30);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Size = new Size(104, 30);
        lblGioiTinh.TabIndex = 4;
        lblGioiTinh.Text = "Giới tính:";
        lblGioiTinh.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboGioiTinh
        // 
        cboGioiTinh.Dock = DockStyle.Fill;
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.FormattingEnabled = true;
        cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        cboGioiTinh.Location = new Point(113, 33);
        cboGioiTinh.Margin = new Padding(3, 3, 12, 3);
        cboGioiTinh.Name = "cboGioiTinh";
        cboGioiTinh.Size = new Size(435, 23);
        cboGioiTinh.TabIndex = 5;
        // 
        // lblSDT
        // 
        lblSDT.AutoSize = true;
        lblSDT.Dock = DockStyle.Fill;
        lblSDT.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSDT.Location = new Point(563, 30);
        lblSDT.Name = "lblSDT";
        lblSDT.Size = new Size(104, 30);
        lblSDT.TabIndex = 6;
        lblSDT.Text = "Số điện thoại:";
        lblSDT.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtSDT
        // 
        txtSDT.Dock = DockStyle.Fill;
        txtSDT.Location = new Point(673, 33);
        txtSDT.Margin = new Padding(3, 3, 12, 3);
        txtSDT.Name = "txtSDT";
        txtSDT.Size = new Size(435, 23);
        txtSDT.TabIndex = 7;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Dock = DockStyle.Fill;
        lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblEmail.Location = new Point(3, 60);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(104, 30);
        lblEmail.TabIndex = 8;
        lblEmail.Text = "Email:";
        lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtEmail
        // 
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Location = new Point(113, 63);
        txtEmail.Margin = new Padding(3, 3, 12, 3);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(435, 23);
        txtEmail.TabIndex = 9;
        // 
        // lblDiaChi
        // 
        lblDiaChi.AutoSize = true;
        lblDiaChi.Dock = DockStyle.Fill;
        lblDiaChi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDiaChi.Location = new Point(563, 60);
        lblDiaChi.Name = "lblDiaChi";
        lblDiaChi.Size = new Size(104, 30);
        lblDiaChi.TabIndex = 10;
        lblDiaChi.Text = "Địa chỉ:";
        lblDiaChi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDiaChi
        // 
        txtDiaChi.Dock = DockStyle.Fill;
        txtDiaChi.Location = new Point(673, 63);
        txtDiaChi.Margin = new Padding(3, 3, 12, 3);
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Size = new Size(435, 23);
        txtDiaChi.TabIndex = 11;
        // 
        // lblChucVu
        // 
        lblChucVu.AutoSize = true;
        lblChucVu.Dock = DockStyle.Fill;
        lblChucVu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblChucVu.Location = new Point(3, 90);
        lblChucVu.Name = "lblChucVu";
        lblChucVu.Size = new Size(104, 30);
        lblChucVu.TabIndex = 12;
        lblChucVu.Text = "Chức vụ:";
        lblChucVu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtChucVu
        // 
        txtChucVu.Dock = DockStyle.Fill;
        txtChucVu.Location = new Point(113, 93);
        txtChucVu.Margin = new Padding(3, 3, 12, 3);
        txtChucVu.Name = "txtChucVu";
        txtChucVu.Size = new Size(435, 23);
        txtChucVu.TabIndex = 13;
        // 
        // lblLuong
        // 
        lblLuong.AutoSize = true;
        lblLuong.Dock = DockStyle.Fill;
        lblLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLuong.Location = new Point(563, 90);
        lblLuong.Name = "lblLuong";
        lblLuong.Size = new Size(104, 30);
        lblLuong.TabIndex = 14;
        lblLuong.Text = "Lương (VNĐ):";
        lblLuong.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudLuong
        // 
        nudLuong.Dock = DockStyle.Fill;
        nudLuong.Location = new Point(673, 93);
        nudLuong.Margin = new Padding(3, 3, 12, 3);
        nudLuong.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudLuong.Name = "nudLuong";
        nudLuong.Size = new Size(435, 23);
        nudLuong.TabIndex = 15;
        nudLuong.ThousandsSeparator = true;
        // 
        // lblTrangThaiForm
        // 
        lblTrangThaiForm.AutoSize = true;
        lblTrangThaiForm.Dock = DockStyle.Fill;
        lblTrangThaiForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThaiForm.Location = new Point(3, 120);
        lblTrangThaiForm.Name = "lblTrangThaiForm";
        lblTrangThaiForm.Size = new Size(104, 33);
        lblTrangThaiForm.TabIndex = 16;
        lblTrangThaiForm.Text = "Trạng thái:";
        lblTrangThaiForm.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "Đang làm", "Nghỉ việc" });
        cboTrangThai.Location = new Point(113, 123);
        cboTrangThai.Margin = new Padding(3, 3, 12, 3);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(435, 23);
        cboTrangThai.TabIndex = 17;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnLuu);
        flowThaoTac.Controls.Add(btnNghiViec);
        flowThaoTac.Location = new Point(24, 300);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1152, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(37, 99, 235);
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(0, 0);
        btnThem.Margin = new Padding(0, 0, 8, 0);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(128, 36);
        btnThem.TabIndex = 0;
        btnThem.Text = "Thêm mới";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(5, 150, 105);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(136, 0);
        btnLuu.Margin = new Padding(0, 0, 8, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(128, 36);
        btnLuu.TabIndex = 1;
        btnLuu.Text = "Lưu thay đổi";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnNghiViec
        // 
        btnNghiViec.BackColor = Color.FromArgb(185, 28, 28);
        btnNghiViec.Cursor = Cursors.Hand;
        btnNghiViec.FlatAppearance.BorderSize = 0;
        btnNghiViec.FlatStyle = FlatStyle.Flat;
        btnNghiViec.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNghiViec.ForeColor = Color.White;
        btnNghiViec.Location = new Point(272, 0);
        btnNghiViec.Margin = new Padding(0, 0, 8, 0);
        btnNghiViec.Name = "btnNghiViec";
        btnNghiViec.Size = new Size(128, 36);
        btnNghiViec.TabIndex = 2;
        btnNghiViec.Text = "Nghỉ việc";
        btnNghiViec.UseVisualStyleBackColor = false;
        btnNghiViec.Click += btnNghiViec_Click;
        // 
        // panelTimKiem
        // 
        panelTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTimKiem.BackColor = Color.White;
        panelTimKiem.Controls.Add(btnTimKiem);
        panelTimKiem.Controls.Add(txtTimKiem);
        panelTimKiem.Controls.Add(lblTimKiem);
        panelTimKiem.Location = new Point(24, 350);
        panelTimKiem.Name = "panelTimKiem";
        panelTimKiem.Padding = new Padding(14, 10, 14, 10);
        panelTimKiem.Size = new Size(1152, 58);
        panelTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(30, 64, 175);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(1020, 13);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(112, 30);
        btnTimKiem.TabIndex = 2;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTimKiem.Location = new Point(140, 16);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập từ khóa tìm kiếm nhân viên...";
        txtTimKiem.Size = new Size(866, 23);
        txtTimKiem.TabIndex = 1;
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.Location = new Point(18, 20);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(116, 15);
        lblTimKiem.TabIndex = 0;
        lblTimKiem.Text = "Tìm theo tên / SĐT:";
        // 
        // dgvNhanVien
        // 
        dgvNhanVien.AllowUserToAddRows = false;
        dgvNhanVien.AllowUserToDeleteRows = false;
        dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvNhanVien.BackgroundColor = Color.White;
        dgvNhanVien.BorderStyle = BorderStyle.None;
        dgvNhanVien.ColumnHeadersHeight = 36;
        dgvNhanVien.Location = new Point(24, 418);
        dgvNhanVien.MultiSelect = false;
        dgvNhanVien.Name = "dgvNhanVien";
        dgvNhanVien.ReadOnly = true;
        dgvNhanVien.RowHeadersVisible = false;
        dgvNhanVien.RowTemplate.Height = 32;
        dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvNhanVien.Size = new Size(1152, 280);
        dgvNhanVien.TabIndex = 4;
        dgvNhanVien.SelectionChanged += dgvNhanVien_SelectionChanged;
        // 
        // panelTrangThai
        // 
        panelTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelTrangThai.BackColor = Color.White;
        panelTrangThai.Controls.Add(lblTrangThaiTimKiem);
        panelTrangThai.Location = new Point(24, 708);
        panelTrangThai.Name = "panelTrangThai";
        panelTrangThai.Padding = new Padding(10, 8, 10, 8);
        panelTrangThai.Size = new Size(1152, 32);
        panelTrangThai.TabIndex = 5;
        // 
        // lblTrangThaiTimKiem
        // 
        lblTrangThaiTimKiem.AutoSize = true;
        lblTrangThaiTimKiem.Dock = DockStyle.Fill;
        lblTrangThaiTimKiem.ForeColor = Color.FromArgb(100, 116, 139);
        lblTrangThaiTimKiem.Location = new Point(10, 8);
        lblTrangThaiTimKiem.Name = "lblTrangThaiTimKiem";
        lblTrangThaiTimKiem.Size = new Size(151, 15);
        lblTrangThaiTimKiem.TabIndex = 0;
        lblTrangThaiTimKiem.Text = "Hiển thị toàn bộ nhân viên.";
        // 
        // FrmNhanVien
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvNhanVien);
        Controls.Add(panelTimKiem);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(850, 480);
        Name = "FrmNhanVien";
        Text = "Quản lý nhân viên";
        Load += FrmNhanVien_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudLuong).EndInit();
        flowThaoTac.ResumeLayout(false);
        panelTimKiem.ResumeLayout(false);
        panelTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
        panelTrangThai.ResumeLayout(false);
        panelTrangThai.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private Label lblNgaySinh;
    private DateTimePicker dtpNgaySinh;
    private Label lblGioiTinh;
    private ComboBox cboGioiTinh;
    private Label lblSDT;
    private TextBox txtSDT;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblDiaChi;
    private TextBox txtDiaChi;
    private Label lblChucVu;
    private TextBox txtChucVu;
    private Label lblLuong;
    private NumericUpDown nudLuong;
    private Label lblTrangThaiForm;
    private ComboBox cboTrangThai;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnLuu;
    private Button btnNghiViec;
    private Panel panelTimKiem;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private DataGridView dgvNhanVien;
    private Panel panelTrangThai;
    private Label lblTrangThaiTimKiem;
}

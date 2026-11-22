namespace He_Thong_GYM.Forms;

partial class FrmThanhVien
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
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
        groupBoxThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        lblNgaySinh = new Label();
        dtpNgaySinh = new DateTimePicker();
        lblGioiTinh = new Label();
        cboGioiTinh = new ComboBox();
        lblSoDienThoai = new Label();
        txtSoDienThoai = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblDiaChi = new Label();
        txtDiaChi = new TextBox();
        lblLoaiThanhVien = new Label();
        cboLoaiThanhVien = new ComboBox();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnLuu = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        panelTimKiem = new Panel();
        btnTimKiem = new Button();
        txtTimKiem = new TextBox();
        lblTimKiem = new Label();
        dgvThanhVien = new DataGridView();
        colMaTV = new DataGridViewTextBoxColumn();
        colHoTen = new DataGridViewTextBoxColumn();
        colSDT = new DataGridViewTextBoxColumn();
        colEmail = new DataGridViewTextBoxColumn();
        colLoai = new DataGridViewTextBoxColumn();
        colNgayDangKy = new DataGridViewTextBoxColumn();
        colTrangThai = new DataGridViewTextBoxColumn();
        panelTrangThai = new Panel();
        lblTrangThaiTimKiem = new Label();
        panelTieuDe.SuspendLayout();
        groupBoxThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        flowThaoTac.SuspendLayout();
        panelTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvThanhVien).BeginInit();
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
        panelTieuDe.Size = new Size(1200, 82);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(299, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Thêm, cập nhật, tìm kiếm và theo dõi hồ sơ thành viên.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(249, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ THÀNH VIÊN";
        // 
        // groupBoxThongTin
        // 
        groupBoxThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBoxThongTin.BackColor = Color.White;
        groupBoxThongTin.Controls.Add(tableThongTin);
        groupBoxThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupBoxThongTin.ForeColor = Color.FromArgb(30, 41, 59);
        groupBoxThongTin.Location = new Point(24, 100);
        groupBoxThongTin.Name = "groupBoxThongTin";
        groupBoxThongTin.Padding = new Padding(16);
        groupBoxThongTin.Size = new Size(1152, 190);
        groupBoxThongTin.TabIndex = 1;
        groupBoxThongTin.TabStop = false;
        groupBoxThongTin.Text = "Thông tin thành viên";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblHoTen, 0, 0);
        tableThongTin.Controls.Add(txtHoTen, 1, 0);
        tableThongTin.Controls.Add(lblNgaySinh, 2, 0);
        tableThongTin.Controls.Add(dtpNgaySinh, 3, 0);
        tableThongTin.Controls.Add(lblGioiTinh, 0, 1);
        tableThongTin.Controls.Add(cboGioiTinh, 1, 1);
        tableThongTin.Controls.Add(lblSoDienThoai, 2, 1);
        tableThongTin.Controls.Add(txtSoDienThoai, 3, 1);
        tableThongTin.Controls.Add(lblEmail, 0, 2);
        tableThongTin.Controls.Add(txtEmail, 1, 2);
        tableThongTin.Controls.Add(lblDiaChi, 2, 2);
        tableThongTin.Controls.Add(txtDiaChi, 3, 2);
        tableThongTin.Controls.Add(lblLoaiThanhVien, 0, 3);
        tableThongTin.Controls.Add(cboLoaiThanhVien, 1, 3);
        tableThongTin.Controls.Add(lblTrangThai, 2, 3);
        tableThongTin.Controls.Add(cboTrangThai, 3, 3);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 34);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 4;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.Size = new Size(1120, 140);
        tableThongTin.TabIndex = 0;
        // 
        // lblHoTen
        // 
        lblHoTen.Location = new Point(3, 0);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(100, 23);
        lblHoTen.TabIndex = 0;
        // 
        // txtHoTen
        // 
        txtHoTen.Location = new Point(113, 3);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(100, 23);
        txtHoTen.TabIndex = 1;
        // 
        // lblNgaySinh
        // 
        lblNgaySinh.Location = new Point(558, 0);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Size = new Size(100, 23);
        lblNgaySinh.TabIndex = 2;
        // 
        // dtpNgaySinh
        // 
        dtpNgaySinh.Dock = DockStyle.Fill;
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        dtpNgaySinh.Location = new Point(678, 3);
        dtpNgaySinh.MaxDate = new DateTime(2026, 11, 16, 0, 0, 0, 0);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.Size = new Size(439, 23);
        dtpNgaySinh.TabIndex = 3;
        dtpNgaySinh.Value = new DateTime(2026, 11, 16, 0, 0, 0, 0);
        // 
        // lblGioiTinh
        // 
        lblGioiTinh.Location = new Point(3, 35);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Size = new Size(100, 23);
        lblGioiTinh.TabIndex = 4;
        // 
        // cboGioiTinh
        // 
        cboGioiTinh.Dock = DockStyle.Fill;
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        cboGioiTinh.Location = new Point(113, 38);
        cboGioiTinh.Name = "cboGioiTinh";
        cboGioiTinh.Size = new Size(439, 23);
        cboGioiTinh.TabIndex = 5;
        // 
        // lblSoDienThoai
        // 
        lblSoDienThoai.Location = new Point(558, 35);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Size = new Size(100, 23);
        lblSoDienThoai.TabIndex = 6;
        // 
        // txtSoDienThoai
        // 
        txtSoDienThoai.Location = new Point(678, 38);
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(100, 23);
        txtSoDienThoai.TabIndex = 7;
        // 
        // lblEmail
        // 
        lblEmail.Location = new Point(3, 70);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(100, 23);
        lblEmail.TabIndex = 8;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(113, 73);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(100, 23);
        txtEmail.TabIndex = 9;
        // 
        // lblDiaChi
        // 
        lblDiaChi.Location = new Point(558, 70);
        lblDiaChi.Name = "lblDiaChi";
        lblDiaChi.Size = new Size(100, 23);
        lblDiaChi.TabIndex = 10;
        // 
        // txtDiaChi
        // 
        txtDiaChi.Location = new Point(678, 73);
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Size = new Size(100, 23);
        txtDiaChi.TabIndex = 11;
        // 
        // lblLoaiThanhVien
        // 
        lblLoaiThanhVien.Location = new Point(3, 105);
        lblLoaiThanhVien.Name = "lblLoaiThanhVien";
        lblLoaiThanhVien.Size = new Size(100, 23);
        lblLoaiThanhVien.TabIndex = 12;
        // 
        // cboLoaiThanhVien
        // 
        cboLoaiThanhVien.Dock = DockStyle.Fill;
        cboLoaiThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiThanhVien.Items.AddRange(new object[] { "Chọn loại thành viên" });
        cboLoaiThanhVien.Location = new Point(113, 108);
        cboLoaiThanhVien.Name = "cboLoaiThanhVien";
        cboLoaiThanhVien.Size = new Size(439, 23);
        cboLoaiThanhVien.TabIndex = 13;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Location = new Point(558, 105);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(100, 23);
        lblTrangThai.TabIndex = 14;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.Items.AddRange(new object[] { "Hoạt động", "Tạm khóa", "Ngừng sử dụng" });
        cboTrangThai.Location = new Point(678, 108);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(439, 23);
        cboTrangThai.TabIndex = 15;

        lblHoTen.AutoSize = true;
        lblHoTen.Dock = DockStyle.Fill;
        lblHoTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHoTen.Text = "Họ và tên:";
        lblHoTen.TextAlign = ContentAlignment.MiddleLeft;
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Dock = DockStyle.Fill;
        lblNgaySinh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNgaySinh.Text = "Ngày sinh:";
        lblNgaySinh.TextAlign = ContentAlignment.MiddleLeft;
        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Dock = DockStyle.Fill;
        lblGioiTinh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGioiTinh.Text = "Giới tính:";
        lblGioiTinh.TextAlign = ContentAlignment.MiddleLeft;
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Dock = DockStyle.Fill;
        lblSoDienThoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSoDienThoai.Text = "Số điện thoại:";
        lblSoDienThoai.TextAlign = ContentAlignment.MiddleLeft;
        lblEmail.AutoSize = true;
        lblEmail.Dock = DockStyle.Fill;
        lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblEmail.Text = "Email:";
        lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        lblDiaChi.AutoSize = true;
        lblDiaChi.Dock = DockStyle.Fill;
        lblDiaChi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDiaChi.Text = "Địa chỉ:";
        lblDiaChi.TextAlign = ContentAlignment.MiddleLeft;
        lblLoaiThanhVien.AutoSize = true;
        lblLoaiThanhVien.Dock = DockStyle.Fill;
        lblLoaiThanhVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLoaiThanhVien.Text = "Loại thành viên:";
        lblLoaiThanhVien.TextAlign = ContentAlignment.MiddleLeft;
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        txtHoTen.Dock = DockStyle.Fill;
        txtHoTen.Margin = new Padding(3, 6, 12, 6);
        txtSoDienThoai.Dock = DockStyle.Fill;
        txtSoDienThoai.Margin = new Padding(3, 6, 12, 6);
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Margin = new Padding(3, 6, 12, 6);
        txtDiaChi.Dock = DockStyle.Fill;
        txtDiaChi.Margin = new Padding(3, 6, 12, 6);
        cboGioiTinh.Margin = new Padding(3, 6, 12, 6);
        cboLoaiThanhVien.Margin = new Padding(3, 6, 12, 6);
        cboTrangThai.Margin = new Padding(3, 6, 12, 6);
        dtpNgaySinh.Margin = new Padding(3, 6, 12, 6);
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnSua);
        flowThaoTac.Controls.Add(btnLuu);
        flowThaoTac.Controls.Add(btnXoa);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Location = new Point(24, 300);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1152, 44);
        flowThaoTac.TabIndex = 2;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(3, 3);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(75, 23);
        btnThem.TabIndex = 0;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.Enabled = false;
        btnSua.Location = new Point(84, 3);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(75, 23);
        btnSua.TabIndex = 1;
        btnSua.Click += btnSua_Click;
        // 
        // btnLuu
        // 
        btnLuu.Location = new Point(165, 3);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(75, 23);
        btnLuu.TabIndex = 2;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnXoa
        // 
        btnXoa.Enabled = false;
        btnXoa.Location = new Point(246, 3);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(75, 23);
        btnXoa.TabIndex = 3;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Location = new Point(327, 3);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(75, 23);
        btnLamMoi.TabIndex = 4;
        btnLamMoi.Click += btnLamMoi_Click;

        btnThem.BackColor = Color.FromArgb(37, 99, 235);
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Margin = new Padding(0, 0, 8, 0);
        btnThem.Size = new Size(120, 36);
        btnThem.Text = "Thêm mới";
        btnThem.UseVisualStyleBackColor = false;
        btnSua.BackColor = Color.FromArgb(217, 119, 6);
        btnSua.Cursor = Cursors.Hand;
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Margin = new Padding(0, 0, 8, 0);
        btnSua.Size = new Size(120, 36);
        btnSua.Text = "Chỉnh sửa";
        btnSua.UseVisualStyleBackColor = false;
        btnLuu.BackColor = Color.FromArgb(5, 150, 105);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Margin = new Padding(0, 0, 8, 0);
        btnLuu.Size = new Size(120, 36);
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        btnXoa.BackColor = Color.FromArgb(185, 28, 28);
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.FlatAppearance.BorderSize = 0;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Margin = new Padding(0, 0, 8, 0);
        btnXoa.Size = new Size(120, 36);
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Margin = new Padding(0, 0, 8, 0);
        btnLamMoi.Size = new Size(120, 36);
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        // 
        // panelTimKiem
        // 
        panelTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTimKiem.BackColor = Color.White;
        panelTimKiem.Controls.Add(btnTimKiem);
        panelTimKiem.Controls.Add(txtTimKiem);
        panelTimKiem.Controls.Add(lblTimKiem);
        panelTimKiem.Location = new Point(24, 355);
        panelTimKiem.Name = "panelTimKiem";
        panelTimKiem.Padding = new Padding(14, 10, 14, 10);
        panelTimKiem.Size = new Size(1152, 58);
        panelTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(30, 64, 175);
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(1020, 13);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(112, 30);
        btnTimKiem.TabIndex = 1;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTimKiem.Location = new Point(184, 16);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập từ khóa tìm kiếm...";
        txtTimKiem.Size = new Size(822, 23);
        txtTimKiem.TabIndex = 0;
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.Location = new Point(18, 20);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(156, 15);
        lblTimKiem.TabIndex = 0;
        lblTimKiem.Text = "Tìm tên hoặc số điện thoại:";
        // 
        // dgvThanhVien
        // 
        dgvThanhVien.AllowUserToAddRows = false;
        dgvThanhVien.AllowUserToDeleteRows = false;
        dgvThanhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvThanhVien.AutoGenerateColumns = false;
        dgvThanhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvThanhVien.BackgroundColor = Color.White;
        dgvThanhVien.BorderStyle = BorderStyle.None;
        dgvThanhVien.ColumnHeadersHeight = 38;
        dgvThanhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvThanhVien.Columns.AddRange(new DataGridViewColumn[] { colMaTV, colHoTen, colSDT, colEmail, colLoai, colNgayDangKy, colTrangThai });
        dgvThanhVien.Location = new Point(24, 423);
        dgvThanhVien.MultiSelect = false;
        dgvThanhVien.Name = "dgvThanhVien";
        dgvThanhVien.ReadOnly = true;
        dgvThanhVien.RowHeadersVisible = false;
        dgvThanhVien.RowTemplate.Height = 32;
        dgvThanhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThanhVien.Size = new Size(1152, 276);
        dgvThanhVien.TabIndex = 4;
        dgvThanhVien.SelectionChanged += dgvThanhVien_SelectionChanged;
        // 
        // colMaTV
        // 
        colMaTV.DataPropertyName = "MaThanhVien";
        colMaTV.FillWeight = 90F;
        colMaTV.HeaderText = "Mã thành viên";
        colMaTV.Name = "colMaTV";
        colMaTV.ReadOnly = true;
        // 
        // colHoTen
        // 
        colHoTen.DataPropertyName = "HoTen";
        colHoTen.HeaderText = "Họ và tên";
        colHoTen.Name = "colHoTen";
        colHoTen.ReadOnly = true;
        // 
        // colSDT
        // 
        colSDT.DataPropertyName = "SDT";
        colSDT.HeaderText = "Số điện thoại";
        colSDT.Name = "colSDT";
        colSDT.ReadOnly = true;
        // 
        // colEmail
        // 
        colEmail.DataPropertyName = "Email";
        colEmail.HeaderText = "Email";
        colEmail.Name = "colEmail";
        colEmail.ReadOnly = true;
        // 
        // colLoai
        // 
        colLoai.DataPropertyName = "TenLoai";
        colLoai.HeaderText = "Loại thành viên";
        colLoai.Name = "colLoai";
        colLoai.ReadOnly = true;
        // 
        // colNgayDangKy
        // 
        colNgayDangKy.DataPropertyName = "NgayDangKy";
        colNgayDangKy.HeaderText = "Ngày đăng ký";
        colNgayDangKy.Name = "colNgayDangKy";
        colNgayDangKy.ReadOnly = true;
        // 
        // colTrangThai
        // 
        colTrangThai.DataPropertyName = "TrangThai";
        colTrangThai.HeaderText = "Trạng thái";
        colTrangThai.Name = "colTrangThai";
        colTrangThai.ReadOnly = true;
        // 
        // panelTrangThai
        // 
        panelTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelTrangThai.BackColor = Color.White;
        panelTrangThai.Controls.Add(lblTrangThaiTimKiem);
        panelTrangThai.Location = new Point(24, 709);
        panelTrangThai.Name = "panelTrangThai";
        panelTrangThai.Size = new Size(1152, 30);
        panelTrangThai.TabIndex = 5;
        // 
        // lblTrangThaiTimKiem
        // 
        lblTrangThaiTimKiem.AutoSize = true;
        lblTrangThaiTimKiem.ForeColor = Color.FromArgb(100, 116, 139);
        lblTrangThaiTimKiem.Location = new Point(10, 8);
        lblTrangThaiTimKiem.Name = "lblTrangThaiTimKiem";
        lblTrangThaiTimKiem.Size = new Size(212, 15);
        lblTrangThaiTimKiem.TabIndex = 0;
        lblTrangThaiTimKiem.Text = "Hiển thị toàn bộ danh sách thành viên.";
        // 
        // FrmThanhVien
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvThanhVien);
        Controls.Add(panelTimKiem);
        Controls.Add(flowThaoTac);
        Controls.Add(groupBoxThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        AutoScroll = true;
        MinimumSize = new Size(850, 480);
        Name = "FrmThanhVien";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý thành viên";
        Load += FrmThanhVien_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupBoxThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        flowThaoTac.ResumeLayout(false);
        panelTimKiem.ResumeLayout(false);
        panelTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvThanhVien).EndInit();
        panelTrangThai.ResumeLayout(false);
        panelTrangThai.PerformLayout();
        ResumeLayout(false);
    }


    #endregion

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupBoxThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private Label lblNgaySinh;
    private DateTimePicker dtpNgaySinh;
    private Label lblGioiTinh;
    private ComboBox cboGioiTinh;
    private Label lblSoDienThoai;
    private TextBox txtSoDienThoai;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblDiaChi;
    private TextBox txtDiaChi;
    private Label lblLoaiThanhVien;
    private ComboBox cboLoaiThanhVien;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnSua;
    private Button btnLuu;
    private Button btnXoa;
    private Button btnLamMoi;
    private Panel panelTimKiem;
    private Button btnTimKiem;
    private TextBox txtTimKiem;
    private Label lblTimKiem;
    private DataGridView dgvThanhVien;
    private DataGridViewTextBoxColumn colMaTV;
    private DataGridViewTextBoxColumn colHoTen;
    private DataGridViewTextBoxColumn colSDT;
    private DataGridViewTextBoxColumn colEmail;
    private DataGridViewTextBoxColumn colLoai;
    private DataGridViewTextBoxColumn colNgayDangKy;
    private DataGridViewTextBoxColumn colTrangThai;
    private Panel panelTrangThai;
    private Label lblTrangThaiTimKiem;
}

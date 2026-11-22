namespace He_Thong_GYM.Forms;

partial class FrmVanHanhGoi
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

    private void InitializeComponent()
    {
        panelTieuDe = new Panel();
        lblMoTa = new Label();
        lblTieuDe = new Label();
        groupCheckIn = new GroupBox();
        btnXoaTimThanhVien = new Button();
        txtTimThanhVien = new TextBox();
        lblTimThanhVien = new Label();
        tableCheckIn = new TableLayoutPanel();
        lblTV = new Label();
        cboThanhVien = new ComboBox();
        lblPhong = new Label();
        cboPhong = new ComboBox();
        btnCheckIn = new Button();
        btnCheckOut = new Button();
        btnChuyenPhong = new Button();
        dgvGoi = new DataGridView();
        groupGoi = new GroupBox();
        tableBaoLuu = new TableLayoutPanel();
        lblTu = new Label();
        dtpTuNgay = new DateTimePicker();
        lblDen = new Label();
        dtpDenNgay = new DateTimePicker();
        lblLyDo = new Label();
        txtLyDo = new TextBox();
        btnBaoLuu = new Button();
        btnMoLaiBaoLuu = new Button();
        flowGiaHan = new FlowLayoutPanel();
        lblNgay = new Label();
        nudSoNgay = new NumericUpDown();
        lblDonViNgay = new Label();
        btnGiaHan = new Button();
        panelTrangThai = new Panel();
        lblTrangThai = new Label();
        panelTieuDe.SuspendLayout();
        groupCheckIn.SuspendLayout();
        tableCheckIn.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoi).BeginInit();
        groupGoi.SuspendLayout();
        tableBaoLuu.SuspendLayout();
        flowGiaHan.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoNgay).BeginInit();
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
        panelTieuDe.Size = new Size(1100, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(372, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Kiểm soát lượt vào/ra phòng gym, gia hạn và bảo lưu gói đang dùng.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(371, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "VẬN HÀNH GÓI TẬP VÀ CHECK-IN";
        // 
        // groupCheckIn
        // 
        groupCheckIn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupCheckIn.BackColor = Color.White;
        groupCheckIn.Controls.Add(btnXoaTimThanhVien);
        groupCheckIn.Controls.Add(txtTimThanhVien);
        groupCheckIn.Controls.Add(lblTimThanhVien);
        groupCheckIn.Controls.Add(tableCheckIn);
        groupCheckIn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupCheckIn.ForeColor = Color.FromArgb(30, 41, 59);
        groupCheckIn.Location = new Point(24, 96);
        groupCheckIn.Name = "groupCheckIn";
        groupCheckIn.Padding = new Padding(16, 10, 16, 10);
        groupCheckIn.Size = new Size(1052, 110);
        groupCheckIn.TabIndex = 1;
        groupCheckIn.TabStop = false;
        groupCheckIn.Text = "Check-in / Check-out";
        // 
        // btnXoaTimThanhVien
        // 
        btnXoaTimThanhVien.BackColor = Color.FromArgb(100, 116, 139);
        btnXoaTimThanhVien.Cursor = Cursors.Hand;
        btnXoaTimThanhVien.FlatAppearance.BorderSize = 0;
        btnXoaTimThanhVien.FlatStyle = FlatStyle.Flat;
        btnXoaTimThanhVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoaTimThanhVien.ForeColor = Color.White;
        btnXoaTimThanhVien.Location = new Point(416, 61);
        btnXoaTimThanhVien.Name = "btnXoaTimThanhVien";
        btnXoaTimThanhVien.Size = new Size(76, 30);
        btnXoaTimThanhVien.TabIndex = 9;
        btnXoaTimThanhVien.Text = "Xóa lọc";
        btnXoaTimThanhVien.UseVisualStyleBackColor = false;
        // 
        // txtTimThanhVien
        // 
        txtTimThanhVien.BorderStyle = BorderStyle.Fixed3D;
        txtTimThanhVien.Font = new Font("Segoe UI", 9F);
        txtTimThanhVien.Location = new Point(109, 65);
        txtTimThanhVien.Name = "txtTimThanhVien";
        txtTimThanhVien.Size = new Size(300, 23);
        txtTimThanhVien.TabIndex = 8;
        // 
        // lblTimThanhVien
        // 
        lblTimThanhVien.AutoSize = true;
        lblTimThanhVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimThanhVien.Location = new Point(16, 69);
        lblTimThanhVien.Name = "lblTimThanhVien";
        lblTimThanhVien.Size = new Size(68, 15);
        lblTimThanhVien.TabIndex = 7;
        lblTimThanhVien.Text = "Tìm kiếm:";
        // 
        // tableCheckIn
        // 
        tableCheckIn.ColumnCount = 7;
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableCheckIn.Controls.Add(lblTV, 0, 0);
        tableCheckIn.Controls.Add(cboThanhVien, 1, 0);
        tableCheckIn.Controls.Add(lblPhong, 2, 0);
        tableCheckIn.Controls.Add(cboPhong, 3, 0);
        tableCheckIn.Controls.Add(btnCheckIn, 4, 0);
        tableCheckIn.Controls.Add(btnCheckOut, 5, 0);
        tableCheckIn.Controls.Add(btnChuyenPhong, 6, 0);
        tableCheckIn.Dock = DockStyle.Top;
        tableCheckIn.Font = new Font("Segoe UI", 9F);
        tableCheckIn.Location = new Point(16, 28);
        tableCheckIn.Name = "tableCheckIn";
        tableCheckIn.RowCount = 1;
        tableCheckIn.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableCheckIn.Size = new Size(1020, 37);
        tableCheckIn.TabIndex = 0;
        // 
        // lblTV
        // 
        lblTV.AutoSize = true;
        lblTV.Dock = DockStyle.Fill;
        lblTV.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTV.Location = new Point(3, 0);
        lblTV.Name = "lblTV";
        lblTV.Size = new Size(84, 37);
        lblTV.TabIndex = 0;
        lblTV.Text = "Thành viên:";
        lblTV.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThanhVien
        // 
        cboThanhVien.Dock = DockStyle.Fill;
        cboThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThanhVien.FormattingEnabled = true;
        cboThanhVien.Location = new Point(93, 7);
        cboThanhVien.Margin = new Padding(3, 7, 8, 3);
        cboThanhVien.Name = "cboThanhVien";
        cboThanhVien.Size = new Size(221, 23);
        cboThanhVien.TabIndex = 1;
        // 
        // lblPhong
        // 
        lblPhong.AutoSize = true;
        lblPhong.Dock = DockStyle.Fill;
        lblPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPhong.Location = new Point(325, 0);
        lblPhong.Name = "lblPhong";
        lblPhong.Size = new Size(84, 37);
        lblPhong.TabIndex = 2;
        lblPhong.Text = "Phòng tập:";
        lblPhong.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboPhong
        // 
        cboPhong.Dock = DockStyle.Fill;
        cboPhong.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPhong.FormattingEnabled = true;
        cboPhong.Location = new Point(415, 7);
        cboPhong.Margin = new Padding(3, 7, 8, 3);
        cboPhong.Name = "cboPhong";
        cboPhong.Size = new Size(221, 23);
        cboPhong.TabIndex = 3;
        // 
        // btnCheckIn
        // 
        btnCheckIn.BackColor = Color.FromArgb(5, 150, 105);
        btnCheckIn.Cursor = Cursors.Hand;
        btnCheckIn.Dock = DockStyle.Fill;
        btnCheckIn.FlatAppearance.BorderSize = 0;
        btnCheckIn.FlatStyle = FlatStyle.Flat;
        btnCheckIn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCheckIn.ForeColor = Color.White;
        btnCheckIn.Location = new Point(647, 3);
        btnCheckIn.Margin = new Padding(3, 3, 6, 3);
        btnCheckIn.Name = "btnCheckIn";
        btnCheckIn.Size = new Size(116, 31);
        btnCheckIn.TabIndex = 4;
        btnCheckIn.Text = "Check-in";
        btnCheckIn.UseVisualStyleBackColor = false;
        // 
        // btnCheckOut
        // 
        btnCheckOut.BackColor = Color.FromArgb(30, 64, 175);
        btnCheckOut.Cursor = Cursors.Hand;
        btnCheckOut.Dock = DockStyle.Fill;
        btnCheckOut.FlatAppearance.BorderSize = 0;
        btnCheckOut.FlatStyle = FlatStyle.Flat;
        btnCheckOut.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCheckOut.ForeColor = Color.White;
        btnCheckOut.Location = new Point(772, 3);
        btnCheckOut.Name = "btnCheckOut";
        btnCheckOut.Size = new Size(119, 31);
        btnCheckOut.TabIndex = 5;
        btnCheckOut.Text = "Check-out";
        btnCheckOut.UseVisualStyleBackColor = false;
        // 
        // btnChuyenPhong
        // 
        btnChuyenPhong.BackColor = Color.FromArgb(13, 148, 136);
        btnChuyenPhong.Cursor = Cursors.Hand;
        btnChuyenPhong.Dock = DockStyle.Fill;
        btnChuyenPhong.FlatAppearance.BorderSize = 0;
        btnChuyenPhong.FlatStyle = FlatStyle.Flat;
        btnChuyenPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnChuyenPhong.ForeColor = Color.White;
        btnChuyenPhong.Location = new Point(897, 3);
        btnChuyenPhong.Name = "btnChuyenPhong";
        btnChuyenPhong.Size = new Size(120, 31);
        btnChuyenPhong.TabIndex = 6;
        btnChuyenPhong.Text = "Chuyển phòng";
        btnChuyenPhong.UseVisualStyleBackColor = false;
        // 
        // dgvGoi
        // 
        dgvGoi.AllowUserToAddRows = false;
        dgvGoi.AllowUserToDeleteRows = false;
        dgvGoi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGoi.BackgroundColor = Color.White;
        dgvGoi.BorderStyle = BorderStyle.None;
        dgvGoi.ColumnHeadersHeight = 36;
        dgvGoi.Location = new Point(24, 220);
        dgvGoi.MultiSelect = false;
        dgvGoi.Name = "dgvGoi";
        dgvGoi.ReadOnly = true;
        dgvGoi.RowHeadersVisible = false;
        dgvGoi.RowTemplate.Height = 32;
        dgvGoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGoi.Size = new Size(1052, 200);
        dgvGoi.TabIndex = 2;
        // 
        // groupGoi
        // 
        groupGoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupGoi.BackColor = Color.White;
        groupGoi.Controls.Add(tableBaoLuu);
        groupGoi.Controls.Add(flowGiaHan);
        groupGoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupGoi.ForeColor = Color.FromArgb(30, 41, 59);
        groupGoi.Location = new Point(24, 432);
        groupGoi.Name = "groupGoi";
        groupGoi.Padding = new Padding(16, 8, 16, 10);
        groupGoi.Size = new Size(1052, 130);
        groupGoi.TabIndex = 3;
        groupGoi.TabStop = false;
        groupGoi.Text = "Gia hạn hoặc bảo lưu gói đang chọn";
        // 
        // tableBaoLuu
        // 
        tableBaoLuu.ColumnCount = 8;
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableBaoLuu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        tableBaoLuu.Controls.Add(lblTu, 0, 0);
        tableBaoLuu.Controls.Add(dtpTuNgay, 1, 0);
        tableBaoLuu.Controls.Add(lblDen, 2, 0);
        tableBaoLuu.Controls.Add(dtpDenNgay, 3, 0);
        tableBaoLuu.Controls.Add(lblLyDo, 4, 0);
        tableBaoLuu.Controls.Add(txtLyDo, 5, 0);
        tableBaoLuu.Controls.Add(btnBaoLuu, 6, 0);
        tableBaoLuu.Controls.Add(btnMoLaiBaoLuu, 7, 0);
        tableBaoLuu.Dock = DockStyle.Bottom;
        tableBaoLuu.Font = new Font("Segoe UI", 9F);
        tableBaoLuu.Location = new Point(16, 75);
        tableBaoLuu.Name = "tableBaoLuu";
        tableBaoLuu.RowCount = 1;
        tableBaoLuu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableBaoLuu.Size = new Size(1020, 45);
        tableBaoLuu.TabIndex = 1;
        // 
        // lblTu
        // 
        lblTu.AutoSize = true;
        lblTu.Dock = DockStyle.Fill;
        lblTu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTu.Location = new Point(3, 0);
        lblTu.Name = "lblTu";
        lblTu.Size = new Size(84, 45);
        lblTu.TabIndex = 0;
        lblTu.Text = "Bảo lưu từ:";
        lblTu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpTuNgay
        // 
        dtpTuNgay.Dock = DockStyle.Fill;
        dtpTuNgay.Format = DateTimePickerFormat.Short;
        dtpTuNgay.Location = new Point(93, 11);
        dtpTuNgay.Margin = new Padding(3, 11, 3, 3);
        dtpTuNgay.Name = "dtpTuNgay";
        dtpTuNgay.Size = new Size(124, 23);
        dtpTuNgay.TabIndex = 1;
        // 
        // lblDen
        // 
        lblDen.AutoSize = true;
        lblDen.Dock = DockStyle.Fill;
        lblDen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDen.Location = new Point(223, 0);
        lblDen.Name = "lblDen";
        lblDen.Size = new Size(64, 45);
        lblDen.TabIndex = 2;
        lblDen.Text = "Đến ngày:";
        lblDen.TextAlign = ContentAlignment.MiddleRight;
        // 
        // dtpDenNgay
        // 
        dtpDenNgay.Dock = DockStyle.Fill;
        dtpDenNgay.Format = DateTimePickerFormat.Short;
        dtpDenNgay.Location = new Point(293, 11);
        dtpDenNgay.Margin = new Padding(3, 11, 3, 3);
        dtpDenNgay.Name = "dtpDenNgay";
        dtpDenNgay.Size = new Size(124, 23);
        dtpDenNgay.TabIndex = 3;
        // 
        // lblLyDo
        // 
        lblLyDo.AutoSize = true;
        lblLyDo.Dock = DockStyle.Fill;
        lblLyDo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLyDo.Location = new Point(423, 0);
        lblLyDo.Name = "lblLyDo";
        lblLyDo.Size = new Size(54, 45);
        lblLyDo.TabIndex = 4;
        lblLyDo.Text = "Lý do:";
        lblLyDo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtLyDo
        // 
        txtLyDo.Dock = DockStyle.Fill;
        txtLyDo.Location = new Point(483, 11);
        txtLyDo.Margin = new Padding(3, 11, 8, 3);
        txtLyDo.Name = "txtLyDo";
        txtLyDo.PlaceholderText = "Lý do bảo lưu gói...";
        txtLyDo.Size = new Size(279, 23);
        txtLyDo.TabIndex = 5;
        // 
        // btnBaoLuu
        // 
        btnBaoLuu.BackColor = Color.FromArgb(124, 58, 237);
        btnBaoLuu.Cursor = Cursors.Hand;
        btnBaoLuu.Dock = DockStyle.Fill;
        btnBaoLuu.FlatAppearance.BorderSize = 0;
        btnBaoLuu.FlatStyle = FlatStyle.Flat;
        btnBaoLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBaoLuu.ForeColor = Color.White;
        btnBaoLuu.Location = new Point(773, 7);
        btnBaoLuu.Margin = new Padding(3, 7, 3, 7);
        btnBaoLuu.Name = "btnBaoLuu";
        btnBaoLuu.Size = new Size(119, 31);
        btnBaoLuu.TabIndex = 6;
        btnBaoLuu.Text = "Bảo lưu";
        btnBaoLuu.UseVisualStyleBackColor = false;
        // 
        // btnMoLaiBaoLuu
        // 
        btnMoLaiBaoLuu.BackColor = Color.FromArgb(14, 116, 144);
        btnMoLaiBaoLuu.Cursor = Cursors.Hand;
        btnMoLaiBaoLuu.Dock = DockStyle.Fill;
        btnMoLaiBaoLuu.FlatAppearance.BorderSize = 0;
        btnMoLaiBaoLuu.FlatStyle = FlatStyle.Flat;
        btnMoLaiBaoLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnMoLaiBaoLuu.ForeColor = Color.White;
        btnMoLaiBaoLuu.Location = new Point(898, 7);
        btnMoLaiBaoLuu.Margin = new Padding(3, 7, 3, 7);
        btnMoLaiBaoLuu.Name = "btnMoLaiBaoLuu";
        btnMoLaiBaoLuu.Size = new Size(119, 31);
        btnMoLaiBaoLuu.TabIndex = 7;
        btnMoLaiBaoLuu.Text = "Mở lại bảo lưu";
        btnMoLaiBaoLuu.UseVisualStyleBackColor = false;
        // 
        // flowGiaHan
        // 
        flowGiaHan.Controls.Add(lblNgay);
        flowGiaHan.Controls.Add(nudSoNgay);
        flowGiaHan.Controls.Add(lblDonViNgay);
        flowGiaHan.Controls.Add(btnGiaHan);
        flowGiaHan.Dock = DockStyle.Top;
        flowGiaHan.Font = new Font("Segoe UI", 9F);
        flowGiaHan.Location = new Point(16, 26);
        flowGiaHan.Name = "flowGiaHan";
        flowGiaHan.Padding = new Padding(0, 4, 0, 4);
        flowGiaHan.Size = new Size(1020, 42);
        flowGiaHan.TabIndex = 0;
        // 
        // lblNgay
        // 
        lblNgay.AutoSize = true;
        lblNgay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNgay.Location = new Point(3, 10);
        lblNgay.Margin = new Padding(3, 6, 3, 0);
        lblNgay.Name = "lblNgay";
        lblNgay.Size = new Size(84, 15);
        lblNgay.TabIndex = 0;
        lblNgay.Text = "Gia hạn thêm:";
        // 
        // nudSoNgay
        // 
        nudSoNgay.Location = new Point(93, 8);
        nudSoNgay.Margin = new Padding(3, 4, 3, 3);
        nudSoNgay.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        nudSoNgay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoNgay.Name = "nudSoNgay";
        nudSoNgay.Size = new Size(90, 23);
        nudSoNgay.TabIndex = 1;
        nudSoNgay.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblDonViNgay
        // 
        lblDonViNgay.AutoSize = true;
        lblDonViNgay.Location = new Point(189, 10);
        lblDonViNgay.Margin = new Padding(3, 6, 16, 0);
        lblDonViNgay.Name = "lblDonViNgay";
        lblDonViNgay.Size = new Size(33, 15);
        lblDonViNgay.TabIndex = 2;
        lblDonViNgay.Text = "ngày";
        // 
        // btnGiaHan
        // 
        btnGiaHan.BackColor = Color.FromArgb(217, 119, 6);
        btnGiaHan.Cursor = Cursors.Hand;
        btnGiaHan.FlatAppearance.BorderSize = 0;
        btnGiaHan.FlatStyle = FlatStyle.Flat;
        btnGiaHan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnGiaHan.ForeColor = Color.White;
        btnGiaHan.Location = new Point(241, 4);
        btnGiaHan.Margin = new Padding(3, 0, 3, 3);
        btnGiaHan.Name = "btnGiaHan";
        btnGiaHan.Size = new Size(120, 30);
        btnGiaHan.TabIndex = 3;
        btnGiaHan.Text = "Gia hạn gói";
        btnGiaHan.UseVisualStyleBackColor = false;
        // 
        // panelTrangThai
        // 
        panelTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelTrangThai.BackColor = Color.White;
        panelTrangThai.Controls.Add(lblTrangThai);
        panelTrangThai.Location = new Point(24, 572);
        panelTrangThai.Name = "panelTrangThai";
        panelTrangThai.Padding = new Padding(8);
        panelTrangThai.Size = new Size(1052, 30);
        panelTrangThai.TabIndex = 4;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.ForeColor = Color.FromArgb(71, 85, 105);
        lblTrangThai.Location = new Point(8, 8);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(216, 15);
        lblTrangThai.TabIndex = 0;
        lblTrangThai.Text = "Chọn thành viên để xem danh sách gói.";
        // 
        // FrmVanHanhGoi
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1100, 615);
        Controls.Add(panelTrangThai);
        Controls.Add(groupGoi);
        Controls.Add(dgvGoi);
        Controls.Add(groupCheckIn);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(850, 500);
        Name = "FrmVanHanhGoi";
        Text = "Vận hành gói tập";
        Load += FrmVanHanhGoi_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupCheckIn.ResumeLayout(false);
        groupCheckIn.PerformLayout();
        tableCheckIn.ResumeLayout(false);
        tableCheckIn.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoi).EndInit();
        groupGoi.ResumeLayout(false);
        tableBaoLuu.ResumeLayout(false);
        tableBaoLuu.PerformLayout();
        flowGiaHan.ResumeLayout(false);
        flowGiaHan.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoNgay).EndInit();
        panelTrangThai.ResumeLayout(false);
        panelTrangThai.PerformLayout();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupCheckIn;
    private TableLayoutPanel tableCheckIn;
    private Label lblTV;
    private ComboBox cboThanhVien;
    private Label lblTimThanhVien;
    private TextBox txtTimThanhVien;
    private Button btnXoaTimThanhVien;
    private Label lblPhong;
    private ComboBox cboPhong;
    private Button btnCheckIn;
    private Button btnCheckOut;
    private Button btnChuyenPhong;
    private DataGridView dgvGoi;
    private GroupBox groupGoi;
    private FlowLayoutPanel flowGiaHan;
    private Label lblNgay;
    private NumericUpDown nudSoNgay;
    private Label lblDonViNgay;
    private Button btnGiaHan;
    private TableLayoutPanel tableBaoLuu;
    private Label lblTu;
    private DateTimePicker dtpTuNgay;
    private Label lblDen;
    private DateTimePicker dtpDenNgay;
    private Label lblLyDo;
    private TextBox txtLyDo;
    private Button btnBaoLuu;
    private Button btnMoLaiBaoLuu;
    private Panel panelTrangThai;
    private Label lblTrangThai;
}

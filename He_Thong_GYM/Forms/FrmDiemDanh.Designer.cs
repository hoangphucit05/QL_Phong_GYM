namespace He_Thong_GYM.Forms;

partial class FrmDiemDanh
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
        groupThaoTac = new GroupBox();
        tableThaoTac = new TableLayoutPanel();
        lblLop = new Label();
        cboLop = new ComboBox();
        lblThanhVien = new Label();
        cboThanhVien = new ComboBox();
        btnDangKy = new Button();
        lblNgay = new Label();
        dtpNgay = new DateTimePicker();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        btnDiemDanh = new Button();
        btnTaoBuoi = new Button();
        lblBuoiHoc = new Label();
        cboBuoiHoc = new ComboBox();
        lblTrangThaiBuoi = new Label();
        lblLocDiemDanh = new Label();
        cboLocDiemDanh = new ComboBox();
        lblSapXep = new Label();
        cboSapXep = new ComboBox();
        btnBoLoc = new Button();
        lblQuyenThaoTac = new Label();
        dgvHocVien = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThaoTac.SuspendLayout();
        tableThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHocVien).BeginInit();
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
        panelTieuDe.Size = new Size(1000, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(359, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Đăng ký thành viên vào lớp học và theo dõi chuyên cần theo ngày.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(326, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "ĐĂNG KÝ VÀ ĐIỂM DANH LỚP";
        // 
        // groupThaoTac
        // 
        groupThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThaoTac.BackColor = Color.White;
        groupThaoTac.Controls.Add(tableThaoTac);
        groupThaoTac.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThaoTac.ForeColor = Color.FromArgb(30, 41, 59);
        groupThaoTac.Location = new Point(24, 96);
        groupThaoTac.Name = "groupThaoTac";
        groupThaoTac.Padding = new Padding(16, 12, 16, 12);
        groupThaoTac.Size = new Size(952, 145);
        groupThaoTac.TabIndex = 1;
        groupThaoTac.TabStop = false;
        groupThaoTac.Text = "Thao tác lớp tập";
        // 
        // tableThaoTac
        // 
        tableThaoTac.ColumnCount = 5;
        tableThaoTac.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        tableThaoTac.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        tableThaoTac.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        tableThaoTac.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        tableThaoTac.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        tableThaoTac.Controls.Add(lblLop, 0, 0);
        tableThaoTac.Controls.Add(cboLop, 1, 0);
        tableThaoTac.Controls.Add(lblThanhVien, 2, 0);
        tableThaoTac.Controls.Add(cboThanhVien, 3, 0);
        tableThaoTac.Controls.Add(btnDangKy, 4, 0);
        tableThaoTac.Controls.Add(lblNgay, 0, 1);
        tableThaoTac.Controls.Add(dtpNgay, 1, 1);
        tableThaoTac.Controls.Add(lblTrangThai, 2, 1);
        tableThaoTac.Controls.Add(cboTrangThai, 3, 1);
        tableThaoTac.Controls.Add(btnDiemDanh, 4, 1);
        tableThaoTac.Dock = DockStyle.Fill;
        tableThaoTac.Font = new Font("Segoe UI", 9F);
        tableThaoTac.Location = new Point(16, 30);
        tableThaoTac.Name = "tableThaoTac";
        tableThaoTac.RowCount = 2;
        tableThaoTac.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThaoTac.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThaoTac.Size = new Size(920, 73);
        tableThaoTac.TabIndex = 0;
        // 
        // lblLop
        // 
        lblLop.AutoSize = true;
        lblLop.Dock = DockStyle.Fill;
        lblLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLop.Location = new Point(3, 0);
        lblLop.Name = "lblLop";
        lblLop.Size = new Size(84, 36);
        lblLop.TabIndex = 0;
        lblLop.Text = "Lớp tập:";
        lblLop.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboLop
        // 
        cboLop.Dock = DockStyle.Fill;
        cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLop.FormattingEnabled = true;
        cboLop.Location = new Point(93, 3);
        cboLop.Name = "cboLop";
        cboLop.Size = new Size(259, 23);
        cboLop.TabIndex = 1;
        // 
        // lblThanhVien
        // 
        lblThanhVien.AutoSize = true;
        lblThanhVien.Dock = DockStyle.Fill;
        lblThanhVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblThanhVien.Location = new Point(358, 0);
        lblThanhVien.Name = "lblThanhVien";
        lblThanhVien.Size = new Size(94, 36);
        lblThanhVien.TabIndex = 2;
        lblThanhVien.Text = "Thành viên:";
        lblThanhVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThanhVien
        // 
        cboThanhVien.Dock = DockStyle.Fill;
        cboThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThanhVien.FormattingEnabled = true;
        cboThanhVien.Location = new Point(458, 3);
        cboThanhVien.Name = "cboThanhVien";
        cboThanhVien.Size = new Size(318, 23);
        cboThanhVien.TabIndex = 3;
        // 
        // btnDangKy
        // 
        btnDangKy.BackColor = Color.FromArgb(37, 99, 235);
        btnDangKy.Cursor = Cursors.Hand;
        btnDangKy.Dock = DockStyle.Fill;
        btnDangKy.FlatAppearance.BorderSize = 0;
        btnDangKy.FlatStyle = FlatStyle.Flat;
        btnDangKy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDangKy.ForeColor = Color.White;
        btnDangKy.Location = new Point(782, 3);
        btnDangKy.Name = "btnDangKy";
        btnDangKy.Size = new Size(135, 30);
        btnDangKy.TabIndex = 4;
        btnDangKy.Text = "Đăng ký lớp";
        btnDangKy.UseVisualStyleBackColor = false;
        // 
        // lblNgay
        // 
        lblNgay.AutoSize = true;
        lblNgay.Dock = DockStyle.Fill;
        lblNgay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNgay.Location = new Point(3, 36);
        lblNgay.Name = "lblNgay";
        lblNgay.Size = new Size(84, 37);
        lblNgay.TabIndex = 5;
        lblNgay.Text = "Ngày tập:";
        lblNgay.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpNgay
        // 
        dtpNgay.Dock = DockStyle.Fill;
        dtpNgay.Format = DateTimePickerFormat.Short;
        dtpNgay.Location = new Point(93, 39);
        dtpNgay.Name = "dtpNgay";
        dtpNgay.Size = new Size(259, 23);
        dtpNgay.TabIndex = 6;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.Location = new Point(358, 36);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(94, 37);
        lblTrangThai.TabIndex = 7;
        lblTrangThai.Text = "Điểm danh:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "Có mặt", "Vắng mặt" });
        cboTrangThai.Location = new Point(458, 39);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(318, 23);
        cboTrangThai.TabIndex = 8;
        // 
        // btnDiemDanh
        // 
        btnDiemDanh.BackColor = Color.FromArgb(5, 150, 105);
        btnDiemDanh.Cursor = Cursors.Hand;
        btnDiemDanh.Dock = DockStyle.Fill;
        btnDiemDanh.FlatAppearance.BorderSize = 0;
        btnDiemDanh.FlatStyle = FlatStyle.Flat;
        btnDiemDanh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDiemDanh.ForeColor = Color.White;
        btnDiemDanh.Location = new Point(782, 39);
        btnDiemDanh.Name = "btnDiemDanh";
        btnDiemDanh.Size = new Size(135, 31);
        btnDiemDanh.TabIndex = 9;
        btnDiemDanh.Text = "Lưu điểm danh";
        btnDiemDanh.UseVisualStyleBackColor = false;
        //
        // btnTaoBuoi
        //
        btnTaoBuoi.BackColor = Color.FromArgb(100, 116, 139);
        btnTaoBuoi.Cursor = Cursors.Hand;
        btnTaoBuoi.FlatAppearance.BorderSize = 0;
        btnTaoBuoi.FlatStyle = FlatStyle.Flat;
        btnTaoBuoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTaoBuoi.ForeColor = Color.White;
        btnTaoBuoi.Location = new Point(24, 249);
        btnTaoBuoi.Name = "btnTaoBuoi";
        btnTaoBuoi.Size = new Size(145, 28);
        btnTaoBuoi.TabIndex = 10;
        btnTaoBuoi.Text = "Tạo buổi học";
        btnTaoBuoi.UseVisualStyleBackColor = false;
        //
        // lblTrangThaiBuoi
        //
        lblBuoiHoc.AutoSize = true;
        lblBuoiHoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBuoiHoc.Location = new Point(184, 254);
        lblBuoiHoc.Name = "lblBuoiHoc";
        lblBuoiHoc.Size = new Size(56, 15);
        lblBuoiHoc.TabIndex = 11;
        lblBuoiHoc.Text = "Buổi học:";
        //
        // cboBuoiHoc
        //
        cboBuoiHoc.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBuoiHoc.FormattingEnabled = true;
        cboBuoiHoc.Location = new Point(245, 250);
        cboBuoiHoc.Name = "cboBuoiHoc";
        cboBuoiHoc.Size = new Size(329, 23);
        cboBuoiHoc.TabIndex = 12;
        //
        // lblTrangThaiBuoi
        //
        lblTrangThaiBuoi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTrangThaiBuoi.ForeColor = Color.FromArgb(71, 85, 105);
        lblTrangThaiBuoi.Location = new Point(584, 250);
        lblTrangThaiBuoi.Name = "lblTrangThaiBuoi";
        lblTrangThaiBuoi.Size = new Size(392, 26);
        lblTrangThaiBuoi.TabIndex = 11;
        lblTrangThaiBuoi.Text = "Chọn lớp và ngày học để xem buổi học.";
        lblTrangThaiBuoi.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblLocDiemDanh
        //
        lblLocDiemDanh.AutoSize = true;
        lblLocDiemDanh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLocDiemDanh.ForeColor = Color.FromArgb(30, 41, 59);
        lblLocDiemDanh.Location = new Point(24, 286);
        lblLocDiemDanh.Name = "lblLocDiemDanh";
        lblLocDiemDanh.Size = new Size(82, 15);
        lblLocDiemDanh.TabIndex = 12;
        lblLocDiemDanh.Text = "Lọc điểm danh:";
        //
        // cboLocDiemDanh
        //
        cboLocDiemDanh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocDiemDanh.FormattingEnabled = true;
        cboLocDiemDanh.Items.AddRange(new object[] { "-- Tất cả --", "Chưa điểm danh", "Có mặt", "Vắng mặt" });
        cboLocDiemDanh.Location = new Point(112, 282);
        cboLocDiemDanh.Name = "cboLocDiemDanh";
        cboLocDiemDanh.Size = new Size(150, 23);
        cboLocDiemDanh.TabIndex = 13;
        //
        // lblSapXep
        //
        lblSapXep.AutoSize = true;
        lblSapXep.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSapXep.ForeColor = Color.FromArgb(30, 41, 59);
        lblSapXep.Location = new Point(278, 286);
        lblSapXep.Name = "lblSapXep";
        lblSapXep.Size = new Size(53, 15);
        lblSapXep.TabIndex = 14;
        lblSapXep.Text = "Sắp xếp:";
        //
        // cboSapXep
        //
        cboSapXep.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSapXep.FormattingEnabled = true;
        cboSapXep.Items.AddRange(new object[] { "Họ tên (A-Z)", "Họ tên (Z-A)", "Mã hội viên tăng", "Mã hội viên giảm", "Trạng thái điểm danh" });
        cboSapXep.Location = new Point(337, 282);
        cboSapXep.Name = "cboSapXep";
        cboSapXep.Size = new Size(180, 23);
        cboSapXep.TabIndex = 15;
        //
        // btnBoLoc
        //
        btnBoLoc.BackColor = Color.FromArgb(71, 85, 105);
        btnBoLoc.Cursor = Cursors.Hand;
        btnBoLoc.FlatAppearance.BorderSize = 0;
        btnBoLoc.FlatStyle = FlatStyle.Flat;
        btnBoLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBoLoc.ForeColor = Color.White;
        btnBoLoc.Location = new Point(530, 280);
        btnBoLoc.Name = "btnBoLoc";
        btnBoLoc.Size = new Size(82, 27);
        btnBoLoc.TabIndex = 16;
        btnBoLoc.Text = "Bỏ lọc";
        btnBoLoc.UseVisualStyleBackColor = false;
        //
        // lblQuyenThaoTac
        //
        lblQuyenThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblQuyenThaoTac.ForeColor = Color.FromArgb(100, 116, 139);
        lblQuyenThaoTac.Location = new Point(625, 282);
        lblQuyenThaoTac.Name = "lblQuyenThaoTac";
        lblQuyenThaoTac.Size = new Size(351, 24);
        lblQuyenThaoTac.TabIndex = 17;
        lblQuyenThaoTac.TextAlign = ContentAlignment.MiddleRight;
        //
        // dgvHocVien
        // 
        dgvHocVien.AllowUserToAddRows = false;
        dgvHocVien.AllowUserToDeleteRows = false;
        dgvHocVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvHocVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHocVien.BackgroundColor = Color.White;
        dgvHocVien.BorderStyle = BorderStyle.None;
        dgvHocVien.ColumnHeadersHeight = 36;
        dgvHocVien.Location = new Point(24, 322);
        dgvHocVien.MultiSelect = false;
        dgvHocVien.Name = "dgvHocVien";
        dgvHocVien.ReadOnly = true;
        dgvHocVien.RowHeadersVisible = false;
        dgvHocVien.RowTemplate.Height = 32;
        dgvHocVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHocVien.Size = new Size(952, 343);
        dgvHocVien.TabIndex = 2;
        // 
        // FrmDiemDanh
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 690);
        Controls.Add(lblTrangThaiBuoi);
        Controls.Add(lblBuoiHoc);
        Controls.Add(cboBuoiHoc);
        Controls.Add(btnTaoBuoi);
        Controls.Add(lblQuyenThaoTac);
        Controls.Add(btnBoLoc);
        Controls.Add(cboSapXep);
        Controls.Add(lblSapXep);
        Controls.Add(cboLocDiemDanh);
        Controls.Add(lblLocDiemDanh);
        Controls.Add(dgvHocVien);
        Controls.Add(groupThaoTac);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmDiemDanh";
        Text = "Điểm danh lớp tập";
        Load += FrmDiemDanh_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThaoTac.ResumeLayout(false);
        tableThaoTac.ResumeLayout(false);
        tableThaoTac.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHocVien).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThaoTac;
    private TableLayoutPanel tableThaoTac;
    private Label lblLop;
    private ComboBox cboLop;
    private Label lblThanhVien;
    private ComboBox cboThanhVien;
    private Button btnDangKy;
    private Label lblNgay;
    private DateTimePicker dtpNgay;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private Button btnDiemDanh;
    private DataGridView dgvHocVien;
    private Button btnTaoBuoi;
    private Label lblBuoiHoc;
    private ComboBox cboBuoiHoc;
    private Label lblTrangThaiBuoi;
    private Label lblLocDiemDanh;
    private ComboBox cboLocDiemDanh;
    private Label lblSapXep;
    private ComboBox cboSapXep;
    private Button btnBoLoc;
    private Label lblQuyenThaoTac;
}

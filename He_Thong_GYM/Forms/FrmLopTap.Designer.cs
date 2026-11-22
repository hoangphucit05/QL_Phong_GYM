namespace He_Thong_GYM.Forms;

partial class FrmLopTap
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
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTenLop = new Label();
        txtTenLop = new TextBox();
        lblLoai = new Label();
        cboLoai = new ComboBox();
        lblHLV = new Label();
        cboHLV = new ComboBox();
        lblSucChua = new Label();
        nudSucChua = new NumericUpDown();
        lblBatDau = new Label();
        dtpBatDau = new DateTimePicker();
        lblKetThuc = new Label();
        dtpKetThuc = new DateTimePicker();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnDongLop = new Button();
        btnLamMoi = new Button();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        lblTrangThaiLoc = new Label();
        cboLocTrangThai = new ComboBox();
        lblThongKe = new Label();
        dgvLop = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSucChua).BeginInit();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLop).BeginInit();
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
        lblMoTa.Size = new Size(337, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Quản lý các lớp tập Yoga, Zumba, Boxing và phân công giảng dạy.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(204, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ LỚP TẬP";
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
        groupThongTin.Size = new Size(1052, 165);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin lớp tập";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTenLop, 0, 0);
        tableThongTin.Controls.Add(txtTenLop, 1, 0);
        tableThongTin.Controls.Add(lblLoai, 2, 0);
        tableThongTin.Controls.Add(cboLoai, 3, 0);
        tableThongTin.Controls.Add(lblHLV, 0, 1);
        tableThongTin.Controls.Add(cboHLV, 1, 1);
        tableThongTin.Controls.Add(lblSucChua, 2, 1);
        tableThongTin.Controls.Add(nudSucChua, 3, 1);
        tableThongTin.Controls.Add(lblBatDau, 0, 2);
        tableThongTin.Controls.Add(dtpBatDau, 1, 2);
        tableThongTin.Controls.Add(lblKetThuc, 2, 2);
        tableThongTin.Controls.Add(dtpKetThuc, 3, 2);
        tableThongTin.Controls.Add(lblTrangThai, 0, 3);
        tableThongTin.Controls.Add(cboTrangThai, 1, 3);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 30);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 4;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tableThongTin.Size = new Size(1020, 123);
        tableThongTin.TabIndex = 0;
        // 
        // lblTenLop
        // 
        lblTenLop.AutoSize = true;
        lblTenLop.Dock = DockStyle.Fill;
        lblTenLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTenLop.Location = new Point(3, 0);
        lblTenLop.Name = "lblTenLop";
        lblTenLop.Size = new Size(114, 30);
        lblTenLop.TabIndex = 0;
        lblTenLop.Text = "Tên lớp:";
        lblTenLop.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTenLop
        // 
        txtTenLop.Dock = DockStyle.Fill;
        txtTenLop.Location = new Point(123, 3);
        txtTenLop.Name = "txtTenLop";
        txtTenLop.PlaceholderText = "Nhập tên lớp...";
        txtTenLop.Size = new Size(384, 23);
        txtTenLop.TabIndex = 1;
        // 
        // lblLoai
        // 
        lblLoai.AutoSize = true;
        lblLoai.Dock = DockStyle.Fill;
        lblLoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLoai.Location = new Point(513, 0);
        lblLoai.Name = "lblLoai";
        lblLoai.Size = new Size(114, 30);
        lblLoai.TabIndex = 2;
        lblLoai.Text = "Loại lớp:";
        lblLoai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboLoai
        // 
        cboLoai.Dock = DockStyle.Fill;
        cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoai.FormattingEnabled = true;
        cboLoai.Location = new Point(633, 3);
        cboLoai.Name = "cboLoai";
        cboLoai.Size = new Size(384, 23);
        cboLoai.TabIndex = 3;
        // 
        // lblHLV
        // 
        lblHLV.AutoSize = true;
        lblHLV.Dock = DockStyle.Fill;
        lblHLV.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHLV.Location = new Point(3, 30);
        lblHLV.Name = "lblHLV";
        lblHLV.Size = new Size(114, 30);
        lblHLV.TabIndex = 4;
        lblHLV.Text = "Huấn luyện viên:";
        lblHLV.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboHLV
        // 
        cboHLV.Dock = DockStyle.Fill;
        cboHLV.DropDownStyle = ComboBoxStyle.DropDownList;
        cboHLV.FormattingEnabled = true;
        cboHLV.Location = new Point(123, 33);
        cboHLV.Name = "cboHLV";
        cboHLV.Size = new Size(384, 23);
        cboHLV.TabIndex = 5;
        // 
        // lblSucChua
        // 
        lblSucChua.AutoSize = true;
        lblSucChua.Dock = DockStyle.Fill;
        lblSucChua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSucChua.Location = new Point(513, 30);
        lblSucChua.Name = "lblSucChua";
        lblSucChua.Size = new Size(114, 30);
        lblSucChua.TabIndex = 6;
        lblSucChua.Text = "Sức chứa tối đa:";
        lblSucChua.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSucChua
        // 
        nudSucChua.Dock = DockStyle.Fill;
        nudSucChua.Location = new Point(633, 33);
        nudSucChua.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        nudSucChua.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSucChua.Name = "nudSucChua";
        nudSucChua.Size = new Size(384, 23);
        nudSucChua.TabIndex = 7;
        nudSucChua.Value = new decimal(new int[] { 20, 0, 0, 0 });
        // 
        // lblBatDau
        // 
        lblBatDau.AutoSize = true;
        lblBatDau.Dock = DockStyle.Fill;
        lblBatDau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBatDau.Location = new Point(3, 60);
        lblBatDau.Name = "lblBatDau";
        lblBatDau.Size = new Size(114, 30);
        lblBatDau.TabIndex = 8;
        lblBatDau.Text = "Giờ bắt đầu:";
        lblBatDau.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpBatDau
        // 
        dtpBatDau.Dock = DockStyle.Fill;
        dtpBatDau.Format = DateTimePickerFormat.Time;
        dtpBatDau.Location = new Point(123, 63);
        dtpBatDau.Name = "dtpBatDau";
        dtpBatDau.ShowUpDown = true;
        dtpBatDau.Size = new Size(384, 23);
        dtpBatDau.TabIndex = 9;
        // 
        // lblKetThuc
        // 
        lblKetThuc.AutoSize = true;
        lblKetThuc.Dock = DockStyle.Fill;
        lblKetThuc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblKetThuc.Location = new Point(513, 60);
        lblKetThuc.Name = "lblKetThuc";
        lblKetThuc.Size = new Size(114, 30);
        lblKetThuc.TabIndex = 10;
        lblKetThuc.Text = "Giờ kết thúc:";
        lblKetThuc.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpKetThuc
        // 
        dtpKetThuc.Dock = DockStyle.Fill;
        dtpKetThuc.Format = DateTimePickerFormat.Time;
        dtpKetThuc.Location = new Point(633, 63);
        dtpKetThuc.Name = "dtpKetThuc";
        dtpKetThuc.ShowUpDown = true;
        dtpKetThuc.Size = new Size(384, 23);
        dtpKetThuc.TabIndex = 11;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.Location = new Point(3, 90);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(114, 33);
        lblTrangThai.TabIndex = 12;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "Đang mở", "Đã đóng" });
        cboTrangThai.Location = new Point(123, 93);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(384, 23);
        cboTrangThai.TabIndex = 13;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnSua);
        flowThaoTac.Controls.Add(btnDongLop);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Controls.Add(lblTimKiem);
        flowThaoTac.Controls.Add(txtTimKiem);
        flowThaoTac.Controls.Add(lblTrangThaiLoc);
        flowThaoTac.Controls.Add(cboLocTrangThai);
        flowThaoTac.Controls.Add(lblThongKe);
        flowThaoTac.Location = new Point(24, 272);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1052, 42);
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
        btnThem.Size = new Size(110, 36);
        btnThem.TabIndex = 0;
        btnThem.Text = "Tạo lớp mới";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(16, 185, 129);
        btnSua.Cursor = Cursors.Hand;
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(118, 0);
        btnSua.Margin = new Padding(0, 0, 8, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 36);
        btnSua.TabIndex = 1;
        btnSua.Text = "Cập nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnDongLop
        // 
        btnDongLop.BackColor = Color.FromArgb(239, 68, 68);
        btnDongLop.Cursor = Cursors.Hand;
        btnDongLop.FlatAppearance.BorderSize = 0;
        btnDongLop.FlatStyle = FlatStyle.Flat;
        btnDongLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDongLop.ForeColor = Color.White;
        btnDongLop.Location = new Point(226, 0);
        btnDongLop.Margin = new Padding(0, 0, 8, 0);
        btnDongLop.Name = "btnDongLop";
        btnDongLop.Size = new Size(100, 36);
        btnDongLop.TabIndex = 2;
        btnDongLop.Text = "Đóng lớp";
        btnDongLop.UseVisualStyleBackColor = false;
        btnDongLop.Click += btnDongLop_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(334, 0);
        btnLamMoi.Margin = new Padding(0, 0, 16, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(90, 36);
        btnLamMoi.TabIndex = 3;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblTimKiem
        // 
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.ForeColor = Color.FromArgb(30, 41, 59);
        lblTimKiem.Location = new Point(257, 0);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(68, 36);
        lblTimKiem.TabIndex = 2;
        lblTimKiem.Text = "Tìm kiếm:";
        lblTimKiem.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(331, 6);
        txtTimKiem.Margin = new Padding(3, 6, 12, 3);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tên lớp, loại lớp, HLV...";
        txtTimKiem.Size = new Size(180, 23);
        txtTimKiem.TabIndex = 3;
        txtTimKiem.TextChanged += BoLocThayDoi;
        // 
        // lblTrangThaiLoc
        // 
        lblTrangThaiLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThaiLoc.ForeColor = Color.FromArgb(30, 41, 59);
        lblTrangThaiLoc.Location = new Point(529, 0);
        lblTrangThaiLoc.Name = "lblTrangThaiLoc";
        lblTrangThaiLoc.Size = new Size(72, 36);
        lblTrangThaiLoc.TabIndex = 4;
        lblTrangThaiLoc.Text = "Trạng thái:";
        lblTrangThaiLoc.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cboLocTrangThai
        // 
        cboLocTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocTrangThai.FormattingEnabled = true;
        cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Đang mở", "Đã đóng" });
        cboLocTrangThai.Location = new Point(607, 6);
        cboLocTrangThai.Margin = new Padding(3, 6, 12, 3);
        cboLocTrangThai.Name = "cboLocTrangThai";
        cboLocTrangThai.Size = new Size(130, 23);
        cboLocTrangThai.TabIndex = 5;
        cboLocTrangThai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // lblThongKe
        // 
        lblThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKe.ForeColor = Color.FromArgb(100, 116, 139);
        lblThongKe.Location = new Point(752, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Size = new Size(200, 36);
        lblThongKe.TabIndex = 6;
        lblThongKe.Text = "Hiển thị: 0 lớp";
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvLop
        // 
        dgvLop.AllowUserToAddRows = false;
        dgvLop.AllowUserToDeleteRows = false;
        dgvLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLop.BackgroundColor = Color.White;
        dgvLop.BorderStyle = BorderStyle.None;
        dgvLop.ColumnHeadersHeight = 36;
        dgvLop.Location = new Point(24, 320);
        dgvLop.MultiSelect = false;
        dgvLop.Name = "dgvLop";
        dgvLop.ReadOnly = true;
        dgvLop.RowHeadersVisible = false;
        dgvLop.RowTemplate.Height = 32;
        dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLop.Size = new Size(1052, 380);
        dgvLop.TabIndex = 3;
        dgvLop.SelectionChanged += dgvLop_SelectionChanged;
        // 
        // FrmLopTap
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1100, 720);
        Controls.Add(dgvLop);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmLopTap";
        Text = "Quản lý lớp tập";
        Load += FrmLopTap_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSucChua).EndInit();
        flowThaoTac.ResumeLayout(false);
        flowThaoTac.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLop).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblTenLop;
    private TextBox txtTenLop;
    private Label lblLoai;
    private ComboBox cboLoai;
    private Label lblHLV;
    private ComboBox cboHLV;
    private Label lblSucChua;
    private NumericUpDown nudSucChua;
    private Label lblBatDau;
    private DateTimePicker dtpBatDau;
    private Label lblKetThuc;
    private DateTimePicker dtpKetThuc;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnSua;
    private Button btnDongLop;
    private Button btnLamMoi;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Label lblTrangThaiLoc;
    private ComboBox cboLocTrangThai;
    private Label lblThongKe;
    private DataGridView dgvLop;
}

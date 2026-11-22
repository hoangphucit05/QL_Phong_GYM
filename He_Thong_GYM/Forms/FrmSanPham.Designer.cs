namespace He_Thong_GYM.Forms;

partial class FrmSanPham
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
        lblTieuDe = new Label();
        lblMoTa = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTenSP = new Label();
        txtTenSP = new TextBox();
        lblDonGia = new Label();
        nudDonGia = new NumericUpDown();
        lblGiaNhap = new Label();
        nudGiaNhap = new NumericUpDown();
        lblTrangThaiSP = new Label();
        cboTrangThaiSP = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnNhapKho = new Button();
        btnBanHang = new Button();
        btnLamMoi = new Button();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        lblThongKe = new Label();
        dgvSanPham = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudDonGia).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudGiaNhap).BeginInit();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
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
        panelTieuDe.Size = new Size(1050, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(390, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ SẢN PHẨM VÀ TỒN KHO";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(360, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Danh mục nước uống, whey protein, phụ kiện và theo dõi tồn kho.";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        groupThongTin.ForeColor = Color.FromArgb(30, 41, 59);
        groupThongTin.Location = new Point(24, 90);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(12, 8, 12, 8);
        groupThongTin.Size = new Size(1002, 75);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin sản phẩm";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21F));
        tableThongTin.Controls.Add(lblTenSP, 0, 0);
        tableThongTin.Controls.Add(txtTenSP, 0, 1);
        tableThongTin.Controls.Add(lblDonGia, 1, 0);
        tableThongTin.Controls.Add(nudDonGia, 1, 1);
        tableThongTin.Controls.Add(lblGiaNhap, 2, 0);
        tableThongTin.Controls.Add(nudGiaNhap, 2, 1);
        tableThongTin.Controls.Add(lblTrangThaiSP, 3, 0);
        tableThongTin.Controls.Add(cboTrangThaiSP, 3, 1);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(12, 24);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableThongTin.Size = new Size(978, 43);
        tableThongTin.TabIndex = 0;
        // 
        // lblTenSP
        // 
        lblTenSP.AutoSize = true;
        lblTenSP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTenSP.ForeColor = Color.FromArgb(71, 85, 105);
        lblTenSP.Location = new Point(3, 0);
        lblTenSP.Name = "lblTenSP";
        lblTenSP.Size = new Size(86, 15);
        lblTenSP.TabIndex = 0;
        lblTenSP.Text = "Tên sản phẩm:";
        // 
        // txtTenSP
        // 
        txtTenSP.Dock = DockStyle.Fill;
        txtTenSP.Location = new Point(3, 21);
        txtTenSP.Name = "txtTenSP";
        txtTenSP.PlaceholderText = "Nhập tên sản phẩm...";
        txtTenSP.Size = new Size(336, 23);
        txtTenSP.TabIndex = 1;
        // 
        // lblDonGia
        // 
        lblDonGia.AutoSize = true;
        lblDonGia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDonGia.ForeColor = Color.FromArgb(71, 85, 105);
        lblDonGia.Location = new Point(345, 0);
        lblDonGia.Name = "lblDonGia";
        lblDonGia.Size = new Size(74, 15);
        lblDonGia.TabIndex = 2;
        lblDonGia.Text = "Đơn giá bán:";
        // 
        // nudDonGia
        // 
        nudDonGia.Dock = DockStyle.Fill;
        nudDonGia.Increment = new decimal(new int[] { 5000, 0, 0, 0 });
        nudDonGia.Location = new Point(345, 21);
        nudDonGia.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        nudDonGia.Name = "nudDonGia";
        nudDonGia.Size = new Size(209, 23);
        nudDonGia.TabIndex = 3;
        nudDonGia.ThousandsSeparator = true;
        // 
        // lblGiaNhap
        // 
        lblGiaNhap.AutoSize = true;
        lblGiaNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGiaNhap.ForeColor = Color.FromArgb(71, 85, 105);
        lblGiaNhap.Location = new Point(560, 0);
        lblGiaNhap.Name = "lblGiaNhap";
        lblGiaNhap.Size = new Size(58, 15);
        lblGiaNhap.TabIndex = 4;
        lblGiaNhap.Text = "Giá nhập:";
        // 
        // nudGiaNhap
        // 
        nudGiaNhap.Dock = DockStyle.Fill;
        nudGiaNhap.Increment = new decimal(new int[] { 5000, 0, 0, 0 });
        nudGiaNhap.Location = new Point(560, 21);
        nudGiaNhap.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        nudGiaNhap.Name = "nudGiaNhap";
        nudGiaNhap.Size = new Size(209, 23);
        nudGiaNhap.TabIndex = 5;
        nudGiaNhap.ThousandsSeparator = true;
        // 
        // lblTrangThaiSP
        // 
        lblTrangThaiSP.AutoSize = true;
        lblTrangThaiSP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThaiSP.ForeColor = Color.FromArgb(71, 85, 105);
        lblTrangThaiSP.Location = new Point(775, 0);
        lblTrangThaiSP.Name = "lblTrangThaiSP";
        lblTrangThaiSP.Size = new Size(65, 15);
        lblTrangThaiSP.TabIndex = 6;
        lblTrangThaiSP.Text = "Trạng thái:";
        // 
        // cboTrangThaiSP
        // 
        cboTrangThaiSP.Dock = DockStyle.Fill;
        cboTrangThaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThaiSP.FormattingEnabled = true;
        cboTrangThaiSP.Items.AddRange(new object[] { "Còn bán", "Ngừng bán" });
        cboTrangThaiSP.Location = new Point(775, 21);
        cboTrangThaiSP.Name = "cboTrangThaiSP";
        cboTrangThaiSP.Size = new Size(200, 23);
        cboTrangThaiSP.TabIndex = 7;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnSua);
        flowThaoTac.Controls.Add(btnNhapKho);
        flowThaoTac.Controls.Add(btnBanHang);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Controls.Add(lblTimKiem);
        flowThaoTac.Controls.Add(txtTimKiem);
        flowThaoTac.Controls.Add(lblTrangThai);
        flowThaoTac.Controls.Add(cboTrangThai);
        flowThaoTac.Controls.Add(lblThongKe);
        flowThaoTac.Location = new Point(24, 172);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1002, 42);
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
        btnThem.Size = new Size(95, 36);
        btnThem.TabIndex = 0;
        btnThem.Text = "Thêm mới";
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
        btnSua.Location = new Point(103, 0);
        btnSua.Margin = new Padding(0, 0, 8, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(95, 36);
        btnSua.TabIndex = 1;
        btnSua.Text = "Cập nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnNhapKho
        // 
        btnNhapKho.BackColor = Color.FromArgb(217, 119, 6);
        btnNhapKho.Cursor = Cursors.Hand;
        btnNhapKho.FlatAppearance.BorderSize = 0;
        btnNhapKho.FlatStyle = FlatStyle.Flat;
        btnNhapKho.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNhapKho.ForeColor = Color.White;
        btnNhapKho.Location = new Point(206, 0);
        btnNhapKho.Margin = new Padding(0, 0, 8, 0);
        btnNhapKho.Name = "btnNhapKho";
        btnNhapKho.Size = new Size(110, 36);
        btnNhapKho.TabIndex = 2;
        btnNhapKho.Text = "Nhập kho (+)";
        btnNhapKho.UseVisualStyleBackColor = false;
        btnNhapKho.Click += btnNhapKho_Click;
        // 
        // btnBanHang
        // 
        btnBanHang.BackColor = Color.FromArgb(5, 150, 105);
        btnBanHang.Cursor = Cursors.Hand;
        btnBanHang.FlatAppearance.BorderSize = 0;
        btnBanHang.FlatStyle = FlatStyle.Flat;
        btnBanHang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBanHang.ForeColor = Color.White;
        btnBanHang.Location = new Point(324, 0);
        btnBanHang.Margin = new Padding(0, 0, 8, 0);
        btnBanHang.Name = "btnBanHang";
        btnBanHang.Size = new Size(110, 36);
        btnBanHang.TabIndex = 3;
        btnBanHang.Text = "Bán sản phẩm";
        btnBanHang.UseVisualStyleBackColor = false;
        btnBanHang.Click += btnBanHang_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(442, 0);
        btnLamMoi.Margin = new Padding(0, 0, 12, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(90, 36);
        btnLamMoi.TabIndex = 4;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblTimKiem
        // 
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.ForeColor = Color.FromArgb(30, 41, 59);
        lblTimKiem.Location = new Point(544, 0);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(65, 36);
        lblTimKiem.TabIndex = 5;
        lblTimKiem.Text = "Tìm kiếm:";
        lblTimKiem.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(612, 6);
        txtTimKiem.Margin = new Padding(3, 6, 8, 3);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tên, mã sản phẩm...";
        txtTimKiem.Size = new Size(140, 23);
        txtTimKiem.TabIndex = 6;
        txtTimKiem.TextChanged += BoLocThayDoi;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
        lblTrangThai.Location = new Point(760, 0);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(68, 36);
        lblTrangThai.TabIndex = 7;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cboTrangThai
        // 
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Còn bán", "Ngừng bán" });
        cboTrangThai.Location = new Point(831, 6);
        cboTrangThai.Margin = new Padding(3, 6, 8, 3);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(110, 23);
        cboTrangThai.TabIndex = 8;
        cboTrangThai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // lblThongKe
        // 
        lblThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKe.ForeColor = Color.FromArgb(100, 116, 139);
        lblThongKe.Location = new Point(949, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Size = new Size(40, 36);
        lblThongKe.TabIndex = 9;
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvSanPham
        // 
        dgvSanPham.AllowUserToAddRows = false;
        dgvSanPham.AllowUserToDeleteRows = false;
        dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSanPham.BackgroundColor = Color.White;
        dgvSanPham.BorderStyle = BorderStyle.None;
        dgvSanPham.ColumnHeadersHeight = 36;
        dgvSanPham.Location = new Point(24, 222);
        dgvSanPham.MultiSelect = false;
        dgvSanPham.Name = "dgvSanPham";
        dgvSanPham.ReadOnly = true;
        dgvSanPham.RowHeadersVisible = false;
        dgvSanPham.RowTemplate.Height = 32;
        dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSanPham.Size = new Size(1002, 455);
        dgvSanPham.TabIndex = 3;
        dgvSanPham.SelectionChanged += dgvSanPham_SelectionChanged;
        // 
        // FrmSanPham
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1050, 700);
        Controls.Add(dgvSanPham);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmSanPham";
        Text = "Quản lý sản phẩm";
        Load += FrmSanPham_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudDonGia).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudGiaNhap).EndInit();
        flowThaoTac.ResumeLayout(false);
        flowThaoTac.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblTenSP;
    private TextBox txtTenSP;
    private Label lblDonGia;
    private NumericUpDown nudDonGia;
    private Label lblGiaNhap;
    private NumericUpDown nudGiaNhap;
    private Label lblTrangThaiSP;
    private ComboBox cboTrangThaiSP;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnSua;
    private Button btnNhapKho;
    private Button btnBanHang;
    private Button btnLamMoi;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private Label lblThongKe;
    private DataGridView dgvSanPham;
}

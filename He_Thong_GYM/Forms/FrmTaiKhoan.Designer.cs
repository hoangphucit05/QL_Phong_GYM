namespace He_Thong_GYM.Forms;

partial class FrmTaiKhoan
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelTieuDe = new Panel();
        lblTieuDe = new Label();
        lblMoTa = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTenDangNhap = new Label();
        txtTenDangNhap = new TextBox();
        lblMatKhau = new Label();
        txtMatKhau = new TextBox();
        lblNhanVien = new Label();
        cboNhanVien = new ComboBox();
        lblVaiTro = new Label();
        cboVaiTro = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnTaoMoi = new Button();
        btnLuu = new Button();
        btnDatLaiMatKhau = new Button();
        btnKhoaMo = new Button();
        dgvTaiKhoan = new DataGridView();
        colMaTK = new DataGridViewTextBoxColumn();
        colTenDangNhap = new DataGridViewTextBoxColumn();
        colNhanVien = new DataGridViewTextBoxColumn();
        colVaiTro = new DataGridViewTextBoxColumn();
        colTrangThai = new DataGridViewTextBoxColumn();
        panelTrangThai = new Panel();
        lblTrangThai = new Label();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).BeginInit();
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
        panelTieuDe.Size = new Size(1200, 82);
        panelTieuDe.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(360, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "TÀI KHOẢN VÀ PHÂN QUYỀN";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(345, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Tạo tài khoản, gán vai trò, đặt lại mật khẩu và khóa truy cập.";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThongTin.Location = new Point(24, 100);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(16);
        groupThongTin.Size = new Size(1152, 120);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin tài khoản";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTenDangNhap, 0, 0);
        tableThongTin.Controls.Add(txtTenDangNhap, 1, 0);
        tableThongTin.Controls.Add(lblMatKhau, 2, 0);
        tableThongTin.Controls.Add(txtMatKhau, 3, 0);
        tableThongTin.Controls.Add(lblNhanVien, 0, 1);
        tableThongTin.Controls.Add(cboNhanVien, 1, 1);
        tableThongTin.Controls.Add(lblVaiTro, 2, 1);
        tableThongTin.Controls.Add(cboVaiTro, 3, 1);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 34);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.Size = new Size(1120, 70);
        tableThongTin.TabIndex = 0;
        // 
        // lblTenDangNhap
        // 
        lblTenDangNhap.AutoSize = true;
        lblTenDangNhap.Dock = DockStyle.Fill;
        lblTenDangNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTenDangNhap.Location = new Point(3, 0);
        lblTenDangNhap.Name = "lblTenDangNhap";
        lblTenDangNhap.Size = new Size(114, 35);
        lblTenDangNhap.TabIndex = 0;
        lblTenDangNhap.Text = "Tên đăng nhập:";
        lblTenDangNhap.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTenDangNhap
        // 
        txtTenDangNhap.Dock = DockStyle.Fill;
        txtTenDangNhap.Location = new Point(123, 6);
        txtTenDangNhap.Margin = new Padding(3, 6, 12, 6);
        txtTenDangNhap.Name = "txtTenDangNhap";
        txtTenDangNhap.Size = new Size(425, 23);
        txtTenDangNhap.TabIndex = 1;
        // 
        // lblMatKhau
        // 
        lblMatKhau.AutoSize = true;
        lblMatKhau.Dock = DockStyle.Fill;
        lblMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMatKhau.Location = new Point(563, 0);
        lblMatKhau.Name = "lblMatKhau";
        lblMatKhau.Size = new Size(114, 35);
        lblMatKhau.TabIndex = 2;
        lblMatKhau.Text = "Mật khẩu mới:";
        lblMatKhau.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtMatKhau
        // 
        txtMatKhau.Dock = DockStyle.Fill;
        txtMatKhau.Location = new Point(683, 6);
        txtMatKhau.Margin = new Padding(3, 6, 12, 6);
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.Size = new Size(425, 23);
        txtMatKhau.TabIndex = 3;
        txtMatKhau.UseSystemPasswordChar = true;
        // 
        // lblNhanVien
        // 
        lblNhanVien.AutoSize = true;
        lblNhanVien.Dock = DockStyle.Fill;
        lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhanVien.Location = new Point(3, 35);
        lblNhanVien.Name = "lblNhanVien";
        lblNhanVien.Size = new Size(114, 35);
        lblNhanVien.TabIndex = 4;
        lblNhanVien.Text = "Nhân viên:";
        lblNhanVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboNhanVien
        // 
        cboNhanVien.Dock = DockStyle.Fill;
        cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhanVien.Location = new Point(123, 38);
        cboNhanVien.Name = "cboNhanVien";
        cboNhanVien.Size = new Size(434, 23);
        cboNhanVien.TabIndex = 5;
        // 
        // lblVaiTro
        // 
        lblVaiTro.AutoSize = true;
        lblVaiTro.Dock = DockStyle.Fill;
        lblVaiTro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblVaiTro.Location = new Point(563, 35);
        lblVaiTro.Name = "lblVaiTro";
        lblVaiTro.Size = new Size(114, 35);
        lblVaiTro.TabIndex = 6;
        lblVaiTro.Text = "Vai trò:";
        lblVaiTro.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboVaiTro
        // 
        cboVaiTro.Dock = DockStyle.Fill;
        cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
        cboVaiTro.Location = new Point(683, 38);
        cboVaiTro.Name = "cboVaiTro";
        cboVaiTro.Size = new Size(434, 23);
        cboVaiTro.TabIndex = 7;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnTaoMoi);
        flowThaoTac.Controls.Add(btnLuu);
        flowThaoTac.Controls.Add(btnDatLaiMatKhau);
        flowThaoTac.Controls.Add(btnKhoaMo);
        flowThaoTac.Location = new Point(24, 230);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1152, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnTaoMoi
        // 
        btnTaoMoi.BackColor = Color.FromArgb(37, 99, 235);
        btnTaoMoi.Cursor = Cursors.Hand;
        btnTaoMoi.FlatAppearance.BorderSize = 0;
        btnTaoMoi.FlatStyle = FlatStyle.Flat;
        btnTaoMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTaoMoi.ForeColor = Color.White;
        btnTaoMoi.Location = new Point(0, 0);
        btnTaoMoi.Margin = new Padding(0, 0, 8, 0);
        btnTaoMoi.Name = "btnTaoMoi";
        btnTaoMoi.Size = new Size(142, 36);
        btnTaoMoi.TabIndex = 0;
        btnTaoMoi.Text = "Tạo mới";
        btnTaoMoi.UseVisualStyleBackColor = false;
        btnTaoMoi.Click += btnTaoMoi_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(5, 150, 105);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(150, 0);
        btnLuu.Margin = new Padding(0, 0, 8, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(142, 36);
        btnLuu.TabIndex = 1;
        btnLuu.Text = "Lưu tài khoản";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnDatLaiMatKhau
        // 
        btnDatLaiMatKhau.BackColor = Color.FromArgb(217, 119, 6);
        btnDatLaiMatKhau.Cursor = Cursors.Hand;
        btnDatLaiMatKhau.FlatAppearance.BorderSize = 0;
        btnDatLaiMatKhau.FlatStyle = FlatStyle.Flat;
        btnDatLaiMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDatLaiMatKhau.ForeColor = Color.White;
        btnDatLaiMatKhau.Location = new Point(300, 0);
        btnDatLaiMatKhau.Margin = new Padding(0, 0, 8, 0);
        btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
        btnDatLaiMatKhau.Size = new Size(142, 36);
        btnDatLaiMatKhau.TabIndex = 2;
        btnDatLaiMatKhau.Text = "Đặt lại mật khẩu";
        btnDatLaiMatKhau.UseVisualStyleBackColor = false;
        btnDatLaiMatKhau.Click += btnDatLaiMatKhau_Click;
        // 
        // btnKhoaMo
        // 
        btnKhoaMo.BackColor = Color.FromArgb(185, 28, 28);
        btnKhoaMo.Cursor = Cursors.Hand;
        btnKhoaMo.FlatAppearance.BorderSize = 0;
        btnKhoaMo.FlatStyle = FlatStyle.Flat;
        btnKhoaMo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnKhoaMo.ForeColor = Color.White;
        btnKhoaMo.Location = new Point(450, 0);
        btnKhoaMo.Margin = new Padding(0, 0, 8, 0);
        btnKhoaMo.Name = "btnKhoaMo";
        btnKhoaMo.Size = new Size(142, 36);
        btnKhoaMo.TabIndex = 3;
        btnKhoaMo.Text = "Khóa / mở khóa";
        btnKhoaMo.UseVisualStyleBackColor = false;
        btnKhoaMo.Click += btnKhoaMo_Click;
        // 
        // dgvTaiKhoan
        // 
        dgvTaiKhoan.AllowUserToAddRows = false;
        dgvTaiKhoan.AllowUserToDeleteRows = false;
        dgvTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTaiKhoan.AutoGenerateColumns = false;
        dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTaiKhoan.BackgroundColor = Color.White;
        dgvTaiKhoan.BorderStyle = BorderStyle.None;
        dgvTaiKhoan.ColumnHeadersHeight = 38;
        dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvTaiKhoan.Columns.AddRange(new DataGridViewColumn[] { colMaTK, colTenDangNhap, colNhanVien, colVaiTro, colTrangThai });
        dgvTaiKhoan.Location = new Point(24, 292);
        dgvTaiKhoan.Name = "dgvTaiKhoan";
        dgvTaiKhoan.ReadOnly = true;
        dgvTaiKhoan.RowHeadersVisible = false;
        dgvTaiKhoan.RowTemplate.Height = 32;
        dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTaiKhoan.Size = new Size(1152, 407);
        dgvTaiKhoan.TabIndex = 3;
        dgvTaiKhoan.SelectionChanged += dgvTaiKhoan_SelectionChanged;
        // 
        // colMaTK
        // 
        colMaTK.DataPropertyName = "MaTK";
        colMaTK.FillWeight = 45F;
        colMaTK.HeaderText = "Mã";
        colMaTK.Name = "colMaTK";
        colMaTK.ReadOnly = true;
        // 
        // colTenDangNhap
        // 
        colTenDangNhap.DataPropertyName = "TenDangNhap";
        colTenDangNhap.FillWeight = 120F;
        colTenDangNhap.HeaderText = "Tên đăng nhập";
        colTenDangNhap.Name = "colTenDangNhap";
        colTenDangNhap.ReadOnly = true;
        // 
        // colNhanVien
        // 
        colNhanVien.DataPropertyName = "HoTenNhanVien";
        colNhanVien.FillWeight = 160F;
        colNhanVien.HeaderText = "Nhân viên";
        colNhanVien.Name = "colNhanVien";
        colNhanVien.ReadOnly = true;
        // 
        // colVaiTro
        // 
        colVaiTro.DataPropertyName = "VaiTro";
        colVaiTro.FillWeight = 130F;
        colVaiTro.HeaderText = "Vai trò";
        colVaiTro.Name = "colVaiTro";
        colVaiTro.ReadOnly = true;
        // 
        // colTrangThai
        // 
        colTrangThai.DataPropertyName = "TrangThai";
        colTrangThai.FillWeight = 90F;
        colTrangThai.HeaderText = "Trạng thái";
        colTrangThai.Name = "colTrangThai";
        colTrangThai.ReadOnly = true;
        // 
        // panelTrangThai
        // 
        panelTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelTrangThai.BackColor = Color.White;
        panelTrangThai.Controls.Add(lblTrangThai);
        panelTrangThai.Location = new Point(24, 709);
        panelTrangThai.Name = "panelTrangThai";
        panelTrangThai.Size = new Size(1152, 30);
        panelTrangThai.TabIndex = 4;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139);
        lblTrangThai.Location = new Point(10, 8);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(149, 15);
        lblTrangThai.TabIndex = 0;
        lblTrangThai.Text = "Chọn tài khoản để quản lý.";
        // 
        // FrmTaiKhoan
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvTaiKhoan);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        AutoScroll = true;
        MinimumSize = new Size(850, 480);
        Name = "FrmTaiKhoan";
        Text = "Tài khoản và phân quyền";
        Load += FrmTaiKhoan_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        flowThaoTac.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).EndInit();
        panelTrangThai.ResumeLayout(false);
        panelTrangThai.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelTieuDe;
    private Panel panelTrangThai;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Label lblTenDangNhap;
    private Label lblMatKhau;
    private Label lblNhanVien;
    private Label lblVaiTro;
    private Label lblTrangThai;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private TextBox txtTenDangNhap;
    private TextBox txtMatKhau;
    private ComboBox cboNhanVien;
    private ComboBox cboVaiTro;
    private FlowLayoutPanel flowThaoTac;
    private Button btnTaoMoi;
    private Button btnLuu;
    private Button btnDatLaiMatKhau;
    private Button btnKhoaMo;
    private DataGridView dgvTaiKhoan;
    private DataGridViewTextBoxColumn colMaTK;
    private DataGridViewTextBoxColumn colTenDangNhap;
    private DataGridViewTextBoxColumn colNhanVien;
    private DataGridViewTextBoxColumn colVaiTro;
    private DataGridViewTextBoxColumn colTrangThai;
}

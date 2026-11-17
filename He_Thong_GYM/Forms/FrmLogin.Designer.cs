namespace He_Thong_GYM.Forms;

partial class FrmLogin
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
        panelThuongHieu = new Panel();
        lblPhuDe = new Label();
        lblThuongHieu = new Label();
        panelDangNhap = new Panel();
        chkHienMatKhau = new CheckBox();
        lblThongBao = new Label();
        btnDangNhap = new Button();
        txtMatKhau = new TextBox();
        lblMatKhau = new Label();
        txtTenDangNhap = new TextBox();
        lblTenDangNhap = new Label();
        lblTieuDe = new Label();
        panelThuongHieu.SuspendLayout();
        panelDangNhap.SuspendLayout();
        SuspendLayout();
        // panelThuongHieu
        panelThuongHieu.BackColor = Color.FromArgb(30, 64, 175);
        panelThuongHieu.Controls.Add(lblPhuDe);
        panelThuongHieu.Controls.Add(lblThuongHieu);
        panelThuongHieu.Dock = DockStyle.Left;
        panelThuongHieu.Location = new Point(0, 0);
        panelThuongHieu.Name = "panelThuongHieu";
        panelThuongHieu.Size = new Size(305, 390);
        panelThuongHieu.TabIndex = 0;
        // lblThuongHieu
        lblThuongHieu.AutoSize = true;
        lblThuongHieu.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
        lblThuongHieu.ForeColor = Color.White;
        lblThuongHieu.Location = new Point(53, 133);
        lblThuongHieu.Name = "lblThuongHieu";
        lblThuongHieu.Size = new Size(210, 46);
        lblThuongHieu.TabIndex = 0;
        lblThuongHieu.Text = "GYM PRO";
        // lblPhuDe
        lblPhuDe.AutoSize = true;
        lblPhuDe.ForeColor = Color.FromArgb(219, 234, 254);
        lblPhuDe.Location = new Point(58, 191);
        lblPhuDe.Name = "lblPhuDe";
        lblPhuDe.Size = new Size(177, 15);
        lblPhuDe.TabIndex = 1;
        lblPhuDe.Text = "Quản lý vận hành thông minh";
        // panelDangNhap
        panelDangNhap.BackColor = Color.White;
        panelDangNhap.Controls.Add(chkHienMatKhau);
        panelDangNhap.Controls.Add(lblThongBao);
        panelDangNhap.Controls.Add(btnDangNhap);
        panelDangNhap.Controls.Add(txtMatKhau);
        panelDangNhap.Controls.Add(lblMatKhau);
        panelDangNhap.Controls.Add(txtTenDangNhap);
        panelDangNhap.Controls.Add(lblTenDangNhap);
        panelDangNhap.Controls.Add(lblTieuDe);
        panelDangNhap.Dock = DockStyle.Fill;
        panelDangNhap.Location = new Point(305, 0);
        panelDangNhap.Name = "panelDangNhap";
        panelDangNhap.Padding = new Padding(48);
        panelDangNhap.Size = new Size(445, 390);
        panelDangNhap.TabIndex = 1;
        // lblTieuDe
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(48, 54);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(160, 32);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "Đăng nhập";
        // lblTenDangNhap
        lblTenDangNhap.AutoSize = true;
        lblTenDangNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTenDangNhap.Location = new Point(51, 116);
        lblTenDangNhap.Name = "lblTenDangNhap";
        lblTenDangNhap.Size = new Size(98, 15);
        lblTenDangNhap.TabIndex = 1;
        lblTenDangNhap.Text = "Tên đăng nhập";
        // txtTenDangNhap
        txtTenDangNhap.Location = new Point(51, 139);
        txtTenDangNhap.Name = "txtTenDangNhap";
        txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
        txtTenDangNhap.Size = new Size(340, 23);
        txtTenDangNhap.TabIndex = 0;
        // lblMatKhau
        lblMatKhau.AutoSize = true;
        lblMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMatKhau.Location = new Point(51, 184);
        lblMatKhau.Name = "lblMatKhau";
        lblMatKhau.Size = new Size(63, 15);
        lblMatKhau.TabIndex = 3;
        lblMatKhau.Text = "Mật khẩu";
        // txtMatKhau
        txtMatKhau.Location = new Point(51, 207);
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.PlaceholderText = "Nhập mật khẩu";
        txtMatKhau.Size = new Size(340, 23);
        txtMatKhau.TabIndex = 1;
        txtMatKhau.UseSystemPasswordChar = true;
        // chkHienMatKhau
        chkHienMatKhau.AutoSize = true;
        chkHienMatKhau.ForeColor = Color.FromArgb(71, 85, 105);
        chkHienMatKhau.Location = new Point(51, 243);
        chkHienMatKhau.Name = "chkHienMatKhau";
        chkHienMatKhau.Size = new Size(112, 19);
        chkHienMatKhau.TabIndex = 2;
        chkHienMatKhau.Text = "Hiện mật khẩu";
        chkHienMatKhau.UseVisualStyleBackColor = true;
        chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
        // btnDangNhap
        btnDangNhap.BackColor = Color.FromArgb(37, 99, 235);
        btnDangNhap.Cursor = Cursors.Hand;
        btnDangNhap.FlatAppearance.BorderSize = 0;
        btnDangNhap.FlatStyle = FlatStyle.Flat;
        btnDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnDangNhap.ForeColor = Color.White;
        btnDangNhap.Location = new Point(51, 286);
        btnDangNhap.Name = "btnDangNhap";
        btnDangNhap.Size = new Size(340, 40);
        btnDangNhap.TabIndex = 3;
        btnDangNhap.Text = "ĐĂNG NHẬP";
        btnDangNhap.UseVisualStyleBackColor = false;
        btnDangNhap.Click += btnDangNhap_Click;
        // lblThongBao
        lblThongBao.ForeColor = Color.FromArgb(185, 28, 28);
        lblThongBao.Location = new Point(51, 337);
        lblThongBao.Name = "lblThongBao";
        lblThongBao.Size = new Size(340, 36);
        lblThongBao.TabIndex = 7;
        // FrmLogin
        AcceptButton = btnDangNhap;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(750, 390);
        Controls.Add(panelDangNhap);
        Controls.Add(panelThuongHieu);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Gym Pro - Đăng nhập";
        panelThuongHieu.ResumeLayout(false);
        panelThuongHieu.PerformLayout();
        panelDangNhap.ResumeLayout(false);
        panelDangNhap.PerformLayout();
        ResumeLayout(false);
    }
    #endregion

    private Panel panelThuongHieu;
    private Label lblPhuDe;
    private Label lblThuongHieu;
    private Panel panelDangNhap;
    private CheckBox chkHienMatKhau;
    private Label lblThongBao;
    private Button btnDangNhap;
    private TextBox txtMatKhau;
    private Label lblMatKhau;
    private TextBox txtTenDangNhap;
    private Label lblTenDangNhap;
    private Label lblTieuDe;
}

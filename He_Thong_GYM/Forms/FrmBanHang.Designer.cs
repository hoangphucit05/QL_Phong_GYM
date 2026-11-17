namespace He_Thong_GYM.Forms;

partial class FrmBanHang
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
        panelNoiDung = new Panel();
        tableNhap = new TableLayoutPanel();
        lblThanhVien = new Label();
        cboThanhVien = new ComboBox();
        lblSanPham = new Label();
        cboSanPham = new ComboBox();
        lblSoLuong = new Label();
        nudSoLuong = new NumericUpDown();
        lblTien = new Label();
        lblThanhTien = new Label();
        panelNut = new FlowLayoutPanel();
        btnBan = new Button();
        btnHuy = new Button();
        panelTieuDe.SuspendLayout();
        panelNoiDung.SuspendLayout();
        tableNhap.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        panelNut.SuspendLayout();
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
        panelTieuDe.Padding = new Padding(24, 16, 24, 12);
        panelTieuDe.Size = new Size(560, 75);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(22, 44);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(256, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Lập hóa đơn xuất bán sản phẩm và trừ tồn kho.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(20, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(168, 28);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BÁN SẢN PHẨM";
        // 
        // panelNoiDung
        // 
        panelNoiDung.Controls.Add(tableNhap);
        panelNoiDung.Dock = DockStyle.Top;
        panelNoiDung.Location = new Point(0, 75);
        panelNoiDung.Name = "panelNoiDung";
        panelNoiDung.Padding = new Padding(24, 16, 24, 8);
        panelNoiDung.Size = new Size(560, 185);
        panelNoiDung.TabIndex = 1;
        // 
        // tableNhap
        // 
        tableNhap.ColumnCount = 2;
        tableNhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableNhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableNhap.Controls.Add(lblThanhVien, 0, 0);
        tableNhap.Controls.Add(cboThanhVien, 1, 0);
        tableNhap.Controls.Add(lblSanPham, 0, 1);
        tableNhap.Controls.Add(cboSanPham, 1, 1);
        tableNhap.Controls.Add(lblSoLuong, 0, 2);
        tableNhap.Controls.Add(nudSoLuong, 1, 2);
        tableNhap.Controls.Add(lblTien, 0, 3);
        tableNhap.Controls.Add(lblThanhTien, 1, 3);
        tableNhap.Dock = DockStyle.Fill;
        tableNhap.Location = new Point(24, 16);
        tableNhap.Name = "tableNhap";
        tableNhap.RowCount = 4;
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        tableNhap.Size = new Size(512, 161);
        tableNhap.TabIndex = 0;
        // 
        // lblThanhVien
        // 
        lblThanhVien.AutoSize = true;
        lblThanhVien.Dock = DockStyle.Fill;
        lblThanhVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblThanhVien.Location = new Point(3, 0);
        lblThanhVien.Name = "lblThanhVien";
        lblThanhVien.Size = new Size(104, 38);
        lblThanhVien.TabIndex = 0;
        lblThanhVien.Text = "Thành viên:";
        lblThanhVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThanhVien
        // 
        cboThanhVien.Dock = DockStyle.Fill;
        cboThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThanhVien.FormattingEnabled = true;
        cboThanhVien.Location = new Point(113, 6);
        cboThanhVien.Margin = new Padding(3, 6, 3, 3);
        cboThanhVien.Name = "cboThanhVien";
        cboThanhVien.Size = new Size(396, 23);
        cboThanhVien.TabIndex = 1;
        // 
        // lblSanPham
        // 
        lblSanPham.AutoSize = true;
        lblSanPham.Dock = DockStyle.Fill;
        lblSanPham.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSanPham.Location = new Point(3, 38);
        lblSanPham.Name = "lblSanPham";
        lblSanPham.Size = new Size(104, 38);
        lblSanPham.TabIndex = 2;
        lblSanPham.Text = "Sản phẩm:";
        lblSanPham.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboSanPham
        // 
        cboSanPham.Dock = DockStyle.Fill;
        cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSanPham.FormattingEnabled = true;
        cboSanPham.Location = new Point(113, 44);
        cboSanPham.Margin = new Padding(3, 6, 3, 3);
        cboSanPham.Name = "cboSanPham";
        cboSanPham.Size = new Size(396, 23);
        cboSanPham.TabIndex = 3;
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Dock = DockStyle.Fill;
        lblSoLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSoLuong.Location = new Point(3, 76);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(104, 38);
        lblSoLuong.TabIndex = 4;
        lblSoLuong.Text = "Số lượng:";
        lblSoLuong.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSoLuong
        // 
        nudSoLuong.Location = new Point(113, 82);
        nudSoLuong.Margin = new Padding(3, 6, 3, 3);
        nudSoLuong.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(140, 23);
        nudSoLuong.TabIndex = 5;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblTien
        // 
        lblTien.AutoSize = true;
        lblTien.Dock = DockStyle.Fill;
        lblTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTien.Location = new Point(3, 114);
        lblTien.Name = "lblTien";
        lblTien.Size = new Size(104, 47);
        lblTien.TabIndex = 6;
        lblTien.Text = "Thành tiền:";
        lblTien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblThanhTien
        // 
        lblThanhTien.AutoSize = true;
        lblThanhTien.Dock = DockStyle.Fill;
        lblThanhTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblThanhTien.ForeColor = Color.FromArgb(5, 150, 105);
        lblThanhTien.Location = new Point(113, 114);
        lblThanhTien.Name = "lblThanhTien";
        lblThanhTien.Size = new Size(396, 47);
        lblThanhTien.TabIndex = 7;
        lblThanhTien.Text = "0 đ";
        lblThanhTien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelNut
        // 
        panelNut.Controls.Add(btnBan);
        panelNut.Controls.Add(btnHuy);
        panelNut.Dock = DockStyle.Bottom;
        panelNut.FlowDirection = FlowDirection.RightToLeft;
        panelNut.Location = new Point(0, 275);
        panelNut.Name = "panelNut";
        panelNut.Padding = new Padding(20, 10, 24, 14);
        panelNut.Size = new Size(560, 60);
        panelNut.TabIndex = 2;
        // 
        // btnBan
        // 
        btnBan.BackColor = Color.FromArgb(5, 150, 105);
        btnBan.Cursor = Cursors.Hand;
        btnBan.FlatAppearance.BorderSize = 0;
        btnBan.FlatStyle = FlatStyle.Flat;
        btnBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBan.ForeColor = Color.White;
        btnBan.Location = new Point(396, 10);
        btnBan.Margin = new Padding(0);
        btnBan.Name = "btnBan";
        btnBan.Size = new Size(120, 36);
        btnBan.TabIndex = 0;
        btnBan.Text = "Tạo hóa đơn";
        btnBan.UseVisualStyleBackColor = false;
        btnBan.Click += btnBan_Click;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(226, 232, 240);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHuy.ForeColor = Color.FromArgb(71, 85, 105);
        btnHuy.Location = new Point(276, 10);
        btnHuy.Margin = new Padding(0, 0, 10, 0);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(110, 36);
        btnHuy.TabIndex = 1;
        btnHuy.Text = "Đóng";
        btnHuy.UseVisualStyleBackColor = false;
        btnHuy.Click += btnHuy_Click;
        // 
        // FrmBanHang
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(560, 335);
        Controls.Add(panelNut);
        Controls.Add(panelNoiDung);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmBanHang";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Bán sản phẩm";
        Load += FrmBanHang_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        panelNoiDung.ResumeLayout(false);
        tableNhap.ResumeLayout(false);
        tableNhap.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
        panelNut.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Panel panelNoiDung;
    private TableLayoutPanel tableNhap;
    private Label lblThanhVien;
    private ComboBox cboThanhVien;
    private Label lblSanPham;
    private ComboBox cboSanPham;
    private Label lblSoLuong;
    private NumericUpDown nudSoLuong;
    private Label lblTien;
    private Label lblThanhTien;
    private FlowLayoutPanel panelNut;
    private Button btnBan;
    private Button btnHuy;
}

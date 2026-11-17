namespace He_Thong_GYM.Forms;

partial class FrmThanhToan
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
        panelNoiDung = new Panel();
        tableNhap = new TableLayoutPanel();
        lblHoaDon = new Label();
        cboHoaDon = new ComboBox();
        lblTrangThaiCongNo = new Label();
        lblCongNo = new Label();
        lblSoTien = new Label();
        nudSoTien = new NumericUpDown();
        lblPhuongThuc = new Label();
        cboPhuongThuc = new ComboBox();
        panelNut = new FlowLayoutPanel();
        btnHuy = new Button();
        btnHoanTien = new Button();
        btnThanhToan = new Button();
        panelTieuDe.SuspendLayout();
        panelNoiDung.SuspendLayout();
        tableNhap.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoTien).BeginInit();
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
        panelTieuDe.Size = new Size(600, 75);
        panelTieuDe.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(20, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(260, 28);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "THANH TOÁN / HOÀN TIỀN";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(22, 44);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(330, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Ghi nhận thanh toán hóa đơn hoặc hoàn tiền cho khách hàng.";
        // 
        // panelNoiDung
        // 
        panelNoiDung.Controls.Add(tableNhap);
        panelNoiDung.Dock = DockStyle.Top;
        panelNoiDung.Location = new Point(0, 75);
        panelNoiDung.Name = "panelNoiDung";
        panelNoiDung.Padding = new Padding(24, 16, 24, 8);
        panelNoiDung.Size = new Size(600, 205);
        panelNoiDung.TabIndex = 1;
        // 
        // tableNhap
        // 
        tableNhap.ColumnCount = 2;
        tableNhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableNhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableNhap.Controls.Add(lblHoaDon, 0, 0);
        tableNhap.Controls.Add(cboHoaDon, 1, 0);
        tableNhap.Controls.Add(lblTrangThaiCongNo, 0, 1);
        tableNhap.Controls.Add(lblCongNo, 1, 1);
        tableNhap.Controls.Add(lblSoTien, 0, 2);
        tableNhap.Controls.Add(nudSoTien, 1, 2);
        tableNhap.Controls.Add(lblPhuongThuc, 0, 3);
        tableNhap.Controls.Add(cboPhuongThuc, 1, 3);
        tableNhap.Dock = DockStyle.Fill;
        tableNhap.Location = new Point(24, 16);
        tableNhap.Name = "tableNhap";
        tableNhap.RowCount = 4;
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableNhap.Size = new Size(552, 181);
        tableNhap.TabIndex = 0;
        // 
        // lblHoaDon
        // 
        lblHoaDon.AutoSize = true;
        lblHoaDon.Dock = DockStyle.Fill;
        lblHoaDon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHoaDon.Location = new Point(3, 0);
        lblHoaDon.Name = "lblHoaDon";
        lblHoaDon.Size = new Size(124, 38);
        lblHoaDon.TabIndex = 0;
        lblHoaDon.Text = "Hóa đơn:";
        lblHoaDon.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboHoaDon
        // 
        cboHoaDon.Dock = DockStyle.Fill;
        cboHoaDon.DropDownStyle = ComboBoxStyle.DropDownList;
        cboHoaDon.FormattingEnabled = true;
        cboHoaDon.Location = new Point(133, 6);
        cboHoaDon.Margin = new Padding(3, 6, 3, 3);
        cboHoaDon.Name = "cboHoaDon";
        cboHoaDon.Size = new Size(416, 23);
        cboHoaDon.TabIndex = 1;
        // 
        // lblTrangThaiCongNo
        // 
        lblTrangThaiCongNo.AutoSize = true;
        lblTrangThaiCongNo.Dock = DockStyle.Fill;
        lblTrangThaiCongNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThaiCongNo.Location = new Point(3, 38);
        lblTrangThaiCongNo.Name = "lblTrangThaiCongNo";
        lblTrangThaiCongNo.Size = new Size(124, 45);
        lblTrangThaiCongNo.TabIndex = 2;
        lblTrangThaiCongNo.Text = "Tình trạng:";
        lblTrangThaiCongNo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCongNo
        // 
        lblCongNo.AutoSize = true;
        lblCongNo.Dock = DockStyle.Fill;
        lblCongNo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblCongNo.ForeColor = Color.FromArgb(30, 41, 59);
        lblCongNo.Location = new Point(133, 38);
        lblCongNo.Name = "lblCongNo";
        lblCongNo.Size = new Size(416, 45);
        lblCongNo.TabIndex = 3;
        lblCongNo.Text = "Chọn hóa đơn để xem công nợ...";
        lblCongNo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSoTien
        // 
        lblSoTien.AutoSize = true;
        lblSoTien.Dock = DockStyle.Fill;
        lblSoTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSoTien.Location = new Point(3, 83);
        lblSoTien.Name = "lblSoTien";
        lblSoTien.Size = new Size(124, 38);
        lblSoTien.TabIndex = 4;
        lblSoTien.Text = "Số tiền (VNĐ):";
        lblSoTien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSoTien
        // 
        nudSoTien.DecimalPlaces = 0;
        nudSoTien.Location = new Point(133, 89);
        nudSoTien.Margin = new Padding(3, 6, 3, 3);
        nudSoTien.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudSoTien.Name = "nudSoTien";
        nudSoTien.Size = new Size(200, 23);
        nudSoTien.TabIndex = 5;
        nudSoTien.ThousandsSeparator = true;
        // 
        // lblPhuongThuc
        // 
        lblPhuongThuc.AutoSize = true;
        lblPhuongThuc.Dock = DockStyle.Fill;
        lblPhuongThuc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPhuongThuc.Location = new Point(3, 121);
        lblPhuongThuc.Name = "lblPhuongThuc";
        lblPhuongThuc.Size = new Size(124, 60);
        lblPhuongThuc.TabIndex = 6;
        lblPhuongThuc.Text = "Phương thức:";
        lblPhuongThuc.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboPhuongThuc
        // 
        cboPhuongThuc.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPhuongThuc.FormattingEnabled = true;
        cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });
        cboPhuongThuc.Location = new Point(133, 127);
        cboPhuongThuc.Margin = new Padding(3, 6, 3, 3);
        cboPhuongThuc.Name = "cboPhuongThuc";
        cboPhuongThuc.Size = new Size(200, 23);
        cboPhuongThuc.TabIndex = 7;
        // 
        // panelNut
        // 
        panelNut.Controls.Add(btnHuy);
        panelNut.Controls.Add(btnHoanTien);
        panelNut.Controls.Add(btnThanhToan);
        panelNut.Dock = DockStyle.Bottom;
        panelNut.FlowDirection = FlowDirection.RightToLeft;
        panelNut.Location = new Point(0, 290);
        panelNut.Name = "panelNut";
        panelNut.Padding = new Padding(20, 10, 24, 14);
        panelNut.Size = new Size(600, 60);
        panelNut.TabIndex = 2;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(226, 232, 240);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHuy.ForeColor = Color.FromArgb(71, 85, 105);
        btnHuy.Location = new Point(446, 10);
        btnHuy.Margin = new Padding(0, 0, 10, 0);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(100, 36);
        btnHuy.TabIndex = 2;
        btnHuy.Text = "Đóng";
        btnHuy.UseVisualStyleBackColor = false;
        btnHuy.Click += btnHuy_Click;
        // 
        // btnHoanTien
        // 
        btnHoanTien.BackColor = Color.FromArgb(185, 28, 28);
        btnHoanTien.Cursor = Cursors.Hand;
        btnHoanTien.FlatAppearance.BorderSize = 0;
        btnHoanTien.FlatStyle = FlatStyle.Flat;
        btnHoanTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHoanTien.ForeColor = Color.White;
        btnHoanTien.Location = new Point(316, 10);
        btnHoanTien.Margin = new Padding(0, 0, 10, 0);
        btnHoanTien.Name = "btnHoanTien";
        btnHoanTien.Size = new Size(120, 36);
        btnHoanTien.TabIndex = 1;
        btnHoanTien.Text = "Hoàn tiền";
        btnHoanTien.UseVisualStyleBackColor = false;
        btnHoanTien.Click += btnHoanTien_Click;
        // 
        // btnThanhToan
        // 
        btnThanhToan.BackColor = Color.FromArgb(5, 150, 105);
        btnThanhToan.Cursor = Cursors.Hand;
        btnThanhToan.FlatAppearance.BorderSize = 0;
        btnThanhToan.FlatStyle = FlatStyle.Flat;
        btnThanhToan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThanhToan.ForeColor = Color.White;
        btnThanhToan.Location = new Point(176, 10);
        btnThanhToan.Margin = new Padding(0, 0, 10, 0);
        btnThanhToan.Name = "btnThanhToan";
        btnThanhToan.Size = new Size(130, 36);
        btnThanhToan.TabIndex = 0;
        btnThanhToan.Text = "Xác nhận thu tiền";
        btnThanhToan.UseVisualStyleBackColor = false;
        btnThanhToan.Click += btnThanhToan_Click;
        // 
        // FrmThanhToan
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(600, 350);
        Controls.Add(panelNut);
        Controls.Add(panelNoiDung);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmThanhToan";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Thanh toán hóa đơn";
        Load += FrmThanhToan_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        panelNoiDung.ResumeLayout(false);
        tableNhap.ResumeLayout(false);
        tableNhap.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoTien).EndInit();
        panelNut.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Panel panelNoiDung;
    private TableLayoutPanel tableNhap;
    private Label lblHoaDon;
    private ComboBox cboHoaDon;
    private Label lblTrangThaiCongNo;
    private Label lblCongNo;
    private Label lblSoTien;
    private NumericUpDown nudSoTien;
    private Label lblPhuongThuc;
    private ComboBox cboPhuongThuc;
    private FlowLayoutPanel panelNut;
    private Button btnThanhToan;
    private Button btnHoanTien;
    private Button btnHuy;
}

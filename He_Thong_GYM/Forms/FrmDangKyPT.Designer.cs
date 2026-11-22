namespace He_Thong_GYM.Forms;

partial class FrmDangKyPT
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
        panel = new Panel();
        title = new Label();
        group = new GroupBox();
        table = new TableLayoutPanel();
        l1 = new Label();
        cboThanhVien = new ComboBox();
        l2 = new Label();
        cboHLV = new ComboBox();
        l3 = new Label();
        cboKhuyenMai = new ComboBox();
        l4 = new Label();
        nudSoBuoi = new NumericUpDown();
        l5 = new Label();
        dtpBatDau = new DateTimePicker();
        l6 = new Label();
        dtpKetThuc = new DateTimePicker();
        l7 = new Label();
        lblTongTien = new Label();
        btnDangKy = new Button();
        panelLocLichSu = new FlowLayoutPanel();
        lblTimKiemLichSu = new Label();
        txtTimKiemLichSu = new TextBox();
        lblLocTrangThai = new Label();
        cboLocTrangThai = new ComboBox();
        btnLamMoiLichSu = new Button();
        lblThongKeLichSu = new Label();
        dgvLichSu = new DataGridView();
        panel.SuspendLayout();
        group.SuspendLayout();
        table.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoBuoi).BeginInit();
        panelLocLichSu.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
        SuspendLayout();
        // 
        // panel
        // 
        panel.BackColor = Color.White;
        panel.Controls.Add(title);
        panel.Dock = DockStyle.Top;
        panel.Location = new Point(0, 0);
        panel.Name = "panel";
        panel.Size = new Size(1200, 82);
        panel.TabIndex = 0;
        // 
        // title
        // 
        title.AutoSize = true;
        title.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        title.ForeColor = Color.FromArgb(15, 23, 42);
        title.Location = new Point(24, 22);
        title.Name = "title";
        title.Size = new Size(362, 30);
        title.TabIndex = 0;
        title.Text = "ĐĂNG KÝ HUẤN LUYỆN VIÊN (PT)";
        // 
        // group
        // 
        group.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        group.BackColor = Color.White;
        group.Controls.Add(table);
        group.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        group.ForeColor = Color.FromArgb(30, 41, 59);
        group.Location = new Point(24, 100);
        group.Name = "group";
        group.Padding = new Padding(16);
        group.Size = new Size(1152, 210);
        group.TabIndex = 1;
        group.TabStop = false;
        group.Text = "Thông tin đăng ký PT";
        // 
        // table
        // 
        table.ColumnCount = 4;
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        table.Controls.Add(l1, 0, 0);
        table.Controls.Add(cboThanhVien, 1, 0);
        table.Controls.Add(l2, 2, 0);
        table.Controls.Add(cboHLV, 3, 0);
        table.Controls.Add(l3, 0, 1);
        table.Controls.Add(cboKhuyenMai, 1, 1);
        table.Controls.Add(l4, 2, 1);
        table.Controls.Add(nudSoBuoi, 3, 1);
        table.Controls.Add(l5, 0, 2);
        table.Controls.Add(dtpBatDau, 1, 2);
        table.Controls.Add(l6, 2, 2);
        table.Controls.Add(dtpKetThuc, 3, 2);
        table.Controls.Add(l7, 0, 3);
        table.Controls.Add(lblTongTien, 1, 3);
        table.Dock = DockStyle.Fill;
        table.Font = new Font("Segoe UI", 9F);
        table.Location = new Point(16, 34);
        table.Name = "table";
        table.RowCount = 4;
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        table.Size = new Size(1120, 160);
        table.TabIndex = 0;
        // 
        // l1
        // 
        l1.Dock = DockStyle.Fill;
        l1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l1.Location = new Point(3, 0);
        l1.Name = "l1";
        l1.Size = new Size(114, 40);
        l1.TabIndex = 0;
        l1.Text = "Thành viên:";
        l1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThanhVien
        // 
        cboThanhVien.Dock = DockStyle.Fill;
        cboThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThanhVien.Location = new Point(123, 6);
        cboThanhVien.Margin = new Padding(3, 6, 12, 6);
        cboThanhVien.Name = "cboThanhVien";
        cboThanhVien.Size = new Size(425, 23);
        cboThanhVien.TabIndex = 1;
        // 
        // l2
        // 
        l2.Dock = DockStyle.Fill;
        l2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l2.Location = new Point(563, 0);
        l2.Name = "l2";
        l2.Size = new Size(114, 40);
        l2.TabIndex = 2;
        l2.Text = "Huấn luyện viên:";
        l2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboHLV
        // 
        cboHLV.Dock = DockStyle.Fill;
        cboHLV.DropDownStyle = ComboBoxStyle.DropDownList;
        cboHLV.Location = new Point(683, 6);
        cboHLV.Margin = new Padding(3, 6, 12, 6);
        cboHLV.Name = "cboHLV";
        cboHLV.Size = new Size(425, 23);
        cboHLV.TabIndex = 3;
        cboHLV.SelectedIndexChanged += GiaThayDoi;
        // 
        // l3
        // 
        l3.Dock = DockStyle.Fill;
        l3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l3.Location = new Point(3, 40);
        l3.Name = "l3";
        l3.Size = new Size(114, 40);
        l3.TabIndex = 4;
        l3.Text = "Khuyến mãi:";
        l3.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboKhuyenMai
        // 
        cboKhuyenMai.Dock = DockStyle.Fill;
        cboKhuyenMai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhuyenMai.Location = new Point(123, 46);
        cboKhuyenMai.Margin = new Padding(3, 6, 12, 6);
        cboKhuyenMai.Name = "cboKhuyenMai";
        cboKhuyenMai.Size = new Size(425, 23);
        cboKhuyenMai.TabIndex = 5;
        cboKhuyenMai.SelectedIndexChanged += GiaThayDoi;
        // 
        // l4
        // 
        l4.Dock = DockStyle.Fill;
        l4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l4.Location = new Point(563, 40);
        l4.Name = "l4";
        l4.Size = new Size(114, 40);
        l4.TabIndex = 6;
        l4.Text = "Số buổi:";
        l4.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSoBuoi
        // 
        nudSoBuoi.Dock = DockStyle.Fill;
        nudSoBuoi.Location = new Point(683, 46);
        nudSoBuoi.Margin = new Padding(3, 6, 12, 6);
        nudSoBuoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoBuoi.Name = "nudSoBuoi";
        nudSoBuoi.Size = new Size(425, 23);
        nudSoBuoi.TabIndex = 7;
        nudSoBuoi.Value = new decimal(new int[] { 12, 0, 0, 0 });
        nudSoBuoi.ValueChanged += GiaThayDoi;
        // 
        // l5
        // 
        l5.Dock = DockStyle.Fill;
        l5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l5.Location = new Point(3, 80);
        l5.Name = "l5";
        l5.Size = new Size(114, 40);
        l5.TabIndex = 8;
        l5.Text = "Ngày bắt đầu:";
        l5.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpBatDau
        // 
        dtpBatDau.Dock = DockStyle.Fill;
        dtpBatDau.Format = DateTimePickerFormat.Short;
        dtpBatDau.Location = new Point(123, 86);
        dtpBatDau.Margin = new Padding(3, 6, 12, 6);
        dtpBatDau.Name = "dtpBatDau";
        dtpBatDau.Size = new Size(425, 23);
        dtpBatDau.TabIndex = 9;
        dtpBatDau.ValueChanged += GiaThayDoi;
        // 
        // l6
        // 
        l6.Dock = DockStyle.Fill;
        l6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l6.Location = new Point(563, 80);
        l6.Name = "l6";
        l6.Size = new Size(114, 40);
        l6.TabIndex = 10;
        l6.Text = "Ngày kết thúc:";
        l6.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpKetThuc
        // 
        dtpKetThuc.Dock = DockStyle.Fill;
        dtpKetThuc.Enabled = false;
        dtpKetThuc.Format = DateTimePickerFormat.Short;
        dtpKetThuc.Location = new Point(683, 86);
        dtpKetThuc.Margin = new Padding(3, 6, 12, 6);
        dtpKetThuc.Name = "dtpKetThuc";
        dtpKetThuc.Size = new Size(425, 23);
        dtpKetThuc.TabIndex = 11;
        // 
        // l7
        // 
        l7.Dock = DockStyle.Fill;
        l7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l7.Location = new Point(3, 120);
        l7.Name = "l7";
        l7.Size = new Size(114, 40);
        l7.TabIndex = 12;
        l7.Text = "Tổng tiền:";
        l7.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTongTien
        // 
        table.SetColumnSpan(lblTongTien, 3);
        lblTongTien.Dock = DockStyle.Fill;
        lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTongTien.ForeColor = Color.FromArgb(5, 150, 105);
        lblTongTien.Location = new Point(123, 120);
        lblTongTien.Name = "lblTongTien";
        lblTongTien.Size = new Size(994, 40);
        lblTongTien.TabIndex = 13;
        lblTongTien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnDangKy
        // 
        btnDangKy.BackColor = Color.FromArgb(37, 99, 235);
        btnDangKy.Cursor = Cursors.Hand;
        btnDangKy.FlatAppearance.BorderSize = 0;
        btnDangKy.FlatStyle = FlatStyle.Flat;
        btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnDangKy.ForeColor = Color.White;
        btnDangKy.Location = new Point(24, 324);
        btnDangKy.Name = "btnDangKy";
        btnDangKy.Size = new Size(160, 36);
        btnDangKy.TabIndex = 2;
        btnDangKy.Text = "Đăng ký PT";
        btnDangKy.UseVisualStyleBackColor = false;
        btnDangKy.Click += btnDangKy_Click;
        // 
        // panelLocLichSu
        // 
        panelLocLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelLocLichSu.Controls.Add(lblTimKiemLichSu);
        panelLocLichSu.Controls.Add(txtTimKiemLichSu);
        panelLocLichSu.Controls.Add(lblLocTrangThai);
        panelLocLichSu.Controls.Add(cboLocTrangThai);
        panelLocLichSu.Controls.Add(btnLamMoiLichSu);
        panelLocLichSu.Controls.Add(lblThongKeLichSu);
        panelLocLichSu.Location = new Point(200, 324);
        panelLocLichSu.Name = "panelLocLichSu";
        panelLocLichSu.Size = new Size(976, 38);
        panelLocLichSu.TabIndex = 4;
        // 
        // lblTimKiemLichSu
        // 
        lblTimKiemLichSu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiemLichSu.ForeColor = Color.FromArgb(30, 41, 59);
        lblTimKiemLichSu.Location = new Point(3, 0);
        lblTimKiemLichSu.Name = "lblTimKiemLichSu";
        lblTimKiemLichSu.Size = new Size(70, 36);
        lblTimKiemLichSu.TabIndex = 0;
        lblTimKiemLichSu.Text = "Tìm kiếm:";
        lblTimKiemLichSu.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtTimKiemLichSu
        // 
        txtTimKiemLichSu.Location = new Point(79, 6);
        txtTimKiemLichSu.Margin = new Padding(3, 6, 12, 3);
        txtTimKiemLichSu.Name = "txtTimKiemLichSu";
        txtTimKiemLichSu.PlaceholderText = "Tên hội viên, huấn luyện viên...";
        txtTimKiemLichSu.Size = new Size(220, 23);
        txtTimKiemLichSu.TabIndex = 1;
        txtTimKiemLichSu.TextChanged += LocLichSuThayDoi;
        // 
        // lblLocTrangThai
        // 
        lblLocTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLocTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
        lblLocTrangThai.Location = new Point(314, 0);
        lblLocTrangThai.Name = "lblLocTrangThai";
        lblLocTrangThai.Size = new Size(72, 36);
        lblLocTrangThai.TabIndex = 2;
        lblLocTrangThai.Text = "Trạng thái:";
        lblLocTrangThai.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cboLocTrangThai
        // 
        cboLocTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocTrangThai.FormattingEnabled = true;
        cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Chờ thanh toán", "Đang học", "Hoàn thành", "Hủy" });
        cboLocTrangThai.Location = new Point(392, 6);
        cboLocTrangThai.Margin = new Padding(3, 6, 12, 3);
        cboLocTrangThai.Name = "cboLocTrangThai";
        cboLocTrangThai.Size = new Size(150, 23);
        cboLocTrangThai.TabIndex = 3;
        cboLocTrangThai.SelectedIndexChanged += LocLichSuThayDoi;
        // 
        // btnLamMoiLichSu
        // 
        btnLamMoiLichSu.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoiLichSu.Cursor = Cursors.Hand;
        btnLamMoiLichSu.FlatAppearance.BorderSize = 0;
        btnLamMoiLichSu.FlatStyle = FlatStyle.Flat;
        btnLamMoiLichSu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoiLichSu.ForeColor = Color.White;
        btnLamMoiLichSu.Location = new Point(557, 4);
        btnLamMoiLichSu.Margin = new Padding(3, 4, 12, 3);
        btnLamMoiLichSu.Name = "btnLamMoiLichSu";
        btnLamMoiLichSu.Size = new Size(100, 28);
        btnLamMoiLichSu.TabIndex = 4;
        btnLamMoiLichSu.Text = "Làm mới";
        btnLamMoiLichSu.UseVisualStyleBackColor = false;
        btnLamMoiLichSu.Click += btnLamMoiLichSu_Click;
        // 
        // lblThongKeLichSu
        // 
        lblThongKeLichSu.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKeLichSu.ForeColor = Color.FromArgb(100, 116, 139);
        lblThongKeLichSu.Location = new Point(672, 0);
        lblThongKeLichSu.Name = "lblThongKeLichSu";
        lblThongKeLichSu.Size = new Size(200, 36);
        lblThongKeLichSu.TabIndex = 5;
        lblThongKeLichSu.Text = "Hiển thị: 0 lượt";
        lblThongKeLichSu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvLichSu
        // 
        dgvLichSu.AllowUserToAddRows = false;
        dgvLichSu.AllowUserToDeleteRows = false;
        dgvLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLichSu.BackgroundColor = Color.White;
        dgvLichSu.BorderStyle = BorderStyle.None;
        dgvLichSu.ColumnHeadersHeight = 38;
        dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvLichSu.Location = new Point(24, 372);
        dgvLichSu.Name = "dgvLichSu";
        dgvLichSu.ReadOnly = true;
        dgvLichSu.RowHeadersVisible = false;
        dgvLichSu.RowTemplate.Height = 32;
        dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLichSu.Size = new Size(1152, 327);
        dgvLichSu.TabIndex = 3;
        // 
        // FrmDangKyPT
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(dgvLichSu);
        Controls.Add(panelLocLichSu);
        Controls.Add(btnDangKy);
        Controls.Add(group);
        Controls.Add(panel);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(850, 480);
        Name = "FrmDangKyPT";
        Text = "Đăng ký PT";
        Load += FrmDangKyPT_Load;
        panel.ResumeLayout(false);
        panel.PerformLayout();
        group.ResumeLayout(false);
        table.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudSoBuoi).EndInit();
        panelLocLichSu.ResumeLayout(false);
        panelLocLichSu.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel;
    private Label title;
    private Label l1;
    private Label l2;
    private Label l3;
    private Label l4;
    private Label l5;
    private Label l6;
    private Label l7;
    private Label lblTongTien;
    private GroupBox group;
    private TableLayoutPanel table;
    private ComboBox cboThanhVien;
    private ComboBox cboHLV;
    private ComboBox cboKhuyenMai;
    private NumericUpDown nudSoBuoi;
    private DateTimePicker dtpBatDau;
    private DateTimePicker dtpKetThuc;
    private Button btnDangKy;
    private FlowLayoutPanel panelLocLichSu;
    private Label lblTimKiemLichSu;
    private TextBox txtTimKiemLichSu;
    private Label lblLocTrangThai;
    private ComboBox cboLocTrangThai;
    private Button btnLamMoiLichSu;
    private Label lblThongKeLichSu;
    private DataGridView dgvLichSu;
}

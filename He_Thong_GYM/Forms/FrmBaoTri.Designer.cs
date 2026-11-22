namespace He_Thong_GYM.Forms;

partial class FrmBaoTri
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
        lblThietBi = new Label();
        cboThietBi = new ComboBox();
        lblNhanVien = new Label();
        cboNhanVien = new ComboBox();
        lblNoiDung = new Label();
        txtNoiDung = new TextBox();
        lblChiPhi = new Label();
        nudChiPhi = new NumericUpDown();
        lblNgayDuKien = new Label();
        dtpNgayDuKien = new DateTimePicker();
        lblHuongDan = new Label();
        flowThaoTac = new FlowLayoutPanel();
        btnTao = new Button();
        btnHoanTat = new Button();
        btnLamMoi = new Button();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        lblTrangThai = new Label();
        cboLocTrangThai = new ComboBox();
        lblThongKe = new Label();
        dgv = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudChiPhi).BeginInit();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
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
        lblMoTa.Size = new Size(317, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Lập phiếu bảo dưỡng, sửa chữa thiết bị và theo dõi chi phí.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(194, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BẢO TRÌ THIẾT BỊ";
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
        groupThongTin.Size = new Size(952, 140);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin phiếu bảo trì";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblThietBi, 0, 0);
        tableThongTin.Controls.Add(cboThietBi, 1, 0);
        tableThongTin.Controls.Add(lblNhanVien, 2, 0);
        tableThongTin.Controls.Add(cboNhanVien, 3, 0);
        tableThongTin.Controls.Add(lblNoiDung, 0, 1);
        tableThongTin.Controls.Add(txtNoiDung, 1, 1);
        tableThongTin.Controls.Add(lblChiPhi, 2, 1);
        tableThongTin.Controls.Add(nudChiPhi, 3, 1);
        tableThongTin.Controls.Add(lblNgayDuKien, 0, 2);
        tableThongTin.Controls.Add(dtpNgayDuKien, 1, 2);
        tableThongTin.Controls.Add(lblHuongDan, 2, 2);
        tableThongTin.SetColumnSpan(lblHuongDan, 2);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 30);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 3;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        tableThongTin.Size = new Size(920, 98);
        tableThongTin.TabIndex = 0;
        // 
        // lblThietBi
        // 
        lblThietBi.AutoSize = true;
        lblThietBi.Dock = DockStyle.Fill;
        lblThietBi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblThietBi.Location = new Point(3, 0);
        lblThietBi.Name = "lblThietBi";
        lblThietBi.Size = new Size(104, 31);
        lblThietBi.TabIndex = 0;
        lblThietBi.Text = "Thiết bị:";
        lblThietBi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThietBi
        // 
        cboThietBi.Dock = DockStyle.Fill;
        cboThietBi.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThietBi.FormattingEnabled = true;
        cboThietBi.Location = new Point(113, 3);
        cboThietBi.Name = "cboThietBi";
        cboThietBi.Size = new Size(339, 23);
        cboThietBi.TabIndex = 1;
        // 
        // lblNhanVien
        // 
        lblNhanVien.AutoSize = true;
        lblNhanVien.Dock = DockStyle.Fill;
        lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhanVien.Location = new Point(458, 0);
        lblNhanVien.Name = "lblNhanVien";
        lblNhanVien.Size = new Size(114, 31);
        lblNhanVien.TabIndex = 2;
        lblNhanVien.Text = "NV phụ trách:";
        lblNhanVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboNhanVien
        // 
        cboNhanVien.Dock = DockStyle.Fill;
        cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhanVien.FormattingEnabled = true;
        cboNhanVien.Location = new Point(578, 3);
        cboNhanVien.Name = "cboNhanVien";
        cboNhanVien.Size = new Size(339, 23);
        cboNhanVien.TabIndex = 3;
        // 
        // lblNoiDung
        // 
        lblNoiDung.AutoSize = true;
        lblNoiDung.Dock = DockStyle.Fill;
        lblNoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNoiDung.Location = new Point(3, 31);
        lblNoiDung.Name = "lblNoiDung";
        lblNoiDung.Size = new Size(104, 32);
        lblNoiDung.TabIndex = 4;
        lblNoiDung.Text = "Nội dung:";
        lblNoiDung.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtNoiDung
        // 
        txtNoiDung.Dock = DockStyle.Fill;
        txtNoiDung.Location = new Point(113, 34);
        txtNoiDung.Name = "txtNoiDung";
        txtNoiDung.PlaceholderText = "Mô tả nội dung bảo trì, linh kiện thay thế...";
        txtNoiDung.Size = new Size(339, 23);
        txtNoiDung.TabIndex = 5;
        // 
        // lblChiPhi
        // 
        lblChiPhi.AutoSize = true;
        lblChiPhi.Dock = DockStyle.Fill;
        lblChiPhi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblChiPhi.Location = new Point(458, 31);
        lblChiPhi.Name = "lblChiPhi";
        lblChiPhi.Size = new Size(114, 32);
        lblChiPhi.TabIndex = 6;
        lblChiPhi.Text = "Chi phí (VNĐ):";
        lblChiPhi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudChiPhi
        // 
        nudChiPhi.Dock = DockStyle.Fill;
        nudChiPhi.Location = new Point(578, 34);
        nudChiPhi.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudChiPhi.Name = "nudChiPhi";
        nudChiPhi.Size = new Size(339, 23);
        nudChiPhi.TabIndex = 7;
        nudChiPhi.ThousandsSeparator = true;
        // 
        // lblNgayDuKien
        // 
        lblNgayDuKien.AutoSize = true;
        lblNgayDuKien.Dock = DockStyle.Fill;
        lblNgayDuKien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNgayDuKien.Location = new Point(3, 62);
        lblNgayDuKien.Name = "lblNgayDuKien";
        lblNgayDuKien.Size = new Size(104, 36);
        lblNgayDuKien.TabIndex = 8;
        lblNgayDuKien.Text = "Dự kiến xong:";
        lblNgayDuKien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpNgayDuKien
        // 
        dtpNgayDuKien.Dock = DockStyle.Fill;
        dtpNgayDuKien.Format = DateTimePickerFormat.Short;
        dtpNgayDuKien.Location = new Point(113, 65);
        dtpNgayDuKien.Name = "dtpNgayDuKien";
        dtpNgayDuKien.ShowCheckBox = true;
        dtpNgayDuKien.Size = new Size(339, 23);
        dtpNgayDuKien.TabIndex = 9;
        // 
        // lblHuongDan
        // 
        lblHuongDan.Dock = DockStyle.Fill;
        lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblHuongDan.ForeColor = Color.FromArgb(100, 116, 139);
        lblHuongDan.Location = new Point(458, 62);
        lblHuongDan.Name = "lblHuongDan";
        lblHuongDan.Size = new Size(459, 36);
        lblHuongDan.TabIndex = 10;
        lblHuongDan.Text = "Thiết bị sẽ chuyển sang Bảo trì khi tạo phiếu.";
        lblHuongDan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnTao);
        flowThaoTac.Controls.Add(btnHoanTat);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Controls.Add(lblTimKiem);
        flowThaoTac.Controls.Add(txtTimKiem);
        flowThaoTac.Controls.Add(lblTrangThai);
        flowThaoTac.Controls.Add(cboLocTrangThai);
        flowThaoTac.Controls.Add(lblThongKe);
        flowThaoTac.Location = new Point(24, 247);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(952, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnTao
        // 
        btnTao.BackColor = Color.FromArgb(5, 150, 105);
        btnTao.Cursor = Cursors.Hand;
        btnTao.FlatAppearance.BorderSize = 0;
        btnTao.FlatStyle = FlatStyle.Flat;
        btnTao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTao.ForeColor = Color.White;
        btnTao.Location = new Point(0, 0);
        btnTao.Margin = new Padding(0, 0, 8, 0);
        btnTao.Name = "btnTao";
        btnTao.Size = new Size(140, 36);
        btnTao.TabIndex = 0;
        btnTao.Text = "Tạo phiếu bảo trì";
        btnTao.UseVisualStyleBackColor = false;
        btnTao.Click += btnTao_Click;
        // 
        // btnHoanTat
        // 
        btnHoanTat.BackColor = Color.FromArgb(37, 99, 235);
        btnHoanTat.Cursor = Cursors.Hand;
        btnHoanTat.FlatAppearance.BorderSize = 0;
        btnHoanTat.FlatStyle = FlatStyle.Flat;
        btnHoanTat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHoanTat.ForeColor = Color.White;
        btnHoanTat.Location = new Point(148, 0);
        btnHoanTat.Margin = new Padding(0, 0, 8, 0);
        btnHoanTat.Name = "btnHoanTat";
        btnHoanTat.Size = new Size(130, 36);
        btnHoanTat.TabIndex = 1;
        btnHoanTat.Text = "Hoàn tất bảo trì";
        btnHoanTat.UseVisualStyleBackColor = false;
        btnHoanTat.Click += btnHoanTat_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(286, 0);
        btnLamMoi.Margin = new Padding(0, 0, 16, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(90, 36);
        btnLamMoi.TabIndex = 2;
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
        txtTimKiem.PlaceholderText = "Tên thiết bị, NV, nội dung...";
        txtTimKiem.Size = new Size(170, 23);
        txtTimKiem.TabIndex = 3;
        txtTimKiem.TextChanged += BoLocThayDoi;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
        lblTrangThai.Location = new Point(519, 0);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(72, 36);
        lblTrangThai.TabIndex = 4;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cboLocTrangThai
        // 
        cboLocTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocTrangThai.FormattingEnabled = true;
        cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Hoàn thành", "Đang xử lý", "Chờ linh kiện" });
        cboLocTrangThai.Location = new Point(597, 6);
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
        lblThongKe.Location = new Point(742, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Size = new Size(190, 36);
        lblThongKe.TabIndex = 6;
        lblThongKe.Text = "Hiển thị: 0 phiếu";
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgv
        // 
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.None;
        dgv.ColumnHeadersHeight = 36;
        dgv.Location = new Point(24, 295);
        dgv.MultiSelect = false;
        dgv.Name = "dgv";
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.RowTemplate.Height = 32;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.Size = new Size(952, 405);
        dgv.TabIndex = 3;
        // 
        // FrmBaoTri
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 690);
        Controls.Add(dgv);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmBaoTri";
        Text = "Quản lý bảo trì";
        Load += FrmBaoTri_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudChiPhi).EndInit();
        flowThaoTac.ResumeLayout(false);
        flowThaoTac.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblThietBi;
    private ComboBox cboThietBi;
    private Label lblNhanVien;
    private ComboBox cboNhanVien;
    private Label lblNoiDung;
    private TextBox txtNoiDung;
    private Label lblChiPhi;
    private NumericUpDown nudChiPhi;
    private Label lblNgayDuKien;
    private DateTimePicker dtpNgayDuKien;
    private Label lblHuongDan;
    private FlowLayoutPanel flowThaoTac;
    private Button btnTao;
    private Button btnHoanTat;
    private Button btnLamMoi;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Label lblTrangThai;
    private ComboBox cboLocTrangThai;
    private Label lblThongKe;
    private DataGridView dgv;
}

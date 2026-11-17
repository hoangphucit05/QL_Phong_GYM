namespace He_Thong_GYM.Forms;

partial class FrmThongKe
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        DataGridViewCellStyle dgvStyleCur = new DataGridViewCellStyle();
        DataGridViewCellStyle dgvStyleNum = new DataGridViewCellStyle();
        DataGridViewCellStyle dgvStyleCur2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dgvStyleCur3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dgvStyleCur4 = new DataGridViewCellStyle();
        panelTieuDe = new Panel();
        lblMoTa = new Label();
        lblTieuDe = new Label();
        panelBoLoc = new Panel();
        btnTatCa = new Button();
        btnThangNay = new Button();
        btnHomNay = new Button();
        btnLoc = new Button();
        dtpDenNgay = new DateTimePicker();
        lblDenNgay = new Label();
        dtpTuNgay = new DateTimePicker();
        lblTuNgay = new Label();
        tableChiSo = new TableLayoutPanel();
        panelTheDoanhThu = new Panel();
        lblDoanhThu = new Label();
        lblTieuDeDoanhThu = new Label();
        panelTheCongNo = new Panel();
        lblCongNo = new Label();
        lblTieuDeCongNo = new Label();
        panelTheThanhVien = new Panel();
        lblThanhVien = new Label();
        lblTieuDeThanhVien = new Label();
        panelTheTonKho = new Panel();
        lblTonKho = new Label();
        lblTieuDeTonKho = new Label();
        panelTheBaoTri = new Panel();
        lblBaoTri = new Label();
        lblTieuDeBaoTri = new Label();
        tabControlBaoCao = new TabControl();
        tabDoanhThu = new TabPage();
        tableDoanhThu = new TableLayoutPanel();
        groupDoanhThuGoi = new GroupBox();
        dgvDoanhThuGoi = new DataGridView();
        colGoiTen = new DataGridViewTextBoxColumn();
        colGoiSoLuot = new DataGridViewTextBoxColumn();
        colGoiDoanhThu = new DataGridViewTextBoxColumn();
        groupDoanhThuThang = new GroupBox();
        dgvDoanhThuThang = new DataGridView();
        colThangThang = new DataGridViewTextBoxColumn();
        colThangNam = new DataGridViewTextBoxColumn();
        colThangSoHD = new DataGridViewTextBoxColumn();
        colThangDoanhThu = new DataGridViewTextBoxColumn();
        tabHoiVien = new TabPage();
        tableHoiVien = new TableLayoutPanel();
        groupCoCauHoiVien = new GroupBox();
        dgvCoCauHoiVien = new DataGridView();
        colHoiVienLoai = new DataGridViewTextBoxColumn();
        colHoiVienTong = new DataGridViewTextBoxColumn();
        colHoiVienHoatDong = new DataGridViewTextBoxColumn();
        groupTopHLV = new GroupBox();
        dgvTopHLV = new DataGridView();
        colHLVTen = new DataGridViewTextBoxColumn();
        colHLVChuyenMon = new DataGridViewTextBoxColumn();
        colHLVSoHocVien = new DataGridViewTextBoxColumn();
        colHLVDoanhThu = new DataGridViewTextBoxColumn();
        tabSanPham = new TabPage();
        groupSanPham = new GroupBox();
        dgvSanPham = new DataGridView();
        colSPTen = new DataGridViewTextBoxColumn();
        colSPGiaBan = new DataGridViewTextBoxColumn();
        colSPDaBan = new DataGridViewTextBoxColumn();
        colSPTonKho = new DataGridViewTextBoxColumn();
        colSPDoanhThu = new DataGridViewTextBoxColumn();
        tabBieuDo = new TabPage();
        panelBieuDo = new Panel();
        lblTieuDeBieuDo = new Label();
        panelTieuDe.SuspendLayout();
        panelBoLoc.SuspendLayout();
        tableChiSo.SuspendLayout();
        panelTheDoanhThu.SuspendLayout();
        panelTheCongNo.SuspendLayout();
        panelTheThanhVien.SuspendLayout();
        panelTheTonKho.SuspendLayout();
        panelTheBaoTri.SuspendLayout();
        tabControlBaoCao.SuspendLayout();
        tabDoanhThu.SuspendLayout();
        tableDoanhThu.SuspendLayout();
        groupDoanhThuGoi.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDoanhThuGoi).BeginInit();
        groupDoanhThuThang.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDoanhThuThang).BeginInit();
        tabHoiVien.SuspendLayout();
        tableHoiVien.SuspendLayout();
        groupCoCauHoiVien.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCoCauHoiVien).BeginInit();
        groupTopHLV.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTopHLV).BeginInit();
        tabSanPham.SuspendLayout();
        groupSanPham.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
        tabBieuDo.SuspendLayout();
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
        panelTieuDe.Padding = new Padding(24, 14, 24, 12);
        panelTieuDe.Size = new Size(1100, 72);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 44);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(420, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Tổng hợp các chỉ số KPI vận hành, phân tích doanh thu và hiệu quả kinh doanh.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 10);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(245, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BÁO CÁO THỐNG KÊ";
        // 
        // panelBoLoc
        // 
        panelBoLoc.BackColor = Color.White;
        panelBoLoc.Controls.Add(btnTatCa);
        panelBoLoc.Controls.Add(btnThangNay);
        panelBoLoc.Controls.Add(btnHomNay);
        panelBoLoc.Controls.Add(btnLoc);
        panelBoLoc.Controls.Add(dtpDenNgay);
        panelBoLoc.Controls.Add(lblDenNgay);
        panelBoLoc.Controls.Add(dtpTuNgay);
        panelBoLoc.Controls.Add(lblTuNgay);
        panelBoLoc.Dock = DockStyle.Top;
        panelBoLoc.Location = new Point(0, 72);
        panelBoLoc.Name = "panelBoLoc";
        panelBoLoc.Padding = new Padding(24, 8, 24, 8);
        panelBoLoc.Size = new Size(1100, 48);
        panelBoLoc.TabIndex = 1;
        // 
        // btnTatCa
        // 
        btnTatCa.BackColor = Color.FromArgb(241, 245, 249);
        btnTatCa.Cursor = Cursors.Hand;
        btnTatCa.FlatAppearance.BorderSize = 0;
        btnTatCa.FlatStyle = FlatStyle.Flat;
        btnTatCa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTatCa.ForeColor = Color.FromArgb(71, 85, 105);
        btnTatCa.Location = new Point(780, 9);
        btnTatCa.Name = "btnTatCa";
        btnTatCa.Size = new Size(90, 28);
        btnTatCa.TabIndex = 7;
        btnTatCa.Text = "Tất cả";
        btnTatCa.UseVisualStyleBackColor = false;
        btnTatCa.Click += btnTatCa_Click;
        // 
        // btnThangNay
        // 
        btnThangNay.BackColor = Color.FromArgb(241, 245, 249);
        btnThangNay.Cursor = Cursors.Hand;
        btnThangNay.FlatAppearance.BorderSize = 0;
        btnThangNay.FlatStyle = FlatStyle.Flat;
        btnThangNay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThangNay.ForeColor = Color.FromArgb(71, 85, 105);
        btnThangNay.Location = new Point(680, 9);
        btnThangNay.Name = "btnThangNay";
        btnThangNay.Size = new Size(90, 28);
        btnThangNay.TabIndex = 6;
        btnThangNay.Text = "Tháng này";
        btnThangNay.UseVisualStyleBackColor = false;
        btnThangNay.Click += btnThangNay_Click;
        // 
        // btnHomNay
        // 
        btnHomNay.BackColor = Color.FromArgb(241, 245, 249);
        btnHomNay.Cursor = Cursors.Hand;
        btnHomNay.FlatAppearance.BorderSize = 0;
        btnHomNay.FlatStyle = FlatStyle.Flat;
        btnHomNay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHomNay.ForeColor = Color.FromArgb(71, 85, 105);
        btnHomNay.Location = new Point(580, 9);
        btnHomNay.Name = "btnHomNay";
        btnHomNay.Size = new Size(90, 28);
        btnHomNay.TabIndex = 5;
        btnHomNay.Text = "Hôm nay";
        btnHomNay.UseVisualStyleBackColor = false;
        btnHomNay.Click += btnHomNay_Click;
        // 
        // btnLoc
        // 
        btnLoc.BackColor = Color.FromArgb(30, 64, 175);
        btnLoc.Cursor = Cursors.Hand;
        btnLoc.FlatAppearance.BorderSize = 0;
        btnLoc.FlatStyle = FlatStyle.Flat;
        btnLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLoc.ForeColor = Color.White;
        btnLoc.Location = new Point(470, 9);
        btnLoc.Name = "btnLoc";
        btnLoc.Size = new Size(100, 28);
        btnLoc.TabIndex = 4;
        btnLoc.Text = "Xem báo cáo";
        btnLoc.UseVisualStyleBackColor = false;
        btnLoc.Click += btnLoc_Click;
        // 
        // dtpDenNgay
        // 
        dtpDenNgay.Format = DateTimePickerFormat.Short;
        dtpDenNgay.Location = new Point(325, 12);
        dtpDenNgay.Name = "dtpDenNgay";
        dtpDenNgay.Size = new Size(130, 23);
        dtpDenNgay.TabIndex = 3;
        // 
        // lblDenNgay
        // 
        lblDenNgay.AutoSize = true;
        lblDenNgay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDenNgay.Location = new Point(255, 16);
        lblDenNgay.Name = "lblDenNgay";
        lblDenNgay.Size = new Size(63, 15);
        lblDenNgay.TabIndex = 2;
        lblDenNgay.Text = "Đến ngày:";
        // 
        // dtpTuNgay
        // 
        dtpTuNgay.Format = DateTimePickerFormat.Short;
        dtpTuNgay.Location = new Point(85, 12);
        dtpTuNgay.Name = "dtpTuNgay";
        dtpTuNgay.Size = new Size(130, 23);
        dtpTuNgay.TabIndex = 1;
        // 
        // lblTuNgay
        // 
        lblTuNgay.AutoSize = true;
        lblTuNgay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTuNgay.Location = new Point(24, 16);
        lblTuNgay.Name = "lblTuNgay";
        lblTuNgay.Size = new Size(54, 15);
        lblTuNgay.TabIndex = 0;
        lblTuNgay.Text = "Từ ngày:";
        // 
        // tableChiSo
        // 
        tableChiSo.ColumnCount = 5;
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableChiSo.Controls.Add(panelTheDoanhThu, 0, 0);
        tableChiSo.Controls.Add(panelTheCongNo, 1, 0);
        tableChiSo.Controls.Add(panelTheThanhVien, 2, 0);
        tableChiSo.Controls.Add(panelTheTonKho, 3, 0);
        tableChiSo.Controls.Add(panelTheBaoTri, 4, 0);
        tableChiSo.Dock = DockStyle.Top;
        tableChiSo.Location = new Point(0, 120);
        tableChiSo.Name = "tableChiSo";
        tableChiSo.Padding = new Padding(16, 6, 16, 6);
        tableChiSo.RowCount = 1;
        tableChiSo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableChiSo.Size = new Size(1100, 96);
        tableChiSo.TabIndex = 2;
        // 
        // panelTheDoanhThu
        // 
        panelTheDoanhThu.BackColor = Color.FromArgb(5, 150, 105);
        panelTheDoanhThu.Controls.Add(lblDoanhThu);
        panelTheDoanhThu.Controls.Add(lblTieuDeDoanhThu);
        panelTheDoanhThu.Dock = DockStyle.Fill;
        panelTheDoanhThu.Location = new Point(22, 12);
        panelTheDoanhThu.Margin = new Padding(6);
        panelTheDoanhThu.Name = "panelTheDoanhThu";
        panelTheDoanhThu.Padding = new Padding(14, 10, 14, 10);
        panelTheDoanhThu.Size = new Size(201, 72);
        panelTheDoanhThu.TabIndex = 0;
        // 
        // lblDoanhThu
        // 
        lblDoanhThu.Dock = DockStyle.Fill;
        lblDoanhThu.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblDoanhThu.ForeColor = Color.White;
        lblDoanhThu.Location = new Point(14, 28);
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Size = new Size(173, 34);
        lblDoanhThu.TabIndex = 1;
        lblDoanhThu.Text = "--";
        lblDoanhThu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTieuDeDoanhThu
        // 
        lblTieuDeDoanhThu.Dock = DockStyle.Top;
        lblTieuDeDoanhThu.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTieuDeDoanhThu.ForeColor = Color.FromArgb(209, 250, 229);
        lblTieuDeDoanhThu.Location = new Point(14, 10);
        lblTieuDeDoanhThu.Name = "lblTieuDeDoanhThu";
        lblTieuDeDoanhThu.Size = new Size(173, 18);
        lblTieuDeDoanhThu.TabIndex = 0;
        lblTieuDeDoanhThu.Text = "DOANH THU THỰC NHẬN";
        // 
        // panelTheCongNo
        // 
        panelTheCongNo.BackColor = Color.FromArgb(220, 38, 38);
        panelTheCongNo.Controls.Add(lblCongNo);
        panelTheCongNo.Controls.Add(lblTieuDeCongNo);
        panelTheCongNo.Dock = DockStyle.Fill;
        panelTheCongNo.Location = new Point(235, 12);
        panelTheCongNo.Margin = new Padding(6);
        panelTheCongNo.Name = "panelTheCongNo";
        panelTheCongNo.Padding = new Padding(14, 10, 14, 10);
        panelTheCongNo.Size = new Size(201, 72);
        panelTheCongNo.TabIndex = 1;
        // 
        // lblCongNo
        // 
        lblCongNo.Dock = DockStyle.Fill;
        lblCongNo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblCongNo.ForeColor = Color.White;
        lblCongNo.Location = new Point(14, 28);
        lblCongNo.Name = "lblCongNo";
        lblCongNo.Size = new Size(173, 34);
        lblCongNo.TabIndex = 1;
        lblCongNo.Text = "--";
        lblCongNo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTieuDeCongNo
        // 
        lblTieuDeCongNo.Dock = DockStyle.Top;
        lblTieuDeCongNo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTieuDeCongNo.ForeColor = Color.FromArgb(254, 226, 226);
        lblTieuDeCongNo.Location = new Point(14, 10);
        lblTieuDeCongNo.Name = "lblTieuDeCongNo";
        lblTieuDeCongNo.Size = new Size(173, 18);
        lblTieuDeCongNo.TabIndex = 0;
        lblTieuDeCongNo.Text = "TỔNG CÔNG NỢ";
        // 
        // panelTheThanhVien
        // 
        panelTheThanhVien.BackColor = Color.FromArgb(30, 64, 175);
        panelTheThanhVien.Controls.Add(lblThanhVien);
        panelTheThanhVien.Controls.Add(lblTieuDeThanhVien);
        panelTheThanhVien.Dock = DockStyle.Fill;
        panelTheThanhVien.Location = new Point(448, 12);
        panelTheThanhVien.Margin = new Padding(6);
        panelTheThanhVien.Name = "panelTheThanhVien";
        panelTheThanhVien.Padding = new Padding(14, 10, 14, 10);
        panelTheThanhVien.Size = new Size(201, 72);
        panelTheThanhVien.TabIndex = 2;
        // 
        // lblThanhVien
        // 
        lblThanhVien.Dock = DockStyle.Fill;
        lblThanhVien.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblThanhVien.ForeColor = Color.White;
        lblThanhVien.Location = new Point(14, 28);
        lblThanhVien.Name = "lblThanhVien";
        lblThanhVien.Size = new Size(173, 34);
        lblThanhVien.TabIndex = 1;
        lblThanhVien.Text = "--";
        lblThanhVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTieuDeThanhVien
        // 
        lblTieuDeThanhVien.Dock = DockStyle.Top;
        lblTieuDeThanhVien.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTieuDeThanhVien.ForeColor = Color.FromArgb(219, 234, 254);
        lblTieuDeThanhVien.Location = new Point(14, 10);
        lblTieuDeThanhVien.Name = "lblTieuDeThanhVien";
        lblTieuDeThanhVien.Size = new Size(173, 18);
        lblTieuDeThanhVien.TabIndex = 0;
        lblTieuDeThanhVien.Text = "HỘI VIÊN HOẠT ĐỘNG";
        // 
        // panelTheTonKho
        // 
        panelTheTonKho.BackColor = Color.FromArgb(217, 119, 6);
        panelTheTonKho.Controls.Add(lblTonKho);
        panelTheTonKho.Controls.Add(lblTieuDeTonKho);
        panelTheTonKho.Dock = DockStyle.Fill;
        panelTheTonKho.Location = new Point(661, 12);
        panelTheTonKho.Margin = new Padding(6);
        panelTheTonKho.Name = "panelTheTonKho";
        panelTheTonKho.Padding = new Padding(14, 10, 14, 10);
        panelTheTonKho.Size = new Size(201, 72);
        panelTheTonKho.TabIndex = 3;
        // 
        // lblTonKho
        // 
        lblTonKho.Dock = DockStyle.Fill;
        lblTonKho.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTonKho.ForeColor = Color.White;
        lblTonKho.Location = new Point(14, 28);
        lblTonKho.Name = "lblTonKho";
        lblTonKho.Size = new Size(173, 34);
        lblTonKho.TabIndex = 1;
        lblTonKho.Text = "--";
        lblTonKho.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTieuDeTonKho
        // 
        lblTieuDeTonKho.Dock = DockStyle.Top;
        lblTieuDeTonKho.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTieuDeTonKho.ForeColor = Color.FromArgb(254, 243, 199);
        lblTieuDeTonKho.Location = new Point(14, 10);
        lblTieuDeTonKho.Name = "lblTieuDeTonKho";
        lblTieuDeTonKho.Size = new Size(173, 18);
        lblTieuDeTonKho.TabIndex = 0;
        lblTieuDeTonKho.Text = "GIÁ TRỊ TỒN KHO";
        // 
        // panelTheBaoTri
        // 
        panelTheBaoTri.BackColor = Color.FromArgb(124, 58, 237);
        panelTheBaoTri.Controls.Add(lblBaoTri);
        panelTheBaoTri.Controls.Add(lblTieuDeBaoTri);
        panelTheBaoTri.Dock = DockStyle.Fill;
        panelTheBaoTri.Location = new Point(874, 12);
        panelTheBaoTri.Margin = new Padding(6);
        panelTheBaoTri.Name = "panelTheBaoTri";
        panelTheBaoTri.Padding = new Padding(14, 10, 14, 10);
        panelTheBaoTri.Size = new Size(204, 72);
        panelTheBaoTri.TabIndex = 4;
        // 
        // lblBaoTri
        // 
        lblBaoTri.Dock = DockStyle.Fill;
        lblBaoTri.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblBaoTri.ForeColor = Color.White;
        lblBaoTri.Location = new Point(14, 28);
        lblBaoTri.Name = "lblBaoTri";
        lblBaoTri.Size = new Size(176, 34);
        lblBaoTri.TabIndex = 1;
        lblBaoTri.Text = "--";
        lblBaoTri.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTieuDeBaoTri
        // 
        lblTieuDeBaoTri.Dock = DockStyle.Top;
        lblTieuDeBaoTri.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTieuDeBaoTri.ForeColor = Color.FromArgb(237, 233, 254);
        lblTieuDeBaoTri.Location = new Point(14, 10);
        lblTieuDeBaoTri.Name = "lblTieuDeBaoTri";
        lblTieuDeBaoTri.Size = new Size(176, 18);
        lblTieuDeBaoTri.TabIndex = 0;
        lblTieuDeBaoTri.Text = "CHI PHÍ BẢO TRÌ";
        // 
        // tabControlBaoCao
        // 
        tabControlBaoCao.Controls.Add(tabDoanhThu);
        tabControlBaoCao.Controls.Add(tabHoiVien);
        tabControlBaoCao.Controls.Add(tabSanPham);
        tabControlBaoCao.Controls.Add(tabBieuDo);
        tabControlBaoCao.Dock = DockStyle.Fill;
        tabControlBaoCao.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        tabControlBaoCao.Location = new Point(0, 216);
        tabControlBaoCao.Name = "tabControlBaoCao";
        tabControlBaoCao.Padding = new Point(16, 8);
        tabControlBaoCao.SelectedIndex = 0;
        tabControlBaoCao.Size = new Size(1100, 484);
        tabControlBaoCao.TabIndex = 3;
        // 
        // tabDoanhThu
        // 
        tabDoanhThu.BackColor = Color.FromArgb(241, 245, 249);
        tabDoanhThu.Controls.Add(tableDoanhThu);
        tabDoanhThu.Font = new Font("Segoe UI", 9F);
        tabDoanhThu.Location = new Point(4, 34);
        tabDoanhThu.Name = "tabDoanhThu";
        tabDoanhThu.Padding = new Padding(12);
        tabDoanhThu.Size = new Size(1092, 446);
        tabDoanhThu.TabIndex = 0;
        tabDoanhThu.Text = "Doanh thu & Dịch vụ";
        // 
        // tableDoanhThu
        // 
        tableDoanhThu.ColumnCount = 2;
        tableDoanhThu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableDoanhThu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableDoanhThu.Controls.Add(groupDoanhThuGoi, 0, 0);
        tableDoanhThu.Controls.Add(groupDoanhThuThang, 1, 0);
        tableDoanhThu.Dock = DockStyle.Fill;
        tableDoanhThu.Location = new Point(12, 12);
        tableDoanhThu.Name = "tableDoanhThu";
        tableDoanhThu.RowCount = 1;
        tableDoanhThu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableDoanhThu.Size = new Size(1068, 422);
        tableDoanhThu.TabIndex = 0;
        // 
        // groupDoanhThuGoi
        // 
        groupDoanhThuGoi.BackColor = Color.White;
        groupDoanhThuGoi.Controls.Add(dgvDoanhThuGoi);
        groupDoanhThuGoi.Dock = DockStyle.Fill;
        groupDoanhThuGoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        groupDoanhThuGoi.ForeColor = Color.FromArgb(30, 41, 59);
        groupDoanhThuGoi.Location = new Point(3, 3);
        groupDoanhThuGoi.Margin = new Padding(3, 3, 8, 3);
        groupDoanhThuGoi.Name = "groupDoanhThuGoi";
        groupDoanhThuGoi.Padding = new Padding(10);
        groupDoanhThuGoi.Size = new Size(523, 416);
        groupDoanhThuGoi.TabIndex = 0;
        groupDoanhThuGoi.TabStop = false;
        groupDoanhThuGoi.Text = "Doanh thu theo Gói tập";
        // 
        // dgvDoanhThuGoi
        // 
        dgvDoanhThuGoi.AllowUserToAddRows = false;
        dgvDoanhThuGoi.AllowUserToDeleteRows = false;
        dgvDoanhThuGoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDoanhThuGoi.BackgroundColor = Color.White;
        dgvDoanhThuGoi.BorderStyle = BorderStyle.None;
        dgvDoanhThuGoi.ColumnHeadersHeight = 36;
        dgvDoanhThuGoi.Columns.AddRange(new DataGridViewColumn[] { colGoiTen, colGoiSoLuot, colGoiDoanhThu });
        dgvDoanhThuGoi.Dock = DockStyle.Fill;
        dgvDoanhThuGoi.Font = new Font("Segoe UI", 9F);
        dgvDoanhThuGoi.Location = new Point(10, 27);
        dgvDoanhThuGoi.Name = "dgvDoanhThuGoi";
        dgvDoanhThuGoi.ReadOnly = true;
        dgvDoanhThuGoi.RowHeadersVisible = false;
        dgvDoanhThuGoi.RowTemplate.Height = 30;
        dgvDoanhThuGoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDoanhThuGoi.Size = new Size(503, 379);
        dgvDoanhThuGoi.TabIndex = 0;
        // 
        // colGoiTen
        // 
        colGoiTen.DataPropertyName = "TenGoi";
        colGoiTen.FillWeight = 140F;
        colGoiTen.HeaderText = "Tên gói tập";
        colGoiTen.Name = "colGoiTen";
        colGoiTen.ReadOnly = true;
        // 
        // colGoiSoLuot
        // 
        colGoiSoLuot.DataPropertyName = "SoLuotDangKy";
        colGoiSoLuot.FillWeight = 80F;
        colGoiSoLuot.HeaderText = "Số lượt ĐK";
        colGoiSoLuot.Name = "colGoiSoLuot";
        colGoiSoLuot.ReadOnly = true;
        // 
        // colGoiDoanhThu
        // 
        colGoiDoanhThu.DataPropertyName = "DoanhThu";
        dgvStyleCur.Format = "N0";
        colGoiDoanhThu.DefaultCellStyle = dgvStyleCur;
        colGoiDoanhThu.FillWeight = 110F;
        colGoiDoanhThu.HeaderText = "Doanh thu (VNĐ)";
        colGoiDoanhThu.Name = "colGoiDoanhThu";
        colGoiDoanhThu.ReadOnly = true;
        // 
        // groupDoanhThuThang
        // 
        groupDoanhThuThang.BackColor = Color.White;
        groupDoanhThuThang.Controls.Add(dgvDoanhThuThang);
        groupDoanhThuThang.Dock = DockStyle.Fill;
        groupDoanhThuThang.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        groupDoanhThuThang.ForeColor = Color.FromArgb(30, 41, 59);
        groupDoanhThuThang.Location = new Point(542, 3);
        groupDoanhThuThang.Margin = new Padding(8, 3, 3, 3);
        groupDoanhThuThang.Name = "groupDoanhThuThang";
        groupDoanhThuThang.Padding = new Padding(10);
        groupDoanhThuThang.Size = new Size(523, 416);
        groupDoanhThuThang.TabIndex = 1;
        groupDoanhThuThang.TabStop = false;
        groupDoanhThuThang.Text = "Doanh thu theo Tháng (Năm hiện tại)";
        // 
        // dgvDoanhThuThang
        // 
        dgvDoanhThuThang.AllowUserToAddRows = false;
        dgvDoanhThuThang.AllowUserToDeleteRows = false;
        dgvDoanhThuThang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDoanhThuThang.BackgroundColor = Color.White;
        dgvDoanhThuThang.BorderStyle = BorderStyle.None;
        dgvDoanhThuThang.ColumnHeadersHeight = 36;
        dgvDoanhThuThang.Columns.AddRange(new DataGridViewColumn[] { colThangThang, colThangNam, colThangSoHD, colThangDoanhThu });
        dgvDoanhThuThang.Dock = DockStyle.Fill;
        dgvDoanhThuThang.Font = new Font("Segoe UI", 9F);
        dgvDoanhThuThang.Location = new Point(10, 27);
        dgvDoanhThuThang.Name = "dgvDoanhThuThang";
        dgvDoanhThuThang.ReadOnly = true;
        dgvDoanhThuThang.RowHeadersVisible = false;
        dgvDoanhThuThang.RowTemplate.Height = 30;
        dgvDoanhThuThang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDoanhThuThang.Size = new Size(503, 379);
        dgvDoanhThuThang.TabIndex = 0;
        // 
        // colThangThang
        // 
        colThangThang.DataPropertyName = "Thang";
        colThangThang.FillWeight = 60F;
        colThangThang.HeaderText = "Tháng";
        colThangThang.Name = "colThangThang";
        colThangThang.ReadOnly = true;
        // 
        // colThangNam
        // 
        colThangNam.DataPropertyName = "Nam";
        colThangNam.FillWeight = 60F;
        colThangNam.HeaderText = "Năm";
        colThangNam.Name = "colThangNam";
        colThangNam.ReadOnly = true;
        // 
        // colThangSoHD
        // 
        colThangSoHD.DataPropertyName = "SoHoaDon";
        colThangSoHD.FillWeight = 75F;
        colThangSoHD.HeaderText = "Số HĐ";
        colThangSoHD.Name = "colThangSoHD";
        colThangSoHD.ReadOnly = true;
        // 
        // colThangDoanhThu
        // 
        colThangDoanhThu.DataPropertyName = "DoanhThu";
        dgvStyleNum.Format = "N0";
        colThangDoanhThu.DefaultCellStyle = dgvStyleNum;
        colThangDoanhThu.FillWeight = 115F;
        colThangDoanhThu.HeaderText = "Doanh thu (VNĐ)";
        colThangDoanhThu.Name = "colThangDoanhThu";
        colThangDoanhThu.ReadOnly = true;
        // 
        // tabHoiVien
        // 
        tabHoiVien.BackColor = Color.FromArgb(241, 245, 249);
        tabHoiVien.Controls.Add(tableHoiVien);
        tabHoiVien.Font = new Font("Segoe UI", 9F);
        tabHoiVien.Location = new Point(4, 34);
        tabHoiVien.Name = "tabHoiVien";
        tabHoiVien.Padding = new Padding(12);
        tabHoiVien.Size = new Size(1092, 446);
        tabHoiVien.TabIndex = 1;
        tabHoiVien.Text = "Hội viên & Huấn luyện viên";
        // 
        // tableHoiVien
        // 
        tableHoiVien.ColumnCount = 2;
        tableHoiVien.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableHoiVien.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableHoiVien.Controls.Add(groupCoCauHoiVien, 0, 0);
        tableHoiVien.Controls.Add(groupTopHLV, 1, 0);
        tableHoiVien.Dock = DockStyle.Fill;
        tableHoiVien.Location = new Point(12, 12);
        tableHoiVien.Name = "tableHoiVien";
        tableHoiVien.RowCount = 1;
        tableHoiVien.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableHoiVien.Size = new Size(1068, 422);
        tableHoiVien.TabIndex = 0;
        // 
        // groupCoCauHoiVien
        // 
        groupCoCauHoiVien.BackColor = Color.White;
        groupCoCauHoiVien.Controls.Add(dgvCoCauHoiVien);
        groupCoCauHoiVien.Dock = DockStyle.Fill;
        groupCoCauHoiVien.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        groupCoCauHoiVien.ForeColor = Color.FromArgb(30, 41, 59);
        groupCoCauHoiVien.Location = new Point(3, 3);
        groupCoCauHoiVien.Margin = new Padding(3, 3, 8, 3);
        groupCoCauHoiVien.Name = "groupCoCauHoiVien";
        groupCoCauHoiVien.Padding = new Padding(10);
        groupCoCauHoiVien.Size = new Size(523, 416);
        groupCoCauHoiVien.TabIndex = 0;
        groupCoCauHoiVien.TabStop = false;
        groupCoCauHoiVien.Text = "Cơ cấu Hội viên theo Loại thẻ";
        // 
        // dgvCoCauHoiVien
        // 
        dgvCoCauHoiVien.AllowUserToAddRows = false;
        dgvCoCauHoiVien.AllowUserToDeleteRows = false;
        dgvCoCauHoiVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCoCauHoiVien.BackgroundColor = Color.White;
        dgvCoCauHoiVien.BorderStyle = BorderStyle.None;
        dgvCoCauHoiVien.ColumnHeadersHeight = 36;
        dgvCoCauHoiVien.Columns.AddRange(new DataGridViewColumn[] { colHoiVienLoai, colHoiVienTong, colHoiVienHoatDong });
        dgvCoCauHoiVien.Dock = DockStyle.Fill;
        dgvCoCauHoiVien.Font = new Font("Segoe UI", 9F);
        dgvCoCauHoiVien.Location = new Point(10, 27);
        dgvCoCauHoiVien.Name = "dgvCoCauHoiVien";
        dgvCoCauHoiVien.ReadOnly = true;
        dgvCoCauHoiVien.RowHeadersVisible = false;
        dgvCoCauHoiVien.RowTemplate.Height = 30;
        dgvCoCauHoiVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCoCauHoiVien.Size = new Size(503, 379);
        dgvCoCauHoiVien.TabIndex = 0;
        // 
        // colHoiVienLoai
        // 
        colHoiVienLoai.DataPropertyName = "TenLoai";
        colHoiVienLoai.FillWeight = 140F;
        colHoiVienLoai.HeaderText = "Hạng thẻ / Loại";
        colHoiVienLoai.Name = "colHoiVienLoai";
        colHoiVienLoai.ReadOnly = true;
        // 
        // colHoiVienTong
        // 
        colHoiVienTong.DataPropertyName = "SoLuong";
        colHoiVienTong.FillWeight = 85F;
        colHoiVienTong.HeaderText = "Tổng số";
        colHoiVienTong.Name = "colHoiVienTong";
        colHoiVienTong.ReadOnly = true;
        // 
        // colHoiVienHoatDong
        // 
        colHoiVienHoatDong.DataPropertyName = "DangHoatDong";
        colHoiVienHoatDong.FillWeight = 95F;
        colHoiVienHoatDong.HeaderText = "Đang hoạt động";
        colHoiVienHoatDong.Name = "colHoiVienHoatDong";
        colHoiVienHoatDong.ReadOnly = true;
        // 
        // groupTopHLV
        // 
        groupTopHLV.BackColor = Color.White;
        groupTopHLV.Controls.Add(dgvTopHLV);
        groupTopHLV.Dock = DockStyle.Fill;
        groupTopHLV.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        groupTopHLV.ForeColor = Color.FromArgb(30, 41, 59);
        groupTopHLV.Location = new Point(542, 3);
        groupTopHLV.Margin = new Padding(8, 3, 3, 3);
        groupTopHLV.Name = "groupTopHLV";
        groupTopHLV.Padding = new Padding(10);
        groupTopHLV.Size = new Size(523, 416);
        groupTopHLV.TabIndex = 1;
        groupTopHLV.TabStop = false;
        groupTopHLV.Text = "Top Huấn luyện viên (PT) Doanh thu cao";
        // 
        // dgvTopHLV
        // 
        dgvTopHLV.AllowUserToAddRows = false;
        dgvTopHLV.AllowUserToDeleteRows = false;
        dgvTopHLV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTopHLV.BackgroundColor = Color.White;
        dgvTopHLV.BorderStyle = BorderStyle.None;
        dgvTopHLV.ColumnHeadersHeight = 36;
        dgvTopHLV.Columns.AddRange(new DataGridViewColumn[] { colHLVTen, colHLVChuyenMon, colHLVSoHocVien, colHLVDoanhThu });
        dgvTopHLV.Dock = DockStyle.Fill;
        dgvTopHLV.Font = new Font("Segoe UI", 9F);
        dgvTopHLV.Location = new Point(10, 27);
        dgvTopHLV.Name = "dgvTopHLV";
        dgvTopHLV.ReadOnly = true;
        dgvTopHLV.RowHeadersVisible = false;
        dgvTopHLV.RowTemplate.Height = 30;
        dgvTopHLV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTopHLV.Size = new Size(503, 379);
        dgvTopHLV.TabIndex = 0;
        // 
        // colHLVTen
        // 
        colHLVTen.DataPropertyName = "HoTen";
        colHLVTen.FillWeight = 120F;
        colHLVTen.HeaderText = "Huấn luyện viên";
        colHLVTen.Name = "colHLVTen";
        colHLVTen.ReadOnly = true;
        // 
        // colHLVChuyenMon
        // 
        colHLVChuyenMon.DataPropertyName = "ChuyenMon";
        colHLVChuyenMon.FillWeight = 110F;
        colHLVChuyenMon.HeaderText = "Chuyên môn";
        colHLVChuyenMon.Name = "colHLVChuyenMon";
        colHLVChuyenMon.ReadOnly = true;
        // 
        // colHLVSoHocVien
        // 
        colHLVSoHocVien.DataPropertyName = "SoHocVien";
        colHLVSoHocVien.FillWeight = 70F;
        colHLVSoHocVien.HeaderText = "Học viên";
        colHLVSoHocVien.Name = "colHLVSoHocVien";
        colHLVSoHocVien.ReadOnly = true;
        // 
        // colHLVDoanhThu
        // 
        colHLVDoanhThu.DataPropertyName = "DoanhThu";
        dgvStyleCur2.Format = "N0";
        colHLVDoanhThu.DefaultCellStyle = dgvStyleCur2;
        colHLVDoanhThu.FillWeight = 110F;
        colHLVDoanhThu.HeaderText = "Doanh thu (VNĐ)";
        colHLVDoanhThu.Name = "colHLVDoanhThu";
        colHLVDoanhThu.ReadOnly = true;
        // 
        // tabSanPham
        // 
        tabSanPham.BackColor = Color.FromArgb(241, 245, 249);
        tabSanPham.Controls.Add(groupSanPham);
        tabSanPham.Font = new Font("Segoe UI", 9F);
        tabSanPham.Location = new Point(4, 34);
        tabSanPham.Name = "tabSanPham";
        tabSanPham.Padding = new Padding(12);
        tabSanPham.Size = new Size(1092, 446);
        tabSanPham.TabIndex = 2;
        tabSanPham.Text = "Sản phẩm & Tồn kho";
        // 
        // groupSanPham
        // 
        groupSanPham.BackColor = Color.White;
        groupSanPham.Controls.Add(dgvSanPham);
        groupSanPham.Dock = DockStyle.Fill;
        groupSanPham.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        groupSanPham.ForeColor = Color.FromArgb(30, 41, 59);
        groupSanPham.Location = new Point(12, 12);
        groupSanPham.Name = "groupSanPham";
        groupSanPham.Padding = new Padding(12);
        groupSanPham.Size = new Size(1068, 422);
        groupSanPham.TabIndex = 0;
        groupSanPham.TabStop = false;
        groupSanPham.Text = "Tình hình bán lẻ & Tồn kho sản phẩm";
        // 
        // dgvSanPham
        // 
        dgvSanPham.AllowUserToAddRows = false;
        dgvSanPham.AllowUserToDeleteRows = false;
        dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSanPham.BackgroundColor = Color.White;
        dgvSanPham.BorderStyle = BorderStyle.None;
        dgvSanPham.ColumnHeadersHeight = 36;
        dgvSanPham.Columns.AddRange(new DataGridViewColumn[] { colSPTen, colSPGiaBan, colSPDaBan, colSPTonKho, colSPDoanhThu });
        dgvSanPham.Dock = DockStyle.Fill;
        dgvSanPham.Font = new Font("Segoe UI", 9F);
        dgvSanPham.Location = new Point(12, 29);
        dgvSanPham.Name = "dgvSanPham";
        dgvSanPham.ReadOnly = true;
        dgvSanPham.RowHeadersVisible = false;
        dgvSanPham.RowTemplate.Height = 30;
        dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSanPham.Size = new Size(1044, 381);
        dgvSanPham.TabIndex = 0;
        // 
        // colSPTen
        // 
        colSPTen.DataPropertyName = "TenSP";
        colSPTen.FillWeight = 150F;
        colSPTen.HeaderText = "Tên sản phẩm";
        colSPTen.Name = "colSPTen";
        colSPTen.ReadOnly = true;
        // 
        // colSPGiaBan
        // 
        colSPGiaBan.DataPropertyName = "DonGia";
        dgvStyleCur3.Format = "N0";
        colSPGiaBan.DefaultCellStyle = dgvStyleCur3;
        colSPGiaBan.FillWeight = 85F;
        colSPGiaBan.HeaderText = "Đơn giá (VNĐ)";
        colSPGiaBan.Name = "colSPGiaBan";
        colSPGiaBan.ReadOnly = true;
        // 
        // colSPDaBan
        // 
        colSPDaBan.DataPropertyName = "DaBan";
        colSPDaBan.FillWeight = 70F;
        colSPDaBan.HeaderText = "Đã bán";
        colSPDaBan.Name = "colSPDaBan";
        colSPDaBan.ReadOnly = true;
        // 
        // colSPTonKho
        // 
        colSPTonKho.DataPropertyName = "TonKho";
        colSPTonKho.FillWeight = 70F;
        colSPTonKho.HeaderText = "Tồn kho";
        colSPTonKho.Name = "colSPTonKho";
        colSPTonKho.ReadOnly = true;
        // 
        // colSPDoanhThu
        // 
        colSPDoanhThu.DataPropertyName = "DoanhThu";
        dgvStyleCur4.Format = "N0";
        colSPDoanhThu.DefaultCellStyle = dgvStyleCur4;
        colSPDoanhThu.FillWeight = 110F;
        colSPDoanhThu.HeaderText = "Doanh số (VNĐ)";
        colSPDoanhThu.Name = "colSPDoanhThu";
        colSPDoanhThu.ReadOnly = true;
        // 
        // tabBieuDo
        // 
        tabBieuDo.BackColor = Color.FromArgb(241, 245, 249);
        tabBieuDo.Controls.Add(panelBieuDo);
        tabBieuDo.Controls.Add(lblTieuDeBieuDo);
        tabBieuDo.Font = new Font("Segoe UI", 9F);
        tabBieuDo.Location = new Point(4, 34);
        tabBieuDo.Name = "tabBieuDo";
        tabBieuDo.Padding = new Padding(16);
        tabBieuDo.Size = new Size(1092, 446);
        tabBieuDo.TabIndex = 3;
        tabBieuDo.Text = "Biểu đồ trực quan";
        // 
        // panelBieuDo
        // 
        panelBieuDo.BackColor = Color.White;
        panelBieuDo.Dock = DockStyle.Fill;
        panelBieuDo.Location = new Point(16, 44);
        panelBieuDo.Name = "panelBieuDo";
        panelBieuDo.Size = new Size(1060, 386);
        panelBieuDo.TabIndex = 1;
        // 
        // lblTieuDeBieuDo
        // 
        lblTieuDeBieuDo.Dock = DockStyle.Top;
        lblTieuDeBieuDo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTieuDeBieuDo.ForeColor = Color.FromArgb(30, 41, 59);
        lblTieuDeBieuDo.Location = new Point(16, 16);
        lblTieuDeBieuDo.Name = "lblTieuDeBieuDo";
        lblTieuDeBieuDo.Size = new Size(1060, 28);
        lblTieuDeBieuDo.TabIndex = 0;
        lblTieuDeBieuDo.Text = "Biểu đồ cột: Diễn biến Doanh thu các tháng trong năm";
        // 
        // FrmThongKe
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1100, 700);
        Controls.Add(tabControlBaoCao);
        Controls.Add(tableChiSo);
        Controls.Add(panelBoLoc);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(950, 580);
        Name = "FrmThongKe";
        Text = "Báo cáo thống kê";
        Load += FrmThongKe_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        panelBoLoc.ResumeLayout(false);
        panelBoLoc.PerformLayout();
        tableChiSo.ResumeLayout(false);
        panelTheDoanhThu.ResumeLayout(false);
        panelTheCongNo.ResumeLayout(false);
        panelTheThanhVien.ResumeLayout(false);
        panelTheTonKho.ResumeLayout(false);
        panelTheBaoTri.ResumeLayout(false);
        tabControlBaoCao.ResumeLayout(false);
        tabDoanhThu.ResumeLayout(false);
        tableDoanhThu.ResumeLayout(false);
        groupDoanhThuGoi.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDoanhThuGoi).EndInit();
        groupDoanhThuThang.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDoanhThuThang).EndInit();
        tabHoiVien.ResumeLayout(false);
        tableHoiVien.ResumeLayout(false);
        groupCoCauHoiVien.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCoCauHoiVien).EndInit();
        groupTopHLV.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTopHLV).EndInit();
        tabSanPham.ResumeLayout(false);
        groupSanPham.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        tabBieuDo.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Panel panelBoLoc;
    private Label lblTuNgay;
    private DateTimePicker dtpTuNgay;
    private Label lblDenNgay;
    private DateTimePicker dtpDenNgay;
    private Button btnLoc;
    private Button btnHomNay;
    private Button btnThangNay;
    private Button btnTatCa;
    private TableLayoutPanel tableChiSo;
    private Panel panelTheDoanhThu;
    private Label lblTieuDeDoanhThu;
    private Label lblDoanhThu;
    private Panel panelTheCongNo;
    private Label lblTieuDeCongNo;
    private Label lblCongNo;
    private Panel panelTheThanhVien;
    private Label lblTieuDeThanhVien;
    private Label lblThanhVien;
    private Panel panelTheTonKho;
    private Label lblTieuDeTonKho;
    private Label lblTonKho;
    private Panel panelTheBaoTri;
    private Label lblTieuDeBaoTri;
    private Label lblBaoTri;
    private TabControl tabControlBaoCao;
    private TabPage tabDoanhThu;
    private TableLayoutPanel tableDoanhThu;
    private GroupBox groupDoanhThuGoi;
    private DataGridView dgvDoanhThuGoi;
    private DataGridViewTextBoxColumn colGoiTen;
    private DataGridViewTextBoxColumn colGoiSoLuot;
    private DataGridViewTextBoxColumn colGoiDoanhThu;
    private GroupBox groupDoanhThuThang;
    private DataGridView dgvDoanhThuThang;
    private DataGridViewTextBoxColumn colThangThang;
    private DataGridViewTextBoxColumn colThangNam;
    private DataGridViewTextBoxColumn colThangSoHD;
    private DataGridViewTextBoxColumn colThangDoanhThu;
    private TabPage tabHoiVien;
    private TableLayoutPanel tableHoiVien;
    private GroupBox groupCoCauHoiVien;
    private DataGridView dgvCoCauHoiVien;
    private DataGridViewTextBoxColumn colHoiVienLoai;
    private DataGridViewTextBoxColumn colHoiVienTong;
    private DataGridViewTextBoxColumn colHoiVienHoatDong;
    private GroupBox groupTopHLV;
    private DataGridView dgvTopHLV;
    private DataGridViewTextBoxColumn colHLVTen;
    private DataGridViewTextBoxColumn colHLVChuyenMon;
    private DataGridViewTextBoxColumn colHLVSoHocVien;
    private DataGridViewTextBoxColumn colHLVDoanhThu;
    private TabPage tabSanPham;
    private GroupBox groupSanPham;
    private DataGridView dgvSanPham;
    private DataGridViewTextBoxColumn colSPTen;
    private DataGridViewTextBoxColumn colSPGiaBan;
    private DataGridViewTextBoxColumn colSPDaBan;
    private DataGridViewTextBoxColumn colSPTonKho;
    private DataGridViewTextBoxColumn colSPDoanhThu;
    private TabPage tabBieuDo;
    private Label lblTieuDeBieuDo;
    private Panel panelBieuDo;
}

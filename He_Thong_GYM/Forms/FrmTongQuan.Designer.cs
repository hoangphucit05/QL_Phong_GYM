namespace He_Thong_GYM.Forms;

partial class FrmTongQuan
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
        panelRoot = new Panel();
        tableLayout = new TableLayoutPanel();
        panelBoLoc = new Panel();
        panelVachTrangTri = new Panel();
        lblTieuDe = new Label();
        lblMoTa = new Label();
        lblKhoangThoiGian = new Label();
        cboKhoang = new ComboBox();
        dtpTuNgay = new DateTimePicker();
        dtpDenNgay = new DateTimePicker();
        btnApDung = new Button();
        btnLamMoi = new Button();
        tableKpi = new TableLayoutPanel();
        tableBieuDo = new TableLayoutPanel();
        boxDoanhThu = new GroupBox();
        boxDonut = new GroupBox();
        boxTrangThai = new GroupBox();
        tableCanhBao = new TableLayoutPanel();
        boxGoiSapHetHan = new GroupBox();
        dgvGoiSapHetHan = new DataGridView();
        colGoiMaTV = new DataGridViewTextBoxColumn();
        colGoiHoTen = new DataGridViewTextBoxColumn();
        colGoiSDT = new DataGridViewTextBoxColumn();
        colGoiTen = new DataGridViewTextBoxColumn();
        colGoiHetHan = new DataGridViewTextBoxColumn();
        colGoiConNgay = new DataGridViewTextBoxColumn();
        colGoiTrangThai = new DataGridViewTextBoxColumn();
        boxCongNo = new GroupBox();
        dgvCongNo = new DataGridView();
        colCNSoHD = new DataGridViewTextBoxColumn();
        colCNHoTen = new DataGridViewTextBoxColumn();
        colCNTongTien = new DataGridViewTextBoxColumn();
        colCNDaThu = new DataGridViewTextBoxColumn();
        colCNConNo = new DataGridViewTextBoxColumn();
        tableHoatDong = new TableLayoutPanel();
        boxHoatDong = new GroupBox();
        dgvHoatDong = new DataGridView();
        colHDThoiGian = new DataGridViewTextBoxColumn();
        colHDHanhDong = new DataGridViewTextBoxColumn();
        colHDDoiTuong = new DataGridViewTextBoxColumn();
        colHDNoiDung = new DataGridViewTextBoxColumn();
        boxGoiBanChay = new GroupBox();
        dgvGoiBanChay = new DataGridView();
        colBCTenGoi = new DataGridViewTextBoxColumn();
        colBCSoLuong = new DataGridViewTextBoxColumn();
        colBCDoanhThu = new DataGridViewTextBoxColumn();
        lblVanHanhTomTat = new Label();
        panelRoot.SuspendLayout();
        tableLayout.SuspendLayout();
        panelBoLoc.SuspendLayout();
        tableBieuDo.SuspendLayout();
        tableCanhBao.SuspendLayout();
        boxGoiSapHetHan.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoiSapHetHan).BeginInit();
        boxCongNo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCongNo).BeginInit();
        tableHoatDong.SuspendLayout();
        boxHoatDong.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHoatDong).BeginInit();
        boxGoiBanChay.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoiBanChay).BeginInit();
        SuspendLayout();
        // 
        // panelRoot
        // 
        panelRoot.AutoScroll = true;
        panelRoot.BackColor = Color.FromArgb(241, 245, 249);
        panelRoot.Controls.Add(tableLayout);
        panelRoot.Dock = DockStyle.Fill;
        panelRoot.Location = new Point(0, 0);
        panelRoot.Name = "panelRoot";
        panelRoot.Padding = new Padding(20, 16, 20, 20);
        panelRoot.Size = new Size(1100, 720);
        panelRoot.TabIndex = 0;
        // 
        // tableLayout
        // 
        tableLayout.AutoSize = true;
        tableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayout.ColumnCount = 1;
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayout.Controls.Add(panelBoLoc, 0, 0);
        tableLayout.Controls.Add(tableKpi, 0, 1);
        tableLayout.Controls.Add(tableBieuDo, 0, 2);
        tableLayout.Controls.Add(tableCanhBao, 0, 3);
        tableLayout.Controls.Add(tableHoatDong, 0, 4);
        tableLayout.Dock = DockStyle.Top;
        tableLayout.Location = new Point(20, 16);
        tableLayout.Name = "tableLayout";
        tableLayout.RowCount = 5;
        tableLayout.RowStyles.Add(new RowStyle());
        tableLayout.RowStyles.Add(new RowStyle());
        tableLayout.RowStyles.Add(new RowStyle());
        tableLayout.RowStyles.Add(new RowStyle());
        tableLayout.RowStyles.Add(new RowStyle());
        tableLayout.Size = new Size(1060, 1175);
        tableLayout.TabIndex = 0;
        // 
        // panelBoLoc
        // 
        panelBoLoc.BackColor = Color.White;
        panelBoLoc.Controls.Add(panelVachTrangTri);
        panelBoLoc.Controls.Add(lblTieuDe);
        panelBoLoc.Controls.Add(lblMoTa);
        panelBoLoc.Controls.Add(lblKhoangThoiGian);
        panelBoLoc.Controls.Add(cboKhoang);
        panelBoLoc.Controls.Add(dtpTuNgay);
        panelBoLoc.Controls.Add(dtpDenNgay);
        panelBoLoc.Controls.Add(btnApDung);
        panelBoLoc.Controls.Add(btnLamMoi);
        panelBoLoc.Dock = DockStyle.Fill;
        panelBoLoc.Location = new Point(3, 3);
        panelBoLoc.Margin = new Padding(3, 3, 3, 10);
        panelBoLoc.Name = "panelBoLoc";
        panelBoLoc.Padding = new Padding(20);
        panelBoLoc.Size = new Size(1054, 118);
        panelBoLoc.TabIndex = 0;
        // 
        // panelVachTrangTri
        // 
        panelVachTrangTri.BackColor = Color.FromArgb(37, 99, 235);
        panelVachTrangTri.Dock = DockStyle.Top;
        panelVachTrangTri.Location = new Point(20, 20);
        panelVachTrangTri.Name = "panelVachTrangTri";
        panelVachTrangTri.Size = new Size(1014, 4);
        panelVachTrangTri.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(20, 18);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(310, 30);
        lblTieuDe.TabIndex = 1;
        lblTieuDe.Text = "📊  TỔNG QUAN VẬN HÀNH";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.Font = new Font("Segoe UI", 9F);
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(22, 52);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(450, 15);
        lblMoTa.TabIndex = 2;
        lblMoTa.Text = "Số liệu được cập nhật trực tiếp từ Database theo khoảng thời gian đã chọn.";
        // 
        // lblKhoangThoiGian
        // 
        lblKhoangThoiGian.AutoSize = true;
        lblKhoangThoiGian.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblKhoangThoiGian.ForeColor = Color.FromArgb(51, 65, 85);
        lblKhoangThoiGian.Location = new Point(22, 82);
        lblKhoangThoiGian.Name = "lblKhoangThoiGian";
        lblKhoangThoiGian.Size = new Size(104, 15);
        lblKhoangThoiGian.TabIndex = 3;
        lblKhoangThoiGian.Text = "Khoảng thời gian";
        // 
        // cboKhoang
        // 
        cboKhoang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhoang.Font = new Font("Segoe UI", 9F);
        cboKhoang.FormattingEnabled = true;
        cboKhoang.Items.AddRange(new object[] { "Hôm nay", "Hôm qua", "7 ngày qua", "Tháng này", "Tháng trước", "Quý này", "Năm nay", "Tùy chọn" });
        cboKhoang.Location = new Point(135, 78);
        cboKhoang.Name = "cboKhoang";
        cboKhoang.Size = new Size(145, 23);
        cboKhoang.TabIndex = 4;
        cboKhoang.SelectedIndexChanged += cboKhoang_SelectedIndexChanged;
        // 
        // dtpTuNgay
        // 
        dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        dtpTuNgay.Font = new Font("Segoe UI", 9F);
        dtpTuNgay.Format = DateTimePickerFormat.Custom;
        dtpTuNgay.Location = new Point(298, 78);
        dtpTuNgay.Name = "dtpTuNgay";
        dtpTuNgay.Size = new Size(105, 23);
        dtpTuNgay.TabIndex = 5;
        dtpTuNgay.ValueChanged += DatePicker_ValueChanged;
        // 
        // dtpDenNgay
        // 
        dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        dtpDenNgay.Font = new Font("Segoe UI", 9F);
        dtpDenNgay.Format = DateTimePickerFormat.Custom;
        dtpDenNgay.Location = new Point(414, 78);
        dtpDenNgay.Name = "dtpDenNgay";
        dtpDenNgay.Size = new Size(105, 23);
        dtpDenNgay.TabIndex = 6;
        dtpDenNgay.ValueChanged += DatePicker_ValueChanged;
        // 
        // btnApDung
        // 
        btnApDung.BackColor = Color.FromArgb(37, 99, 235);
        btnApDung.Cursor = Cursors.Hand;
        btnApDung.FlatAppearance.BorderSize = 0;
        btnApDung.FlatStyle = FlatStyle.Flat;
        btnApDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnApDung.ForeColor = Color.White;
        btnApDung.Location = new Point(530, 75);
        btnApDung.Name = "btnApDung";
        btnApDung.Size = new Size(88, 30);
        btnApDung.TabIndex = 7;
        btnApDung.Text = "✔ Áp dụng";
        btnApDung.UseVisualStyleBackColor = false;
        btnApDung.Click += btnApDung_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(5, 150, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(626, 75);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(88, 30);
        btnLamMoi.TabIndex = 8;
        btnLamMoi.Text = "⟳ Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // tableKpi
        // 
        tableKpi.ColumnCount = 4;
        tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableKpi.Dock = DockStyle.Fill;
        tableKpi.Location = new Point(3, 134);
        tableKpi.Margin = new Padding(3, 3, 3, 10);
        tableKpi.Name = "tableKpi";
        tableKpi.RowCount = 2;
        tableKpi.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableKpi.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableKpi.Size = new Size(1054, 196);
        tableKpi.TabIndex = 1;
        // 
        // tableBieuDo
        // 
        tableBieuDo.ColumnCount = 3;
        tableBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        tableBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
        tableBieuDo.Controls.Add(boxDoanhThu, 0, 0);
        tableBieuDo.Controls.Add(boxDonut, 1, 0);
        tableBieuDo.Controls.Add(boxTrangThai, 2, 0);
        tableBieuDo.Dock = DockStyle.Fill;
        tableBieuDo.Location = new Point(3, 343);
        tableBieuDo.Margin = new Padding(3, 3, 3, 10);
        tableBieuDo.Name = "tableBieuDo";
        tableBieuDo.RowCount = 1;
        tableBieuDo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableBieuDo.Size = new Size(1054, 285);
        tableBieuDo.TabIndex = 2;
        // 
        // boxDoanhThu
        // 
        boxDoanhThu.BackColor = Color.White;
        boxDoanhThu.Dock = DockStyle.Fill;
        boxDoanhThu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxDoanhThu.ForeColor = Color.FromArgb(15, 23, 42);
        boxDoanhThu.Location = new Point(4, 3);
        boxDoanhThu.Margin = new Padding(4, 3, 6, 3);
        boxDoanhThu.Name = "boxDoanhThu";
        boxDoanhThu.Padding = new Padding(10);
        boxDoanhThu.Size = new Size(432, 279);
        boxDoanhThu.TabIndex = 0;
        boxDoanhThu.TabStop = false;
        boxDoanhThu.Text = "📊 Doanh thu theo ngày";
        // 
        // boxDonut
        // 
        boxDonut.BackColor = Color.White;
        boxDonut.Dock = DockStyle.Fill;
        boxDonut.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxDonut.ForeColor = Color.FromArgb(15, 23, 42);
        boxDonut.Location = new Point(446, 3);
        boxDonut.Margin = new Padding(4, 3, 6, 3);
        boxDonut.Name = "boxDonut";
        boxDonut.Padding = new Padding(10);
        boxDonut.Size = new Size(253, 279);
        boxDonut.TabIndex = 1;
        boxDonut.TabStop = false;
        boxDonut.Text = "🍩 Trạng thái hội viên";
        // 
        // boxTrangThai
        // 
        boxTrangThai.BackColor = Color.White;
        boxTrangThai.Dock = DockStyle.Fill;
        boxTrangThai.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxTrangThai.ForeColor = Color.FromArgb(15, 23, 42);
        boxTrangThai.Location = new Point(709, 3);
        boxTrangThai.Margin = new Padding(4, 3, 4, 3);
        boxTrangThai.Name = "boxTrangThai";
        boxTrangThai.Padding = new Padding(10);
        boxTrangThai.Size = new Size(341, 279);
        boxTrangThai.TabIndex = 2;
        boxTrangThai.TabStop = false;
        boxTrangThai.Text = "📈 Phân bổ hội viên";
        // 
        // tableCanhBao
        // 
        tableCanhBao.ColumnCount = 2;
        tableCanhBao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tableCanhBao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        tableCanhBao.Controls.Add(boxGoiSapHetHan, 0, 0);
        tableCanhBao.Controls.Add(boxCongNo, 1, 0);
        tableCanhBao.Dock = DockStyle.Fill;
        tableCanhBao.Location = new Point(3, 641);
        tableCanhBao.Margin = new Padding(3, 3, 3, 10);
        tableCanhBao.Name = "tableCanhBao";
        tableCanhBao.RowCount = 1;
        tableCanhBao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableCanhBao.Size = new Size(1054, 285);
        tableCanhBao.TabIndex = 3;
        // 
        // boxGoiSapHetHan
        // 
        boxGoiSapHetHan.BackColor = Color.White;
        boxGoiSapHetHan.Controls.Add(dgvGoiSapHetHan);
        boxGoiSapHetHan.Dock = DockStyle.Fill;
        boxGoiSapHetHan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxGoiSapHetHan.ForeColor = Color.FromArgb(15, 23, 42);
        boxGoiSapHetHan.Location = new Point(4, 3);
        boxGoiSapHetHan.Margin = new Padding(4, 3, 6, 3);
        boxGoiSapHetHan.Name = "boxGoiSapHetHan";
        boxGoiSapHetHan.Padding = new Padding(10);
        boxGoiSapHetHan.Size = new Size(622, 279);
        boxGoiSapHetHan.TabIndex = 0;
        boxGoiSapHetHan.TabStop = false;
        boxGoiSapHetHan.Text = "⚠️ Gói sắp hết hạn (Nhấn đúp để mở Vận hành)";
        // 
        // dgvGoiSapHetHan
        // 
        dgvGoiSapHetHan.AllowUserToAddRows = false;
        dgvGoiSapHetHan.AllowUserToDeleteRows = false;
        dgvGoiSapHetHan.AllowUserToResizeRows = false;
        dgvGoiSapHetHan.BackgroundColor = Color.White;
        dgvGoiSapHetHan.BorderStyle = BorderStyle.None;
        dgvGoiSapHetHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvGoiSapHetHan.ColumnHeadersHeight = 32;
        dgvGoiSapHetHan.Columns.AddRange(new DataGridViewColumn[] { colGoiMaTV, colGoiHoTen, colGoiSDT, colGoiTen, colGoiHetHan, colGoiConNgay, colGoiTrangThai });
        dgvGoiSapHetHan.Dock = DockStyle.Fill;
        dgvGoiSapHetHan.EnableHeadersVisualStyles = false;
        dgvGoiSapHetHan.Font = new Font("Segoe UI", 8.25F);
        dgvGoiSapHetHan.GridColor = Color.FromArgb(226, 232, 240);
        dgvGoiSapHetHan.Location = new Point(10, 27);
        dgvGoiSapHetHan.MultiSelect = false;
        dgvGoiSapHetHan.Name = "dgvGoiSapHetHan";
        dgvGoiSapHetHan.ReadOnly = true;
        dgvGoiSapHetHan.RowHeadersVisible = false;
        dgvGoiSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGoiSapHetHan.Size = new Size(602, 242);
        dgvGoiSapHetHan.TabIndex = 0;
        dgvGoiSapHetHan.CellDoubleClick += dgvGoiSapHetHan_CellDoubleClick;
        // 
        // colGoiMaTV
        // 
        colGoiMaTV.DataPropertyName = "MaThanhVien";
        colGoiMaTV.HeaderText = "Mã TV";
        colGoiMaTV.Name = "colGoiMaTV";
        colGoiMaTV.ReadOnly = true;
        colGoiMaTV.Width = 75;
        // 
        // colGoiHoTen
        // 
        colGoiHoTen.DataPropertyName = "HoTen";
        colGoiHoTen.HeaderText = "Hội viên";
        colGoiHoTen.Name = "colGoiHoTen";
        colGoiHoTen.ReadOnly = true;
        colGoiHoTen.Width = 120;
        // 
        // colGoiSDT
        // 
        colGoiSDT.DataPropertyName = "SDT";
        colGoiSDT.HeaderText = "SĐT";
        colGoiSDT.Name = "colGoiSDT";
        colGoiSDT.ReadOnly = true;
        colGoiSDT.Width = 85;
        // 
        // colGoiTen
        // 
        colGoiTen.DataPropertyName = "TenGoi";
        colGoiTen.HeaderText = "Gói tập";
        colGoiTen.Name = "colGoiTen";
        colGoiTen.ReadOnly = true;
        colGoiTen.Width = 100;
        // 
        // colGoiHetHan
        // 
        colGoiHetHan.DataPropertyName = "NgayKetThuc";
        colGoiHetHan.HeaderText = "Hết hạn";
        colGoiHetHan.Name = "colGoiHetHan";
        colGoiHetHan.ReadOnly = true;
        colGoiHetHan.Width = 80;
        // 
        // colGoiConNgay
        // 
        colGoiConNgay.DataPropertyName = "SoNgayConLai";
        colGoiConNgay.HeaderText = "Còn ngày";
        colGoiConNgay.Name = "colGoiConNgay";
        colGoiConNgay.ReadOnly = true;
        colGoiConNgay.Width = 65;
        // 
        // colGoiTrangThai
        // 
        colGoiTrangThai.DataPropertyName = "TrangThai";
        colGoiTrangThai.HeaderText = "Trạng thái";
        colGoiTrangThai.Name = "colGoiTrangThai";
        colGoiTrangThai.ReadOnly = true;
        colGoiTrangThai.Width = 77;
        // 
        // boxCongNo
        // 
        boxCongNo.BackColor = Color.White;
        boxCongNo.Controls.Add(dgvCongNo);
        boxCongNo.Dock = DockStyle.Fill;
        boxCongNo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxCongNo.ForeColor = Color.FromArgb(15, 23, 42);
        boxCongNo.Location = new Point(636, 3);
        boxCongNo.Margin = new Padding(4, 3, 4, 3);
        boxCongNo.Name = "boxCongNo";
        boxCongNo.Padding = new Padding(10);
        boxCongNo.Size = new Size(414, 279);
        boxCongNo.TabIndex = 1;
        boxCongNo.TabStop = false;
        boxCongNo.Text = "💳 Công nợ cần xử lý (Nhấn đúp để mở Hóa đơn)";
        // 
        // dgvCongNo
        // 
        dgvCongNo.AllowUserToAddRows = false;
        dgvCongNo.AllowUserToDeleteRows = false;
        dgvCongNo.AllowUserToResizeRows = false;
        dgvCongNo.BackgroundColor = Color.White;
        dgvCongNo.BorderStyle = BorderStyle.None;
        dgvCongNo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvCongNo.ColumnHeadersHeight = 32;
        dgvCongNo.Columns.AddRange(new DataGridViewColumn[] { colCNSoHD, colCNHoTen, colCNTongTien, colCNDaThu, colCNConNo });
        dgvCongNo.Dock = DockStyle.Fill;
        dgvCongNo.EnableHeadersVisualStyles = false;
        dgvCongNo.Font = new Font("Segoe UI", 8.25F);
        dgvCongNo.GridColor = Color.FromArgb(226, 232, 240);
        dgvCongNo.Location = new Point(10, 27);
        dgvCongNo.MultiSelect = false;
        dgvCongNo.Name = "dgvCongNo";
        dgvCongNo.ReadOnly = true;
        dgvCongNo.RowHeadersVisible = false;
        dgvCongNo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCongNo.Size = new Size(394, 242);
        dgvCongNo.TabIndex = 0;
        dgvCongNo.CellDoubleClick += dgvCongNo_CellDoubleClick;
        // 
        // colCNSoHD
        // 
        colCNSoHD.DataPropertyName = "SoHoaDon";
        colCNSoHD.HeaderText = "Hóa đơn";
        colCNSoHD.Name = "colCNSoHD";
        colCNSoHD.ReadOnly = true;
        colCNSoHD.Width = 85;
        // 
        // colCNHoTen
        // 
        colCNHoTen.DataPropertyName = "HoTen";
        colCNHoTen.HeaderText = "Hội viên";
        colCNHoTen.Name = "colCNHoTen";
        colCNHoTen.ReadOnly = true;
        colCNHoTen.Width = 110;
        // 
        // colCNTongTien
        // 
        colCNTongTien.DataPropertyName = "TongTien";
        colCNTongTien.HeaderText = "Tổng";
        colCNTongTien.Name = "colCNTongTien";
        colCNTongTien.ReadOnly = true;
        colCNTongTien.Width = 75;
        // 
        // colCNDaThu
        // 
        colCNDaThu.DataPropertyName = "DaThanhToan";
        colCNDaThu.HeaderText = "Đã thu";
        colCNDaThu.Name = "colCNDaThu";
        colCNDaThu.ReadOnly = true;
        colCNDaThu.Width = 70;
        // 
        // colCNConNo
        // 
        colCNConNo.DataPropertyName = "ConNo";
        colCNConNo.HeaderText = "Còn nợ";
        colCNConNo.Name = "colCNConNo";
        colCNConNo.ReadOnly = true;
        colCNConNo.Width = 70;
        // 
        // tableHoatDong
        // 
        tableHoatDong.ColumnCount = 2;
        tableHoatDong.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tableHoatDong.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        tableHoatDong.Controls.Add(boxHoatDong, 0, 0);
        tableHoatDong.Controls.Add(boxGoiBanChay, 1, 0);
        tableHoatDong.Dock = DockStyle.Fill;
        tableHoatDong.Location = new Point(3, 939);
        tableHoatDong.Margin = new Padding(3, 3, 3, 10);
        tableHoatDong.Name = "tableHoatDong";
        tableHoatDong.RowCount = 1;
        tableHoatDong.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableHoatDong.Size = new Size(1054, 300);
        tableHoatDong.TabIndex = 4;
        // 
        // boxHoatDong
        // 
        boxHoatDong.BackColor = Color.White;
        boxHoatDong.Controls.Add(dgvHoatDong);
        boxHoatDong.Dock = DockStyle.Fill;
        boxHoatDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxHoatDong.ForeColor = Color.FromArgb(15, 23, 42);
        boxHoatDong.Location = new Point(4, 3);
        boxHoatDong.Margin = new Padding(4, 3, 6, 3);
        boxHoatDong.Name = "boxHoatDong";
        boxHoatDong.Padding = new Padding(10);
        boxHoatDong.Size = new Size(622, 294);
        boxHoatDong.TabIndex = 0;
        boxHoatDong.TabStop = false;
        boxHoatDong.Text = "📋 Hoạt động gần đây";
        // 
        // dgvHoatDong
        // 
        dgvHoatDong.AllowUserToAddRows = false;
        dgvHoatDong.AllowUserToDeleteRows = false;
        dgvHoatDong.AllowUserToResizeRows = false;
        dgvHoatDong.BackgroundColor = Color.White;
        dgvHoatDong.BorderStyle = BorderStyle.None;
        dgvHoatDong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvHoatDong.ColumnHeadersHeight = 32;
        dgvHoatDong.Columns.AddRange(new DataGridViewColumn[] { colHDThoiGian, colHDHanhDong, colHDDoiTuong, colHDNoiDung });
        dgvHoatDong.Dock = DockStyle.Fill;
        dgvHoatDong.EnableHeadersVisualStyles = false;
        dgvHoatDong.Font = new Font("Segoe UI", 8.25F);
        dgvHoatDong.GridColor = Color.FromArgb(226, 232, 240);
        dgvHoatDong.Location = new Point(10, 27);
        dgvHoatDong.MultiSelect = false;
        dgvHoatDong.Name = "dgvHoatDong";
        dgvHoatDong.ReadOnly = true;
        dgvHoatDong.RowHeadersVisible = false;
        dgvHoatDong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHoatDong.Size = new Size(602, 257);
        dgvHoatDong.TabIndex = 0;
        // 
        // colHDThoiGian
        // 
        colHDThoiGian.DataPropertyName = "ThoiGian";
        colHDThoiGian.HeaderText = "Thời gian";
        colHDThoiGian.Name = "colHDThoiGian";
        colHDThoiGian.ReadOnly = true;
        colHDThoiGian.Width = 100;
        // 
        // colHDHanhDong
        // 
        colHDHanhDong.DataPropertyName = "HanhDong";
        colHDHanhDong.HeaderText = "Hành động";
        colHDHanhDong.Name = "colHDHanhDong";
        colHDHanhDong.ReadOnly = true;
        colHDHanhDong.Width = 100;
        // 
        // colHDDoiTuong
        // 
        colHDDoiTuong.DataPropertyName = "DoiTuong";
        colHDDoiTuong.HeaderText = "Đối tượng";
        colHDDoiTuong.Name = "colHDDoiTuong";
        colHDDoiTuong.ReadOnly = true;
        colHDDoiTuong.Width = 90;
        // 
        // colHDNoiDung
        // 
        colHDNoiDung.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colHDNoiDung.DataPropertyName = "NoiDung";
        colHDNoiDung.HeaderText = "Nội dung chi tiết";
        colHDNoiDung.Name = "colHDNoiDung";
        colHDNoiDung.ReadOnly = true;
        // 
        // boxGoiBanChay
        // 
        boxGoiBanChay.BackColor = Color.White;
        boxGoiBanChay.Controls.Add(dgvGoiBanChay);
        boxGoiBanChay.Controls.Add(lblVanHanhTomTat);
        boxGoiBanChay.Dock = DockStyle.Fill;
        boxGoiBanChay.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        boxGoiBanChay.ForeColor = Color.FromArgb(15, 23, 42);
        boxGoiBanChay.Location = new Point(636, 3);
        boxGoiBanChay.Margin = new Padding(4, 3, 4, 3);
        boxGoiBanChay.Name = "boxGoiBanChay";
        boxGoiBanChay.Padding = new Padding(10);
        boxGoiBanChay.Size = new Size(414, 294);
        boxGoiBanChay.TabIndex = 1;
        boxGoiBanChay.TabStop = false;
        boxGoiBanChay.Text = "🏆 Gói tập bán chạy & Vận hành";
        // 
        // dgvGoiBanChay
        // 
        dgvGoiBanChay.AllowUserToAddRows = false;
        dgvGoiBanChay.AllowUserToDeleteRows = false;
        dgvGoiBanChay.AllowUserToResizeRows = false;
        dgvGoiBanChay.BackgroundColor = Color.White;
        dgvGoiBanChay.BorderStyle = BorderStyle.None;
        dgvGoiBanChay.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvGoiBanChay.ColumnHeadersHeight = 32;
        dgvGoiBanChay.Columns.AddRange(new DataGridViewColumn[] { colBCTenGoi, colBCSoLuong, colBCDoanhThu });
        dgvGoiBanChay.Dock = DockStyle.Fill;
        dgvGoiBanChay.EnableHeadersVisualStyles = false;
        dgvGoiBanChay.Font = new Font("Segoe UI", 8.25F);
        dgvGoiBanChay.GridColor = Color.FromArgb(226, 232, 240);
        dgvGoiBanChay.Location = new Point(10, 75);
        dgvGoiBanChay.MultiSelect = false;
        dgvGoiBanChay.Name = "dgvGoiBanChay";
        dgvGoiBanChay.ReadOnly = true;
        dgvGoiBanChay.RowHeadersVisible = false;
        dgvGoiBanChay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGoiBanChay.Size = new Size(394, 209);
        dgvGoiBanChay.TabIndex = 1;
        // 
        // colBCTenGoi
        // 
        colBCTenGoi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBCTenGoi.DataPropertyName = "TenGoi";
        colBCTenGoi.HeaderText = "Tên gói tập";
        colBCTenGoi.Name = "colBCTenGoi";
        colBCTenGoi.ReadOnly = true;
        // 
        // colBCSoLuong
        // 
        colBCSoLuong.DataPropertyName = "SoLuotDangKy";
        colBCSoLuong.HeaderText = "Đăng ký";
        colBCSoLuong.Name = "colBCSoLuong";
        colBCSoLuong.ReadOnly = true;
        colBCSoLuong.Width = 70;
        // 
        // colBCDoanhThu
        // 
        colBCDoanhThu.DataPropertyName = "DoanhThu";
        colBCDoanhThu.HeaderText = "Doanh thu";
        colBCDoanhThu.Name = "colBCDoanhThu";
        colBCDoanhThu.ReadOnly = true;
        colBCDoanhThu.Width = 90;
        // 
        // lblVanHanhTomTat
        // 
        lblVanHanhTomTat.BackColor = Color.FromArgb(248, 250, 252);
        lblVanHanhTomTat.Dock = DockStyle.Top;
        lblVanHanhTomTat.Font = new Font("Segoe UI", 8.25F);
        lblVanHanhTomTat.ForeColor = Color.FromArgb(71, 85, 105);
        lblVanHanhTomTat.Location = new Point(10, 27);
        lblVanHanhTomTat.Name = "lblVanHanhTomTat";
        lblVanHanhTomTat.Padding = new Padding(4);
        lblVanHanhTomTat.Size = new Size(394, 48);
        lblVanHanhTomTat.TabIndex = 0;
        lblVanHanhTomTat.Text = "Đang tải dữ liệu vận hành...";
        // 
        // FrmTongQuan
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1100, 720);
        Controls.Add(panelRoot);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(950, 600);
        Name = "FrmTongQuan";
        Text = "Tổng quan vận hành";
        Load += FrmTongQuan_Load;
        panelRoot.ResumeLayout(false);
        panelRoot.PerformLayout();
        tableLayout.ResumeLayout(false);
        panelBoLoc.ResumeLayout(false);
        panelBoLoc.PerformLayout();
        tableBieuDo.ResumeLayout(false);
        tableCanhBao.ResumeLayout(false);
        boxGoiSapHetHan.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvGoiSapHetHan).EndInit();
        boxCongNo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCongNo).EndInit();
        tableHoatDong.ResumeLayout(false);
        boxHoatDong.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvHoatDong).EndInit();
        boxGoiBanChay.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvGoiBanChay).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelRoot;
    private TableLayoutPanel tableLayout;
    private Panel panelBoLoc;
    private Panel panelVachTrangTri;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Label lblKhoangThoiGian;
    private ComboBox cboKhoang;
    private DateTimePicker dtpTuNgay;
    private DateTimePicker dtpDenNgay;
    private Button btnApDung;
    private Button btnLamMoi;
    private TableLayoutPanel tableKpi;
    private TableLayoutPanel tableBieuDo;
    private GroupBox boxDoanhThu;
    private GroupBox boxDonut;
    private GroupBox boxTrangThai;
    private TableLayoutPanel tableCanhBao;
    private GroupBox boxGoiSapHetHan;
    private DataGridView dgvGoiSapHetHan;
    private DataGridViewTextBoxColumn colGoiMaTV;
    private DataGridViewTextBoxColumn colGoiHoTen;
    private DataGridViewTextBoxColumn colGoiSDT;
    private DataGridViewTextBoxColumn colGoiTen;
    private DataGridViewTextBoxColumn colGoiHetHan;
    private DataGridViewTextBoxColumn colGoiConNgay;
    private DataGridViewTextBoxColumn colGoiTrangThai;
    private GroupBox boxCongNo;
    private DataGridView dgvCongNo;
    private DataGridViewTextBoxColumn colCNSoHD;
    private DataGridViewTextBoxColumn colCNHoTen;
    private DataGridViewTextBoxColumn colCNTongTien;
    private DataGridViewTextBoxColumn colCNDaThu;
    private DataGridViewTextBoxColumn colCNConNo;
    private TableLayoutPanel tableHoatDong;
    private GroupBox boxHoatDong;
    private DataGridView dgvHoatDong;
    private DataGridViewTextBoxColumn colHDThoiGian;
    private DataGridViewTextBoxColumn colHDHanhDong;
    private DataGridViewTextBoxColumn colHDDoiTuong;
    private DataGridViewTextBoxColumn colHDNoiDung;
    private GroupBox boxGoiBanChay;
    private DataGridView dgvGoiBanChay;
    private DataGridViewTextBoxColumn colBCTenGoi;
    private DataGridViewTextBoxColumn colBCSoLuong;
    private DataGridViewTextBoxColumn colBCDoanhThu;
    private Label lblVanHanhTomTat;
}

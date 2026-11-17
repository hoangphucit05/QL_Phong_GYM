namespace He_Thong_GYM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            panelThuongHieu = new Panel();
            lblThuongHieu = new Label();
            lblPhuDe = new Label();
            flowMenu = new FlowLayoutPanel();
            btnTongQuan = new Button();
            btnThanhVien = new Button();
            btnGoiTap = new Button();
            btnDangKyGoi = new Button();
            btnVanHanhGoi = new Button();
            btnKhuyenMai = new Button();
            btnHuanLuyenVien = new Button();
            btnDangKyPT = new Button();
            btnLopTap = new Button();
            btnDiemDanh = new Button();
            btnHoaDon = new Button();
            btnSanPham = new Button();
            btnThietBi = new Button();
            btnBaoTri = new Button();
            btnPhongTap = new Button();
            btnNhanVien = new Button();
            btnTaiKhoan = new Button();
            btnThongKe = new Button();
            btnDangXuat = new Button();
            panelTieuDe = new Panel();
            lblTieuDeHeThong = new Label();
            panelNguoiDung = new Panel();
            lblVaiTro = new Label();
            lblTenNguoiDung = new Label();
            lblThoiGian = new Label();
            btnThongTin = new Button();
            panelNoiDung = new Panel();
            panelDashboard = new Panel();
            groupBoxChaoMung = new GroupBox();
            lblMoTaNoiDung = new Label();
            lblTieuDeNoiDung = new Label();
            panelThongKe = new TableLayoutPanel();
            groupBoxThanhVien = new GroupBox();
            lblSoThanhVien = new Label();
            labelThanhVien = new Label();
            groupBoxGoiTap = new GroupBox();
            lblSoGoiTap = new Label();
            labelGoiTap = new Label();
            groupBoxDoanhThu = new GroupBox();
            lblSoDoanhThu = new Label();
            labelDoanhThu = new Label();
            groupBoxLopTap = new GroupBox();
            lblSoLopTap = new Label();
            labelLopTap = new Label();
            panelMenu.SuspendLayout();
            panelThuongHieu.SuspendLayout();
            flowMenu.SuspendLayout();
            panelTieuDe.SuspendLayout();
            panelNguoiDung.SuspendLayout();
            panelNoiDung.SuspendLayout();
            panelDashboard.SuspendLayout();
            groupBoxChaoMung.SuspendLayout();
            panelThongKe.SuspendLayout();
            groupBoxThanhVien.SuspendLayout();
            groupBoxGoiTap.SuspendLayout();
            groupBoxDoanhThu.SuspendLayout();
            groupBoxLopTap.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(20, 32, 51);
            panelMenu.Controls.Add(flowMenu);
            panelMenu.Controls.Add(panelThuongHieu);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(252, 760);
            panelMenu.TabIndex = 0;
            // 
            // panelThuongHieu
            // 
            panelThuongHieu.Controls.Add(lblThuongHieu);
            panelThuongHieu.Controls.Add(lblPhuDe);
            panelThuongHieu.Dock = DockStyle.Top;
            panelThuongHieu.Location = new Point(0, 0);
            panelThuongHieu.Name = "panelThuongHieu";
            panelThuongHieu.Padding = new Padding(22, 18, 12, 12);
            panelThuongHieu.Size = new Size(252, 100);
            panelThuongHieu.TabIndex = 0;
            // 
            // lblThuongHieu
            // 
            lblThuongHieu.AutoSize = true;
            lblThuongHieu.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblThuongHieu.ForeColor = Color.White;
            lblThuongHieu.Location = new Point(20, 14);
            lblThuongHieu.Name = "lblThuongHieu";
            lblThuongHieu.Size = new Size(163, 36);
            lblThuongHieu.TabIndex = 0;
            lblThuongHieu.Text = "GYM PRO";
            // 
            // lblPhuDe
            // 
            lblPhuDe.AutoSize = true;
            lblPhuDe.Font = new Font("Segoe UI", 9F);
            lblPhuDe.ForeColor = Color.FromArgb(157, 177, 201);
            lblPhuDe.Location = new Point(23, 60);
            lblPhuDe.Name = "lblPhuDe";
            lblPhuDe.Size = new Size(159, 15);
            lblPhuDe.TabIndex = 1;
            lblPhuDe.Text = "Hệ thống quản lý phòng gym";
            // 
            // flowMenu
            // 
            flowMenu.AutoScroll = true;
            flowMenu.Controls.Add(btnTongQuan);
            flowMenu.Controls.Add(btnThanhVien);
            flowMenu.Controls.Add(btnGoiTap);
            flowMenu.Controls.Add(btnDangKyGoi);
            flowMenu.Controls.Add(btnVanHanhGoi);
            flowMenu.Controls.Add(btnKhuyenMai);
            flowMenu.Controls.Add(btnHuanLuyenVien);
            flowMenu.Controls.Add(btnDangKyPT);
            flowMenu.Controls.Add(btnLopTap);
            flowMenu.Controls.Add(btnDiemDanh);
            flowMenu.Controls.Add(btnHoaDon);
            flowMenu.Controls.Add(btnSanPham);
            flowMenu.Controls.Add(btnThietBi);
            flowMenu.Controls.Add(btnBaoTri);
            flowMenu.Controls.Add(btnPhongTap);
            flowMenu.Controls.Add(btnNhanVien);
            flowMenu.Controls.Add(btnTaiKhoan);
            flowMenu.Controls.Add(btnThongKe);
            flowMenu.Controls.Add(btnDangXuat);
            flowMenu.Dock = DockStyle.Fill;
            flowMenu.FlowDirection = FlowDirection.TopDown;
            flowMenu.Location = new Point(0, 100);
            flowMenu.Name = "flowMenu";
            flowMenu.Padding = new Padding(12, 12, 12, 20);
            flowMenu.Size = new Size(252, 660);
            flowMenu.TabIndex = 1;
            flowMenu.WrapContents = false;
            // 
            // btnTongQuan
            // 
            btnTongQuan.BackColor = Color.FromArgb(29, 78, 216);
            btnTongQuan.Cursor = Cursors.Hand;
            btnTongQuan.FlatAppearance.BorderSize = 0;
            btnTongQuan.FlatStyle = FlatStyle.Flat;
            btnTongQuan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTongQuan.ForeColor = Color.White;
            btnTongQuan.Location = new Point(15, 15);
            btnTongQuan.Margin = new Padding(3, 3, 3, 7);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.Size = new Size(216, 42);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Tag = "Tổng quan";
            btnTongQuan.Text = "Tổng quan";
            btnTongQuan.TextAlign = ContentAlignment.MiddleLeft;
            btnTongQuan.UseVisualStyleBackColor = false;
            btnTongQuan.Click += MenuButton_Click;
            // 
            // btnThanhVien
            // 
            CauHinhNutMenu(btnThanhVien, "Quản lý thành viên", "Quản lý thành viên", 1);
            // 
            // btnGoiTap
            // 
            CauHinhNutMenu(btnGoiTap, "Quản lý gói tập", "Quản lý gói tập", 2);
            // 
            // btnDangKyGoi
            // 
            CauHinhNutMenu(btnDangKyGoi, "Đăng ký gói tập", "Đăng ký gói tập", 3);
            CauHinhNutMenu(btnVanHanhGoi, "Vận hành gói / Check-in", "Vận hành gói / Check-in", 4);
            CauHinhNutMenu(btnKhuyenMai, "Khuyến mãi", "Khuyến mãi", 4);
            // 
            // btnHuanLuyenVien
            // 
            CauHinhNutMenu(btnHuanLuyenVien, "Huấn luyện viên / PT", "Huấn luyện viên / PT", 5);
            CauHinhNutMenu(btnDangKyPT, "Đăng ký PT", "Đăng ký PT", 5);
            // 
            // btnLopTap
            // 
            CauHinhNutMenu(btnLopTap, "Lớp tập", "Lớp tập", 7);
            CauHinhNutMenu(btnDiemDanh, "Đăng ký và điểm danh", "Đăng ký và điểm danh", 8);
            // 
            // btnHoaDon
            // 
            CauHinhNutMenu(btnHoaDon, "Hóa đơn và thanh toán", "Hóa đơn và thanh toán", 7);
            // 
            // btnSanPham
            // 
            CauHinhNutMenu(btnSanPham, "Sản phẩm và bán hàng", "Sản phẩm và bán hàng", 8);
            // 
            // btnThietBi
            // 
            CauHinhNutMenu(btnThietBi, "Thiết bị", "Thiết bị", 10);
            CauHinhNutMenu(btnBaoTri, "Bảo trì thiết bị", "Bảo trì thiết bị", 11);
            CauHinhNutMenu(btnPhongTap, "Phòng tập", "Phòng tập", 12);
            // 
            // btnNhanVien
            // 
            CauHinhNutMenu(btnNhanVien, "Nhân viên và tài khoản", "Nhân viên và tài khoản", 10);
            // 
            // btnTaiKhoan
            // 
            CauHinhNutMenu(btnTaiKhoan, "Tài khoản và phân quyền", "Tài khoản và phân quyền", 11);
            // 
            // btnThongKe
            // 
            CauHinhNutMenu(btnThongKe, "Báo cáo thống kê", "Báo cáo thống kê", 12);
            // 
            // btnDangXuat
            // 
            btnDangXuat.BackColor = Color.FromArgb(127, 29, 29);
            btnDangXuat.Cursor = Cursors.Hand;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(15, 540);
            btnDangXuat.Margin = new Padding(3, 14, 3, 3);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(216, 42);
            btnDangXuat.TabIndex = 11;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.TextAlign = ContentAlignment.MiddleLeft;
            btnDangXuat.UseVisualStyleBackColor = false;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // panelTieuDe
            // 
            panelTieuDe.BackColor = Color.White;
            panelTieuDe.Controls.Add(btnThongTin);
            panelTieuDe.Controls.Add(lblThoiGian);
            panelTieuDe.Controls.Add(panelNguoiDung);
            panelTieuDe.Controls.Add(lblTieuDeHeThong);
            panelTieuDe.Dock = DockStyle.Top;
            panelTieuDe.Location = new Point(252, 0);
            panelTieuDe.Name = "panelTieuDe";
            panelTieuDe.Size = new Size(1088, 82);
            panelTieuDe.TabIndex = 1;
            // 
            // lblTieuDeHeThong
            // 
            lblTieuDeHeThong.AutoSize = true;
            lblTieuDeHeThong.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDeHeThong.ForeColor = Color.FromArgb(15, 23, 42);
            lblTieuDeHeThong.Location = new Point(30, 24);
            lblTieuDeHeThong.Name = "lblTieuDeHeThong";
            lblTieuDeHeThong.Size = new Size(392, 30);
            lblTieuDeHeThong.TabIndex = 0;
            lblTieuDeHeThong.Text = "PHẦN MỀM QUẢN LÝ PHÒNG GYM";
            // 
            // panelNguoiDung
            // 
            panelNguoiDung.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelNguoiDung.Controls.Add(lblVaiTro);
            panelNguoiDung.Controls.Add(lblTenNguoiDung);
            panelNguoiDung.Location = new Point(763, 14);
            panelNguoiDung.Name = "panelNguoiDung";
            panelNguoiDung.Size = new Size(180, 54);
            panelNguoiDung.TabIndex = 1;
            // 
            // lblVaiTro
            // 
            lblVaiTro.AutoSize = true;
            lblVaiTro.ForeColor = Color.FromArgb(100, 116, 139);
            lblVaiTro.Location = new Point(3, 30);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(91, 15);
            lblVaiTro.TabIndex = 1;
            lblVaiTro.Text = "Quản trị hệ thống";
            // 
            // lblTenNguoiDung
            // 
            lblTenNguoiDung.AutoSize = true;
            lblTenNguoiDung.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTenNguoiDung.ForeColor = Color.FromArgb(30, 41, 59);
            lblTenNguoiDung.Location = new Point(3, 7);
            lblTenNguoiDung.Name = "lblTenNguoiDung";
            lblTenNguoiDung.Size = new Size(107, 19);
            lblTenNguoiDung.TabIndex = 0;
            lblTenNguoiDung.Text = "Người dùng";
            // 
            // lblThoiGian
            // 
            lblThoiGian.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblThoiGian.ForeColor = Color.FromArgb(100, 116, 139);
            lblThoiGian.Location = new Point(496, 33);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(245, 20);
            lblThoiGian.TabIndex = 2;
            lblThoiGian.Text = "Đang tải thời gian...";
            lblThoiGian.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnThongTin
            // 
            btnThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThongTin.BackColor = Color.FromArgb(241, 245, 249);
            btnThongTin.FlatAppearance.BorderSize = 0;
            btnThongTin.FlatStyle = FlatStyle.Flat;
            btnThongTin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThongTin.ForeColor = Color.FromArgb(30, 64, 175);
            btnThongTin.Location = new Point(958, 22);
            btnThongTin.Name = "btnThongTin";
            btnThongTin.Size = new Size(104, 34);
            btnThongTin.TabIndex = 3;
            btnThongTin.Tag = "Thông tin cá nhân";
            btnThongTin.Text = "Tài khoản";
            btnThongTin.UseVisualStyleBackColor = false;
            btnThongTin.Click += MenuButton_Click;
            // 
            // panelNoiDung
            // 
            panelNoiDung.AutoScroll = true;
            panelNoiDung.BackColor = Color.FromArgb(241, 245, 249);
            panelNoiDung.Controls.Add(panelDashboard);
            panelNoiDung.Dock = DockStyle.Fill;
            panelNoiDung.Location = new Point(252, 82);
            panelNoiDung.Name = "panelNoiDung";
            panelNoiDung.Padding = new Padding(0);
            panelNoiDung.Size = new Size(1088, 678);
            panelNoiDung.TabIndex = 2;
            // 
            // panelDashboard
            // 
            panelDashboard.AutoScroll = true;
            panelDashboard.Controls.Add(panelThongKe);
            panelDashboard.Controls.Add(groupBoxChaoMung);
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Padding = new Padding(24);
            panelDashboard.Size = new Size(1088, 678);
            panelDashboard.TabIndex = 0;
            // 
            // groupBoxChaoMung
            // 
            groupBoxChaoMung.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxChaoMung.BackColor = Color.White;
            groupBoxChaoMung.Controls.Add(lblMoTaNoiDung);
            groupBoxChaoMung.Controls.Add(lblTieuDeNoiDung);
            groupBoxChaoMung.FlatStyle = FlatStyle.Flat;
            groupBoxChaoMung.Location = new Point(0, 0);
            groupBoxChaoMung.Name = "groupBoxChaoMung";
            groupBoxChaoMung.Padding = new Padding(22);
            groupBoxChaoMung.Size = new Size(1028, 128);
            groupBoxChaoMung.TabIndex = 0;
            groupBoxChaoMung.TabStop = false;
            // 
            // lblTieuDeNoiDung
            // 
            lblTieuDeNoiDung.AutoSize = true;
            lblTieuDeNoiDung.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTieuDeNoiDung.ForeColor = Color.FromArgb(15, 23, 42);
            lblTieuDeNoiDung.Location = new Point(25, 25);
            lblTieuDeNoiDung.Name = "lblTieuDeNoiDung";
            lblTieuDeNoiDung.Size = new Size(131, 32);
            lblTieuDeNoiDung.TabIndex = 0;
            lblTieuDeNoiDung.Text = "Tổng quan";
            // 
            // lblMoTaNoiDung
            // 
            lblMoTaNoiDung.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMoTaNoiDung.Font = new Font("Segoe UI", 10F);
            lblMoTaNoiDung.ForeColor = Color.FromArgb(71, 85, 105);
            lblMoTaNoiDung.Location = new Point(28, 68);
            lblMoTaNoiDung.Name = "lblMoTaNoiDung";
            lblMoTaNoiDung.Size = new Size(969, 31);
            lblMoTaNoiDung.TabIndex = 1;
            lblMoTaNoiDung.Text = "Theo dõi nhanh hoạt động phòng gym và truy cập các chức năng quản lý từ thanh menu.";
            // 
            // panelThongKe
            // 
            panelThongKe.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelThongKe.ColumnCount = 4;
            panelThongKe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelThongKe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelThongKe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelThongKe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panelThongKe.Controls.Add(groupBoxThanhVien, 0, 0);
            panelThongKe.Controls.Add(groupBoxGoiTap, 1, 0);
            panelThongKe.Controls.Add(groupBoxDoanhThu, 2, 0);
            panelThongKe.Controls.Add(groupBoxLopTap, 3, 0);
            panelThongKe.Location = new Point(0, 154);
            panelThongKe.Name = "panelThongKe";
            panelThongKe.Padding = new Padding(0, 0, 0, 8);
            panelThongKe.RowCount = 1;
            panelThongKe.RowStyles.Add(new RowStyle(SizeType.Absolute, 126F));
            panelThongKe.Size = new Size(1028, 138);
            panelThongKe.TabIndex = 1;
            // 
            // groupBoxThanhVien
            // 
            groupBoxThanhVien.BackColor = Color.White;
            groupBoxThanhVien.Controls.Add(lblSoThanhVien);
            groupBoxThanhVien.Controls.Add(labelThanhVien);
            groupBoxThanhVien.Dock = DockStyle.Fill;
            groupBoxThanhVien.Location = new Point(3, 3);
            groupBoxThanhVien.Margin = new Padding(3, 3, 10, 3);
            groupBoxThanhVien.Name = "groupBoxThanhVien";
            groupBoxThanhVien.Padding = new Padding(18);
            groupBoxThanhVien.Size = new Size(244, 120);
            groupBoxThanhVien.TabIndex = 0;
            groupBoxThanhVien.TabStop = false;
            // 
            // labelThanhVien
            // 
            labelThanhVien.AutoSize = true;
            labelThanhVien.ForeColor = Color.FromArgb(71, 85, 105);
            labelThanhVien.Location = new Point(21, 25);
            labelThanhVien.Name = "labelThanhVien";
            labelThanhVien.Size = new Size(120, 15);
            labelThanhVien.TabIndex = 0;
            labelThanhVien.Text = "TỔNG THÀNH VIÊN";
            // 
            // lblSoThanhVien
            // 
            lblSoThanhVien.AutoSize = true;
            lblSoThanhVien.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblSoThanhVien.ForeColor = Color.FromArgb(37, 99, 235);
            lblSoThanhVien.Location = new Point(18, 51);
            lblSoThanhVien.Name = "lblSoThanhVien";
            lblSoThanhVien.Size = new Size(58, 45);
            lblSoThanhVien.TabIndex = 1;
            lblSoThanhVien.Text = "--";
            // 
            // groupBoxGoiTap
            // 
            groupBoxGoiTap.BackColor = Color.White;
            groupBoxGoiTap.Controls.Add(lblSoGoiTap);
            groupBoxGoiTap.Controls.Add(labelGoiTap);
            groupBoxGoiTap.Dock = DockStyle.Fill;
            groupBoxGoiTap.Location = new Point(260, 3);
            groupBoxGoiTap.Margin = new Padding(3, 3, 10, 3);
            groupBoxGoiTap.Name = "groupBoxGoiTap";
            groupBoxGoiTap.Padding = new Padding(18);
            groupBoxGoiTap.Size = new Size(244, 120);
            groupBoxGoiTap.TabIndex = 1;
            groupBoxGoiTap.TabStop = false;
            // 
            // labelGoiTap
            // 
            labelGoiTap.AutoSize = true;
            labelGoiTap.ForeColor = Color.FromArgb(71, 85, 105);
            labelGoiTap.Location = new Point(21, 25);
            labelGoiTap.Name = "labelGoiTap";
            labelGoiTap.Size = new Size(141, 15);
            labelGoiTap.TabIndex = 0;
            labelGoiTap.Text = "GÓI SẮP HẾT HẠN";
            // 
            // lblSoGoiTap
            // 
            lblSoGoiTap.AutoSize = true;
            lblSoGoiTap.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblSoGoiTap.ForeColor = Color.FromArgb(217, 119, 6);
            lblSoGoiTap.Location = new Point(18, 51);
            lblSoGoiTap.Name = "lblSoGoiTap";
            lblSoGoiTap.Size = new Size(58, 45);
            lblSoGoiTap.TabIndex = 1;
            lblSoGoiTap.Text = "--";
            // 
            // groupBoxDoanhThu
            // 
            groupBoxDoanhThu.BackColor = Color.White;
            groupBoxDoanhThu.Controls.Add(lblSoDoanhThu);
            groupBoxDoanhThu.Controls.Add(labelDoanhThu);
            groupBoxDoanhThu.Dock = DockStyle.Fill;
            groupBoxDoanhThu.Location = new Point(517, 3);
            groupBoxDoanhThu.Margin = new Padding(3, 3, 10, 3);
            groupBoxDoanhThu.Name = "groupBoxDoanhThu";
            groupBoxDoanhThu.Padding = new Padding(18);
            groupBoxDoanhThu.Size = new Size(244, 120);
            groupBoxDoanhThu.TabIndex = 2;
            groupBoxDoanhThu.TabStop = false;
            // 
            // labelDoanhThu
            // 
            labelDoanhThu.AutoSize = true;
            labelDoanhThu.ForeColor = Color.FromArgb(71, 85, 105);
            labelDoanhThu.Location = new Point(21, 25);
            labelDoanhThu.Name = "labelDoanhThu";
            labelDoanhThu.Size = new Size(121, 15);
            labelDoanhThu.TabIndex = 0;
            labelDoanhThu.Text = "DOANH THU HÔM NAY";
            // 
            // lblSoDoanhThu
            // 
            lblSoDoanhThu.AutoSize = true;
            lblSoDoanhThu.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSoDoanhThu.ForeColor = Color.FromArgb(5, 150, 105);
            lblSoDoanhThu.Location = new Point(18, 55);
            lblSoDoanhThu.Name = "lblSoDoanhThu";
            lblSoDoanhThu.Size = new Size(50, 38);
            lblSoDoanhThu.TabIndex = 1;
            lblSoDoanhThu.Text = "--";
            // 
            // groupBoxLopTap
            // 
            groupBoxLopTap.BackColor = Color.White;
            groupBoxLopTap.Controls.Add(lblSoLopTap);
            groupBoxLopTap.Controls.Add(labelLopTap);
            groupBoxLopTap.Dock = DockStyle.Fill;
            groupBoxLopTap.Location = new Point(774, 3);
            groupBoxLopTap.Name = "groupBoxLopTap";
            groupBoxLopTap.Padding = new Padding(18);
            groupBoxLopTap.Size = new Size(251, 120);
            groupBoxLopTap.TabIndex = 3;
            groupBoxLopTap.TabStop = false;
            // 
            // labelLopTap
            // 
            labelLopTap.AutoSize = true;
            labelLopTap.ForeColor = Color.FromArgb(71, 85, 105);
            labelLopTap.Location = new Point(21, 25);
            labelLopTap.Name = "labelLopTap";
            labelLopTap.Size = new Size(95, 15);
            labelLopTap.TabIndex = 0;
            labelLopTap.Text = "LỚP ĐANG MỞ";
            // 
            // lblSoLopTap
            // 
            lblSoLopTap.AutoSize = true;
            lblSoLopTap.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblSoLopTap.ForeColor = Color.FromArgb(124, 58, 237);
            lblSoLopTap.Location = new Point(18, 51);
            lblSoLopTap.Name = "lblSoLopTap";
            lblSoLopTap.Size = new Size(58, 45);
            lblSoLopTap.TabIndex = 1;
            lblSoLopTap.Text = "--";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1340, 760);
            Controls.Add(panelNoiDung);
            Controls.Add(panelTieuDe);
            Controls.Add(panelMenu);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1100, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gym Pro - Quản lý phòng gym";
            Load += Form1_Load;
            panelMenu.ResumeLayout(false);
            panelThuongHieu.ResumeLayout(false);
            panelThuongHieu.PerformLayout();
            flowMenu.ResumeLayout(false);
            panelTieuDe.ResumeLayout(false);
            panelTieuDe.PerformLayout();
            panelNguoiDung.ResumeLayout(false);
            panelNguoiDung.PerformLayout();
            panelNoiDung.ResumeLayout(false);
            panelDashboard.ResumeLayout(false);
            groupBoxChaoMung.ResumeLayout(false);
            groupBoxChaoMung.PerformLayout();
            panelThongKe.ResumeLayout(false);
            groupBoxThanhVien.ResumeLayout(false);
            groupBoxThanhVien.PerformLayout();
            groupBoxGoiTap.ResumeLayout(false);
            groupBoxGoiTap.PerformLayout();
            groupBoxDoanhThu.ResumeLayout(false);
            groupBoxDoanhThu.PerformLayout();
            groupBoxLopTap.ResumeLayout(false);
            groupBoxLopTap.PerformLayout();
            ResumeLayout(false);
        }

        private void CauHinhNutMenu(Button button, string text, string tag, int tabIndex)
        {
            button.BackColor = Color.FromArgb(20, 32, 51);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10F);
            button.ForeColor = Color.FromArgb(226, 232, 240);
            button.Margin = new Padding(3, 3, 3, 4);
            button.Name = $"btn{tabIndex}";
            button.Size = new Size(216, 38);
            button.TabIndex = tabIndex;
            button.Tag = tag;
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.UseVisualStyleBackColor = false;
            button.Click += MenuButton_Click;
        }

        #endregion

        private Panel panelMenu;
        private Panel panelThuongHieu;
        private Label lblThuongHieu;
        private Label lblPhuDe;
        private FlowLayoutPanel flowMenu;
        private Button btnTongQuan;
        private Button btnThanhVien;
        private Button btnGoiTap;
        private Button btnDangKyGoi;
        private Button btnVanHanhGoi;
        private Button btnKhuyenMai;
        private Button btnHuanLuyenVien;
        private Button btnDangKyPT;
        private Button btnLopTap;
        private Button btnDiemDanh;
        private Button btnHoaDon;
        private Button btnSanPham;
        private Button btnThietBi;
        private Button btnBaoTri;
        private Button btnPhongTap;
        private Button btnNhanVien;
        private Button btnTaiKhoan;
        private Button btnThongKe;
        private Button btnDangXuat;
        private Panel panelTieuDe;
        private Label lblTieuDeHeThong;
        private Panel panelNguoiDung;
        private Label lblVaiTro;
        private Label lblTenNguoiDung;
        private Label lblThoiGian;
        private Button btnThongTin;
        private Panel panelNoiDung;
        private Panel panelDashboard;
        private GroupBox groupBoxChaoMung;
        private Label lblMoTaNoiDung;
        private Label lblTieuDeNoiDung;
        private TableLayoutPanel panelThongKe;
        private GroupBox groupBoxThanhVien;
        private Label lblSoThanhVien;
        private Label labelThanhVien;
        private GroupBox groupBoxGoiTap;
        private Label lblSoGoiTap;
        private Label labelGoiTap;
        private GroupBox groupBoxDoanhThu;
        private Label lblSoDoanhThu;
        private Label labelDoanhThu;
        private GroupBox groupBoxLopTap;
        private Label lblSoLopTap;
        private Label labelLopTap;
    }
}

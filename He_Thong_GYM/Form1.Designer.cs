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
            btnThanhVien.BackColor = Color.FromArgb(20, 32, 51);
            btnThanhVien.Cursor = Cursors.Hand;
            btnThanhVien.FlatAppearance.BorderSize = 0;
            btnThanhVien.FlatStyle = FlatStyle.Flat;
            btnThanhVien.Font = new Font("Segoe UI", 10F);
            btnThanhVien.ForeColor = Color.FromArgb(226, 232, 240);
            btnThanhVien.Margin = new Padding(3, 3, 3, 4);
            btnThanhVien.Name = "btnThanhVien";
            btnThanhVien.Size = new Size(216, 38);
            btnThanhVien.TabIndex = 1;
            btnThanhVien.Tag = "Quản lý thành viên";
            btnThanhVien.Text = "Quản lý thành viên";
            btnThanhVien.TextAlign = ContentAlignment.MiddleLeft;
            btnThanhVien.UseVisualStyleBackColor = false;
            btnThanhVien.Click += MenuButton_Click;
            // 
            // btnGoiTap
            // 
            btnGoiTap.BackColor = Color.FromArgb(20, 32, 51);
            btnGoiTap.Cursor = Cursors.Hand;
            btnGoiTap.FlatAppearance.BorderSize = 0;
            btnGoiTap.FlatStyle = FlatStyle.Flat;
            btnGoiTap.Font = new Font("Segoe UI", 10F);
            btnGoiTap.ForeColor = Color.FromArgb(226, 232, 240);
            btnGoiTap.Margin = new Padding(3, 3, 3, 4);
            btnGoiTap.Name = "btnGoiTap";
            btnGoiTap.Size = new Size(216, 38);
            btnGoiTap.TabIndex = 2;
            btnGoiTap.Tag = "Quản lý gói tập";
            btnGoiTap.Text = "Quản lý gói tập";
            btnGoiTap.TextAlign = ContentAlignment.MiddleLeft;
            btnGoiTap.UseVisualStyleBackColor = false;
            btnGoiTap.Click += MenuButton_Click;
            // 
            // btnDangKyGoi
            // 
            btnDangKyGoi.BackColor = Color.FromArgb(20, 32, 51);
            btnDangKyGoi.Cursor = Cursors.Hand;
            btnDangKyGoi.FlatAppearance.BorderSize = 0;
            btnDangKyGoi.FlatStyle = FlatStyle.Flat;
            btnDangKyGoi.Font = new Font("Segoe UI", 10F);
            btnDangKyGoi.ForeColor = Color.FromArgb(226, 232, 240);
            btnDangKyGoi.Margin = new Padding(3, 3, 3, 4);
            btnDangKyGoi.Name = "btnDangKyGoi";
            btnDangKyGoi.Size = new Size(216, 38);
            btnDangKyGoi.TabIndex = 3;
            btnDangKyGoi.Tag = "Đăng ký gói tập";
            btnDangKyGoi.Text = "Đăng ký gói tập";
            btnDangKyGoi.TextAlign = ContentAlignment.MiddleLeft;
            btnDangKyGoi.UseVisualStyleBackColor = false;
            btnDangKyGoi.Click += MenuButton_Click;
            // 
            // btnVanHanhGoi
            // 
            btnVanHanhGoi.BackColor = Color.FromArgb(20, 32, 51);
            btnVanHanhGoi.Cursor = Cursors.Hand;
            btnVanHanhGoi.FlatAppearance.BorderSize = 0;
            btnVanHanhGoi.FlatStyle = FlatStyle.Flat;
            btnVanHanhGoi.Font = new Font("Segoe UI", 10F);
            btnVanHanhGoi.ForeColor = Color.FromArgb(226, 232, 240);
            btnVanHanhGoi.Margin = new Padding(3, 3, 3, 4);
            btnVanHanhGoi.Name = "btnVanHanhGoi";
            btnVanHanhGoi.Size = new Size(216, 38);
            btnVanHanhGoi.TabIndex = 4;
            btnVanHanhGoi.Tag = "Vận hành gói / Check-in";
            btnVanHanhGoi.Text = "Vận hành gói / Check-in";
            btnVanHanhGoi.TextAlign = ContentAlignment.MiddleLeft;
            btnVanHanhGoi.UseVisualStyleBackColor = false;
            btnVanHanhGoi.Click += MenuButton_Click;
            // 
            // btnKhuyenMai
            // 
            btnKhuyenMai.BackColor = Color.FromArgb(20, 32, 51);
            btnKhuyenMai.Cursor = Cursors.Hand;
            btnKhuyenMai.FlatAppearance.BorderSize = 0;
            btnKhuyenMai.FlatStyle = FlatStyle.Flat;
            btnKhuyenMai.Font = new Font("Segoe UI", 10F);
            btnKhuyenMai.ForeColor = Color.FromArgb(226, 232, 240);
            btnKhuyenMai.Margin = new Padding(3, 3, 3, 4);
            btnKhuyenMai.Name = "btnKhuyenMai";
            btnKhuyenMai.Size = new Size(216, 38);
            btnKhuyenMai.TabIndex = 5;
            btnKhuyenMai.Tag = "Khuyến mãi";
            btnKhuyenMai.Text = "Khuyến mãi";
            btnKhuyenMai.TextAlign = ContentAlignment.MiddleLeft;
            btnKhuyenMai.UseVisualStyleBackColor = false;
            btnKhuyenMai.Click += MenuButton_Click;
            // 
            // btnHuanLuyenVien
            // 
            btnHuanLuyenVien.BackColor = Color.FromArgb(20, 32, 51);
            btnHuanLuyenVien.Cursor = Cursors.Hand;
            btnHuanLuyenVien.FlatAppearance.BorderSize = 0;
            btnHuanLuyenVien.FlatStyle = FlatStyle.Flat;
            btnHuanLuyenVien.Font = new Font("Segoe UI", 10F);
            btnHuanLuyenVien.ForeColor = Color.FromArgb(226, 232, 240);
            btnHuanLuyenVien.Margin = new Padding(3, 3, 3, 4);
            btnHuanLuyenVien.Name = "btnHuanLuyenVien";
            btnHuanLuyenVien.Size = new Size(216, 38);
            btnHuanLuyenVien.TabIndex = 6;
            btnHuanLuyenVien.Tag = "Huấn luyện viên / PT";
            btnHuanLuyenVien.Text = "Huấn luyện viên / PT";
            btnHuanLuyenVien.TextAlign = ContentAlignment.MiddleLeft;
            btnHuanLuyenVien.UseVisualStyleBackColor = false;
            btnHuanLuyenVien.Click += MenuButton_Click;
            // 
            // btnDangKyPT
            // 
            btnDangKyPT.BackColor = Color.FromArgb(20, 32, 51);
            btnDangKyPT.Cursor = Cursors.Hand;
            btnDangKyPT.FlatAppearance.BorderSize = 0;
            btnDangKyPT.FlatStyle = FlatStyle.Flat;
            btnDangKyPT.Font = new Font("Segoe UI", 10F);
            btnDangKyPT.ForeColor = Color.FromArgb(226, 232, 240);
            btnDangKyPT.Margin = new Padding(3, 3, 3, 4);
            btnDangKyPT.Name = "btnDangKyPT";
            btnDangKyPT.Size = new Size(216, 38);
            btnDangKyPT.TabIndex = 7;
            btnDangKyPT.Tag = "Đăng ký PT";
            btnDangKyPT.Text = "Đăng ký PT";
            btnDangKyPT.TextAlign = ContentAlignment.MiddleLeft;
            btnDangKyPT.UseVisualStyleBackColor = false;
            btnDangKyPT.Click += MenuButton_Click;
            // 
            // btnLopTap
            // 
            btnLopTap.BackColor = Color.FromArgb(20, 32, 51);
            btnLopTap.Cursor = Cursors.Hand;
            btnLopTap.FlatAppearance.BorderSize = 0;
            btnLopTap.FlatStyle = FlatStyle.Flat;
            btnLopTap.Font = new Font("Segoe UI", 10F);
            btnLopTap.ForeColor = Color.FromArgb(226, 232, 240);
            btnLopTap.Margin = new Padding(3, 3, 3, 4);
            btnLopTap.Name = "btnLopTap";
            btnLopTap.Size = new Size(216, 38);
            btnLopTap.TabIndex = 8;
            btnLopTap.Tag = "Lớp tập";
            btnLopTap.Text = "Lớp tập";
            btnLopTap.TextAlign = ContentAlignment.MiddleLeft;
            btnLopTap.UseVisualStyleBackColor = false;
            btnLopTap.Click += MenuButton_Click;
            // 
            // btnDiemDanh
            // 
            btnDiemDanh.BackColor = Color.FromArgb(20, 32, 51);
            btnDiemDanh.Cursor = Cursors.Hand;
            btnDiemDanh.FlatAppearance.BorderSize = 0;
            btnDiemDanh.FlatStyle = FlatStyle.Flat;
            btnDiemDanh.Font = new Font("Segoe UI", 10F);
            btnDiemDanh.ForeColor = Color.FromArgb(226, 232, 240);
            btnDiemDanh.Margin = new Padding(3, 3, 3, 4);
            btnDiemDanh.Name = "btnDiemDanh";
            btnDiemDanh.Size = new Size(216, 38);
            btnDiemDanh.TabIndex = 9;
            btnDiemDanh.Tag = "Đăng ký và điểm danh";
            btnDiemDanh.Text = "Đăng ký và điểm danh";
            btnDiemDanh.TextAlign = ContentAlignment.MiddleLeft;
            btnDiemDanh.UseVisualStyleBackColor = false;
            btnDiemDanh.Click += MenuButton_Click;
            // 
            // btnHoaDon
            // 
            btnHoaDon.BackColor = Color.FromArgb(20, 32, 51);
            btnHoaDon.Cursor = Cursors.Hand;
            btnHoaDon.FlatAppearance.BorderSize = 0;
            btnHoaDon.FlatStyle = FlatStyle.Flat;
            btnHoaDon.Font = new Font("Segoe UI", 10F);
            btnHoaDon.ForeColor = Color.FromArgb(226, 232, 240);
            btnHoaDon.Margin = new Padding(3, 3, 3, 4);
            btnHoaDon.Name = "btnHoaDon";
            btnHoaDon.Size = new Size(216, 38);
            btnHoaDon.TabIndex = 10;
            btnHoaDon.Tag = "Hóa đơn và thanh toán";
            btnHoaDon.Text = "Hóa đơn và thanh toán";
            btnHoaDon.TextAlign = ContentAlignment.MiddleLeft;
            btnHoaDon.UseVisualStyleBackColor = false;
            btnHoaDon.Click += MenuButton_Click;
            // 
            // btnSanPham
            // 
            btnSanPham.BackColor = Color.FromArgb(20, 32, 51);
            btnSanPham.Cursor = Cursors.Hand;
            btnSanPham.FlatAppearance.BorderSize = 0;
            btnSanPham.FlatStyle = FlatStyle.Flat;
            btnSanPham.Font = new Font("Segoe UI", 10F);
            btnSanPham.ForeColor = Color.FromArgb(226, 232, 240);
            btnSanPham.Margin = new Padding(3, 3, 3, 4);
            btnSanPham.Name = "btnSanPham";
            btnSanPham.Size = new Size(216, 38);
            btnSanPham.TabIndex = 11;
            btnSanPham.Tag = "Sản phẩm và bán hàng";
            btnSanPham.Text = "Sản phẩm và bán hàng";
            btnSanPham.TextAlign = ContentAlignment.MiddleLeft;
            btnSanPham.UseVisualStyleBackColor = false;
            btnSanPham.Click += MenuButton_Click;
            // 
            // btnThietBi
            // 
            btnThietBi.BackColor = Color.FromArgb(20, 32, 51);
            btnThietBi.Cursor = Cursors.Hand;
            btnThietBi.FlatAppearance.BorderSize = 0;
            btnThietBi.FlatStyle = FlatStyle.Flat;
            btnThietBi.Font = new Font("Segoe UI", 10F);
            btnThietBi.ForeColor = Color.FromArgb(226, 232, 240);
            btnThietBi.Margin = new Padding(3, 3, 3, 4);
            btnThietBi.Name = "btnThietBi";
            btnThietBi.Size = new Size(216, 38);
            btnThietBi.TabIndex = 12;
            btnThietBi.Tag = "Thiết bị";
            btnThietBi.Text = "Thiết bị";
            btnThietBi.TextAlign = ContentAlignment.MiddleLeft;
            btnThietBi.UseVisualStyleBackColor = false;
            btnThietBi.Click += MenuButton_Click;
            // 
            // btnBaoTri
            // 
            btnBaoTri.BackColor = Color.FromArgb(20, 32, 51);
            btnBaoTri.Cursor = Cursors.Hand;
            btnBaoTri.FlatAppearance.BorderSize = 0;
            btnBaoTri.FlatStyle = FlatStyle.Flat;
            btnBaoTri.Font = new Font("Segoe UI", 10F);
            btnBaoTri.ForeColor = Color.FromArgb(226, 232, 240);
            btnBaoTri.Margin = new Padding(3, 3, 3, 4);
            btnBaoTri.Name = "btnBaoTri";
            btnBaoTri.Size = new Size(216, 38);
            btnBaoTri.TabIndex = 13;
            btnBaoTri.Tag = "Bảo trì thiết bị";
            btnBaoTri.Text = "Bảo trì thiết bị";
            btnBaoTri.TextAlign = ContentAlignment.MiddleLeft;
            btnBaoTri.UseVisualStyleBackColor = false;
            btnBaoTri.Click += MenuButton_Click;
            // 
            // btnPhongTap
            // 
            btnPhongTap.BackColor = Color.FromArgb(20, 32, 51);
            btnPhongTap.Cursor = Cursors.Hand;
            btnPhongTap.FlatAppearance.BorderSize = 0;
            btnPhongTap.FlatStyle = FlatStyle.Flat;
            btnPhongTap.Font = new Font("Segoe UI", 10F);
            btnPhongTap.ForeColor = Color.FromArgb(226, 232, 240);
            btnPhongTap.Margin = new Padding(3, 3, 3, 4);
            btnPhongTap.Name = "btnPhongTap";
            btnPhongTap.Size = new Size(216, 38);
            btnPhongTap.TabIndex = 14;
            btnPhongTap.Tag = "Phòng tập";
            btnPhongTap.Text = "Phòng tập";
            btnPhongTap.TextAlign = ContentAlignment.MiddleLeft;
            btnPhongTap.UseVisualStyleBackColor = false;
            btnPhongTap.Click += MenuButton_Click;
            // 
            // btnNhanVien
            // 
            btnNhanVien.BackColor = Color.FromArgb(20, 32, 51);
            btnNhanVien.Cursor = Cursors.Hand;
            btnNhanVien.FlatAppearance.BorderSize = 0;
            btnNhanVien.FlatStyle = FlatStyle.Flat;
            btnNhanVien.Font = new Font("Segoe UI", 10F);
            btnNhanVien.ForeColor = Color.FromArgb(226, 232, 240);
            btnNhanVien.Margin = new Padding(3, 3, 3, 4);
            btnNhanVien.Name = "btnNhanVien";
            btnNhanVien.Size = new Size(216, 38);
            btnNhanVien.TabIndex = 15;
            btnNhanVien.Tag = "Nhân viên và tài khoản";
            btnNhanVien.Text = "Nhân viên và tài khoản";
            btnNhanVien.TextAlign = ContentAlignment.MiddleLeft;
            btnNhanVien.UseVisualStyleBackColor = false;
            btnNhanVien.Click += MenuButton_Click;
            // 
            // btnTaiKhoan
            // 
            btnTaiKhoan.BackColor = Color.FromArgb(20, 32, 51);
            btnTaiKhoan.Cursor = Cursors.Hand;
            btnTaiKhoan.FlatAppearance.BorderSize = 0;
            btnTaiKhoan.FlatStyle = FlatStyle.Flat;
            btnTaiKhoan.Font = new Font("Segoe UI", 10F);
            btnTaiKhoan.ForeColor = Color.FromArgb(226, 232, 240);
            btnTaiKhoan.Margin = new Padding(3, 3, 3, 4);
            btnTaiKhoan.Name = "btnTaiKhoan";
            btnTaiKhoan.Size = new Size(216, 38);
            btnTaiKhoan.TabIndex = 16;
            btnTaiKhoan.Tag = "Tài khoản và phân quyền";
            btnTaiKhoan.Text = "Tài khoản và phân quyền";
            btnTaiKhoan.TextAlign = ContentAlignment.MiddleLeft;
            btnTaiKhoan.UseVisualStyleBackColor = false;
            btnTaiKhoan.Click += MenuButton_Click;
            // 
            // btnThongKe
            // 
            btnThongKe.BackColor = Color.FromArgb(20, 32, 51);
            btnThongKe.Cursor = Cursors.Hand;
            btnThongKe.FlatAppearance.BorderSize = 0;
            btnThongKe.FlatStyle = FlatStyle.Flat;
            btnThongKe.Font = new Font("Segoe UI", 10F);
            btnThongKe.ForeColor = Color.FromArgb(226, 232, 240);
            btnThongKe.Margin = new Padding(3, 3, 3, 4);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(216, 38);
            btnThongKe.TabIndex = 17;
            btnThongKe.Tag = "Báo cáo thống kê";
            btnThongKe.Text = "Báo cáo thống kê";
            btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += MenuButton_Click;
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

using He_Thong_GYM.Forms;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM
{
    public partial class Form1 : Form
    {
        private Form? formConDangMo;
        private Button? _nutMenuHienTai;
        private readonly DashboardBLL _dashboardBLL = new();
        private readonly NguoiDung? _nguoiDung;
        private TableLayoutPanel? _dashboardRoot;
        private ComboBox? _cboKhoangDashboard;
        private DateTimePicker? _dtpTuNgayDashboard;
        private DateTimePicker? _dtpDenNgayDashboard;
        private Button? _btnApDungDashboard;
        private Button? _btnLamMoiDashboard;
        private DashboardBarPanel? _chartDoanhThu;
        private DashboardBarPanel? _chartTrangThai;
        private DashboardDonutPanel? _chartDonut;
        private DataGridView? _dgvGoiSapHetHan;
        private DataGridView? _dgvCongNo;
        private DataGridView? _dgvHoatDong;
        private DataGridView? _dgvGoiBanChay;
        private Label? _lblVanHanhTomTat;
        private GroupBox? _boxDoanhThuDashboard;
        private GroupBox? _boxCongNoDashboard;
        private GroupBox? _boxDonutDashboard;
        private readonly Dictionary<string, Label> _kpiDashboard = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, Label> _kpiSubDashboard = new(StringComparer.OrdinalIgnoreCase);
        private bool _dangKhoiTaoDashboard;

        public Form1() : this(null)
        {
        }

        public Form1(NguoiDung? nguoiDung)
        {
            _nguoiDung = nguoiDung;
            InitializeComponent();
            TaiLogo();
            PhanQuyenMenu();
            KhoiTaoDashboardNangCao();
        }

        private void PhanQuyenMenu()
        {
            foreach (Control control in flowMenu.Controls)
            {
                if (control is not Button button || button == btnTongQuan || button == btnDangXuat)
                    continue;

                button.Visible = button.Tag is string menu && AuthorizationPolicy.CanAccessMenu(menu);
            }
        }

        private void TaiLogo()
        {
            try
            {
                string[] cacDuongDan = [
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Huy hiệu GYM PRO neon xanh lam.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_sidebar.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "Huy hiệu GYM PRO neon xanh lam.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "images", "Huy hiệu GYM PRO neon xanh lam.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "logo_sidebar.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_gym.png")
                ];

                foreach (var path in cacDuongDan)
                {
                    if (File.Exists(path))
                    {
                        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                        using var temp = Image.FromStream(stream);
                        picLogo.Image = new Bitmap(temp);
                        return;
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu có ngoại lệ để không làm gián đoạn ứng dụng
            }
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                CapNhatNutMenuDangChon(btnTongQuan);
                lblThoiGian.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm");
                if (_nguoiDung is not null)
                {
                    lblTenNguoiDung.Text = _nguoiDung.HoTen;
                    lblVaiTro.Text = _nguoiDung.VaiTro;
                }

                await _dashboardBLL.CapNhatGoiHetHanAsync(_nguoiDung?.MaNV);
                await TaiTongQuanAsync();
            }
            catch (Exception)
            {
                lblMoTaNoiDung.Text = "Chưa tải được Dashboard. Hãy kiểm tra appsettings.json và kết nối SQL Server.";
            }
        }

        private void CapNhatNutMenuDangChon(Button? nutMoi)
        {
            if (_nutMenuHienTai is not null && _nutMenuHienTai != btnThongTin && _nutMenuHienTai != btnDangXuat)
            {
                _nutMenuHienTai.BackColor = Color.FromArgb(20, 32, 51);
                _nutMenuHienTai.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }

            _nutMenuHienTai = nutMoi;

            if (_nutMenuHienTai is not null && _nutMenuHienTai != btnThongTin && _nutMenuHienTai != btnDangXuat)
            {
                _nutMenuHienTai.BackColor = Color.FromArgb(37, 99, 235);
                _nutMenuHienTai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        private void MenuButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button || button.Tag is not string chucNang)
            {
                return;
            }

            if (button != btnThongTin && button != btnDangXuat)
            {
                CapNhatNutMenuDangChon(button);
            }

            if (!AuthorizationPolicy.CanAccessMenu(chucNang))
            {
                MessageBox.Show("Tài khoản hiện tại không có quyền truy cập chức năng này.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblTieuDeNoiDung.Text = chucNang;
            lblMoTaNoiDung.Text = $"Khu vực {chucNang.ToLowerInvariant()} đang sẵn sàng để tích hợp dữ liệu SQL Server.";

            if (chucNang == "Thông tin cá nhân")
            {
                if (SessionManager.CoVaiTro("Admin"))
                {
                    HienThiFormCon(new FrmTaiKhoan());
                    return;
                }

                MessageBox.Show(
                    $"Tài khoản: {SessionManager.NguoiDungHienTai?.TenDangNhap ?? "Khách"}\n" +
                    $"Họ và tên: {SessionManager.NguoiDungHienTai?.HoTen ?? "--"}\n" +
                    $"Vai trò: {SessionManager.NguoiDungHienTai?.VaiTro ?? "--"}",
                    "Thông tin người dùng hiện tại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (chucNang == "Quản lý thành viên")
            {
                HienThiFormCon(new FrmThanhVien());
                return;
            }

            if (chucNang == "Quản lý gói tập")
            {
                HienThiFormCon(new FrmGoiTap());
                return;
            }

            if (chucNang == "Huấn luyện viên / PT")
            {
                HienThiFormCon(new FrmHLV());
                return;
            }

            if (chucNang == "Đăng ký gói tập")
            {
                HienThiFormCon(new FrmDangKyGoi());
                return;
            }

            if (chucNang == "Vận hành gói / Check-in")
            {
                HienThiFormCon(new FrmVanHanhGoi());
                return;
            }

            if (chucNang == "Khuyến mãi")
            {
                HienThiFormCon(new FrmKhuyenMai());
                return;
            }

            if (chucNang == "Đăng ký PT")
            {
                HienThiFormCon(new FrmDangKyPT());
                return;
            }

            if (chucNang == "Lớp tập") { HienThiFormCon(new FrmLopTap()); return; }
            if (chucNang == "Đăng ký và điểm danh") { HienThiFormCon(new FrmDiemDanh()); return; }
            if (chucNang == "Hóa đơn và thanh toán") { HienThiFormCon(new FrmHoaDon()); return; }
            if (chucNang == "Sản phẩm và bán hàng") { HienThiFormCon(new FrmSanPham()); return; }
            if (chucNang == "Thiết bị") { HienThiFormCon(new FrmThietBi()); return; }
            if (chucNang == "Bảo trì thiết bị") { HienThiFormCon(new FrmBaoTri()); return; }
            if (chucNang == "Phòng tập") { HienThiFormCon(new FrmPhongTap()); return; }
            if (chucNang == "Báo cáo thống kê") { HienThiFormCon(new FrmThongKe()); return; }

            if (chucNang == "Nhân viên và tài khoản")
            {
                HienThiFormCon(new FrmNhanVien());
                return;
            }

            if (chucNang == "Tài khoản và phân quyền")
            {
                if (!SessionManager.CoVaiTro("Admin"))
                {
                    MessageBox.Show("Chỉ tài khoản Admin được quản lý tài khoản và phân quyền.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                HienThiFormCon(new FrmTaiKhoan());
                return;
            }

            if (chucNang == "Tổng quan")
            {
                HienThiTongQuan();
            }
        }

        private void HienThiFormCon(Form formCon)
        {
            formConDangMo?.Dispose();
            formConDangMo = formCon;
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;
            panelNoiDung.Controls.Add(formCon);
            panelDashboard.Visible = false;
            formCon.Show();
            formCon.BringToFront();
        }

        private void HienThiTongQuan()
        {
            CapNhatNutMenuDangChon(btnTongQuan);
            formConDangMo?.Dispose();
            formConDangMo = null;
            panelDashboard.Visible = true;
            panelDashboard.BringToFront();
            _ = TaiTongQuanAsync();
        }

        private async Task TaiTongQuanAsync()
        {
            try
            {
                var (tuNgay, denNgay) = LayKhoangDashboard();
                var tongQuan = await _dashboardBLL.LayTongQuanAsync(tuNgay, denNgay);
                CapNhatSoLieuDashboard(tongQuan, tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                lblMoTaNoiDung.Text = $"Chưa tải được Dashboard: {ex.Message}";
                if (_btnApDungDashboard is not null) _btnApDungDashboard.Enabled = true;
            }
        }

        private void KhoiTaoDashboardNangCao()
        {
            panelDashboard.Controls.Clear();
            panelDashboard.AutoScroll = true;
            panelDashboard.BackColor = Color.FromArgb(241, 245, 249);

            _dashboardRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Location = new Point(24, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Width = Math.Max(780, panelDashboard.ClientSize.Width - 48),
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(0)
            };
            _dashboardRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelDashboard.Controls.Add(_dashboardRoot);
            panelDashboard.Resize += (_, _) =>
            {
                if (_dashboardRoot is not null)
                    _dashboardRoot.Width = Math.Max(780, panelDashboard.ClientSize.Width - 48);
            };

            // === Row 0: Tiêu đề + Bộ lọc thời gian ===
            _dashboardRoot.Controls.Add(TaoBoLocDashboard(), 0, 0);

            // === Row 1: Dải KPI chính (8 thẻ: 4 cột x 2 hàng) ===
            _dashboardRoot.Controls.Add(TaoKpiDashboard(), 0, 1);

            // === Row 2: Biểu đồ doanh thu (bar) + Donut trạng thái hội viên ===
            var hangBieuDo = TaoHangBaCot(285, "📊 Doanh thu theo ngày", out _boxDoanhThuDashboard, "🍩 Trạng thái hội viên", out _boxDonutDashboard, "📈 Phân bổ hội viên", out var boxTrangThai);
            _chartDoanhThu = new DashboardBarPanel { Dock = DockStyle.Fill };
            _chartDonut = new DashboardDonutPanel { Dock = DockStyle.Fill };
            _chartTrangThai = new DashboardBarPanel { Dock = DockStyle.Fill };
            _boxDoanhThuDashboard!.Controls.Add(_chartDoanhThu);
            _boxDonutDashboard!.Controls.Add(_chartDonut);
            boxTrangThai.Controls.Add(_chartTrangThai);
            _dashboardRoot.Controls.Add(hangBieuDo, 0, 2);

            // === Row 3: Cảnh báo: Gói sắp hết hạn + Công nợ ===
            var hangCanhBao = TaoHangHaiCot(285, "⚠️ Gói sắp hết hạn", out var boxGoi, out _boxCongNoDashboard);
            _dgvGoiSapHetHan = TaoBangNangCao();
            _dgvCongNo = TaoBangNangCao();
            CauHinhBangGoiSapHetHan(_dgvGoiSapHetHan);
            CauHinhBangCongNo(_dgvCongNo);
            boxGoi.Controls.Add(_dgvGoiSapHetHan);
            _boxCongNoDashboard!.Controls.Add(_dgvCongNo);
            _dgvGoiSapHetHan.CellDoubleClick += (_, _) => HienThiFormCon(new FrmVanHanhGoi());
            _dgvCongNo.CellDoubleClick += (_, _) => HienThiFormCon(new FrmHoaDon());
            _dashboardRoot.Controls.Add(hangCanhBao, 0, 3);

            // === Row 4: Hoạt động gần đây + Gói bán chạy ===
            var hangHoatDong = TaoHangHaiCot(300, "📋 Hoạt động gần đây", out var boxHoatDong, out var boxBanChay);
            _dgvHoatDong = TaoBangNangCao();
            _dgvGoiBanChay = TaoBangNangCao();
            CauHinhBangHoatDong(_dgvHoatDong);
            CauHinhBangGoiBanChay(_dgvGoiBanChay);
            boxHoatDong.Controls.Add(_dgvHoatDong);
            _lblVanHanhTomTat = new Label { Dock = DockStyle.Top, Height = 48, AutoEllipsis = true, Font = new Font("Segoe UI", 8.25F), ForeColor = Color.FromArgb(71, 85, 105), Padding = new Padding(4, 4, 4, 6), BackColor = Color.FromArgb(248, 250, 252) };
            boxBanChay.Controls.Add(_dgvGoiBanChay);
            boxBanChay.Controls.Add(_lblVanHanhTomTat);
            _dashboardRoot.Controls.Add(hangHoatDong, 0, 4);

            _dangKhoiTaoDashboard = true;
            _cboKhoangDashboard!.SelectedIndex = 0;
            CapNhatNgayTheoLuaChon();
            _dangKhoiTaoDashboard = false;
        }

        private Panel TaoBoLocDashboard()
        {
            var panel = new Panel { Height = 118, Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20) };

            // Vạch gradient trên cùng
            var vachTren = new Panel { Dock = DockStyle.Top, Height = 4, BackColor = Color.FromArgb(37, 99, 235) };
            panel.Controls.Add(vachTren);

            var tieuDe = new Label { AutoSize = true, Text = "📊  TỔNG QUAN VẬN HÀNH", Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), Location = new Point(20, 18) };
            var moTa = new Label { AutoSize = true, Text = "Số liệu được cập nhật trực tiếp từ Database theo khoảng thời gian đã chọn.", Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(22, 52) };
            panel.Controls.Add(tieuDe);
            panel.Controls.Add(moTa);

            var nhanKhoang = new Label { AutoSize = true, Text = "Khoảng thời gian", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), Location = new Point(22, 82) };
            _cboKhoangDashboard = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9F), Location = new Point(135, 78), Width = 145, BackColor = Color.FromArgb(248, 250, 252) };
            _cboKhoangDashboard.Items.AddRange(["Hôm nay", "Hôm qua", "7 ngày qua", "Tháng này", "Tháng trước", "Quý này", "Năm nay", "Tùy chọn"]);
            _cboKhoangDashboard.SelectedIndexChanged += (_, _) => { CapNhatNgayTheoLuaChon(); if (!_dangKhoiTaoDashboard && _cboKhoangDashboard.SelectedIndex != 7) _ = TaiTongQuanAsync(); };
            _dtpTuNgayDashboard = TaoDatePicker();
            _dtpDenNgayDashboard = TaoDatePicker();
            _dtpTuNgayDashboard.Location = new Point(298, 78);
            _dtpDenNgayDashboard.Location = new Point(414, 78);
            _dtpTuNgayDashboard.ValueChanged += (_, _) => CapNhatTrangThaiNutApDung();
            _dtpDenNgayDashboard.ValueChanged += (_, _) => CapNhatTrangThaiNutApDung();
            _btnApDungDashboard = TaoNutDashboard("✔ Áp dụng", Color.FromArgb(37, 99, 235));
            _btnApDungDashboard.Location = new Point(530, 75);
            _btnApDungDashboard.Click += async (_, _) => await TaiTongQuanAsync();
            _btnLamMoiDashboard = TaoNutDashboard("⟳ Làm mới", Color.FromArgb(5, 150, 105));
            _btnLamMoiDashboard.Location = new Point(626, 75);
            _btnLamMoiDashboard.Click += async (_, _) => await TaiTongQuanAsync();
            panel.Controls.AddRange([nhanKhoang, _cboKhoangDashboard, _dtpTuNgayDashboard, _dtpDenNgayDashboard, _btnApDungDashboard, _btnLamMoiDashboard]);
            return panel;
        }

        private static DateTimePicker TaoDatePicker() => new()
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Font = new Font("Segoe UI", 9F),
            Width = 105,
            Height = 25,
            ShowUpDown = false
        };

        private static Button TaoNutDashboard(string text, Color color) => new()
        {
            Text = text,
            BackColor = color,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Size = new Size(88, 32)
        };

        private TableLayoutPanel TaoKpiDashboard()
        {
            var table = new TableLayoutPanel { ColumnCount = 4, RowCount = 2, Height = 196, Dock = DockStyle.Fill, BackColor = Color.FromArgb(241, 245, 249), Padding = new Padding(0, 10, 0, 6) };
            for (var i = 0; i < 4; i++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (var i = 0; i < 2; i++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            var cards = new[]
            {
                ("👥", "Tổng hội viên", "TongThanhVien", "TV_Sub", Color.FromArgb(37,99,235)),
                ("✅", "Hội viên hoạt động", "ThanhVienHoatDong", "TVHD_Sub", Color.FromArgb(5,150,105)),
                ("⏰", "Gói sắp hết hạn", "GoiSapHetHan", "GSH_Sub", Color.FromArgb(217,119,6)),
                ("📦", "Gói đang sử dụng", "GoiDangSuDung", "GSD_Sub", Color.FromArgb(124,58,237)),
                ("💰", "Doanh thu trong kỳ", "DoanhThuTrongKy", "DT_Sub", Color.FromArgb(5,150,105)),
                ("📋", "Tổng công nợ", "TongCongNo", "CN_Sub", Color.FromArgb(220,38,38)),
                ("🏃", "Đang có mặt", "DangCoMat", "CM_Sub", Color.FromArgb(14,116,144)),
                ("🏋️", "Buổi PT trong kỳ", "BuoiPTTrongKy", "PT_Sub", Color.FromArgb(190,24,93))
            };
            for (var i = 0; i < cards.Length; i++) table.Controls.Add(TaoTheKpi(cards[i].Item1, cards[i].Item2, cards[i].Item3, cards[i].Item4, cards[i].Item5), i % 4, i / 4);
            return table;
        }

        private Panel TaoTheKpi(string icon, string tieuDe, string khoa, string khoaPhu, Color mau)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(4, 4, 8, 4), Padding = new Padding(14, 8, 8, 6) };
            var vach = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = mau };
            var iconLabel = new Label { AutoSize = true, Text = icon, Font = new Font("Segoe UI", 16F), Location = new Point(16, 6) };
            var label = new Label { AutoSize = true, Text = tieuDe.ToUpperInvariant(), Font = new Font("Segoe UI", 7.5F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(44, 11) };
            var giaTri = new Label { AutoSize = true, Text = "--", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = mau, Location = new Point(16, 34) };
            var phuDe = new Label { AutoSize = true, Text = "", Font = new Font("Segoe UI", 7.5F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(16, 64) };
            _kpiDashboard[khoa] = giaTri;
            _kpiSubDashboard[khoaPhu] = phuDe;
            panel.Controls.Add(phuDe); panel.Controls.Add(giaTri); panel.Controls.Add(label); panel.Controls.Add(iconLabel); panel.Controls.Add(vach);
            return panel;
        }

        private static TableLayoutPanel TaoHangHaiCot(int chieuCao, string trai, out GroupBox boxTrai, out GroupBox boxPhai)
        {
            var table = new TableLayoutPanel { ColumnCount = 2, RowCount = 1, Height = chieuCao, Dock = DockStyle.Fill, BackColor = Color.FromArgb(241, 245, 249), Padding = new Padding(0, 0, 0, 8) };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            boxTrai = TaoNhomDashboard(trai); boxPhai = TaoNhomDashboard(trai == "📊 Doanh thu theo ngày" ? "📈 Phân bổ hội viên" : trai == "⚠️ Gói sắp hết hạn" ? "💳 Công nợ cần xử lý" : "🏆 Gói bán chạy");
            table.Controls.Add(boxTrai, 0, 0); table.Controls.Add(boxPhai, 1, 0);
            return table;
        }

        private static TableLayoutPanel TaoHangBaCot(int chieuCao, string cot1, out GroupBox box1, string cot2, out GroupBox box2, string cot3, out GroupBox box3)
        {
            var table = new TableLayoutPanel { ColumnCount = 3, RowCount = 1, Height = chieuCao, Dock = DockStyle.Fill, BackColor = Color.FromArgb(241, 245, 249), Padding = new Padding(0, 0, 0, 8) };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            box1 = TaoNhomDashboard(cot1);
            box2 = TaoNhomDashboard(cot2);
            box3 = TaoNhomDashboard(cot3);
            table.Controls.Add(box1, 0, 0); table.Controls.Add(box2, 1, 0); table.Controls.Add(box3, 2, 0);
            return table;
        }

        private static GroupBox TaoNhomDashboard(string tieuDe) => new()
        {
            Text = tieuDe,
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            ForeColor = Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Padding = new Padding(10),
            Margin = new Padding(4, 3, 8, 3)
        };

        private static DataGridView TaoBangNangCao() => new()
        {
            Dock = DockStyle.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoGenerateColumns = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false,
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
            EnableHeadersVisualStyles = false,
            ColumnHeadersHeight = 32,
            ColumnHeadersDefaultCellStyle = { BackColor = Color.FromArgb(241, 245, 249), ForeColor = Color.FromArgb(30, 41, 59), Font = new Font("Segoe UI", 8F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleLeft },
            DefaultCellStyle = { Font = new Font("Segoe UI", 8.25F), SelectionBackColor = Color.FromArgb(219, 234, 254), SelectionForeColor = Color.FromArgb(15, 23, 42), Padding = new Padding(2, 0, 2, 0) },
            AlternatingRowsDefaultCellStyle = { BackColor = Color.FromArgb(248, 250, 252) },
            GridColor = Color.FromArgb(226, 232, 240),
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        };

        private static void ThemCot(DataGridView bang, string ten, string tieuDe, int doRong, string? dinhDang = null)
        {
            bang.Columns.Add(new DataGridViewTextBoxColumn { Name = ten, HeaderText = tieuDe, Width = doRong, DataPropertyName = ten, DefaultCellStyle = new DataGridViewCellStyle { Format = dinhDang ?? string.Empty } });
        }

        private static void CauHinhBangGoiSapHetHan(DataGridView bang)
        {
            ThemCot(bang, "MaThanhVien", "Mã TV", 72); ThemCot(bang, "HoTen", "Hội viên", 115); ThemCot(bang, "SDT", "SĐT", 86); ThemCot(bang, "TenGoi", "Gói", 100); ThemCot(bang, "NgayKetThuc", "Hết hạn", 78, "dd/MM/yyyy"); ThemCot(bang, "SoNgayConLai", "Còn ngày", 62); ThemCot(bang, "TrangThai", "Trạng thái", 82);
        }

        private static void CauHinhBangCongNo(DataGridView bang)
        {
            ThemCot(bang, "SoHoaDon", "Hóa đơn", 82); ThemCot(bang, "HoTen", "Hội viên", 110); ThemCot(bang, "TongTien", "Tổng", 82, "N0"); ThemCot(bang, "DaThanhToan", "Đã thu", 82, "N0"); ThemCot(bang, "ConNo", "Còn nợ", 82, "N0");
        }

        private static void CauHinhBangHoatDong(DataGridView bang)
        {
            ThemCot(bang, "ThoiGian", "Thời gian", 100, "dd/MM HH:mm"); ThemCot(bang, "HanhDong", "Hoạt động", 105); ThemCot(bang, "DoiTuong", "Đối tượng", 100); ThemCot(bang, "NoiDung", "Nội dung", 230);
        }

        private static void CauHinhBangGoiBanChay(DataGridView bang)
        {
            ThemCot(bang, "TenGoi", "Tên gói", 150); ThemCot(bang, "SoLuotDangKy", "Đăng ký", 70, "N0"); ThemCot(bang, "DoanhThu", "Doanh thu", 100, "N0");
        }

        private (DateTime TuNgay, DateTime DenNgay) LayKhoangDashboard()
        {
            if (_cboKhoangDashboard is null || _dtpTuNgayDashboard is null || _dtpDenNgayDashboard is null)
                return (DateTime.Today, DateTime.Today);
            var homNay = DateTime.Today;
            return _cboKhoangDashboard.SelectedIndex switch
            {
                1 => (homNay.AddDays(-1), homNay.AddDays(-1)),
                2 => (homNay.AddDays(-6), homNay),
                3 => (new DateTime(homNay.Year, homNay.Month, 1), homNay),
                4 => (new DateTime(homNay.Year, homNay.Month, 1).AddMonths(-1), new DateTime(homNay.Year, homNay.Month, 1).AddDays(-1)),
                5 => (new DateTime(homNay.Year, ((homNay.Month - 1) / 3) * 3 + 1, 1), homNay),
                6 => (new DateTime(homNay.Year, 1, 1), homNay),
                7 => (_dtpTuNgayDashboard.Value.Date, _dtpDenNgayDashboard.Value.Date),
                _ => (homNay, homNay)
            };
        }

        private void CapNhatNgayTheoLuaChon()
        {
            if (_cboKhoangDashboard is null || _dtpTuNgayDashboard is null || _dtpDenNgayDashboard is null) return;
            var khoang = _cboKhoangDashboard.SelectedIndex;
            var homNay = DateTime.Today;
            if (khoang == 7) { _dtpTuNgayDashboard.Enabled = true; _dtpDenNgayDashboard.Enabled = true; return; }
            var (tu, den) = LayKhoangDashboard();
            _dtpTuNgayDashboard.Value = tu; _dtpDenNgayDashboard.Value = den;
            _dtpTuNgayDashboard.Enabled = false; _dtpDenNgayDashboard.Enabled = false;
            _ = homNay;
        }

        private void CapNhatTrangThaiNutApDung()
        {
            if (_btnApDungDashboard is not null && _dtpTuNgayDashboard is not null && _dtpDenNgayDashboard is not null)
                _btnApDungDashboard.Enabled = _dtpTuNgayDashboard.Value.Date <= _dtpDenNgayDashboard.Value.Date;
        }

        private void CapNhatSoLieuDashboard(TongQuanDashboard data, DateTime tuNgay, DateTime denNgay)
        {
            var coQuyenTaiChinh = !SessionManager.CoVaiTro("HLV", "Huấn luyện viên");
            GanKpi("TongThanhVien", data.TongThanhVien.ToString("N0")); GanKpi("ThanhVienHoatDong", data.ThanhVienHoatDong.ToString("N0")); GanKpi("GoiSapHetHan", data.GoiSapHetHan.ToString("N0")); GanKpi("GoiDangSuDung", data.GoiDangSuDung.ToString("N0"));
            GanKpi("DoanhThuTrongKy", coQuyenTaiChinh ? $"{data.DoanhThuTrongKy:N0} đ" : "Ẩn"); GanKpi("TongCongNo", coQuyenTaiChinh ? $"{data.TongCongNo:N0} đ" : "Ẩn"); GanKpi("DangCoMat", data.DangCoMat.ToString("N0")); GanKpi("BuoiPTTrongKy", data.BuoiPTTrongKy.ToString("N0"));

            // Phụ đề KPI
            GanKpiSub("TV_Sub", $"Mới: {data.ThanhVienMoi:N0} • Hết hạn: {data.ThanhVienHetHan:N0}");
            GanKpiSub("TVHD_Sub", $"Bảo lưu: {data.ThanhVienBaoLuu:N0}");
            GanKpiSub("GSH_Sub", $"Hết hạn: {data.GoiHetHan:N0} • Bảo lưu: {data.GoiBaoLuu:N0}");
            GanKpiSub("GSD_Sub", $"Gói tập đang bán: {data.TongGoiTapDangBan:N0}");
            GanKpiSub("DT_Sub", coQuyenTaiChinh ? $"Hôm nay: {data.DoanhThuHomNay:N0} đ • Tháng: {data.DoanhThuThangNay:N0} đ" : "");
            GanKpiSub("CN_Sub", coQuyenTaiChinh ? $"{data.CongNoChiTiet.Count:N0} hóa đơn chưa thu đủ" : "");
            GanKpiSub("CM_Sub", $"Check-in: {data.CheckInTrongKy:N0} • Cao điểm: {(data.GioCaoDiem >= 0 ? $"{data.GioCaoDiem:00}:00" : "--")}");
            GanKpiSub("PT_Sub", $"Hoàn thành: {data.BuoiPTDaHoanThanh:N0} • Chờ: {data.BuoiPTChuaThucHien:N0}");

            lblSoThanhVien.Text = data.TongThanhVien.ToString("N0"); lblSoGoiTap.Text = data.GoiSapHetHan.ToString("N0"); lblSoDoanhThu.Text = $"{data.DoanhThuHomNay:N0} đ"; lblSoLopTap.Text = data.LopDangMo.ToString("N0");
            lblTieuDeNoiDung.Text = "Tổng quan vận hành";
            lblMoTaNoiDung.Text = $"Dữ liệu từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy} • Cập nhật {DateTime.Now:dd/MM/yyyy HH:mm}";

            if (_chartDoanhThu is not null) _chartDoanhThu.SetData(data.DoanhThuTheoNgay.Select(x => ($"{x.Ngay:dd/MM}", x.DoanhThu, Color.FromArgb(37, 99, 235))));
            if (_chartTrangThai is not null) _chartTrangThai.SetData(data.ThanhVienTheoTrangThai.Select((x, i) => (x.TrangThai, (decimal)x.SoLuong, new[] { Color.FromArgb(5, 150, 105), Color.FromArgb(220, 38, 38), Color.FromArgb(217, 119, 6), Color.FromArgb(100, 116, 139) }[i % 4])));

            // Cập nhật Donut chart trạng thái hội viên
            if (_chartDonut is not null)
            {
                var donutData = data.ThanhVienTheoTrangThai.Select((x, i) => (x.TrangThai, (decimal)x.SoLuong, new[]
                {
                    Color.FromArgb(5, 150, 105),   // Hoạt động - xanh lá
                    Color.FromArgb(220, 38, 38),    // Hết hạn - đỏ
                    Color.FromArgb(217, 119, 6),    // Bảo lưu - cam
                    Color.FromArgb(100, 116, 139),  // Khác - xám
                    Color.FromArgb(14, 116, 144),   // Teal
                    Color.FromArgb(124, 58, 237)    // Tím
                }[i % 6]));
                _chartDonut.SetData(donutData);
            }

            NapBangGoiSapHetHan(data.GoiSapHetHanChiTiet); NapBangCongNo(data.CongNoChiTiet); NapBangHoatDong(data.HoatDongGanDay); NapBangGoiBanChay(data.GoiBanChay);
            if (_lblVanHanhTomTat is not null)
            {
                var gioCaoDiem = data.GioCaoDiem >= 0 ? $"{data.GioCaoDiem:00}:00" : "--";
                _lblVanHanhTomTat.Text = $"📊 Check-in: {data.CheckInTrongKy:N0}  •  Check-out: {data.CheckOutTrongKy:N0}  •  Cao điểm: {gioCaoDiem}\n🔄 Bảo lưu: {data.GoiBaoLuu:N0} gói  •  Gia hạn: {data.GoiGiaHan:N0}  •  PT hoàn thành: {data.BuoiPTDaHoanThanh:N0}/{data.BuoiPTTrongKy:N0}";
            }
            if (_boxDoanhThuDashboard is not null) _boxDoanhThuDashboard.Visible = coQuyenTaiChinh;
            if (_boxCongNoDashboard is not null) _boxCongNoDashboard.Visible = coQuyenTaiChinh;
            if (_boxDonutDashboard is not null) _boxDonutDashboard.Visible = true;
        }

        private void GanKpi(string khoa, string giaTri) { if (_kpiDashboard.TryGetValue(khoa, out var label)) label.Text = giaTri; }
        private void GanKpiSub(string khoa, string giaTri) { if (_kpiSubDashboard.TryGetValue(khoa, out var label)) label.Text = giaTri; }

        private void NapBangGoiSapHetHan(List<DashboardSapHetHanView> items)
        {
            if (_dgvGoiSapHetHan is null) return; _dgvGoiSapHetHan.Rows.Clear(); foreach (var x in items) { var i = _dgvGoiSapHetHan.Rows.Add(x.MaThanhVien,x.HoTen,x.SDT,x.TenGoi,x.NgayKetThuc,x.SoNgayConLai,x.TrangThai); var row = _dgvGoiSapHetHan.Rows[i]; row.DefaultCellStyle.BackColor = x.SoNgayConLai <= 0 ? Color.FromArgb(254,226,226) : x.SoNgayConLai <= 3 ? Color.FromArgb(255,237,213) : Color.FromArgb(254,249,195); }
        }

        private void NapBangCongNo(List<DashboardCongNoView> items)
        {
            if (_dgvCongNo is null) return; _dgvCongNo.Rows.Clear(); foreach (var x in items) _dgvCongNo.Rows.Add(x.SoHoaDon,x.HoTen,x.TongTien,x.DaThanhToan,x.ConNo);
        }

        private void NapBangHoatDong(List<DashboardHoatDongView> items)
        {
            if (_dgvHoatDong is null) return; _dgvHoatDong.Rows.Clear(); foreach (var x in items) _dgvHoatDong.Rows.Add(x.ThoiGian,x.HanhDong,x.DoiTuong,x.NoiDung);
        }

        private void NapBangGoiBanChay(List<DashboardGoiBanChayView> items)
        {
            if (_dgvGoiBanChay is null) return; _dgvGoiBanChay.Rows.Clear(); foreach (var x in items) _dgvGoiBanChay.Rows.Add(x.TenGoi,x.SoLuotDangKy,x.DoanhThu);
        }

        private void btnDangXuat_Click(object? sender, EventArgs e)
        {
            var xacNhan = MessageBox.Show(
                "Bạn có muốn đăng xuất khỏi hệ thống?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                SessionManager.DangXuat();
                Close();
            }
        }
    }
}

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

        public Form1() : this(null)
        {
        }

        public Form1(NguoiDung? nguoiDung)
        {
            _nguoiDung = nguoiDung;
            InitializeComponent();
            TaiLogo();
            PhanQuyenMenu();
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
                var tongQuan = await _dashboardBLL.LayTongQuanAsync();
                lblSoThanhVien.Text = tongQuan.TongThanhVien.ToString("N0");
                lblSoGoiTap.Text = tongQuan.GoiSapHetHan.ToString("N0");
                lblSoDoanhThu.Text = $"{tongQuan.DoanhThuHomNay:N0} đ";
                lblSoLopTap.Text = tongQuan.LopDangMo.ToString("N0");
            }
            catch (Exception)
            {
                lblMoTaNoiDung.Text = "Chưa tải được Dashboard. Hãy kiểm tra appsettings.json và kết nối SQL Server.";
            }
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

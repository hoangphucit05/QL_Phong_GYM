using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmLopTap : Form
{
    private readonly LopTapBLL _bll = new();
    private readonly DangKyBLL _dk = new();

    public FrmLopTap()
    {
        InitializeComponent();
    }

    private async void FrmLopTap_Load(object? sender, EventArgs e)
    {
        try
        {
            cboLoai.DataSource = (await _bll.LayLoaiAsync())
                .Select(x => new KeyValuePair<int, string>(x.Item1, x.Item2))
                .ToList();
            cboLoai.DisplayMember = "Value";
            cboLoai.ValueMember = "Key";

            cboHLV.DataSource = await _dk.LayHLVAsync();
            cboHLV.DisplayMember = nameof(HuanLuyenVien.HoTen);
            cboHLV.ValueMember = nameof(HuanLuyenVien.MaHLV);

            dgvLop.DataSource = await _bll.LayDanhSachAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboHLV.SelectedItem is not HuanLuyenVien h || cboLoai.SelectedValue is not int loai)
            {
                throw new InvalidOperationException("Hãy chọn loại lớp và HLV.");
            }

            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                throw new InvalidOperationException("Vui lòng nhập tên lớp tập.");
            }

            await _bll.ThemAsync(new LopTap
            {
                TenLop = txtTenLop.Text.Trim(),
                MaLoaiLop = loai,
                MaHLV = h.MaHLV,
                SucChua = (int)nudSucChua.Value,
                GioBatDau = dtpBatDau.Value.TimeOfDay,
                GioKetThuc = dtpKetThuc.Value.TimeOfDay,
                TrangThai = cboTrangThai.Text
            });

            dgvLop.DataSource = await _bll.LayDanhSachAsync();
            txtTenLop.Clear();
            MessageBox.Show("Đã tạo lớp tập mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể lưu lớp tập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

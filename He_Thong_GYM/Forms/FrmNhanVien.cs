using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;
namespace He_Thong_GYM.Forms;
public partial class FrmNhanVien:Form
{
 private readonly NhanVienBLL _bll=new();private int? _maNV;
 public FrmNhanVien(){InitializeComponent();}
 private async void FrmNhanVien_Load(object? s,EventArgs e){cboGioiTinh.SelectedIndex=0;cboTrangThai.SelectedIndex=0;await TaiAsync();}
 private async Task TaiAsync(string? k=null){try{dgvNhanVien.DataSource=await _bll.LayDanhSachAsync(k);lblTrangThaiTimKiem.Text=string.IsNullOrWhiteSpace(k)?"Hiển thị toàn bộ nhân viên.":$"Đã lọc: {k}";}catch(Exception ex){MessageBox.Show(ex.Message,"Lỗi dữ liệu",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
 private void dgvNhanVien_SelectionChanged(object? s,EventArgs e){if(dgvNhanVien.CurrentRow?.DataBoundItem is not NhanVien n)return;_maNV=n.MaNV;txtHoTen.Text=n.HoTen;dtpNgaySinh.Value=n.NgaySinh;cboGioiTinh.SelectedItem=n.GioiTinh??"Khác";txtSDT.Text=n.SDT;txtEmail.Text=n.Email;txtDiaChi.Text=n.DiaChi;txtChucVu.Text=n.ChucVu;nudLuong.Value=n.Luong??0;cboTrangThai.SelectedItem=n.TrangThai;}
 private void btnThem_Click(object? s,EventArgs e){_maNV=null;txtHoTen.Clear();txtSDT.Clear();txtEmail.Clear();txtDiaChi.Clear();txtChucVu.Clear();nudLuong.Value=0;dtpNgaySinh.Value=DateTime.Today.AddYears(-22);cboGioiTinh.SelectedIndex=0;cboTrangThai.SelectedIndex=0;txtHoTen.Focus();}
 private async void btnLuu_Click(object? s,EventArgs e){try{await _bll.LuuAsync(new NhanVien{MaNV=_maNV??0,HoTen=txtHoTen.Text.Trim(),NgaySinh=dtpNgaySinh.Value,GioiTinh=cboGioiTinh.Text,SDT=txtSDT.Text.Trim(),Email=string.IsNullOrWhiteSpace(txtEmail.Text)?null:txtEmail.Text.Trim(),DiaChi=string.IsNullOrWhiteSpace(txtDiaChi.Text)?null:txtDiaChi.Text.Trim(),ChucVu=txtChucVu.Text.Trim(),Luong=nudLuong.Value,TrangThai=cboTrangThai.Text});await TaiAsync(txtTimKiem.Text);MessageBox.Show("Đã lưu nhân viên.","Thành công",MessageBoxButtons.OK,MessageBoxIcon.Information);}catch(Exception ex){MessageBox.Show(ex.Message,"Không thể lưu",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
 private async void btnTimKiem_Click(object? s,EventArgs e)=>await TaiAsync(txtTimKiem.Text);
 private async void btnNghiViec_Click(object? s,EventArgs e){if(_maNV is null)return;if(MessageBox.Show("Chuyển nhân viên sang trạng thái nghỉ việc và khóa tài khoản?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;try{await _bll.NghiViecAsync(_maNV.Value);await TaiAsync(txtTimKiem.Text);}catch(Exception ex){MessageBox.Show(ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
}

using He_Thong_GYM.BLL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.Forms;
public partial class FrmHLV:Form
{
 private readonly HuanLuyenVienBLL _hlvBLL=new();private readonly NhanVienBLL _nhanVienBLL=new();private int? _maHLV;
 public FrmHLV(){InitializeComponent();}
 private async void FrmHLV_Load(object? s,EventArgs e){try{cboNhanVien.DataSource=await _nhanVienBLL.LayNhanVienDangLamAsync();cboNhanVien.DisplayMember=nameof(NhanVien.HienThi);cboNhanVien.ValueMember=nameof(NhanVien.MaNV);await TaiAsync();}catch(Exception ex){MessageBox.Show(UiErrorMessage.Get(ex),"Lỗi dữ liệu",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
 private async Task TaiAsync(string? k=null){try{dgvHLV.DataSource=await _hlvBLL.LayDanhSachAsync(k);lblTrangThai.Text=string.IsNullOrWhiteSpace(k)?"Hiển thị toàn bộ huấn luyện viên.":$"Đã lọc: {k}";}catch(Exception ex){MessageBox.Show(UiErrorMessage.Get(ex),"Lỗi dữ liệu",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
 private void dgvHLV_SelectionChanged(object? s,EventArgs e){if(dgvHLV.CurrentRow?.DataBoundItem is not HuanLuyenVien h)return;_maHLV=h.MaHLV;cboNhanVien.SelectedValue=h.MaNV;txtChuyenMon.Text=h.ChuyenMon;nudKinhNghiem.Value=h.KinhNghiem??0;nudPhiThue.Value=h.PhiThue??0;}
 private void btnThem_Click(object? s,EventArgs e){_maHLV=null;txtChuyenMon.Clear();nudKinhNghiem.Value=0;nudPhiThue.Value=0;if(cboNhanVien.Items.Count>0)cboNhanVien.SelectedIndex=0;}
 private async void btnLuu_Click(object? s,EventArgs e){try{await _hlvBLL.LuuAsync(new HuanLuyenVien{MaHLV=_maHLV??0,MaNV=cboNhanVien.SelectedValue is int maNV?maNV:0,ChuyenMon=string.IsNullOrWhiteSpace(txtChuyenMon.Text)?null:txtChuyenMon.Text.Trim(),KinhNghiem=(int)nudKinhNghiem.Value,PhiThue=nudPhiThue.Value});await TaiAsync(txtTimKiem.Text);MessageBox.Show("Đã lưu hồ sơ HLV.","Thành công",MessageBoxButtons.OK,MessageBoxIcon.Information);}catch(Exception ex){MessageBox.Show(UiErrorMessage.Get(ex),"Không thể lưu",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
 private async void btnTimKiem_Click(object? s,EventArgs e)=>await TaiAsync(txtTimKiem.Text);
}

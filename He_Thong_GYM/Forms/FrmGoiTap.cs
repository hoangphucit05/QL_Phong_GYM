using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;
namespace He_Thong_GYM.Forms;
public partial class FrmGoiTap:Form
{
    private readonly GoiTapBLL _bll=new(); private int? _maGoi;
    public FrmGoiTap(){InitializeComponent();}
    private async void FrmGoiTap_Load(object? s,EventArgs e){cboTrangThai.SelectedIndex=0;await TaiAsync();}
    private async Task TaiAsync(string? k=null){try{dgvGoiTap.DataSource=await _bll.LayDanhSachAsync(k);lblTrangThai.Text=string.IsNullOrWhiteSpace(k)?"Hiển thị toàn bộ gói tập.":$"Đã lọc: {k}";}catch(Exception ex){lblTrangThai.Text="Không tải được dữ liệu. Kiểm tra SQL Server.";MessageBox.Show(UiErrorMessage.Get(ex),"Lỗi dữ liệu",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
    private void dgvGoiTap_SelectionChanged(object? s,EventArgs e){if(dgvGoiTap.CurrentRow?.DataBoundItem is not GoiTap g)return;_maGoi=g.MaGoi;txtTenGoi.Text=g.TenGoi;nudThoiHan.Value=g.ThoiHanNgay;nudGia.Value=g.Gia;txtMoTa.Text=g.MoTa;cboTrangThai.SelectedItem=g.TrangThai;}
    private void btnThem_Click(object? s,EventArgs e){_maGoi=null;txtTenGoi.Clear();txtMoTa.Clear();nudThoiHan.Value=30;nudGia.Value=0;cboTrangThai.SelectedIndex=0;txtTenGoi.Focus();}
    private async void btnLuu_Click(object? s,EventArgs e){try{await _bll.LuuAsync(new GoiTap{MaGoi=_maGoi??0,TenGoi=txtTenGoi.Text.Trim(),ThoiHanNgay=(int)nudThoiHan.Value,Gia=nudGia.Value,MoTa=txtMoTa.Text.Trim(),TrangThai=cboTrangThai.Text});await TaiAsync(txtTimKiem.Text);MessageBox.Show("Đã lưu gói tập.","Thành công",MessageBoxButtons.OK,MessageBoxIcon.Information);}catch(Exception ex){MessageBox.Show(UiErrorMessage.Get(ex),"Không thể lưu",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
    private async void btnTimKiem_Click(object? s,EventArgs e)=>await TaiAsync(txtTimKiem.Text);
    private async void btnNgungApDung_Click(object? s,EventArgs e){if(_maGoi is null)return;if(MessageBox.Show("Ngừng áp dụng gói này?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;await _bll.NgungApDungAsync(_maGoi.Value);await TaiAsync(txtTimKiem.Text);}
}

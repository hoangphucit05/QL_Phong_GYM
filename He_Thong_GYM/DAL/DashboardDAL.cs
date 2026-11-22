using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace He_Thong_GYM.DAL;

public sealed class DashboardDAL
{
    public async Task<int> CapNhatGoiHetHanAsync(int? maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_CapNhatGoiHetHan", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add("@MaNV", SqlDbType.Int).Value = (object?)maNV ?? DBNull.Value;
        var output = command.Parameters.Add("@SoLuongCapNhat", SqlDbType.Int);
        output.Direction = ParameterDirection.Output;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        return output.Value is DBNull ? 0 : Convert.ToInt32(output.Value);
    }

    public Task<TongQuanDashboard> LayTongQuanAsync() => LayTongQuanAsync(DateTime.Today, DateTime.Today);

    public async Task<TongQuanDashboard> LayTongQuanAsync(DateTime tuNgay, DateTime denNgay)
    {
        if (denNgay.Date < tuNgay.Date)
            throw new ArgumentException("Khoảng ngày Dashboard không hợp lệ.", nameof(denNgay));

        const string sql = """
            SELECT
                (SELECT COUNT(*) FROM dbo.ThanhVien WHERE ISNULL(DaXoa, 0) = 0),
                (SELECT COUNT(*) FROM dbo.ThanhVien WHERE ISNULL(DaXoa, 0) = 0 AND TrangThai = N'Hoạt động'),
                (SELECT COUNT(DISTINCT t.MaTV) FROM dbo.ThanhVien t JOIN dbo.DangKyGoi d ON d.MaTV=t.MaTV WHERE ISNULL(t.DaXoa, 0)=0 AND (d.TrangThai=N'Hết hạn' OR d.NgayKetThuc < @HomNay)),
                (SELECT COUNT(DISTINCT t.MaTV) FROM dbo.ThanhVien t JOIN dbo.DangKyGoi d ON d.MaTV=t.MaTV WHERE ISNULL(t.DaXoa, 0)=0 AND d.TrangThai=N'Tạm dừng'),
                (SELECT COUNT(*) FROM dbo.ThanhVien WHERE ISNULL(DaXoa, 0)=0 AND NgayDangKy BETWEEN @TuNgay AND @DenNgay),
                (SELECT COUNT(*) FROM dbo.GoiTap WHERE TrangThai=N'Đang áp dụng'),
                (SELECT COUNT(*) FROM dbo.DangKyGoi WHERE TrangThai=N'Còn hạn' AND NgayBatDau <= @HomNay AND NgayKetThuc >= @HomNay),
                (SELECT COUNT(*) FROM dbo.DangKyGoi WHERE TrangThai=N'Còn hạn' AND NgayKetThuc BETWEEN @HomNay AND DATEADD(DAY, 7, @HomNay)),
                (SELECT COUNT(*) FROM dbo.DangKyGoi WHERE TrangThai=N'Hết hạn' OR (NgayKetThuc < @HomNay AND TrangThai NOT IN(N'Hủy',N'Tạm dừng'))),
                (SELECT COUNT(*) FROM dbo.DangKyGoi WHERE TrangThai=N'Tạm dừng'),
                (SELECT COUNT(*) FROM dbo.DangKyGoi WHERE NgayBatDau BETWEEN @TuNgay AND @DenNgay),
                (SELECT COUNT(*) FROM dbo.LichSuGoiTap WHERE HanhDong=N'Gia hạn' AND CAST(ThoiGian AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COALESCE(SUM(CASE WHEN COALESCE(LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' AND CAST(NgayThanhToan AS date)=@HomNay),
                (SELECT COALESCE(SUM(CASE WHEN COALESCE(LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' AND YEAR(NgayThanhToan)=YEAR(@HomNay) AND MONTH(NgayThanhToan)=MONTH(@HomNay)),
                (SELECT COALESCE(SUM(CASE WHEN COALESCE(LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' AND CAST(NgayThanhToan AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COALESCE(SUM(CASE WHEN COALESCE(LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' AND CAST(NgayThanhToan AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COALESCE(SUM(CASE WHEN h.TongTien > COALESCE(p.DaTra,0) THEN h.TongTien-COALESCE(p.DaTra,0) ELSE 0 END),0) FROM dbo.HoaDon h OUTER APPLY (SELECT SUM(CASE WHEN COALESCE(tt.LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN tt.SoTien ELSE -tt.SoTien END) AS DaTra FROM dbo.ThanhToan tt WHERE tt.MaHD=h.MaHD AND tt.TrangThai=N'Thành công') p WHERE h.TrangThai NOT IN(N'Hủy',N'Đã hoàn tiền')),
                (SELECT COUNT(*) FROM dbo.LichSuCheckIn WHERE CAST(ThoiGianVao AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COUNT(*) FROM dbo.LichSuCheckIn WHERE ThoiGianRa IS NOT NULL AND CAST(ThoiGianRa AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COUNT(*) FROM dbo.LichSuCheckIn WHERE ThoiGianRa IS NULL),
                COALESCE((SELECT TOP (1) DATEPART(HOUR, ThoiGianVao) FROM dbo.LichSuCheckIn WHERE CAST(ThoiGianVao AS date) BETWEEN @TuNgay AND @DenNgay GROUP BY DATEPART(HOUR, ThoiGianVao) ORDER BY COUNT(*) DESC, DATEPART(HOUR, ThoiGianVao)), -1),
                (SELECT COUNT(*) FROM dbo.LopTap WHERE TrangThai=N'Đang mở'),
                (SELECT COUNT(*) FROM dbo.HuanLuyenVien h JOIN dbo.NhanVien n ON n.MaNV=h.MaNV WHERE n.TrangThai=N'Đang làm'),
                (SELECT COUNT(*) FROM dbo.LichTapPT WHERE CAST(ThoiGianBatDau AS date) BETWEEN @TuNgay AND @DenNgay),
                (SELECT COUNT(*) FROM dbo.LichTapPT WHERE CAST(ThoiGianBatDau AS date) BETWEEN @TuNgay AND @DenNgay AND TrangThai=N'Hoàn thành'),
                (SELECT COUNT(*) FROM dbo.LichTapPT WHERE CAST(ThoiGianBatDau AS date) BETWEEN @TuNgay AND @DenNgay AND TrangThai=N'Đã đặt');

            SELECT TOP (100) t.MaThanhVien,t.HoTen,t.SDT,g.TenGoi,d.NgayBatDau,d.NgayKetThuc,DATEDIFF(DAY,@HomNay,d.NgayKetThuc),d.TrangThai
            FROM dbo.DangKyGoi d JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
            WHERE d.TrangThai IN(N'Còn hạn',N'Hết hạn') AND d.NgayKetThuc <= DATEADD(DAY,14,@HomNay)
            ORDER BY d.NgayKetThuc,t.HoTen;

            WITH CongNo AS
            (
                SELECT h.SoHoaDon,t.MaThanhVien,t.HoTen,h.TongTien,h.NgayLap,h.TrangThai,COALESCE(p.DaTra,0) AS DaThanhToan,h.TongTien-COALESCE(p.DaTra,0) AS ConNo
                FROM dbo.HoaDon h JOIN dbo.ThanhVien t ON t.MaTV=h.MaTV
                OUTER APPLY (SELECT SUM(CASE WHEN COALESCE(tt.LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN tt.SoTien ELSE -tt.SoTien END) AS DaTra FROM dbo.ThanhToan tt WHERE tt.MaHD=h.MaHD AND tt.TrangThai=N'Thành công') p
                WHERE h.TrangThai NOT IN(N'Hủy',N'Đã hoàn tiền')
            )
            SELECT TOP (100) SoHoaDon,MaThanhVien,HoTen,TongTien,DaThanhToan,ConNo,NgayLap,TrangThai FROM CongNo WHERE ConNo > 0 ORDER BY ConNo DESC,NgayLap DESC;

            SELECT TOP (20) ThoiGian,HanhDong,COALESCE(DoiTuong,N''),COALESCE(NoiDung,N'') FROM dbo.NhatKyHeThong WHERE CAST(ThoiGian AS date) BETWEEN @TuNgay AND @DenNgay ORDER BY ThoiGian DESC;

            SELECT CONVERT(date,NgayThanhToan),SUM(CASE WHEN COALESCE(LoaiGiaoDich,N'Thanh toán')=N'Thanh toán' THEN SoTien ELSE -SoTien END) FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' AND CAST(NgayThanhToan AS date) BETWEEN @TuNgay AND @DenNgay GROUP BY CONVERT(date,NgayThanhToan) ORDER BY CONVERT(date,NgayThanhToan);

            SELECT TrangThai,COUNT(*) FROM dbo.ThanhVien WHERE ISNULL(DaXoa,0)=0 GROUP BY TrangThai ORDER BY COUNT(*) DESC;

            SELECT TOP (10) g.TenGoi,COUNT(*) AS SoLuotDangKy,COALESCE(SUM(d.GiaSauGiam),0) AS DoanhThu FROM dbo.DangKyGoi d JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi WHERE d.NgayBatDau BETWEEN @TuNgay AND @DenNgay GROUP BY g.TenGoi ORDER BY SoLuotDangKy DESC,DoanhThu DESC;
            """;

        var dashboard = new TongQuanDashboard();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
        command.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
        command.Parameters.Add("@HomNay", SqlDbType.Date).Value = DateTime.Today;
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            dashboard.TongThanhVien=reader.GetInt32(0); dashboard.ThanhVienHoatDong=reader.GetInt32(1); dashboard.ThanhVienHetHan=reader.GetInt32(2); dashboard.ThanhVienBaoLuu=reader.GetInt32(3); dashboard.ThanhVienMoi=reader.GetInt32(4);
            dashboard.TongGoiTapDangBan=reader.GetInt32(5); dashboard.GoiDangSuDung=reader.GetInt32(6); dashboard.GoiSapHetHan=reader.GetInt32(7); dashboard.GoiHetHan=reader.GetInt32(8); dashboard.GoiBaoLuu=reader.GetInt32(9); dashboard.GoiMoi=reader.GetInt32(10); dashboard.GoiGiaHan=reader.GetInt32(11);
            dashboard.DoanhThuHomNay=reader.GetDecimal(12); dashboard.DoanhThuThangNay=reader.GetDecimal(13); dashboard.DoanhThuTrongKy=reader.GetDecimal(14); dashboard.TongDaThuTrongKy=reader.GetDecimal(15); dashboard.TongCongNo=reader.GetDecimal(16);
            dashboard.CheckInTrongKy=reader.GetInt32(17); dashboard.CheckOutTrongKy=reader.GetInt32(18); dashboard.DangCoMat=reader.GetInt32(19); dashboard.GioCaoDiem=reader.GetInt32(20); dashboard.LopDangMo=reader.GetInt32(21); dashboard.TongPT=reader.GetInt32(22); dashboard.BuoiPTTrongKy=reader.GetInt32(23); dashboard.BuoiPTDaHoanThanh=reader.GetInt32(24); dashboard.BuoiPTChuaThucHien=reader.GetInt32(25);
        }

        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.GoiSapHetHanChiTiet.Add(new DashboardSapHetHanView { MaThanhVien=reader.GetString(0),HoTen=reader.GetString(1),SDT=reader.GetString(2),TenGoi=reader.GetString(3),NgayBatDau=reader.GetDateTime(4),NgayKetThuc=reader.GetDateTime(5),SoNgayConLai=reader.GetInt32(6),TrangThai=reader.GetString(7) });
        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.CongNoChiTiet.Add(new DashboardCongNoView { SoHoaDon=reader.IsDBNull(0)?string.Empty:reader.GetString(0),MaThanhVien=reader.GetString(1),HoTen=reader.GetString(2),TongTien=reader.GetDecimal(3),DaThanhToan=reader.GetDecimal(4),ConNo=reader.GetDecimal(5),NgayLap=reader.GetDateTime(6),TrangThai=reader.GetString(7) });
        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.HoatDongGanDay.Add(new DashboardHoatDongView { ThoiGian=reader.GetDateTime(0),HanhDong=reader.GetString(1),DoiTuong=reader.GetString(2),NoiDung=reader.GetString(3) });
        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.DoanhThuTheoNgay.Add(new DashboardDoanhThuView { Ngay=reader.GetDateTime(0),DoanhThu=reader.GetDecimal(1) });
        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.ThanhVienTheoTrangThai.Add(new DashboardTrangThaiView { TrangThai=reader.GetString(0),SoLuong=reader.GetInt32(1) });
        await reader.NextResultAsync();
        while (await reader.ReadAsync()) dashboard.GoiBanChay.Add(new DashboardGoiBanChayView { TenGoi=reader.GetString(0),SoLuotDangKy=reader.GetInt32(1),DoanhThu=reader.GetDecimal(2) });

        return dashboard;
    }
}

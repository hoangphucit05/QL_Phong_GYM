using System.Data;
using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;

namespace He_Thong_GYM.DAL;

public sealed class ThongKeDAL
{
    public async Task<Dictionary<string, decimal>> LayTongQuanAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
    {
        const string sql = @"
            SELECT N'Thành viên' AS Ten, COUNT(*) AS GiaTri FROM dbo.ThanhVien WHERE TrangThai = N'Hoạt động'
            UNION ALL
            SELECT N'Doanh thu thực', ISNULL(SUM(CASE WHEN LoaiGiaoDich = N'Thanh toán' THEN SoTien ELSE -SoTien END), 0)
            FROM dbo.ThanhToan
            WHERE TrangThai = N'Thành công'
              AND (@TuNgay IS NULL OR NgayThanhToan >= @TuNgay)
              AND (@DenNgay IS NULL OR NgayThanhToan <= @DenNgay)
            UNION ALL
            SELECT N'Công nợ', ISNULL(SUM(TongTien - ISNULL(p.DaTra, 0)), 0)
            FROM dbo.HoaDon h
            OUTER APPLY (
                SELECT SUM(CASE WHEN LoaiGiaoDich = N'Thanh toán' THEN SoTien ELSE -SoTien END) AS DaTra
                FROM dbo.ThanhToan
                WHERE MaHD = h.MaHD AND TrangThai = N'Thành công'
            ) p
            WHERE h.TrangThai <> N'Hủy'
              AND (@TuNgay IS NULL OR h.NgayLap >= @TuNgay)
              AND (@DenNgay IS NULL OR h.NgayLap <= @DenNgay)
            UNION ALL
            SELECT N'Giá trị tồn', ISNULL(SUM(SoLuongTon * DonGia), 0) FROM dbo.SanPham
            UNION ALL
            SELECT N'Chi phí bảo trì', ISNULL(SUM(ChiPhi), 0) 
            FROM dbo.BaoTriThietBi
            WHERE (@TuNgay IS NULL OR NgayBaoTri >= @TuNgay)
              AND (@DenNgay IS NULL OR NgayBaoTri <= @DenNgay);";

        var ketQua = new Dictionary<string, decimal>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TuNgay", (object?)tuNgay ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DenNgay", (object?)denNgay ?? DBNull.Value);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            ketQua[reader.GetString(0)] = reader.GetDecimal(1);
        }
        return ketQua;
    }

    public async Task<List<ThongKeGoiTapView>> LayDoanhThuGoiTapAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
    {
        const string sql = @"
            SELECT gt.TenGoi,
                   COUNT(dkg.MaDKG) AS SoLuotDangKy,
                   ISNULL(SUM(dkg.GiaSauGiam), 0) AS DoanhThu
            FROM dbo.GoiTap gt
            LEFT JOIN dbo.DangKyGoi dkg ON gt.MaGoi = dkg.MaGoi
                 AND (@TuNgay IS NULL OR dkg.NgayBatDau >= @TuNgay)
                 AND (@DenNgay IS NULL OR dkg.NgayBatDau <= @DenNgay)
            GROUP BY gt.TenGoi
            ORDER BY DoanhThu DESC;";

        var list = new List<ThongKeGoiTapView>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TuNgay", (object?)tuNgay ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DenNgay", (object?)denNgay ?? DBNull.Value);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new ThongKeGoiTapView
            {
                TenGoi = reader.GetString(0),
                SoLuotDangKy = reader.GetInt32(1),
                DoanhThu = reader.GetDecimal(2)
            });
        }
        return list;
    }

    public async Task<List<ThongKeDoanhThuThangView>> LayDoanhThuTheoThangAsync(int nam)
    {
        const string sql = @"
            SELECT MONTH(NgayThanhToan) AS Thang,
                   YEAR(NgayThanhToan) AS Nam,
                   COUNT(DISTINCT MaHD) AS SoHoaDon,
                   ISNULL(SUM(CASE WHEN LoaiGiaoDich = N'Thanh toán' THEN SoTien ELSE -SoTien END), 0) AS DoanhThu
            FROM dbo.ThanhToan
            WHERE TrangThai = N'Thành công' AND YEAR(NgayThanhToan) = @Nam
            GROUP BY MONTH(NgayThanhToan), YEAR(NgayThanhToan)
            ORDER BY Thang;";

        var list = new List<ThongKeDoanhThuThangView>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nam", nam);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new ThongKeDoanhThuThangView
            {
                Thang = reader.GetInt32(0),
                Nam = reader.GetInt32(1),
                SoHoaDon = reader.GetInt32(2),
                DoanhThu = reader.GetDecimal(3)
            });
        }
        return list;
    }

    public async Task<List<ThongKeHoiVienView>> LayCoCauHoiVienAsync()
    {
        const string sql = @"
            SELECT ltv.TenLoai,
                   COUNT(tv.MaTV) AS SoLuong,
                   ISNULL(SUM(CASE WHEN tv.TrangThai = N'Hoạt động' THEN 1 ELSE 0 END), 0) AS DangHoatDong
            FROM dbo.LoaiThanhVien ltv
            LEFT JOIN dbo.ThanhVien tv ON ltv.MaLoaiTV = tv.MaLoaiTV
            GROUP BY ltv.TenLoai
            ORDER BY SoLuong DESC;";

        var list = new List<ThongKeHoiVienView>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new ThongKeHoiVienView
            {
                TenLoai = reader.GetString(0),
                SoLuong = reader.GetInt32(1),
                DangHoatDong = reader.GetInt32(2)
            });
        }
        return list;
    }

    public async Task<List<ThongKeHLVView>> LayTopHuanLuyenVienAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
    {
        const string sql = @"
            SELECT nv.HoTen,
                   ISNULL(hlv.ChuyenMon, N'--') AS ChuyenMon,
                   COUNT(dk.MaDKPT) AS SoHocVien,
                   ISNULL(SUM(dk.TongTien), 0) AS DoanhThu
            FROM dbo.HuanLuyenVien hlv
            JOIN dbo.NhanVien nv ON hlv.MaNV = nv.MaNV
            LEFT JOIN dbo.DangKyPT dk ON hlv.MaHLV = dk.MaHLV
                 AND (@TuNgay IS NULL OR dk.NgayBatDau >= @TuNgay)
                 AND (@DenNgay IS NULL OR dk.NgayBatDau <= @DenNgay)
            GROUP BY nv.HoTen, hlv.ChuyenMon
            ORDER BY DoanhThu DESC;";

        var list = new List<ThongKeHLVView>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TuNgay", (object?)tuNgay ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DenNgay", (object?)denNgay ?? DBNull.Value);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new ThongKeHLVView
            {
                HoTen = reader.GetString(0),
                ChuyenMon = reader.GetString(1),
                SoHocVien = reader.GetInt32(2),
                DoanhThu = reader.GetDecimal(3)
            });
        }
        return list;
    }

    public async Task<List<ThongKeSanPhamView>> LayThongKeSanPhamAsync()
    {
        const string sql = @"
            SELECT sp.TenSP,
                   sp.DonGia,
                   ISNULL(cthd.DaBan, 0) + ISNULL(pbh.DaBan, 0) AS DaBan,
                   sp.SoLuongTon AS TonKho,
                   ISNULL(cthd.DoanhThu, 0) + ISNULL(pbh.DoanhThu, 0) AS DoanhThu
            FROM dbo.SanPham sp
            OUTER APPLY (
                SELECT SUM(ct.SoLuong) AS DaBan,
                       SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
                FROM dbo.ChiTietHoaDon ct
                WHERE ct.LoaiDichVu = N'Sản phẩm' AND ct.MaThamChieu = sp.MaSP
            ) cthd
            OUTER APPLY (
                SELECT SUM(pb.SoLuong) AS DaBan,
                       SUM(pb.SoLuong * pb.DonGia) AS DoanhThu
                FROM dbo.ChiTietPhieuBanHang pb
                WHERE pb.MaSP = sp.MaSP
            ) pbh
            ORDER BY DaBan DESC, sp.MaSP DESC;";

        var list = new List<ThongKeSanPhamView>();
        await using var conn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new ThongKeSanPhamView
            {
                TenSP = reader.GetString(0),
                DonGia = reader.GetDecimal(1),
                DaBan = reader.GetInt32(2),
                TonKho = reader.GetInt32(3),
                DoanhThu = reader.GetDecimal(4)
            });
        }
        return list;
    }
}

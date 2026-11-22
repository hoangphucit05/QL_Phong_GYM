-- Migration 27: Phân quyền dịch vụ/phòng theo từng gói tập.
-- Mỗi lần Check-in phải dùng đúng gói người dùng chọn và phòng được cấp quyền.
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET ANSI_WARNINGS ON;
GO
SET ARITHABORT ON;
GO

IF OBJECT_ID(N'dbo.GoiTapDichVu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GoiTapDichVu
    (
        MaGoi INT NOT NULL,
        MaDichVu VARCHAR(30) NOT NULL,
        TenDichVu NVARCHAR(100) NOT NULL,
        SoLuotToiDa INT NULL,
        TrangThai BIT NOT NULL CONSTRAINT DF_GoiTapDichVu_TrangThai DEFAULT (1),
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_GoiTapDichVu PRIMARY KEY (MaGoi, MaDichVu),
        CONSTRAINT FK_GoiTapDichVu_GoiTap FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi),
        CONSTRAINT CK_GoiTapDichVu_SoLuot CHECK (SoLuotToiDa IS NULL OR SoLuotToiDa > 0)
    );
END;
GO

IF OBJECT_ID(N'dbo.GoiTapPhong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GoiTapPhong
    (
        MaGoi INT NOT NULL,
        MaPhong INT NOT NULL,
        TrangThai BIT NOT NULL CONSTRAINT DF_GoiTapPhong_TrangThai DEFAULT (1),
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_GoiTapPhong PRIMARY KEY (MaGoi, MaPhong),
        CONSTRAINT FK_GoiTapPhong_GoiTap FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi),
        CONSTRAINT FK_GoiTapPhong_PhongTap FOREIGN KEY (MaPhong) REFERENCES dbo.PhongTap(MaPhong)
    );
END;
GO

-- Mọi gói đang áp dụng đều có quyền sử dụng phòng; phòng cụ thể được giới hạn bên dưới.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, N'PHONG_TAP', N'Phòng tập theo quyền gói'
FROM dbo.GoiTap g
WHERE NOT EXISTS
(
    SELECT 1 FROM dbo.GoiTapDichVu q
    WHERE q.MaGoi = g.MaGoi AND q.MaDichVu = N'PHONG_TAP'
);

-- Premium có thêm lớp nhóm.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, N'LOP_NHOM', N'Lớp nhóm'
FROM dbo.GoiTap g
WHERE g.TenGoi LIKE N'Premium%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=N'LOP_NHOM');

-- VIP được dùng lớp nhóm, khu vực VIP và PT ưu tiên.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, v.MaDichVu, v.TenDichVu
FROM dbo.GoiTap g
CROSS APPLY (VALUES
    ('LOP_NHOM', N'Lớp nhóm'),
    ('KHU_VIP', N'Khu vực VIP'),
    ('PT_UU_TIEN', N'Ưu tiên đặt lịch PT')
) v(MaDichVu, TenDichVu)
WHERE g.TenGoi LIKE N'VIP%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=v.MaDichVu);

-- Gói chuyên đề và gói PT có quyền đúng với nội dung gói.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, v.MaDichVu, v.TenDichVu
FROM dbo.GoiTap g
CROSS APPLY (VALUES
    (N'Yoga%', 'LOP_YOGA', N'Lớp Yoga'),
    (N'Cardio%', 'KHU_CARDIO', N'Khu Cardio'),
    (N'PT Combo%', 'PT', N'Huấn luyện cá nhân (PT)')
) v(TenGoiPattern, MaDichVu, TenDichVu)
WHERE g.TenGoi LIKE v.TenGoiPattern
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=v.MaDichVu);
GO

-- Gán phòng cho dữ liệu demo hiện có.
-- Basic/Sinh viên/Family: phòng gym tổng hợp.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON p.MaPhong = 1
WHERE (g.TenGoi LIKE N'Basic%' OR g.TenGoi LIKE N'Sinh viên%' OR g.TenGoi LIKE N'Family%')
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- Premium: gym, cardio và phòng đa năng cho lớp nhóm.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON p.LoaiPhong IN (N'Phòng gym', N'Phòng cardio', N'Phòng đa năng')
WHERE g.TenGoi LIKE N'Premium%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- VIP: tất cả phòng đang hoạt động.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
CROSS JOIN dbo.PhongTap p
WHERE g.TenGoi LIKE N'VIP%' AND p.TrangThai=N'Hoạt động'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- Gói Yoga/Cardio/PT Combo.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON
       (g.TenGoi LIKE N'Yoga%' AND p.LoaiPhong=N'Phòng yoga')
    OR (g.TenGoi LIKE N'Cardio%' AND p.LoaiPhong=N'Phòng cardio')
    OR (g.TenGoi LIKE N'PT Combo%' AND p.LoaiPhong IN (N'Phòng gym',N'Phòng cardio'))
WHERE NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT,
    @MaPhong INT=NULL,
    @Kenh NVARCHAR(20)=N'Quầy',
    @MaCheckIn BIGINT OUTPUT,
    @MaNV INT=NULL,
    @MaDKG INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52106,N'Tài khoản thực hiện check-in không có quyền hoặc không còn hoạt động.',1;

        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(),
                @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)),
                @MaDKGDuocChon INT;

        IF @MaPhong IS NOT NULL AND NOT EXISTS
        (
            SELECT 1 FROM dbo.PhongTap
            WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động'
        )
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;

        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010,N'Thành viên đang có lượt check-in mở.',1;

        -- Có MaDKG: bắt buộc dùng đúng gói nhân viên đã chọn trên màn hình.
        -- Không có MaDKG: giữ tương thích cho các luồng cũ và chọn FIFO.
        SELECT TOP(1) @MaDKGDuocChon=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV
          AND (@MaDKG IS NULL OR d.MaDKG=@MaDKG)
          AND d.TrangThai=N'Còn hạn'
          AND d.NgayBatDau<=CAST(@HienTai AS DATE)
          AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0
          AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR
               (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR
               (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS
          (
              SELECT 1 FROM dbo.HoaDonGoiTap x
              JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
              JOIN dbo.HoaDon h ON h.MaHD=c.MaHD
              WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán'
          )
          AND EXISTS
          (
              SELECT 1 FROM dbo.GoiTapDichVu q
              WHERE q.MaGoi=d.MaGoi AND q.MaDichVu='PHONG_TAP' AND q.TrangThai=1
          )
          AND (@MaPhong IS NULL OR EXISTS
          (
              SELECT 1 FROM dbo.GoiTapPhong qp
              WHERE qp.MaGoi=d.MaGoi AND qp.MaPhong=@MaPhong AND qp.TrangThai=1
          ))
        ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC;

        IF @MaDKGDuocChon IS NULL
            THROW 50011,N'Gói đã chọn không hợp lệ, chưa thanh toán, hết hạn, hết lượt hoặc không được cấp quyền phòng này.',1;

        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh)
        VALUES(@MaTV,@MaDKGDuocChon,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());

        UPDATE dbo.DangKyGoi
        SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai
        WHERE MaDKG=@MaDKGDuocChon;

        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung)
        VALUES(@MaNV,N'Check-in',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaCheckIn),
               CONCAT(@Kenh,N'; gói đăng ký #',@MaDKGDuocChon));
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ChuyenPhongCheckIn
    @MaTV INT,
    @MaPhongMoi INT,
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52110,N'Tài khoản thực hiện chuyển phòng không có quyền hoặc không còn hoạt động.',1;

        DECLARE @MaPhongCu INT,@MaDKG INT,@MaGoi INT,@SucChua INT,@SoNguoiDangTap INT;
        SELECT TOP(1) @MaPhongCu=c.MaPhong,@MaDKG=c.MaDKG,@MaGoi=d.MaGoi
        FROM dbo.LichSuCheckIn c WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.DangKyGoi d ON d.MaDKG=c.MaDKG
        WHERE c.MaTV=@MaTV AND c.ThoiGianRa IS NULL
        ORDER BY c.ThoiGianVao DESC;

        IF @MaDKG IS NULL THROW 50012,N'Không tìm thấy lượt check-in mở.',1;
        IF @MaPhongCu=@MaPhongMoi THROW 50047,N'Phòng mới phải khác phòng hiện tại.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.PhongTap WITH(UPDLOCK,HOLDLOCK) WHERE MaPhong=@MaPhongMoi AND TrangThai=N'Hoạt động')
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.GoiTapPhong WHERE MaGoi=@MaGoi AND MaPhong=@MaPhongMoi AND TrangThai=1)
            THROW 50049,N'Gói hiện tại không được phép sử dụng phòng mới.',1;

        SELECT @SucChua=SucChua FROM dbo.PhongTap WHERE MaPhong=@MaPhongMoi;
        SELECT @SoNguoiDangTap=COUNT(*) FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK)
        WHERE MaPhong=@MaPhongMoi AND ThoiGianRa IS NULL;
        IF @SoNguoiDangTap>=@SucChua THROW 50048,N'Phòng mới đã đủ sức chứa.',1;

        UPDATE dbo.LichSuCheckIn SET MaPhong=@MaPhongMoi
        WHERE MaTV=@MaTV AND ThoiGianRa IS NULL;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung)
        VALUES(@MaNV,N'Chuyển phòng',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaTV),
               CONCAT(N'Chuyển từ phòng #',COALESCE(CONVERT(NVARCHAR(20),@MaPhongCu),N'Chưa gán'),N' sang phòng #',@MaPhongMoi));
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

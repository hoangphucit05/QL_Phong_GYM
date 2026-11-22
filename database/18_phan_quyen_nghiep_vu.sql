/*
   Migration 18 - lớp bảo vệ quyền ở database cho các giao dịch có MaNV.
   Lớp UI/BLL vẫn là nơi quyết định quyền menu; các trigger này là guard cuối
   cho những bảng nhạy cảm có actor rõ ràng, tránh insert trực tiếp bằng MaNV
   không thuộc tài khoản hoạt động/hợp lệ.
*/
USE QL_PhongGym;
GO

CREATE OR ALTER FUNCTION dbo.fn_NhanVienCoVaiTro
(
    @MaNV INT,
    @VaiTroChoPhep NVARCHAR(400)
)
RETURNS BIT
AS
BEGIN
    DECLARE @KetQua BIT = 0;
    IF EXISTS
    (
        SELECT 1
        FROM dbo.TaiKhoan tk
        WHERE tk.MaNV = @MaNV
          AND tk.TrangThai = N'Hoạt động'
          AND CHARINDEX(N'|' + tk.VaiTro + N'|', N'|' + @VaiTroChoPhep + N'|') > 0
    ) SET @KetQua = 1;
    RETURN @KetQua;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_HoaDon
ON dbo.HoaDon
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Lễ tân|Kế toán|Nhân viên bán hàng|Nhân viên|HLV|Huấn luyện viên') = 0
    )
        THROW 52101, N'Tài khoản thực hiện hóa đơn không có quyền hoặc không còn hoạt động.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_ThanhToan
ON dbo.ThanhToan
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Kế toán|Lễ tân') = 0
    )
        THROW 52102, N'Tài khoản thực hiện thanh toán không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_LichSuTonKho
ON dbo.LichSuTonKho
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Nhân viên bán hàng|Nhân viên') = 0
    )
        THROW 52103, N'Tài khoản thực hiện biến động kho không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_BaoTri
ON dbo.BaoTriThietBi
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Nhân viên') = 0
    )
        THROW 52104, N'Tài khoản thực hiện bảo trì không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_LichSuGoi
ON dbo.LichSuGoiTap
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NOT NULL
          AND dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Lễ tân|Nhân viên') = 0
    )
        THROW 52105, N'Tài khoản cập nhật vòng đời gói không có quyền.', 1;
END;
GO

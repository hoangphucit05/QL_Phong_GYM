-- Mở rộng danh mục phòng và thiết bị theo mô hình gym thực tế.
-- Migration idempotent: chỉ thêm loại/bản ghi chưa tồn tại, không sửa mã cũ.
USE QL_PhongGym;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
GO

DECLARE @ConstraintName SYSNAME;
SELECT TOP (1) @ConstraintName = cc.name
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID(N'dbo.PhongTap')
  AND cc.definition LIKE N'%LoaiPhong%';

IF @ConstraintName IS NOT NULL
    EXEC(N'ALTER TABLE dbo.PhongTap DROP CONSTRAINT [' + @ConstraintName + N']');

IF NOT EXISTS
(
    SELECT 1 FROM sys.check_constraints
    WHERE parent_object_id=OBJECT_ID(N'dbo.PhongTap')
      AND name=N'CK_PhongTap_LoaiPhong_v5'
)
BEGIN
    ALTER TABLE dbo.PhongTap ADD CONSTRAINT CK_PhongTap_LoaiPhong_v5 CHECK
    (
        LoaiPhong IN
        (
            N'Phòng gym',N'Phòng yoga',N'Phòng cardio',N'Phòng boxing',N'Phòng đa năng',
            N'Phòng functional',N'Phòng spinning',N'Phòng pilates',N'Phòng dance',N'Phòng phục hồi'
        )
    );
END;
GO

INSERT dbo.PhongTap(TenPhong,LoaiPhong,SucChua,DienTich,Tang,TrangThai)
SELECT x.TenPhong,x.LoaiPhong,x.SucChua,x.DienTich,x.Tang,N'Hoạt động'
FROM (VALUES
    (N'Phòng Functional',N'Phòng functional',25,130.00,1),
    (N'Phòng Spinning',N'Phòng spinning',24,90.00,2),
    (N'Phòng Pilates',N'Phòng pilates',20,85.00,2),
    (N'Phòng Dance & Aerobic',N'Phòng dance',35,110.00,3),
    (N'Phòng Phục Hồi & Giãn Cơ',N'Phòng phục hồi',16,70.00,1)
) x(TenPhong,LoaiPhong,SucChua,DienTich,Tang)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PhongTap p WHERE p.TenPhong=x.TenPhong);
GO

INSERT dbo.LoaiThietBi(TenLoaiTB,MoTa)
SELECT x.TenLoaiTB,x.MoTa
FROM (VALUES
    (N'Máy elliptical',N'Tập cardio toàn thân'),
    (N'Xe đạp spinning',N'Tập đạp xe trong nhà'),
    (N'Máy stair climber',N'Tập leo cầu thang'),
    (N'Giàn tạ đa năng',N'Tập nhiều nhóm cơ'),
    (N'Máy Smith',N'Tập sức mạnh có dẫn hướng'),
    (N'Kettlebell',N'Tập chức năng và sức mạnh'),
    (N'Dây battle rope',N'Tập sức bền toàn thân'),
    (N'Dây kháng lực',N'Tập phục hồi và chức năng'),
    (N'Con lăn foam roller',N'Giãn cơ và phục hồi'),
    (N'Máy Pilates Reformer',N'Tập Pilates chuyên sâu'),
    (N'Bao cát boxing',N'Tập boxing'),
    (N'Gương tập',N'Hỗ trợ quan sát kỹ thuật')
) x(TenLoaiTB,MoTa)
WHERE NOT EXISTS (SELECT 1 FROM dbo.LoaiThietBi l WHERE l.TenLoaiTB=x.TenLoaiTB);
GO

INSERT dbo.ThietBi(MaThietBi,TenTB,MaLoaiTB,NgayMua,TinhTrang,MaPhong)
SELECT x.MaThietBi,x.TenTB,l.MaLoaiTB,x.NgayMua,N'Hoạt động',p.MaPhong
FROM (VALUES
    ('TB000016',N'Máy elliptical E1',N'Máy elliptical','2025-01-12',N'Phòng Cardio'),
    ('TB000017',N'Xe đạp spinning SP1',N'Xe đạp spinning','2025-01-20',N'Phòng Spinning'),
    ('TB000018',N'Máy stair climber ST1',N'Máy stair climber','2025-02-05',N'Phòng Cardio'),
    ('TB000019',N'Giàn tạ đa năng GX1',N'Giàn tạ đa năng','2025-02-18',N'Phòng Functional'),
    ('TB000020',N'Máy Smith SM1',N'Máy Smith','2025-03-01',N'Phòng Gym Nâng Cao'),
    ('TB000021',N'Bộ Kettlebell KB1',N'Kettlebell','2025-03-10',N'Phòng Functional'),
    ('TB000022',N'Dây battle rope BR1',N'Dây battle rope','2025-03-15',N'Phòng Functional'),
    ('TB000023',N'Bộ dây kháng lực RB1',N'Dây kháng lực','2025-03-20',N'Phòng Phục Hồi & Giãn Cơ'),
    ('TB000024',N'Bộ foam roller FR1',N'Con lăn foam roller','2025-03-25',N'Phòng Phục Hồi & Giãn Cơ'),
    ('TB000025',N'Pilates Reformer PL1',N'Máy Pilates Reformer','2025-04-01',N'Phòng Pilates'),
    ('TB000026',N'Bao cát boxing BX1',N'Bao cát boxing','2025-04-10',N'Phòng Boxing'),
    ('TB000027',N'Gương tập D1',N'Gương tập','2025-04-15',N'Phòng Dance & Aerobic')
) x(MaThietBi,TenTB,TenLoaiTB,NgayMua,TenPhong)
JOIN dbo.LoaiThietBi l ON l.TenLoaiTB=x.TenLoaiTB
JOIN dbo.PhongTap p ON p.TenPhong=x.TenPhong
WHERE NOT EXISTS (SELECT 1 FROM dbo.ThietBi t WHERE t.MaThietBi=x.MaThietBi);
GO

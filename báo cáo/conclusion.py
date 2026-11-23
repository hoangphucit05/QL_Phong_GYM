# -*- coding: utf-8 -*-
"""
Kết luận và Kiến nghị + Tài liệu tham khảo
"""

def add_conclusion_and_refs(doc, helpers):
    add_h1 = helpers['add_h1']
    add_h2 = helpers['add_h2']
    p_styled = helpers['p_styled']

    # KẾT LUẬN VÀ KIẾN NGHỊ (Trang độc lập, không ghi chương mục)
    doc.add_page_break()
    p_styled("KẾT LUẬN VÀ KIẾN NGHỊ", align=helpers['WD_ALIGN_PARAGRAPH'].CENTER, 
             bold=True, size_pt=15.5, space_before=16, space_after=16, color_rgb=(0, 32, 96))

    p_styled("1. Kết quả đạt được của đề tài", bold=True, size_pt=13.5, space_before=8, space_after=4)
    p_styled("Sau một thời gian nghiêm túc nghiên cứu lý thuyết, khảo sát thực tế tại Trung tâm Thể hình & Yoga GymPro và tiến hành phân tích thiết kế, nhóm đã hoàn thành toàn diện các mục tiêu đề ra cho bài tập lớn môn học Phân tích và Thiết kế Hệ thống Thông tin:")
    p_styled("• Về mặt phân tích nghiệp vụ: Nắm bắt sâu sắc và mô hình hóa chi tiết toàn bộ các luồng công việc thực tế tại một trung tâm thể thao hiện đại, bao gồm quản lý hội viên, quản lý gói tập, vận hành bảo lưu/chuyển nhượng/nâng cấp gói, dịch vụ huấn luyện viên cá nhân PT kèm 1:1, lớp học nhóm Yoga/Zumba, kiểm soát quầy check-in theo thuật toán FIFO, bán lẻ hàng hóa POS quầy bar, quản lý kho hàng, bảo trì trang thiết bị máy tập và quản trị tài chính hóa đơn.")
    p_styled("• Về mặt mô hình hóa hệ thống: Xây dựng hoàn chỉnh hệ thống sơ đồ có cấu trúc chuẩn mực gồm Sơ đồ phân cấp chức năng BFD (8 phân hệ cấp 1, 28 chức năng cấp 2), Biểu đồ luồng dữ liệu DFD từ mức ngữ cảnh (Context Diagram), mức đỉnh (Level 0) đến mức dưới đỉnh (Level 2), và Bảng phân tích xác định tiến trình, tác nhân, kho dữ liệu.")
    p_styled("• Về mặt thiết kế cơ sở dữ liệu: Thiết kế mô hình thực thể liên kết ERD và chuyển đổi thành cơ sở dữ liệu quan hệ hoàn chỉnh gồm 47 bảng trên Microsoft SQL Server. CSDL được chứng minh đạt chuẩn 3NF và BCNF, tích hợp 20 trigger tự động sinh mã chuẩn hóa nghiệp vụ, cập nhật tồn kho và 4 Stored Procedure lõi đảm bảo tính toàn vẹn giao dịch.")
    p_styled("• Về mặt chương trình thực nghiệm: Xây dựng thành công ứng dụng Windows Forms trên nền tảng .NET 10.0 với 21 màn hình giao diện hiện đại, đạt chuẩn thẩm mỹ cao, tốc độ phản hồi tức thời. Hệ thống đã vượt qua quy trình kiểm thử QA toàn diện với 100% test case hồi quy tự động rollback an toàn và 0 cảnh báo lỗi layout.")
    p_styled("• Về mặt bảo mật: Triển khai giải pháp mã hóa mật khẩu PBKDF2 SHA-512, cơ chế chống tấn công brute-force tự động khóa tài khoản sau 5 lần nhập sai và phân quyền người dùng theo vai trò (RBAC) nghiêm ngặt cả ở tầng UI lẫn tầng CSDL.")

    p_styled("2. Đánh giá ưu điểm và hạn chế của hệ thống", bold=True, size_pt=13.5, space_before=12, space_after=4)
    p_styled("a. Ưu điểm nổi bật:")
    p_styled("• Bám sát thực tế: Các tính năng như thuật toán check-in FIFO (ưu tiên trừ hạn gói kết thúc sớm), chặn check-in phòng bảo trì, chặn bảo lưu ngày quá khứ, tự động mở thu tiền ngay sau khi đăng ký gói giải quyết trúng các vướng mắc vận hành thực tế tại phòng gym.")
    p_styled("• Toàn vẹn và an toàn tài chính: Cơ chế kiểm soát công nợ chặt chẽ, thanh toán chia đợt, chặn hủy hóa đơn đã thanh toán thành công giúp chủ cơ sở ngăn ngừa triệt để nguy cơ thất thoát doanh thu.")
    p_styled("• Hiệu năng cao: Sử dụng ADO.NET trực tiếp kết hợp Stored Procedures thay vì ORM cồng kềnh giúp phần mềm vận hành cực kỳ mượt mà, phản hồi tức thì dưới 10ms.")

    p_styled("b. Hạn chế còn tồn tại:")
    p_styled("• Ứng dụng hiện tại mới triển khai trên nền tảng Desktop WinForms nội bộ tại quầy, chưa phát triển Mobile App dành riêng cho hội viên tự xem lịch tập và đặt chỗ lớp học trên điện thoại cá nhân.")
    p_styled("• Chưa tích hợp phần cứng cổng xoay thông minh (Tripod Turnstile) và thiết bị nhận diện khuôn mặt AI (FaceID) thực tế mà hiện đang giả lập qua cổng quét mã thẻ.")

    p_styled("3. Hướng phát triển và kiến nghị mở rộng", bold=True, size_pt=13.5, space_before=12, space_after=4)
    p_styled("• Xây dựng Mobile App (Flutter / React Native) dành cho Hội viên để tự theo dõi lịch sử tập luyện, đặt lịch hẹn với HLV cá nhân và nhận thông báo nhắc lịch tự động.")
    p_styled("• Tích hợp cổng thanh toán trực tuyến tự động (VietQR tĩnh, VNPAY, MoMo) có webhook đồng bộ trạng thái hóa đơn tức thì mà không cần thu ngân thao tác thủ công.")
    p_styled("• Kết nối trực tiếp với phần cứng cổng kiểm soát phân làn tự động (Flap Barrier) và thiết bị camera AI nhận diện khuôn mặt tại cửa ra vào để tối ưu hóa nhân sự lễ tân.")

    # TÀI LIỆU THAM KHẢO (Trang độc lập)
    doc.add_page_break()
    p_styled("TÀI LIỆU THAM KHẢO", align=helpers['WD_ALIGN_PARAGRAPH'].CENTER, 
             bold=True, size_pt=15.5, space_before=16, space_after=16, color_rgb=(0, 32, 96))

    refs = [
        "[1]. Nguyễn Văn Ba, Phân tích và thiết kế hệ thống thông tin, NXB Đại học Quốc gia Hà Nội, 2006.",
        "[2]. Đỗ Trung Tuấn, Cơ sở dữ liệu, NXB Giáo dục, 2005.",
        "[3]. Đồng Thị Bích Thủy, Phân tích và thiết kế hệ thống thông tin hướng cấu trúc, NXB Đại học Quốc gia TP. Hồ Chí Minh, 2008.",
        "[4]. Jeffrey L. Whitten, Lonnie D. Bentley, Systems Analysis and Design Methods (7th Edition), McGraw-Hill Education, 2007.",
        "[5]. Abraham Silberschatz, Henry F. Korth, S. Sudarshan, Database System Concepts (7th Edition), McGraw-Hill Higher Education, 2019.",
        "[6]. Christian Nagel, Professional C# and .NET 2021 Edition, Wrox / John Wiley & Sons, 2021.",
        "[7]. Benjamin Rosenzweig, Elena Rakhimov, Oracle SQL By Example (5th Edition), Pearson Education, 2015.",
        "[8]. Microsoft Corporation, SQL Server Documentation & ADO.NET Architecture Guide, Microsoft Learn, 2024.",
        "[9]. OWASP Foundation, Password Storage Cheat Sheet & PBKDF2 Standards, OWASP Security Guidelines, 2023."
    ]

    for ref in refs:
        p_ref = doc.add_paragraph()
        p_ref.alignment = helpers['WD_ALIGN_PARAGRAPH'].JUSTIFY
        p_ref.paragraph_format.left_indent = helpers['Cm'](1.0)
        p_ref.paragraph_format.first_line_indent = helpers['Cm'](-1.0)
        p_ref.paragraph_format.space_before = helpers['Pt'](4)
        p_ref.paragraph_format.space_after = helpers['Pt'](4)
        p_ref.paragraph_format.line_spacing = 1.15

        # Format [idx]. Author (bold), Title (italic), Publisher, Year
        parts = ref.split(',', 2)
        r1 = p_ref.add_run(parts[0] + ",") # [1]. Author
        r1.font.name = 'Times New Roman'
        r1.font.size = helpers['Pt'](13)
        r1.bold = True

        if len(parts) > 1:
            title_and_rest = parts[1]
            r2 = p_ref.add_run(title_and_rest + ",") # Title
            r2.font.name = 'Times New Roman'
            r2.font.size = helpers['Pt'](13)
            r2.italic = True

        if len(parts) > 2:
            rest = parts[2]
            r3 = p_ref.add_run(rest) # Publisher, Year
            r3.font.name = 'Times New Roman'
            r3.font.size = helpers['Pt'](13)

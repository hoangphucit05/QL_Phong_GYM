# -*- coding: utf-8 -*-
"""
Chương 3: Phân tích hệ thống
"""

def add_chapter_3(doc, helpers):
    add_h1 = helpers['add_h1']
    add_h2 = helpers['add_h2']
    add_h3 = helpers['add_h3']
    add_h4 = helpers['add_h4']
    p_styled = helpers['p_styled']
    add_tbl = helpers['add_tbl']
    add_diagram_box = helpers['add_diagram_box']

    add_h1("CHƯƠNG 3: PHÂN TÍCH HỆ THỐNG")

    # 3.1
    add_h2("3.1. Mô tả bài toán nghiệp vụ chi tiết")
    p_styled("Dựa trên kết quả khảo sát thực tế quy trình vận hành tại Trung tâm GymPro, bài toán quản lý được phân rã thành các luồng nghiệp vụ chi tiết như sau:")

    add_h3("3.1.1. Nghiệp vụ Quản lý Hội viên và Thẻ tập")
    p_styled("• Tiếp nhận hội viên mới: Khi khách hàng đến trung tâm, nhân viên lễ tân thu thập các thông tin cá nhân: Họ và tên, Ngày sinh, Giới tính, Số điện thoại (bắt buộc, là định danh liên lạc chính), Số CCCD/CMND, Địa chỉ, Email và ảnh chụp trực tiếp tại quầy.")
    p_styled("• Kiểm tra tính hợp lệ và chống trùng lặp: Hệ thống tự động kiểm tra số điện thoại; nếu đã tồn tại hội viên có SĐT này, phần mềm cảnh báo trùng lặp để nhân viên tra cứu hồ sơ cũ thay vì tạo mới.")
    p_styled("• Cấp phát mã hội viên tự động: Khi bấm 'Lưu', hệ thống tự động sinh Mã thành viên theo quy chuẩn nghiệp vụ TVxxxxxx (ví dụ: TV000001, TV000002) thông qua Trigger CSDL. Đồng thời cấp thẻ hội viên (mã vạch / mã QR) để sử dụng cho thao tác check-in quầy.")
    p_styled("• Phân hạng hội viên: Hệ thống tự động theo dõi điểm tích lũy và doanh số để phân hạng thành viên (Thường, Bạc - Silver, Vàng - Gold, Kim cương - Diamond) với các chính sách ưu đãi chiết khấu riêng biệt.")

    add_h3("3.1.2. Nghiệp vụ Quản lý Danh mục Gói tập và Đăng ký Gói tập")
    p_styled("• Quản lý danh mục gói: Ban giám đốc cấu hình các gói tập linh hoạt trên hệ thống (Mã gói GOIxxxxxx, Tên gói tập, Thời hạn sử dụng tính theo ngày hoặc tháng, Đơn giá niêm yết, Khung giờ áp dụng, Danh sách phòng tập và tiện ích được phép vào).")
    p_styled("• Quy trình đăng ký gói tập: Lễ tân chọn Hội viên -> Chọn Gói tập -> Chọn Mã khuyến mãi / Voucher giảm giá (nếu có). Hệ thống tự động tính toán tiền giảm giá, thành tiền phải trả và ngày kết thúc dự kiến.")
    p_styled("• Tự động mở hóa đơn thu tiền: Ngay sau khi bấm 'Đăng ký gói', phần mềm tự động hiển thị hộp thoại xác nhận: 'Bạn có muốn mở hóa đơn để thu tiền ngay không?'. Nếu nhân viên đồng ý, hệ thống tự động chuyển tiếp thông tin sang màn hình Hóa đơn & Thanh toán, giảm thiểu tối đa thao tác nhập liệu lặp lại cho thu ngân.")

    add_h3("3.1.3. Nghiệp vụ Vận hành Gói tập (Bảo lưu, Chuyển nhượng, Nâng cấp)")
    p_styled("Để giải quyết các tình huống phát sinh thực tế của khách hàng, hệ thống hỗ trợ 3 nghiệp vụ vận hành gói tập chuyên sâu:")
    p_styled("• Bảo lưu gói tập: Khi hội viên đi công tác xa, ốm đau hoặc bận việc đột xuất, lễ tân lập phiếu bảo lưu. Người dùng chọn ngày bắt đầu bảo lưu (chặn hoàn toàn việc chọn ngày trong quá khứ) và số ngày bảo lưu (tối đa không vượt quá giới hạn chính sách của gói). Trong thời gian bảo lưu, gói tập chuyển trạng thái 'Đang bảo lưu' và không thể check-in. Khi hết hạn bảo lưu, hệ thống tự động cộng dồn số ngày bảo lưu vào ngày kết thúc của gói.")
    p_styled("• Chuyển nhượng gói tập: Cho phép hội viên chuyển nhượng quyền sử dụng gói tập còn hạn cho một hội viên khác (kèm phí chuyển nhượng theo quy định). Hệ thống tự động hủy gói cũ của người chuyển và tạo gói tập mới tương ứng cho người nhận.")
    p_styled("• Nâng cấp gói tập: Hội viên có thể nâng cấp từ gói tập thời hạn ngắn lên gói dài hạn hơn (ví dụ: từ Gói 1 Tháng lên Gói VIP 1 Năm). Hệ thống tự động tính toán giá trị còn lại của gói cũ để khấu trừ vào hóa đơn gói mới.")

    add_h3("3.1.4. Nghiệp vụ Quản lý Huấn luyện viên cá nhân (PT) và Hợp đồng PT")
    p_styled("• Quản lý danh sách HLV: Lưu trữ hồ sơ chuyên môn, bằng cấp, chứng chỉ thể thao, số điện thoại, đánh giá sao và danh sách học viên hiện đang phụ trách của từng huấn luyện viên.")
    p_styled("• Đăng ký hợp đồng PT: Hội viên đăng ký dịch vụ tập kèm 1:1 với HLV theo số lượng buổi tập (ví dụ: gói 12 buổi, 24 buổi, 36 buổi). Hệ thống lưu vết hợp đồng DPT-YYYYMMDD-xxxx, số buổi đăng ký, số buổi đã tập và ngày hết hạn hợp đồng.")
    p_styled("• Lập lịch tập PT & Theo dõi buổi tập: Mỗi khi hoàn thành một buổi hướng dẫn tập luyện, HLV và hội viên xác nhận trên hệ thống; số buổi còn lại của hội viên tự động giảm đi 1 và ghi nhận vào bảng tích lũy công chuẩn bị tính hoa hồng cho HLV.")

    add_h3("3.1.5. Nghiệp vụ Quản lý Lớp học nhóm (Group-X / Yoga) và Điểm danh")
    p_styled("• Quản lý lớp học: Thiết lập mã lớp (LOPxxxxxx), tên lớp (Yoga Asana, Zumba Dance, Spinning đạp xe trong nhà, Boxing đối kháng), HLV phụ trách, phòng tập, thứ trong tuần, khung giờ học và sĩ số tối đa của lớp học.")
    p_styled("• Đăng ký lớp và xếp chỗ: Hội viên có gói tập phù hợp được phép đăng ký vào lớp. Khi sĩ số lớp đã đạt tối đa, hệ thống tự động chặn đăng ký mới hoặc đưa vào danh sách chờ (Waiting List).")
    p_styled("• Điểm danh lớp học: Khi đến giờ học, HLV hoặc nhân viên mở màn hình điểm danh, đánh dấu các hội viên có mặt thực tế, tự động cập nhật nhật ký điểm danh và gửi thông báo nếu lớp có sự thay đổi lịch đột xuất.")

    add_h3("3.1.6. Nghiệp vụ Check-in ra / vào phòng tập tại Quầy lễ tân")
    p_styled("• Check-in nhanh: Hội viên quét mã thẻ hoặc nhân viên nhập SĐT/Mã hội viên. Hệ thống kích hoạt Stored Procedure sp_CheckIn để kiểm tra đa chiều:")
    p_styled("  1. Kiểm tra trạng thái hội viên: Đang hoạt động, bị khóa hay đã thôi tập.")
    p_styled("  2. Kiểm tra gói tập hợp lệ: Gói phải đang có trạng thái 'Hoạt động', ngày hiện tại nằm trong khoảng [NgayBatDau, NgayKetThuc].")
    p_styled("  3. Thuật toán FIFO (First In First Out): Nếu hội viên sở hữu nhiều gói tập cùng loại, hệ thống tự động ưu tiên khấu trừ và ghi nhận lượt vào gói có ngày hết hạn gần nhất trước, tránh để hội viên bị mất quyền lợi.")
    p_styled("  4. Kiểm tra điều kiện phòng tập: Tự động chặn check-in nếu phòng tập tương ứng đang ở trạng thái 'Đang bảo trì' hoặc 'Đóng cửa'.")
    p_styled("• Kết quả hiển thị: Nếu hợp lệ, hệ thống báo đèn xanh, phát âm thanh chào mừng, hiển thị hình ảnh hội viên, gói tập và mở cửa xoay tự động (nếu có kết nối thiết bị kiểm soát ra vào). Nếu không hợp lệ, hệ thống báo đèn đỏ và hiển thị lý do cụ thể (Gói hết hạn, Sai khung giờ, Chưa đóng học phí).")

    add_h3("3.1.7. Nghiệp vụ Bán lẻ hàng hóa POS và Quản lý Kho hàng")
    p_styled("• Danh mục hàng hóa: Quản lý danh sách nước khoáng, nước tăng lực, bột sữa Whey Protein, Vitamin khoáng chất, găng tay, đai lưng. Mỗi sản phẩm có Mã SP (SPxxxxxx), Đơn vị tính, Giá nhập, Giá bán lẻ và Số lượng tồn kho hiện tại.")
    p_styled("• Bán hàng lẻ tại quầy POS: Nhân viên bán hàng tạo phiếu bán hàng nhanh cho Hội viên hoặc Khách vãng lai (không bắt buộc nhập mã thành viên). Chọn sản phẩm và số lượng -> bấm Thanh toán -> In phiếu bán hàng bill nhiệt.")
    p_styled("• Tự động trừ kho và ghi nhật ký: Trigger CSDL TR_ChiTietPhieuBanHang_CapNhatTonKho tự động trừ trực tiếp số lượng tồn kho của sản phẩm và ghi một bản ghi biến động vào bảng LichSuTonKho. Nếu số lượng mua vượt quá số tồn trong kho, hệ thống cảnh báo và chặn thanh toán.")
    p_styled("• Nhập kho: Khi nhập lô hàng mới từ nhà cung cấp, thủ kho lập Phiếu nhập kho; hệ thống tự động cộng tăng số tồn và lưu vết giá vốn.")

    add_h3("3.1.8. Nghiệp vụ Quản lý Trang thiết bị và Lập phiếu Bảo trì")
    p_styled("• Quản lý thiết bị: Mỗi máy chạy bộ, giàn tạ, máy ép ngực được cấp một Mã thiết bị (TBxxxxxx), phân bổ về từng phòng tập cụ thể và theo dõi trạng thái hoạt động ('Hoạt động', 'Cần bảo trì', 'Đang sửa chữa', 'Hỏng nặng/Ngừng sử dụng').")
    p_styled("• Lập phiếu bảo trì: Khi thiết bị gặp sự cố hoặc đến hạn bảo dưỡng định kỳ, nhân viên kỹ thuật tạo Phiếu bảo trì (sp_BaoTriThietBi), mô tả chi tiết lỗi, dự toán chi phí và chọn đơn vị sửa chữa. Trạng thái thiết bị tự động chuyển sang 'Bảo trì'.")
    p_styled("• Nghiệm thu và đưa vào sử dụng: Sau khi sửa chữa xong, kỹ thuật viên thực hiện nghiệm thu (sp_HoanTatBaoTri), nhập chi phí thực tế phát sinh; trạng thái thiết bị tự động khôi phục lại 'Hoạt động' bình thường.")

    add_h3("3.1.9. Nghiệp vụ Quản lý Hóa đơn, Thanh toán đa phương thức và Công nợ")
    p_styled("• Quản lý hóa đơn: Mọi giao dịch phát sinh dòng tiền (Đăng ký gói, Đăng ký PT, Mua sản phẩm POS, Phí bảo lưu/chuyển nhượng) đều được tổng hợp thành Hóa đơn có mã chuẩn hóa HD-YYYYMMDD-xxxx.")
    p_styled("• Thanh toán linh hoạt: Hệ thống hỗ trợ thanh toán nhiều đợt và đa dạng hình thức: Tiền mặt, Chuyển khoản QR ngân hàng (tự động tạo mã VietQR theo cú pháp), Quẹt thẻ tín dụng POS ngân hàng.")
    p_styled("• Kiểm soát công nợ: Tự động ghi nhận số tiền còn thiếu (ConNo) của từng hóa đơn; khi hội viên thanh toán các đợt tiếp theo, hệ thống sinh biên nhận thanh toán TT-YYYYMMDD-xxxx và cập nhật số dư nợ.")
    p_styled("• Kiểm soát hủy hóa đơn: Chặn hoàn toàn việc hủy hóa đơn đã phát sinh giao dịch thanh toán thành công để chống biển thủ tiền thu ngân. Khi hủy hóa đơn bán hàng chưa thanh toán, hệ thống tự động hoàn trả số lượng hàng về kho.")

    add_h3("3.1.10. Nghiệp vụ Báo cáo Thống kê và Quản trị Hệ thống")
    p_styled("• Báo cáo tài chính: Báo cáo tổng doanh thu theo ngày, theo tuần, tháng và năm; cơ cấu doanh thu theo từng mảng (Gói tập, PT cá nhân, Lớp học, Bán lẻ POS); báo cáo tổng công nợ chưa thu hồi.")
    p_styled("• Báo cáo vận hành: Báo cáo tần suất check-in theo từng khung giờ trong ngày (xác định giờ cao điểm để bố trí nhân viên); danh sách hội viên sắp hết hạn gói tập trong 7 ngày tới để lễ tân liên hệ chăm sóc và tư vấn tái đăng ký.")
    p_styled("• Quản trị bảo mật: Cấp phát tài khoản, phân quyền vai trò người dùng, theo dõi lịch sử đăng nhập và nhật ký tác động dữ liệu hệ thống (Audit Log).")

    # 3.2
    add_h2("3.2. Sơ đồ phân cấp chức năng BFD")
    p_styled("Dựa trên kết quả phân tích bài toán nghiệp vụ, toàn bộ hệ thống quản lý phòng gym GymPro được phân rã thành sơ đồ phân cấp chức năng có cấu trúc 3 tầng gồm 8 phân hệ nghiệp vụ chính và 28 chức năng cụ thể:")

    bfd_chart = """
========================================================================================
             HỆ THỐNG QUẢN LÝ PHÒNG GYM (GYMPRO FITNESS & YOGA)
========================================================================================
  |
  +-- 1.0 QUẢN LÝ HỘI VIÊN & THẺ TẬP
  |     |-- 1.1 Tiếp nhận & Đăng ký hồ sơ hội viên mới
  |     |-- 1.2 Cập nhật & Tra cứu thông tin hội viên
  |     |-- 1.3 Quản lý chỉ số cơ thể InBody & Hồ sơ sức khỏe
  |     +-- 1.4 Phân hạng & Tích lũy điểm hội viên
  |
  +-- 2.0 QUẢN LÝ GÓI TẬP & VẬN HÀNH
  |     |-- 2.1 Quản lý danh mục gói tập & chính sách giá
  |     |-- 2.2 Đăng ký gói tập & Áp dụng mã khuyến mãi
  |     |-- 2.3 Gia hạn gói tập & Nâng cấp hạng gói
  |     +-- 2.4 Bảo lưu & Chuyển nhượng gói tập
  |
  +-- 3.0 QUẢN LÝ HUẤN LUYỆN VIÊN (PT) & LỚP TẬP
  |     |-- 3.1 Quản lý hồ sơ Huấn luyện viên & Lịch công tác
  |     |-- 3.2 Đăng ký hợp đồng PT kèm 1:1 & Theo dõi buổi tập
  |     |-- 3.3 Quản lý danh mục lớp học nhóm (Yoga, Zumba, Boxing)
  |     +-- 3.4 Đăng ký lớp học & Quản lý danh sách chờ
  |
  +-- 4.0 CHECK-IN & ĐIỂM DANH QUẦY
  |     |-- 4.1 Check-in hội viên vào phòng tập (Thuật toán FIFO)
  |     |-- 4.2 Kiểm soát trạng thái phòng & Cảnh báo an toàn
  |     +-- 4.3 Điểm danh hội viên tham gia lớp nhóm / Buổi tập PT
  |
  +-- 5.0 BÁN HÀNG LẺ POS & QUẢN LÝ KHO
  |     |-- 5.1 Quản lý danh mục sản phẩm, nước uống & phụ kiện
  |     |-- 5.2 Lập phiếu bán hàng lẻ POS tại quầy Bar
  |     |-- 5.3 Lập phiếu nhập kho hàng hóa từ Nhà cung cấp
  |     +-- 5.4 Kiểm kê & Theo dõi lịch sử biến động tồn kho
  |
  +-- 6.0 QUẢN LÝ THIẾT BỊ & PHÒNG TẬP
  |     |-- 6.1 Quản lý danh mục trang thiết bị máy tập
  |     |-- 6.2 Phân bổ thiết bị theo từng phòng tập chức năng
  |     |-- 6.3 Lập phiếu bảo trì, sửa chữa máy móc
  |     +-- 6.4 Nghiệm thu & Khôi phục trạng thái hoạt động máy tập
  |
  +-- 7.0 QUẢN LÝ HÓA ĐƠN & THANH TOÁN TÀI CHÍNH
  |     |-- 7.1 Lập và quản lý hóa đơn dịch vụ / bán hàng
  |     |-- 7.2 Thu tiền thanh toán đa phương thức (Tiền mặt/QR/Thẻ)
  |     |-- 7.3 Quản lý thanh toán từng đợt & Theo dõi công nợ
  |     +-- 7.4 In biên nhận & Kiểm soát hủy hóa đơn an toàn
  |
  +-- 8.0 BÁO CÁO THỐNG KÊ & QUẢN TRỊ HỆ THỐNG
        |-- 8.1 Thống kê doanh thu đa chiều & Biểu đồ tài chính
        |-- 8.2 Báo cáo tần suất check-in & Giờ cao điểm
        |-- 8.3 Báo cáo hội viên sắp hết hạn gói tập
        |-- 8.4 Quản lý tài khoản & Phân quyền vai trò (RBAC)
        +-- 8.5 Giám sát nhật ký hệ thống & Khóa tài khoản sai mật khẩu
========================================================================================
"""
    add_diagram_box(bfd_chart, caption="Sơ đồ 3.1: Sơ đồ phân cấp chức năng BFD toàn hệ thống GymPro")

    headers_bfd_desc = ["Mã CN", "Tên chức năng nghiệp vụ", "Bộ phận chịu trách nhiệm", "Mô tả kết quả xử lý"]
    rows_bfd_desc = [
        ["1.0", "Quản lý Hội viên & Thẻ tập", "Lễ tân / CSKH", "Thu thập, cấp mã TVxxxxxx, lưu trữ và tra cứu hồ sơ hội viên."],
        ["2.0", "Quản lý Gói tập & Vận hành", "Lễ tân / Ban Quản lý", "Lập danh mục gói, đăng ký mới, bảo lưu, chuyển nhượng, nâng cấp gói."],
        ["3.0", "Quản lý HLV & Lớp học", "HLV Trưởng / Lễ tân", "Lập hồ sơ HLV, hợp đồng PT 1:1, mở lớp nhóm và xếp lịch tập."],
        ["4.0", "Check-in & Điểm danh quầy", "Lễ tân / HLV", "Quẹt thẻ vào cổng, kiểm tra hợp lệ gói (FIFO), điểm danh sĩ số lớp."],
        ["5.0", "Bán hàng POS & Quản lý Kho", "Nhân viên Bán hàng / Thủ kho", "Bán lẻ đồ uống/Whey/đồ tập, lập phiếu nhập kho, tự động trừ tồn kho."],
        ["6.0", "Quản lý Thiết bị & Phòng tập", "Kỹ thuật viên bảo trì", "Theo dõi vị trí máy, lập phiếu sửa chữa, nghiệm thu phục hồi thiết bị."],
        ["7.0", "Quản lý Hóa đơn & Thu tiền", "Thu ngân / Kế toán", "Xuất hóa đơn dịch vụ, thu tiền nhiều đợt, quản lý nợ, in phiếu thu."],
        ["8.0", "Báo cáo Thống kê & Quản trị", "Ban Giám đốc / IT Admin", "Xuất báo cáo doanh thu, mật độ tập luyện, phân quyền và bảo mật."]
    ]
    add_tbl(headers_bfd_desc, rows_bfd_desc, col_widths=[1.5, 4.2, 3.8, 6.0], caption="Bảng 3.2: Bảng phân rã sơ đồ chức năng BFD toàn hệ thống")

    # 3.3
    add_h2("3.3. Bảng phân tích xác định tiến trình, tác nhân và hồ sơ dữ liệu")
    p_styled("Thông qua việc phân tích ngữ nghĩa các động từ (hành động xử lý) và danh từ (đối tượng tham gia, hồ sơ lưu trữ) trong mô tả bài toán nghiệp vụ, bảng phân tích xác định tiến trình, tác nhân và kho dữ liệu được xây dựng như sau:")

    headers_process = ["STT", "Hành động nghiệp vụ (Động từ + Bổ ngữ)", "Tác nhân Ngoài", "Tác nhân Trong", "Tiến trình tương ứng", "Hồ sơ dữ liệu (Kho D)"]
    rows_process = [
        ["1", "Đăng ký thông tin hội viên", "Hội viên", "Lễ tân", "1.1 Tiếp nhận hội viên", "Ghi D1 (ThanhVien)"],
        ["2", "Cập nhật hồ sơ sức khỏe InBody", "Hội viên", "HLV / Lễ tân", "1.3 Quản lý sức khỏe", "Ghi D1 (HoSoSucKhoe)"],
        ["3", "Đăng ký hợp đồng gói tập", "Hội viên", "Lễ tân", "2.2 Đăng ký gói tập", "Đọc D1, D2; Ghi D3, D8"],
        ["4", "Lập phiếu bảo lưu gói tập", "Hội viên", "Lễ tân", "2.4 Bảo lưu gói", "Đọc D3; Ghi D3 (BaoLuu)"],
        ["5", "Chuyển nhượng gói cho người khác", "Hội viên", "Lễ tân", "2.4 Chuyển nhượng gói", "Đọc/Ghi D3, D8"],
        ["6", "Đăng ký dịch vụ Huấn luyện viên PT", "Hội viên", "Lễ tân / HLV", "3.2 Đăng ký PT", "Đọc D1, D4; Ghi D4, D8"],
        ["7", "Mở lớp học nhóm và xếp lịch", "—", "HLV Trưởng", "3.3 Quản lý lớp tập", "Ghi D4 (LopTap)"],
        ["8", "Điểm danh học viên vào lớp / ca PT", "Hội viên", "HLV", "4.3 Điểm danh", "Đọc D4; Ghi D5 (DiemDanh)"],
        ["9", "Quẹt thẻ check-in vào phòng tập", "Hội viên", "Lễ tân", "4.1 Check-in quầy", "Đọc D1, D3, D7; Ghi D5"],
        ["10", "Mua hàng lẻ tại quầy bar POS", "Hội viên / Vãng lai", "Nhân viên Bán hàng", "5.2 Bán hàng POS", "Đọc/Ghi D6, D8"],
        ["11", "Nhập hàng hóa từ Nhà cung cấp", "Nhà cung cấp", "Thủ kho", "5.3 Nhập kho hàng", "Đọc/Ghi D6"],
        ["12", "Lập phiếu bảo trì máy tập bị hỏng", "Đơn vị bảo trì", "Kỹ thuật viên", "6.3 Lập phiếu bảo trì", "Đọc/Ghi D7 (ThietBi)"],
        ["13", "Nghiệm thu khôi phục máy tập", "Đơn vị bảo trì", "Kỹ thuật viên", "6.4 Nghiệm thu máy", "Đọc/Ghi D7"],
        ["14", "Thu tiền thanh toán hóa đơn", "Hội viên / Khách hàng", "Thu ngân / Kế toán", "7.2 Thu tiền hóa đơn", "Đọc D8; Ghi D8 (ThanhToan)"],
        ["15", "Xem báo cáo tổng hợp doanh thu", "—", "Ban Giám đốc", "8.1 Báo cáo doanh thu", "Đọc D3, D4, D6, D8"],
        ["16", "Phân quyền và bảo mật tài khoản", "—", "IT Admin", "8.4 Quản trị hệ thống", "Đọc/Ghi D9 (TaiKhoan)"]
    ]
    add_tbl(headers_process, rows_process, col_widths=[1.0, 3.8, 2.5, 2.5, 2.8, 2.9], caption="Bảng 3.1: Bảng phân tích xác định Tiến trình, Tác nhân và Hồ sơ dữ liệu")

    p_styled("Quy ước danh mục các kho dữ liệu (Data Stores):")
    p_styled("• D1: Hồ sơ Hội viên & Thẻ tập (ThanhVien, HoSoSucKhoe, ChiSoCoThe)")
    p_styled("• D2: Danh mục Gói tập & Khuyến mãi (GoiTap, KhuyenMai, GoiTapPhong)")
    p_styled("• D3: Hợp đồng Đăng ký gói & Vận hành (DangKyGoi, BaoLuuGoiTap, LichSuGoiTap)")
    p_styled("• D4: Hồ sơ Huấn luyện viên & Lớp học (HuanLuyenVien, DangKyPT, LopTap, DangKyLop, LichHoc)")
    p_styled("• D5: Nhật ký Check-in & Điểm danh (LichSuCheckIn, DiemDanhLop, LichTapPT)")
    p_styled("• D6: Sản phẩm & Quản lý Kho (SanPham, PhieuBanHang, PhieuNhapKho, LichSuTonKho)")
    p_styled("• D7: Trang thiết bị & Phòng tập (ThietBi, BaoTriThietBi, PhongTap, LoaiThietBi)")
    p_styled("• D8: Sổ sách Hóa đơn & Giao dịch thanh toán (HoaDon, ChiTietHoaDon, ThanhToan)")
    p_styled("• D9: Hồ sơ Nhân sự & Tài khoản phân quyền (NhanVien, TaiKhoan, VaiTro, LichSuDangNhap)")

    # 3.4
    add_h2("3.4. Biểu đồ luồng dữ liệu (DFD)")

    add_h3("3.4.1. Biểu đồ luồng dữ liệu mức ngữ cảnh (Context Diagram)")
    p_styled("Biểu đồ luồng dữ liệu mức ngữ cảnh cung cấp cái nhìn tổng quan nhất về ranh giới của Hệ thống GymPro với môi trường xung quanh, mô tả tương tác giữa hệ thống với 4 tác nhân ngoài chính:")
    p_styled("1. Hội viên & Khách hàng: Gửi thông tin cá nhân, yêu cầu mua gói/đăng ký PT/mua sản phẩm, thanh toán tiền và nhận về Thẻ hội viên, Hóa đơn/Biên lai, Kết quả check-in và Lịch tập.")
    p_styled("2. Ban Giám đốc: Gửi các quyết định kinh doanh, cấu hình bảng giá gói và nhận về các Báo cáo thống kê doanh thu, báo cáo tài chính và hiệu suất vận hành.")
    p_styled("3. Nhà cung cấp sản phẩm: Cung ứng hàng hóa, thực phẩm bổ sung, thiết bị và nhận về Đơn đặt hàng, Phiếu nhập kho và Tiền thanh toán.")
    p_styled("4. Đơn vị bảo trì thiết bị: Tiếp nhận Phiếu yêu cầu sửa chữa thiết bị, báo giá và nhận về Biên bản nghiệm thu và Tiền công sửa chữa.")

    context_diagram_txt = """
+-----------------------+                               +-----------------------+
|                       |  -- Thông tin đăng ký TV ---> |                       |
|                       |  -- Yêu cầu đăng ký gói/PT -> |                       |
|                       |  -- Thanh toán tiền dịch vụ-> |                       |
|      HỘI VIÊN &       |  -- Quẹt thẻ check-in ------> |                       |
|      KHÁCH HÀNG       |                               |                       |
|                       |  <- Cấp thẻ hội viên & PIN -- |                       |
|                       |  <- Hóa đơn & Biên nhận TT -- |                       |
|                       |  <- Xác nhận check-in/Lịch -- |                       |
+-----------------------+                               |                       |
                                                        |      HỆ THỐNG         |
+-----------------------+                               |  THÔNG TIN QUẢN LÝ    |
|                       |  -- Yêu cầu báo cáo thống kê> |      PHÒNG GYM        |
|     BAN GIÁM ĐỐC      |  -- Thiết lập chính sách giá> |       (GYMPRO)        |
|                       |  <- Báo cáo doanh thu tài chí|                       |
|                       |  <- Báo cáo hiệu suất phòng - |                       |
+-----------------------+                               |                       |
                                                        |                       |
+-----------------------+                               |                       |
|                       |  -- Báo giá & Giao hàng hóa-> |                       |
|     NHÀ CUNG CẤP      |  <- Phiếu đặt hàng/nhập kho-- |                       |
|                       |  <- Thanh toán tiền hàng ---- |                       |
+-----------------------+                               |                       |
                                                        |                       |
+-----------------------+                               |                       |
|   ĐƠN VỊ BẢO TRÌ      |  -- Tiếp nhận phiếu sửa chữa> |                       |
|      THIẾT BỊ         |  <- Nghiệm thu & Chi phí ---- |                       |
+-----------------------+                               +-----------------------+
"""
    add_diagram_box(context_diagram_txt, caption="Sơ đồ 3.2: Biểu đồ luồng dữ liệu DFD mức ngữ cảnh (Context Diagram)")

    add_h3("3.4.2. Biểu đồ luồng dữ liệu mức đỉnh (Level 0)")
    p_styled("Tiến trình 0 được phân rã thành 8 tiến trình nghiệp vụ cốt lõi ở mức đỉnh (Level 0), kết nối trao đổi dữ liệu với các tác nhân và 9 kho dữ liệu (D1 đến D9):")

    dfd_level0_txt = """
+========================================================================================+
|                       BIỂU ĐỒ LUỒNG DỮ LIỆU MỨC ĐỈNH (LEVEL 0)                         |
+========================================================================================+
  [Hội viên] ---> (1.0 Quản lý Hội viên) ----------> [D1: Hồ sơ Hội viên]
                          |
                          v (Mã TV)
  [Hội viên] ---> (2.0 Đăng ký & Vận hành Gói) ----> [D2: Danh mục Gói]
                          |                    ----> [D3: Hợp đồng ĐK Gói]
                          v (Yêu cầu thanh toán)
  [Hội viên] ---> (3.0 Quản lý PT & Lớp học) ------> [D4: HLV & Lớp học]
                          |
                          v
  [Hội viên] ---> (4.0 Check-in & Điểm danh) <------ [D1, D3, D4, D7]
                          |                    -----> [D5: Nhật ký Check-in]
                          v
  [Khách/TV] ---> (5.0 Bán hàng POS & Kho) --------> [D6: Sản phẩm & Kho]
                          |                    <---- [Nhà cung cấp]
                          v (Doanh thu POS)
  [Kỹ thuật] ---> (6.0 Quản lý Thiết bị & Sửa chữa)-> [D7: Thiết bị & Bảo trì]
                                               <---- [Đơn vị bảo trì]
                          |
                          v
  [Hội viên] ---> (7.0 Quản lý Hóa đơn & Thu tiền)-> [D8: Hóa đơn & Thu tiền]
                          |
                          v (Dữ liệu tài chính)
  [Ban GĐ]   <--- (8.0 Báo cáo Thống kê & Quản trị)<- [D1, D3, D5, D6, D8, D9]
+========================================================================================+
"""
    add_diagram_box(dfd_level0_txt, caption="Sơ đồ 3.3: Biểu đồ luồng dữ liệu DFD mức đỉnh (Level 0)")

    add_h3("3.4.3. Biểu đồ luồng dữ liệu mức dưới đỉnh (Level 2 cho các phân hệ chính)")
    
    add_h4("a. Phân hệ 2.0: Đăng ký & Vận hành Gói tập (DFD Level 2)")
    p_styled("Tiến trình 2.0 bao gồm các tiến trình con xử lý đăng ký, tính khuyến mãi và các thao tác bảo lưu/chuyển nhượng:")
    p_styled("• 2.1 Tiếp nhận yêu cầu & Chọn gói tập: Đọc dữ liệu từ D2 (GoiTap) và D1 (ThanhVien).")
    p_styled("• 2.2 Áp dụng mã khuyến mãi & Tính giá: Kiểm tra tính hợp lệ của mã giảm giá trong D2 (KhuyenMai).")
    p_styled("• 2.3 Tạo hợp đồng đăng ký gói: Ghi thông tin vào D3 (DangKyGoi), đồng thời gửi yêu cầu tạo hóa đơn sang Phân hệ 7.0.")
    p_styled("• 2.4 Xử lý bảo lưu gói: Kiểm tra ngày hợp lệ, ghi nhận vào BaoLuuGoiTap và cập nhật ngày kết thúc trong D3.")
    p_styled("• 2.5 Xử lý chuyển nhượng gói: Cập nhật trạng thái 'Chuyển nhượng' cho gói cũ và tạo hợp đồng gói mới cho hội viên tiếp nhận.")

    dfd_sub_2_txt = """
                 +----------------------------------------------+
                 |    DFD MỨC 2 - PHÂN HỆ 2.0: ĐĂNG KÝ GÓI      |
                 +----------------------------------------------+
  [Hội viên] ----> (2.1 Chọn gói) <----- [D2: GoiTap]
                        |
                        v
                   (2.2 Tính KM) <------ [D2: KhuyenMai]
                        |
                        v
                   (2.3 Tạo ĐK Gói) ---> [D3: DangKyGoi]
                        |           ---> [7.0 Tạo Hóa đơn]
                        |
       [Bảo lưu] -------> (2.4 Bảo lưu) -----> [D3: BaoLuuGoiTap]
       [Chuyển nhượng] -> (2.5 Chuyển nhượng)-> [D3: DangKyGoi Mới]
"""
    add_diagram_box(dfd_sub_2_txt, caption="Sơ đồ 3.4: Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Đăng ký & Vận hành Gói tập")

    add_h4("b. Phân hệ 4.0: Check-in quầy & Điểm danh lớp học (DFD Level 2)")
    p_styled("Tiến trình 4.0 đóng vai trò bảo vệ cổng kiểm soát an ninh và quyền lợi ra vào của trung tâm:")
    p_styled("• 4.1 Tiếp nhận mã thẻ / SĐT: Đọc thông tin hội viên từ D1.")
    p_styled("• 4.2 Kiểm tra gói tập & Thuật toán FIFO: Truy vấn D3 tìm các gói tập đang còn hạn sử dụng; nếu hội viên có nhiều gói, tự động chọn gói có hạn kết thúc sớm nhất để trừ lượt/ngày.")
    p_styled("• 4.3 Kiểm tra trạng thái phòng tập: Đọc dữ liệu từ D7 (PhongTap); nếu phòng đang bảo trì thì từ chối check-in.")
    p_styled("• 4.4 Ghi nhận lượt check-in thành công: Ghi bản ghi vào D5 (LichSuCheckIn), phát tín hiệu mở cửa.")
    p_styled("• 4.5 Điểm danh lớp học / buổi tập PT: HLV chọn lớp/học viên, xác nhận sĩ số và ghi vào D5 (DiemDanhLop / LichTapPT).")

    dfd_sub_4_txt = """
                 +----------------------------------------------+
                 |    DFD MỨC 2 - PHÂN HỆ 4.0: CHECK-IN QUẦY    |
                 +----------------------------------------------+
  [Thẻ TV/SĐT] --> (4.1 Nhận diện) <---- [D1: ThanhVien]
                        |
                        v
                   (4.2 Kiểm tra FIFO) < [D3: DangKyGoi]
                        |
                        v
                   (4.3 Kiểm tra Phòng)< [D7: PhongTap]
                        |
          +-------------+-------------+
          | (Hợp lệ)                  | (Không hợp lệ)
          v                           v
     (4.4 Ghi nhận Check-in)     (Báo lỗi từ chối: Hết hạn / Bảo trì)
          |
          +---> [D5: LichSuCheckIn] -> [Mở cửa xoay / Thông báo màn hình]
"""
    add_diagram_box(dfd_sub_4_txt, caption="Sơ đồ 3.5: Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Check-in quầy")

    add_h4("c. Phân hệ 5.0: Bán hàng lẻ POS & Quản lý Kho (DFD Level 2)")
    p_styled("Tiến trình 5.0 xử lý toàn bộ hoạt động kinh doanh bán lẻ tại quầy Bar và kiểm soát tồn kho:")
    p_styled("• 5.1 Quét mã vạch / Chọn sản phẩm: Đọc thông tin đơn giá và tồn kho từ D6 (SanPham).")
    p_styled("• 5.2 Lập phiếu bán hàng POS: Nhập số lượng, tính tổng tiền, liên kết mã hội viên hoặc chọn 'Khách vãng lai'.")
    p_styled("• 5.3 Trừ tồn kho tự động: Kích hoạt Trigger trừ kho trong D6 (SanPham) và lưu lịch sử vào LichSuTonKho.")
    p_styled("• 5.4 Nhập kho hàng hóa: Tiếp nhận hóa đơn giao hàng từ Nhà cung cấp, tạo PhieuNhapKho và cập nhật tăng số tồn kho.")

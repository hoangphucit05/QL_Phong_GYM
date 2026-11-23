# -*- coding: utf-8 -*-
"""
Chương 1: Tổng quan về công ty / cơ sở nơi khảo sát
"""

def add_chapter_1(doc, helpers):
    add_h1 = helpers['add_h1']
    add_h2 = helpers['add_h2']
    add_h3 = helpers['add_h3']
    p_styled = helpers['p_styled']
    add_tbl = helpers['add_tbl']
    add_diagram_box = helpers['add_diagram_box']
    add_image_box = helpers['add_image_box']

    add_h1("CHƯƠNG 1: TỔNG QUAN VỀ CƠ SỞ KHẢO SÁT")

    # 1.1
    add_h2("1.1. Giới thiệu về công ty / cơ sở nơi khảo sát")
    
    p_styled("Trong quá trình thực hiện bài tập lớn môn học Phân tích và Thiết kế Hệ thống Thông tin, nhóm nghiên cứu đã tiến hành khảo sát thực tế tại Trung tâm Thể hình & Yoga GymPro (tên thương hiệu: GymPro Fitness & Yoga Center).")

    p_styled("Thông tin tổng quan về cơ sở khảo sát:")
    p_styled("• Tên đơn vị: Trung tâm Thể hình & Yoga GymPro (GymPro Fitness & Yoga Center)")
    p_styled("• Địa chỉ cơ sở: Số 450-451 Đường Lê Văn Việt, Phường Tăng Nhơn Phú A, Thành phố Thủ Đức, TP. Hồ Chí Minh")
    p_styled("• Quy mô diện tích: Hơn 1.200 m² mặt sàn, bao gồm 2 tầng tập luyện chuyên biệt và 1 tầng dịch vụ tiện ích")
    p_styled("• Năm thành lập: 2020")
    p_styled("• Khách hàng mục tiêu: Học sinh, sinh viên các trường đại học lân cận (ĐH Giao thông Vận tải Phân hiệu TP.HCM, ĐH Sư phạm Kỹ thuật, Học viện Công nghệ Bưu chính Viễn thông...), nhân viên văn phòng, cư dân đô thị và người yêu thích thể thao thể hình trong khu vực")
    p_styled("• Số lượng hội viên thường xuyên: Trên 3.000 hội viên hoạt động tích cực với hàng trăm lượt check-in mỗi ngày.")

    add_image_box("banner_GYM.png", width_cm=14.0, caption="Hình 1.1: Không gian tập luyện hiện đại tại Trung tâm GymPro")

    add_h3("1.1.1. Lịch sử hình thành và tầm nhìn phát triển")
    p_styled("Được thành lập từ năm 2020, Trung tâm GymPro xuất phát điểm từ một phòng tập thể hình quy mô vừa phục vụ cộng đồng địa phương. Trải qua hơn 6 năm xây dựng và phát triển không ngừng, trung tâm đã liên tục đầu tư nâng cấp máy móc, mở rộng diện tích sàn tập, chuẩn hóa chất lượng đội ngũ Huấn luyện viên (Personal Trainer - PT) và đa dạng hóa các bộ môn tập luyện theo xu hướng thể thao hiện đại.")
    
    p_styled("• Tầm nhìn: Trở thành chuỗi trung tâm thể dục thể hình và chăm sóc sức khỏe hàng đầu tại khu vực TP. Thủ Đức và TP. Hồ Chí Minh, mang đến không gian tập luyện chuẩn quốc tế, tràn đầy năng lượng tích cực cho mọi đối tượng khách hàng.")
    p_styled("• Sứ mệnh: Truyền cảm hứng rèn luyện lối sống năng động, khỏe khoắn; cung cấp dịch vụ huấn luyện cá nhân chuyên sâu, khoa học và dịch vụ khách hàng tận tâm với mức chi phí hợp lý nhất.")
    p_styled("• Giá trị cốt lõi: 'Chuyên nghiệp - Tận tâm - Hiện đại - Hiệu quả'. Luôn đặt trải nghiệm và sự hài lòng của hội viên làm trung tâm của mọi hoạt động vận hành.")

    add_h3("1.1.2. Cơ sở vật chất và phân khu chức năng")
    p_styled("Trung tâm được thiết kế theo phong cách công nghiệp hiện đại (Industrial Athletic Style) với tone màu neon năng động, hệ thống chiếu sáng và âm thanh kích thích tinh thần tập luyện. Không gian phòng tập được phân chia khoa học thành các khu vực chức năng riêng biệt:")
    
    p_styled("1. Khu vực Cardio (Tim mạch): Trang bị hơn 25 máy chạy bộ chuyên dụng cao cấp, máy trượt tuyết (elliptical), máy leo cầu thang (stairmaster) và dàn xe đạp tập trong nhà kết nối màn hình tương tác.")
    p_styled("2. Khu vực Tạ tự do (Free-Weights Zone): Đầy đủ các bộ tạ đơn (Dumbbell) từ 2kg đến 50kg, đòn tạ Olympic, dàn ghế tập đa góc (Flat/Incline/Decline Bench), khung gánh tạ đa năng Power Rack đáp ứng nhu cầu tập nặng của các vận động viên và hội viên nâng cao.")
    p_styled("3. Khu vực Máy khối kháng lực (Pin-Loaded & Plate-Loaded Machines): Hệ thống máy tập cơ ngực, cơ lưng xô, cơ chân đùi, cơ vai và bụng nhập khẩu nguyên chiếc từ các thương hiệu thiết bị thể hình hàng đầu thế giới.")
    p_styled("4. Phòng tập Nhóm Group-X & Yoga Studio: Diện tích hơn 150m², sàn gỗ cao cấp giảm chấn, hệ thống gương bao quanh, chuyên phục vụ các lớp Yoga cổ điển, Power Yoga, Zumba dance hiện đại, Pilates, BodyPump và Boxing đối kháng.")
    p_styled("5. Khu vực Quầy Bar dinh dưỡng & Bán lẻ POS: Cung cấp các loại nước giải khát điện giải, nước khoáng, thực phẩm bổ sung dinh dưỡng thể hình cao cấp (Whey Protein, BCAA, Creatine, Pre-workout), thanh bánh protein bar và phụ kiện bảo hộ tập luyện (dây quấn cổ tay, đai lưng nâng tạ, găng tay gym).")
    p_styled("6. Khu vực Tiện ích và Phục hồi: Tủ gửi đồ thông minh (Smart Locker) bảo mật cao, phòng tắm nóng lạnh riêng tư, phòng xông hơi khô (Sauna) và xông hơi ướt (Steam Room) giúp hội viên thư giãn cơ bắp sau mỗi buổi tập nặng; khu vực bàn đo chỉ số cơ thể chuyên nghiệp InBody 270/370.")

    add_h3("1.1.3. Các dịch vụ chính cung cấp")
    p_styled("Trung tâm GymPro cung cấp hệ sinh thái dịch vụ thể thao toàn diện, bao gồm:")
    p_styled("• Các gói tập luyện tự do (Membership Packages): Gói tập theo ngày, gói tháng cơ bản, gói quý, gói 6 tháng và gói VIP 1 năm không giới hạn thời gian tập luyện và không gian phòng tập.")
    p_styled("• Dịch vụ Huấn luyện viên cá nhân (Personal Training - 1 kèm 1): Khách hàng được thiết kế giáo án tập luyện riêng biệt, hướng dẫn chi tiết từng kỹ thuật động tác, tư vấn chế độ dinh dưỡng khoa học và theo dõi sự thay đổi của các chỉ số cơ thể (cân nặng, tỷ lệ mỡ, khối lượng cơ bắp).")
    p_styled("• Các lớp học nhóm (Group Fitness Classes): Lớp học diễn ra theo lịch cố định hàng tuần dưới sự dẫn dắt của các huấn luyện viên chuyên môn cao, mang lại không khí sôi động, tăng cường tương tác cộng đồng.")
    p_styled("• Bán lẻ sản phẩm và phụ kiện thể thao: Phục vụ trực tiếp nhu cầu dinh dưỡng và dụng cụ hỗ trợ tập luyện tại chỗ.")

    # 1.2
    add_h2("1.2. Sơ đồ tổ chức của công ty / cơ sở")
    p_styled("Bộ máy tổ chức quản lý của Trung tâm GymPro được xây dựng theo mô hình trực tuyến - chức năng, phân định rõ ràng quyền hạn và trách nhiệm của từng bộ phận nhằm đảm bảo tính thông suốt trong vận hành hàng ngày.")

    org_chart = """
+-----------------------------------------------------------------------+
|                    BAN GIÁM ĐỐC TRUNG TÂM (CEO)                       |
|           - Quản lý điều hành chung, chiến lược kinh doanh            |
|           - Phê duyệt tài chính, chính sách gói tập & nhân sự        |
+-----------------------------------+-----------------------------------+
                                    |
     +------------------------------+------------------------------+
     |                              |                              |
+----+-------------------+   +-----+------------------+   +-------+--------------------+
|   BỘ PHẬN LỄ TÂN &     |   |   BỘ PHẬN HUẤN LUYỆN   |   |   BỘ PHẬN KỸ THUẬT &       |
| CHĂM SÓC KHÁCH HÀNG    |   |     VIÊN (HLV - PT)    |   |    BẢO TRÌ THIẾT BỊ        |
| - Đón tiếp, check-in   |   | - Dạy kèm PT 1:1       |   | - Kiểm tra máy tập         |
| - Tư vấn & đăng ký gói |   | - Đứng lớp Group-X     |   | - Lập phiếu sửa chữa       |
| - Quản lý hồ sơ TV     |   | - Điểm danh học viên   |   | - Nghiệm thu vận hành      |
+------------------------+   +------------------------+   +----------------------------+
     |                              |                              |
+----+-------------------+   +-----+------------------+   +-------+--------------------+
|  BỘ PHẬN BÁN HÀNG &    |   |   BỘ PHẬN KẾ TOÁN &    |   |   QUẢN TRỊ VIÊN HỆ THỐNG   |
|     QUẢN LÝ KHO POS    |   |        TÀI CHÍNH       |   |        (IT ADMIN)          |
| - Bán nước/Whey/đồ tập |   | - Quản lý hóa đơn      |   | - Phân quyền tài khoản     |
| - Nhập kho hàng hóa    |   | - Thu/chi/hoàn tiền    |   | - Sao lưu dữ liệu CSDL     |
| - Quản lý tồn kho      |   | - Báo cáo doanh thu    |   | - Giám sát bảo mật         |
+------------------------+   +------------------------+   +----------------------------+
"""
    add_diagram_box(org_chart, caption="Sơ đồ 1.1: Sơ đồ cơ cấu tổ chức bộ máy quản lý Trung tâm GymPro")

    add_h3("1.2.1. Chức năng và nhiệm vụ chi tiết của từng bộ phận")

    headers_dept = ["STT", "Bộ phận / Vị trí", "Nhân sự", "Chức năng và nhiệm vụ chính trong hệ thống"]
    rows_dept = [
        ["1", "Ban Giám đốc (CEO)", "02 người", "Lãnh đạo toàn diện, phê duyệt các gói tập mới, quy định bảng giá, chính sách chiết khấu/khuyến mãi, theo dõi tổng quan các chỉ số KPI doanh thu và tình hình hội viên qua hệ thống báo cáo."],
        ["2", "Bộ phận Lễ tân & CSKH", "06 người", "Tiếp đón khách hàng; thực hiện thủ tục đăng ký hội viên mới; tư vấn gói tập; tiếp nhận yêu cầu vận hành gói (gia hạn, bảo lưu, chuyển nhượng); thực hiện check-in quầy kiểm soát ra vào; thu tiền dịch vụ."],
        ["3", "Bộ phận Huấn luyện viên (PT)", "15 người", "Đo chỉ số cơ thể InBody; lên giáo án tập luyện cá nhân hóa; thực hiện các buổi tập PT 1:1; giảng dạy các lớp nhóm (Yoga, Zumba, Boxing); thực hiện điểm danh học viên trên phần mềm."],
        ["4", "Bộ phận Bán hàng & Kho POS", "04 người", "Thực hiện bán lẻ đồ uống, thực phẩm bổ sung dinh dưỡng và phụ kiện thể thao tại quầy bar; tiếp nhận hàng hóa nhập kho từ nhà cung cấp; kiểm kê tồn kho và đối soát hao hụt."],
        ["5", "Bộ phận Kỹ thuật & Bảo trì", "03 người", "Kiểm tra định kỳ tình trạng an toàn của toàn bộ máy tập, thiết bị âm thanh, ánh sáng, phòng xông hơi; lập phiếu báo hỏng và lên lịch bảo trì; nghiệm thu đưa máy móc trở lại hoạt động."],
        ["6", "Bộ phận Kế toán & Tài chính", "02 người", "Kiểm soát toàn bộ hóa đơn bán hàng và dịch vụ; quản lý thu công nợ của hội viên thanh toán theo đợt; lập phiếu chi bảo trì, nhập hàng; đối soát doanh thu hàng ngày; chốt sổ sách kế toán."],
        ["7", "Quản trị hệ thống (IT Admin)", "01 người", "Quản lý tài khoản đăng nhập; phân quyền vai trò cho nhân viên; bảo vệ tính toàn vẹn CSDL; kiểm tra nhật ký truy cập hệ thống; đảm bảo hạ tầng mạng và phần mềm hoạt động 24/7."]
    ]
    add_tbl(headers_dept, rows_dept, col_widths=[1.2, 3.8, 2.0, 8.5], caption="Bảng 1.1: Cơ cấu nhân sự và nhiệm vụ các phòng ban tại Trung tâm GymPro")

    add_h3("1.2.2. Quy trình phối hợp vận hành thực tế giữa các bộ phận")
    p_styled("Hoạt động phục vụ hội viên tại GymPro là một chuỗi phối hợp nhịp nhàng, liên tục giữa các bộ phận:")
    p_styled("• Khi có khách hàng mới: Lễ tân tiếp đón, tư vấn nhu cầu và ghi nhận thông tin đăng ký lên phần mềm; phân hệ tự động cấp Mã thành viên duy nhất (TV000001). Sau đó, Lễ tân hỗ trợ khách đo chỉ số InBody và kết nối với Huấn luyện viên trưởng để phân công PT phù hợp.")
    p_styled("• Khi khách hàng sử dụng dịch vụ hàng ngày: Hội viên quẹt thẻ hoặc đọc SĐT tại quầy lễ tân để Check-in. Hệ thống tự động kiểm tra tính hợp lệ của gói tập (còn hạn, đúng khung giờ, đúng phòng tập). Nếu có lịch học PT hoặc lớp nhóm trong ngày, phần mềm tự động hiển thị nhắc nhở để nhân viên hướng dẫn khách vào đúng phòng tập.")
    p_styled("• Khi mua sắm tại quầy Bar: Nhân viên bán hàng mở phân hệ POS, quét mã vạch sản phẩm hoặc chọn trên danh mục, xuất phiếu bán hàng và hệ thống tự động trừ trừ kho tức thời mà không cần can thiệp thủ công.")
    p_styled("• Khi phát hiện sự cố máy móc: Bất kỳ nhân viên hoặc HLV nào phát hiện thiết bị có dấu hiệu kêu to, lỏng ốc hoặc hỏng hóc sẽ báo ngay cho Kỹ thuật viên. Kỹ thuật viên mở phần mềm lập 'Phiếu bảo trì', hệ thống tự động đổi trạng thái thiết bị sang 'Bảo trì' và ngăn chặn việc phân công máy đó cho hội viên tập luyện, đảm bảo an toàn tuyệt đối.")
    p_styled("• Kết thúc ca làm việc: Kế toán mở màn hình báo cáo, đối chiếu doanh thu thực tế thu được (tiền mặt, chuyển khoản QR ngân hàng, cà thẻ POS) với dữ liệu phần mềm, chốt ca nhanh chóng và minh bạch.")

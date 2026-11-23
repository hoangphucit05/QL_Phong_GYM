# -*- coding: utf-8 -*-
"""
Chương 2: Cơ sở lý thuyết
"""

def add_chapter_2(doc, helpers):
    add_h1 = helpers['add_h1']
    add_h2 = helpers['add_h2']
    add_h3 = helpers['add_h3']
    p_styled = helpers['p_styled']
    add_tbl = helpers['add_tbl']
    add_diagram_box = helpers['add_diagram_box']

    add_h1("CHƯƠNG 2: CƠ SỞ LÝ THUYẾT")

    # 2.1
    add_h2("2.1. Tổng quan về Hệ thống thông tin quản lý (MIS)")
    p_styled("Hệ thống thông tin quản lý (Management Information System - MIS) là một hệ thống tích hợp giữa con người, thiết bị phần cứng, phần mềm ứng dụng, mạng truyền thông và các cơ sở dữ liệu có nhiệm vụ thu thập, lưu trữ, xử lý và phân phối thông tin hỗ trợ cho việc ra quyết định, điều hành, kiểm soát và phân tích các hoạt động trong một tổ chức.")

    p_styled("Trong môi trường kinh doanh dịch vụ thể thao và rèn luyện thể hình hiện đại, một hệ thống thông tin quản lý phòng gym đóng vai trò huyết mạch:")
    p_styled("• Tự động hóa các thao tác thủ công, giảm thiểu sai sót do con người gây ra trong tính toán học phí, hạn sử dụng gói tập và quản lý công nợ.")
    p_styled("• Chuẩn hóa dữ liệu tập trung, giúp ban quản lý nắm bắt tức thời bức tranh toàn cảnh về doanh thu, số lượng hội viên mới, tỷ lệ gia hạn và hiệu suất khai thác thiết bị máy tập.")
    p_styled("• Nâng cao chất lượng dịch vụ khách hàng: giúp quá trình tiếp đón, làm thủ tục check-in diễn ra trong vài giây, gia tăng mức độ hài lòng và giữ chân hội viên gắn bó lâu dài.")

    # 2.2
    add_h2("2.2. Phương pháp phân tích và thiết kế hệ thống có cấu trúc")
    p_styled("Phương pháp phân tích và thiết kế hệ thống có cấu trúc (Structured Systems Analysis and Design Methodology - SSADM) là một phương pháp luận kinh điển, có tính khoa học và thực tiễn cao trong ngành kỹ thuật phần mềm. Phương pháp này tiếp cận bài toán theo hướng từ trên xuống (Top-Down Approach) và phân rã chức năng (Functional Decomposition).")

    p_styled("Các nguyên lý cốt lõi của phương pháp tiếp cận có cấu trúc bao gồm:")
    p_styled("• Trừu tượng hóa (Abstraction): Tách biệt giữa 'hệ thống làm gì' (phân tích logic - What) và 'hệ thống làm như thế nào' (thiết kế vật lý - How).")
    p_styled("• Phân rã chức năng (Decomposition): Chia nhỏ một bài toán lớn, phức tạp thành các phân hệ chức năng nhỏ hơn, độc lập tương đối và dễ quản lý, triển khai.")
    p_styled("• Mô hình hóa bằng sơ đồ trực quan: Sử dụng hệ thống công cụ đồ họa chuẩn hóa như Sơ đồ phân cấp chức năng (BFD), Biểu đồ luồng dữ liệu (DFD) và Mô hình thực thể liên kết (ERD) để mô tả bài toán một cách trực quan, giúp các kỹ sư phần mềm và người sử dụng nghiệp vụ dễ dàng thống nhất yêu cầu.")

    # 2.3
    add_h2("2.3. Sơ đồ phân cấp chức năng (BFD)")
    p_styled("Sơ đồ phân cấp chức năng (Business Function Diagram - BFD) là công cụ dùng để biểu diễn cấu trúc phân rã có thứ bậc các chức năng nghiệp vụ của một tổ chức hoặc hệ thống thông tin. Sơ đồ BFD có dạng hình cây (Tree Structure):")
    p_styled("• Gốc (Root): Đại diện cho toàn bộ hệ thống thông tin cần nghiên cứu.")
    p_styled("• Các nút mức 1 (Level 1): Biểu diễn các phân hệ chức năng nghiệp vụ lớn.")
    p_styled("• Các nút mức 2, mức 3 (Level 2, 3...): Biểu diễn các chức năng chi tiết hơn thu được sau quá trình phân rã.")

    p_styled("Nguyên tắc xây dựng sơ đồ BFD:")
    p_styled("1. Tính đầy đủ: Tổng hợp các chức năng con ở cấp dưới phải bao quát toàn bộ nội dung của chức năng cấp trên trực tiếp.")
    p_styled("2. Tính độc lập: Các chức năng ở cùng một cấp cần giảm thiểu tối đa sự chồng chéo, giao thoa về nhiệm vụ nghiệp vụ.")
    p_styled("3. Tên chức năng: Bắt buộc phải là một mệnh đề động từ (Động từ + Bổ ngữ), ví dụ: 'Đăng ký gói tập', 'Lập phiếu bảo trì', 'Xuất báo cáo doanh thu'. Không dùng danh từ đơn thuần để đặt tên cho chức năng.")

    # 2.4
    add_h2("2.4. Biểu đồ luồng dữ liệu (DFD)")
    p_styled("Biểu đồ luồng dữ liệu (Data Flow Diagram - DFD) là công cụ mô hình hóa trực quan dùng để biểu diễn sự chuyển dịch, biến đổi và lưu trữ của dòng thông tin trong hệ thống. DFD tập trung mô tả đường đi của dữ liệu từ các tác nhân bên ngoài, qua các tiến trình xử lý, vào các kho dữ liệu và ngược lại.")

    add_h3("2.4.1. Các thành phần cơ bản trong DFD")
    p_styled("Theo chuẩn ký pháp Gane & Sarson (hoặc Yourdon & DeMarco), biểu đồ DFD bao gồm 4 phần tử cơ bản:")
    
    headers_dfd = ["Phần tử", "Ký hiệu hình học", "Ý nghĩa / Vai trò nghiệp vụ"]
    rows_dfd = [
        ["Tiến trình (Process)", "Hình chữ nhật bo tròn góc (hoặc elip), có đánh số thứ tự", "Đại diện cho một thao tác xử lý, biến đổi hoặc kiểm tra dữ liệu vào thành dữ liệu ra. Tên tiến trình luôn bắt đầu bằng một động từ."],
        ["Luồng dữ liệu (Data Flow)", "Mũi tên có hướng, bên cạnh có ghi tên thông tin", "Chỉ hướng di chuyển của gói dữ liệu giữa các tiến trình, kho dữ liệu và tác nhân ngoài. Dữ liệu di chuyển có thể là tài liệu, biểu mẫu, tham số hoặc bản ghi CSDL."],
        ["Kho dữ liệu (Data Store)", "Hai đường thẳng song song (hoặc hình chữ nhật hở một đầu)", "Nơi lưu trữ dữ liệu lâu dài để các tiến trình có thể đọc ra (truy vấn) hoặc ghi vào (thêm/sửa/xóa). Thường tương ứng với các bảng trong CSDL."],
        ["Tác nhân ngoài (External Entity)", "Hình chữ nhật nét liền", "Đối tượng nằm ngoài ranh giới hệ thống (người, phòng ban, tổ chức, hệ thống ngoài) nhưng có tương tác cung cấp dữ liệu đầu vào hoặc tiếp nhận kết quả đầu ra từ hệ thống."]
    ]
    add_tbl(headers_dfd, rows_dfd, col_widths=[3.0, 4.0, 8.5], caption="Bảng 2.1: Các thành phần ký hiệu cơ bản trong Biểu đồ luồng dữ liệu (DFD)")

    add_h3("2.4.2. Các cấp độ biểu diễn của DFD")
    p_styled("Biểu đồ luồng dữ liệu được xây dựng theo phương pháp làm mịn dần qua 3 cấp độ:")
    p_styled("• Mức ngữ cảnh (Context Diagram / Level 0): Cung cấp góc nhìn bao quát nhất về toàn bộ hệ thống như một tiến trình duy nhất (tiến trình 0), xung quanh là các tác nhân ngoài và các luồng thông tin vào/ra chính. Mức này không chứa kho dữ liệu.")
    p_styled("• Mức đỉnh (Level 1 / Level 0 Decomposition): Phân rã tiến trình 0 thành các tiến trình nghiệp vụ cốt lõi (tương ứng với các nút mức 1 của sơ đồ BFD), đồng thời xuất hiện các kho dữ liệu chính của hệ thống.")
    p_styled("• Mức dưới đỉnh (Level 2 / Functional Detailed Level): Phân rã từng tiến trình mức đỉnh thành các thao tác xử lý cụ thể, chi tiết hơn nhằm làm rõ các quy tắc nghiệp vụ chuyên sâu.")

    add_h3("2.4.3. Các nguyên tắc bảo toàn luồng dữ liệu")
    p_styled("Để đảm bảo tính đúng đắn và nhất quán logic, sơ đồ DFD phải tuân thủ nghiêm ngặt các nguyên tắc:")
    p_styled("• Nguyên tắc bảo toàn luồng (Balancing): Mọi luồng dữ liệu đi vào hoặc đi ra khỏi một tiến trình ở mức cao phải xuất hiện đầy đủ trong sơ đồ phân rã mức dưới của tiến trình đó.")
    p_styled("• Tránh hố đen (Black Hole): Không có tiến trình nào chỉ có luồng vào mà không có bất kỳ luồng ra nào.")
    p_styled("• Tránh sự nảy sinh kỳ diệu (Miracle): Không có tiến trình nào chỉ có luồng ra mà không có luồng dữ liệu vào.")
    p_styled("• Không kết nối trực tiếp: Hai tác nhân ngoài không nối trực tiếp với nhau; hai kho dữ liệu không nối trực tiếp với nhau; tác nhân ngoài không được đọc/ghi trực tiếp vào kho dữ liệu mà bắt buộc phải thông qua một tiến trình xử lý.")

    # 2.5
    add_h2("2.5. Cơ sở dữ liệu quan hệ và Mô hình thực thể liên kết (ERD)")
    p_styled("Mô hình thực thể liên kết (Entity-Relationship Diagram - ERD) do Peter Chen đề xuất năm 1976, là công cụ mô hình hóa khái niệm dữ liệu ở mức trừu tượng cao, độc lập với hệ quản trị cơ sở dữ liệu cụ thể.")
    
    p_styled("Các thành phần chính của ERD bao gồm:")
    p_styled("• Thực thể (Entity): Một đối tượng, sự vật, hiện tượng trong thế giới thực có sự tồn tại độc lập và cần lưu trữ thông tin (ví dụ: Thành viên, Gói tập, Thiết bị).")
    p_styled("• Thuộc tính (Attribute): Các đặc trưng, tính chất mô tả thực thể (ví dụ: Họ tên, Số điện thoại, Giá gói tập). Thuộc tính có thể là thuộc tính khóa (Key Attribute), thuộc tính đơn, thuộc tính đa trị hoặc thuộc tính dẫn xuất.")
    p_styled("• Mối quan hệ liên kết (Relationship): Mối liên hệ logic giữa hai hoặc nhiều thực thể. Mối liên kết được phân loại theo bản số (Cardinality): Quan hệ 1 - 1, Quan hệ 1 - N và Quan hệ N - N.")

    p_styled("Chuyển đổi từ mô hình ERD sang Mô hình dữ liệu quan hệ (Relational Model):")
    p_styled("• Mỗi thực thể mạnh được chuyển đổi thành một bảng (Table) trong CSDL, các thuộc tính của thực thể trở thành các cột (Columns).")
    p_styled("• Mối quan hệ 1 - N được hiện thực hóa bằng cách đặt Khóa chính (Primary Key - PK) của bảng phía 1 làm Khóa ngoại (Foreign Key - FK) trong bảng phía N.")
    p_styled("• Mối quan hệ N - N được tách thành hai mối quan hệ 1 - N thông qua việc tạo lập một bảng liên kết trung gian (Junction Table), khóa chính của bảng trung gian là khóa hợp thành từ hai khóa ngoại của hai bảng tham gia.")

    # 2.6
    add_h2("2.6. Lý thuyết chuẩn hóa cơ sở dữ liệu")
    p_styled("Chuẩn hóa cơ sở dữ liệu (Database Normalization) do E.F. Codd khởi xướng, là quá trình cấu trúc lại các bảng dữ liệu nhằm loại bỏ sự dư thừa thông tin (Data Redundancy) và hạn chế các dị thường khi thao tác dữ liệu (Anomaly: thêm, xóa, sửa).")

    p_styled("Các dạng chuẩn cốt lõi:")
    p_styled("• Dạng chuẩn 1 (1NF - First Normal Form): Một lược đồ quan hệ đạt 1NF nếu và chỉ nếu tất cả các thuộc tính của nó đều chứa các giá trị nguyên tố (Atomic Values), không chứa thuộc tính lặp lại (Repeating Groups) hoặc thuộc tính phức hợp/đa trị.")
    p_styled("• Dạng chuẩn 2 (2NF - Second Normal Form): Một quan hệ đạt 2NF nếu nó đã đạt 1NF và mọi thuộc tính không khóa đều phụ thuộc hàm đầy đủ vào khóa chính (không phụ thuộc vào một phần của khóa chính trong trường hợp khóa chính là khóa hợp thành).")
    p_styled("• Dạng chuẩn 3 (3NF - Third Normal Form): Một quan hệ đạt 3NF nếu nó đã đạt 2NF và không có thuộc tính không khóa nào phụ thuộc bắc cầu (Transitive Dependency) vào khóa chính thông qua một thuộc tính không khóa khác.")
    p_styled("• Dạng chuẩn Boyce-Codd (BCNF): Là dạng mở rộng chặt chẽ hơn của 3NF, yêu cầu trong mọi phụ thuộc hàm không tầm thường X -> Y thì X phải là một siêu khóa (Superkey).")

    # 2.7
    add_h2("2.7. Công nghệ phần mềm và Kiến trúc hệ thống áp dụng")
    p_styled("Để hiện thực hóa hệ thống quản lý phòng gym GymPro đáp ứng yêu cầu vận hành thực tế tại quầy, tính toán nhanh chóng và độ tin cậy tuyệt đối, dự án lựa chọn hệ sinh thái công nghệ phần mềm vững chắc:")

    add_h3("2.7.1. Nền tảng phát triển ứng dụng .NET 10.0 và C# Windows Forms")
    p_styled("• C# (C-Sharp): Ngôn ngữ lập trình hướng đối tượng hiện đại, kiểu tĩnh mạnh mẽ, cú pháp an toàn và hiệu năng thực thi vượt trội.")
    p_styled("• Windows Forms (.NET 10.0-windows): Nền tảng phát triển ứng dụng desktop tối ưu cho máy tính quầy thu ngân và máy tính trạm của nhân viên. Giao diện WinForms phản hồi tức thời (độ trễ dưới 10ms), tương thích hoàn hảo với các thiết bị ngoại vi chuyên dụng tại quầy (máy quét mã vạch Barcode/QR, máy in hóa đơn nhiệt bill POS 80mm, webcam chụp ảnh hội viên và máy quẹt thẻ chip/từ).")

    add_h3("2.7.2. Hệ quản trị cơ sở dữ liệu Microsoft SQL Server & ADO.NET")
    p_styled("• Microsoft SQL Server: Hệ quản trị CSDL quan hệ cấp doanh nghiệp (Enterprise RDBMS) với khả năng xử lý giao dịch đồng thời (ACID Transaction) mạnh mẽ, bảo mật cao và tính sẵn sàng vượt trội.")
    p_styled("• ADO.NET kết hợp Stored Procedures & Triggers: Thay vì sử dụng các thư viện ORM cồng kềnh dễ làm suy giảm hiệu năng trên các tập dữ liệu lớn, dự án sử dụng trực tiếp Microsoft.Data.SqlClient với cơ chế Parameterized Queries chống hoàn toàn lỗi tiêm nhiễm mã độc SQL Injection. Đồng thời, các nghiệp vụ then chốt (như Check-in quầy, trừ hạn gói FIFO, tính toán tổng tiền hóa đơn, tự động cập nhật tồn kho khi bán hàng) được đóng gói trong Stored Procedures và Triggers nội tại của SQL Server, đảm bảo tính nhất quán dữ liệu ở mức cơ sở dữ liệu cao nhất.")

    add_h3("2.7.3. Kiến trúc phân tầng 3 lớp (3-Tier Architecture)")
    p_styled("Mã nguồn ứng dụng được tổ chức nghiêm ngặt theo mô hình 3 lớp phân định ranh giới trách nhiệm rõ ràng:")
    p_styled("1. Tầng Giao diện (Presentation Layer - UI / Forms): Chịu trách nhiệm hiển thị dữ liệu lên màn hình người dùng, bắt các sự kiện nhập liệu chuột, bàn phím và điều hướng giao diện. Các màn hình UI kế thừa từ Form của WinForms.")
    p_styled("2. Tầng Nghiệp vụ (Business Logic Layer - BLL): Đóng gói toàn bộ các quy tắc nghiệp vụ của phòng gym: kiểm tra hợp lệ thông tin hội viên, xác thực tài khoản đăng nhập, tính toán tỷ lệ chiết khấu khuyến mãi, kiểm tra điều kiện bảo lưu/nâng cấp gói tập trước khi chuyển tiếp dữ liệu xuống tầng dưới.")
    p_styled("3. Tầng Truy cập Dữ liệu (Data Access Layer - DAL): Độc quyền giao tiếp với SQL Server thông qua SqlConnection, SqlCommand; thực thi các truy vấn SELECT, INSERT, UPDATE, DELETE và ánh xạ dữ liệu thành các đối tượng Model dùng chung.")

    add_h3("2.7.4. Kiến trúc An toàn và Bảo mật hệ thống")
    p_styled("Hệ thống GymPro được trang bị cơ chế bảo mật đa lớp theo chuẩn công nghiệp:")
    p_styled("• Bảo mật Mật khẩu: Sử dụng thuật toán băm chậm chuyên dụng PBKDF2 (Password-Based Key Derivation Function 2) với hàm băm SHA-512 và muối bảo mật (Salt) ngẫu nhiên 128-bit. Mật khẩu không bao giờ được lưu trữ dưới dạng bản rõ (plaintext) trong CSDL.")
    p_styled("• Cơ chế phòng thủ tấn công Bruteforce (Account Lockout): Khi một người dùng nhập sai mật khẩu quá 5 lần liên tiếp, hệ thống tự động khóa tài khoản tạm thời trong vòng 15 phút và ghi nhận địa chỉ máy trạm cùng thời gian vi phạm vào bảng nhật ký LichSuDangNhap.")
    p_styled("• Kiểm soát truy cập dựa trên vai trò (Role-Based Access Control - RBAC): Hệ thống phân quyền chặt chẽ 6 nhóm vai trò người dùng (Admin, Lễ tân, Huấn luyện viên, Bán hàng, Kế toán, Kỹ thuật bảo trì). Quyền hạn được kiểm soát cả ở tầng UI (ẩn/vô hiệu hóa các nút bấm, menu không thuộc thẩm quyền) lẫn tầng BLL và Stored Procedure (chặn hoàn toàn việc gọi thao tác trái phép ngay cả khi can thiệp vào tầng giao diện).")

# -*- coding: utf-8 -*-
"""
Bộ tạo tài liệu Báo cáo Bài tập lớn Môn Phân tích Thiết kế Hệ thống Thông tin
Đề tài: Quản lý Phòng Gym (GymPro Fitness & Yoga)
Xuất ra đồng thời:
1. BAO_CAO_BAI_TAP_LON_PTTKHTTT_GYMPRO.docx (chuẩn Word của trường ĐH GTVT - Phân hiệu TP.HCM)
2. BAO_CAO_BAI_TAP_LON_PTTKHTTT_GYMPRO.md (bản Markdown đầy đủ chuẩn GitHub-flavored)
"""

import os
import sys
import docx
from docx.shared import Inches, Pt, RGBColor, Cm
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT, WD_ALIGN_VERTICAL
from docx.oxml import OxmlElement, parse_xml
from docx.oxml.ns import nsdecls, qn

REPORT_DIR = r"D:\BTL PTTKYC\He_Thong_GYM\báo cáo"
DOCX_OUT = os.path.join(REPORT_DIR, "BAO_CAO_BAI_TAP_LON_PTTKHTTT_GYMPRO.docx")
MD_OUT = os.path.join(REPORT_DIR, "BAO_CAO_BAI_TAP_LON_PTTKHTTT_GYMPRO.md")
IMG_DIR = r"D:\BTL PTTKYC\He_Thong_GYM\images"

from generate_data import ABBREVIATIONS, TABLES_LIST, FIGURES_LIST
from chapter1 import add_chapter_1
from chapter2 import add_chapter_2
from chapter3 import add_chapter_3
from chapter4 import add_chapter_4
from conclusion import add_conclusion_and_refs

def set_cell_border(cell, **kwargs):
    tcPr = cell._tc.get_or_add_tcPr()
    tcBorders = parse_xml(f'<w:tcBorders {nsdecls("w")}/>')
    for border_name in ['top', 'left', 'bottom', 'right']:
        edge_data = kwargs.get(border_name)
        if edge_data:
            val = edge_data.get('val', 'single')
            sz = str(edge_data.get('sz', 4))
            color = edge_data.get('color', 'C0C0C0')
            b_elm = parse_xml(f'<w:{border_name} {nsdecls("w")} w:val="{val}" w:sz="{sz}" w:space="0" w:color="{color}"/>')
            tcBorders.append(b_elm)
        else:
            b_elm = parse_xml(f'<w:{border_name} {nsdecls("w")} w:val="single" w:sz="4" w:space="0" w:color="C0C0C0"/>')
            tcBorders.append(b_elm)
    tcPr.append(tcBorders)

def set_cell_background(cell, color_hex="F2F2F2"):
    shd = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{color_hex}"/>')
    cell._tc.get_or_add_tcPr().append(shd)

def create_report_docx():
    print("Bắt đầu khởi tạo tài liệu Word (.docx)...")
    doc = docx.Document()

    # Khổ giấy A4, Lề trái 3.5cm, phải 2.0cm, trên 2.0cm, dưới 2.0cm
    section = doc.sections[0]
    section.page_width = Cm(21.0)
    section.page_height = Cm(29.7)
    section.top_margin = Cm(2.0)
    section.bottom_margin = Cm(2.0)
    section.left_margin = Cm(3.5)
    section.right_margin = Cm(2.0)

    # Cấu hình Style mặc định
    normal_style = doc.styles['Normal']
    normal_style.font.name = 'Times New Roman'
    normal_style.font.size = Pt(13)
    normal_style.paragraph_format.line_spacing = 1.15
    normal_style.paragraph_format.space_before = Pt(6)
    normal_style.paragraph_format.space_after = Pt(6)

    def p_styled(text="", align=WD_ALIGN_PARAGRAPH.JUSTIFY, bold=False, italic=False, 
                 size_pt=13, space_before=6, space_after=6, line_spacing=1.15, color_rgb=(0,0,0)):
        p = doc.add_paragraph()
        p.alignment = align
        p.paragraph_format.space_before = Pt(space_before)
        p.paragraph_format.space_after = Pt(space_after)
        p.paragraph_format.line_spacing = line_spacing
        if text:
            r = p.add_run(text)
            r.font.name = 'Times New Roman'
            r.font.size = Pt(size_pt)
            r.bold = bold
            r.italic = italic
            r.font.color.rgb = RGBColor(*color_rgb)
        return p

    def add_h1(text):
        doc.add_page_break()
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        p.paragraph_format.space_before = Pt(16)
        p.paragraph_format.space_after = Pt(12)
        p.paragraph_format.line_spacing = 1.15
        r = p.add_run(text)
        r.font.name = 'Times New Roman'
        r.font.size = Pt(15.5)
        r.bold = True
        r.font.color.rgb = RGBColor(0, 32, 96)
        return p

    def add_h2(text):
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.space_before = Pt(12)
        p.paragraph_format.space_after = Pt(6)
        p.paragraph_format.line_spacing = 1.15
        r = p.add_run(text)
        r.font.name = 'Times New Roman'
        r.font.size = Pt(14)
        r.bold = True
        r.font.color.rgb = RGBColor(0, 51, 102)
        return p

    def add_h3(text):
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.space_before = Pt(8)
        p.paragraph_format.space_after = Pt(4)
        p.paragraph_format.line_spacing = 1.15
        r = p.add_run(text)
        r.font.name = 'Times New Roman'
        r.font.size = Pt(13)
        r.bold = True
        r.font.color.rgb = RGBColor(30, 30, 30)
        return p

    def add_h4(text):
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.space_before = Pt(6)
        p.paragraph_format.space_after = Pt(3)
        p.paragraph_format.line_spacing = 1.15
        r = p.add_run(text)
        r.font.name = 'Times New Roman'
        r.font.size = Pt(13)
        r.bold = True
        r.italic = True
        r.font.color.rgb = RGBColor(60, 60, 60)
        return p

    def add_tbl(headers, rows_data, col_widths=None, caption=""):
        if caption:
            cp = doc.add_paragraph()
            cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
            cp.paragraph_format.space_before = Pt(8)
            cp.paragraph_format.space_after = Pt(4)
            r_c = cp.add_run(caption)
            r_c.font.name = 'Times New Roman'
            r_c.font.size = Pt(12.5)
            r_c.bold = True

        tbl = doc.add_table(rows=len(rows_data) + 1, cols=len(headers))
        tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
        tbl.autofit = False

        hdr_cells = tbl.rows[0].cells
        for i, h in enumerate(headers):
            hdr_cells[i].text = h
            set_cell_background(hdr_cells[i], "D9E1F2")
            set_cell_border(hdr_cells[i], top={'sz': 6, 'color': '2F5597'}, bottom={'sz': 6, 'color': '2F5597'})
            hdr_cells[i].vertical_alignment = WD_ALIGN_VERTICAL.CENTER
            hp = hdr_cells[i].paragraphs[0]
            hp.alignment = WD_ALIGN_PARAGRAPH.CENTER
            hp.paragraph_format.space_before = Pt(4)
            hp.paragraph_format.space_after = Pt(4)
            hp.paragraph_format.line_spacing = 1.05
            for hr in hp.runs:
                hr.font.name = 'Times New Roman'
                hr.font.size = Pt(11.5)
                hr.bold = True
                hr.font.color.rgb = RGBColor(10, 40, 80)

        for r_i, row_vals in enumerate(rows_data):
            r_cells = tbl.rows[r_i + 1].cells
            bg_col = "F9FAFB" if r_i % 2 == 1 else "FFFFFF"
            for c_i, val in enumerate(row_vals):
                r_cells[c_i].text = str(val)
                set_cell_background(r_cells[c_i], bg_col)
                set_cell_border(r_cells[c_i], top={'sz': 4, 'color': 'D3D3D3'}, bottom={'sz': 4, 'color': 'D3D3D3'})
                r_cells[c_i].vertical_alignment = WD_ALIGN_VERTICAL.CENTER
                dp = r_cells[c_i].paragraphs[0]
                dp.alignment = WD_ALIGN_PARAGRAPH.LEFT if c_i > 1 else WD_ALIGN_PARAGRAPH.CENTER
                dp.paragraph_format.space_before = Pt(3)
                dp.paragraph_format.space_after = Pt(3)
                dp.paragraph_format.line_spacing = 1.05
                for dr in dp.runs:
                    dr.font.name = 'Times New Roman'
                    dr.font.size = Pt(11.5)

        if col_widths and len(col_widths) == len(headers):
            for row in tbl.rows:
                for idx, w in enumerate(col_widths):
                    row.cells[idx].width = Cm(w)

        sp = doc.add_paragraph()
        sp.paragraph_format.space_before = Pt(2)
        sp.paragraph_format.space_after = Pt(6)

    def add_diagram_box(content, caption=""):
        if caption:
            cp = doc.add_paragraph()
            cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
            cp.paragraph_format.space_before = Pt(8)
            cp.paragraph_format.space_after = Pt(4)
            r_c = cp.add_run(caption)
            r_c.font.name = 'Times New Roman'
            r_c.font.size = Pt(12.5)
            r_c.bold = True
            r_c.italic = True

        tbl = doc.add_table(rows=1, cols=1)
        tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
        tbl.autofit = False
        tbl.rows[0].cells[0].width = Cm(15.5)
        c = tbl.rows[0].cells[0]
        c.text = content.strip()
        set_cell_background(c, "F4F6F7")
        set_cell_border(c, top={'sz': 6, 'color': '4A6572'}, bottom={'sz': 6, 'color': '4A6572'},
                           left={'sz': 18, 'color': '1F4E79'}, right={'sz': 6, 'color': '4A6572'})
        p = c.paragraphs[0]
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.space_before = Pt(6)
        p.paragraph_format.space_after = Pt(6)
        p.paragraph_format.line_spacing = 1.10
        for r in p.runs:
            r.font.name = 'Consolas'
            r.font.size = Pt(10)
            r.font.color.rgb = RGBColor(20, 20, 20)

        sp = doc.add_paragraph()
        sp.paragraph_format.space_before = Pt(2)
        sp.paragraph_format.space_after = Pt(6)

    def add_image_box(img_filename, width_cm=14.0, caption=""):
        fpath = os.path.join(IMG_DIR, img_filename)
        if os.path.exists(fpath):
            ip = doc.add_paragraph()
            ip.alignment = WD_ALIGN_PARAGRAPH.CENTER
            ip.paragraph_format.space_before = Pt(8)
            ip.paragraph_format.space_after = Pt(4)
            run = ip.add_run()
            try:
                run.add_picture(fpath, width=Cm(width_cm))
            except Exception as e:
                print(f"Lỗi thêm ảnh {img_filename}: {e}")
            if caption:
                cp = doc.add_paragraph()
                cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
                cp.paragraph_format.space_before = Pt(2)
                cp.paragraph_format.space_after = Pt(8)
                rc = cp.add_run(caption)
                rc.font.name = 'Times New Roman'
                rc.font.size = Pt(12)
                rc.italic = True

    helpers = {
        'add_h1': add_h1,
        'add_h2': add_h2,
        'add_h3': add_h3,
        'add_h4': add_h4,
        'p_styled': p_styled,
        'add_tbl': add_tbl,
        'add_diagram_box': add_diagram_box,
        'add_image_box': add_image_box,
        'WD_ALIGN_PARAGRAPH': WD_ALIGN_PARAGRAPH,
        'Pt': Pt,
        'Cm': Cm,
        'RGBColor': RGBColor
    }

    # =========================================================================
    # 1. TRANG BÌA NGOÀI (BÌA CỨNG)
    # =========================================================================
    p_styled("TRƯỜNG ĐẠI HỌC GIAO THÔNG VẬN TẢI", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=10, space_after=2)
    p_styled("PHÂN HIỆU TẠI TP. HỒ CHÍ MINH", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=2)
    p_styled("KHOA CÔNG NGHỆ THÔNG TIN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=18)

    add_image_box("logo_gym.png", width_cm=4.5)

    p_styled("BÁO CÁO BÀI TẬP LỚN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=16, space_before=20, space_after=6, color_rgb=(0, 32, 96))
    p_styled("HỌC PHẦN: PHÂN TÍCH THIẾT KẾ HỆ THỐNG THÔNG TIN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=18)

    p_styled("ĐỀ TÀI:", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=6, space_after=4)
    p_styled("PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG THÔNG TIN\nQUẢN LÝ PHÒNG GYM (GYMPRO FITNESS & YOGA)", 
             align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=16, space_before=2, space_after=40, color_rgb=(192, 0, 0))

    tbl_info = [
        ["Giảng viên hướng dẫn:", "TS. TRẦN PHONG NHÃ"],
        ["Sinh viên thực hiện:", "NHÓM SINH VIÊN THỰC HIỆN"],
        ["Lớp chuyên ngành:", "KỸ THUẬT PHẦN MỀM"],
        ["Khóa đào tạo:", "2023 - 2027"]
    ]
    for row in tbl_info:
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.left_indent = Cm(3.0)
        p.paragraph_format.space_before = Pt(2)
        p.paragraph_format.space_after = Pt(2)
        r1 = p.add_run(f"{row[0]:<26}")
        r1.font.name = 'Times New Roman'
        r1.font.size = Pt(13)
        r1.bold = True
        r2 = p.add_run(f" {row[1]}")
        r2.font.name = 'Times New Roman'
        r2.font.size = Pt(13)
        r2.bold = True

    p_styled("Tp. Hồ Chí Minh, Năm 2026", align=WD_ALIGN_PARAGRAPH.CENTER, italic=True, size_pt=13, space_before=50, space_after=0)

    # =========================================================================
    # 2. TRANG BÌA TRONG (BÌA PHỤ)
    # =========================================================================
    doc.add_page_break()
    p_styled("TRƯỜNG ĐẠI HỌC GIAO THÔNG VẬN TẢI", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=10, space_after=2)
    p_styled("PHÂN HIỆU TẠI TP. HỒ CHÍ MINH", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=2)
    p_styled("KHOA CÔNG NGHỆ THÔNG TIN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=18)

    p_styled("BÁO CÁO BÀI TẬP LỚN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=16, space_before=20, space_after=6, color_rgb=(0, 32, 96))
    p_styled("HỌC PHẦN: PHÂN TÍCH THIẾT KẾ HỆ THỐNG THÔNG TIN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=0, space_after=18)

    p_styled("ĐỀ TÀI:", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=14, space_before=6, space_after=4)
    p_styled("PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG THÔNG TIN\nQUẢN LÝ PHÒNG GYM (GYMPRO FITNESS & YOGA)", 
             align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=16, space_before=2, space_after=35, color_rgb=(192, 0, 0))

    for row in tbl_info:
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.LEFT
        p.paragraph_format.left_indent = Cm(3.0)
        p.paragraph_format.space_before = Pt(2)
        p.paragraph_format.space_after = Pt(2)
        r1 = p.add_run(f"{row[0]:<26}")
        r1.font.name = 'Times New Roman'
        r1.font.size = Pt(13)
        r1.bold = True
        r2 = p.add_run(f" {row[1]}")
        r2.font.name = 'Times New Roman'
        r2.font.size = Pt(13)
        r2.bold = True

    p_styled("Tp. Hồ Chí Minh, Năm 2026", align=WD_ALIGN_PARAGRAPH.CENTER, italic=True, size_pt=13, space_before=60, space_after=0)

    # =========================================================================
    # 3. LỜI MỞ ĐẦU (Không đánh số trang)
    # =========================================================================
    doc.add_page_break()
    p_styled("LỜI MỞ ĐẦU", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=16, color_rgb=(0, 32, 96))

    p_styled("Trong xã hội hiện đại ngày nay, mức sống của người dân ngày càng được nâng cao kéo theo nhu cầu rèn luyện thể chất, nâng cao sức khỏe và hoàn thiện vóc dáng ngày một gia tăng. Các trung tâm thể dục thể hình (Gym & Fitness Center), câu lạc bộ Yoga và thể dục thẩm mỹ xuất hiện ngày càng nhiều với quy mô ngày càng mở rộng, số lượng trang thiết bị đa dạng và cơ cấu dịch vụ ngày càng phong phú (từ tập tự do, đăng ký huấn luyện viên cá nhân PT 1:1, tham gia các lớp học nhóm như Yoga, Zumba, Boxing cho đến bán lẻ thực phẩm bổ sung, phụ kiện tập luyện).")

    p_styled("Tuy nhiên, công tác quản lý tại nhiều phòng gym hiện nay vẫn còn gặp rất nhiều hạn chế do áp dụng các phương thức thủ công, ghi chép sổ sách hoặc sử dụng các bảng tính Excel rời rạc. Cách làm này bộc lộ những nhược điểm nghiêm trọng:")
    
    bullets_mo_dau = [
        "Thất thoát doanh thu và khó kiểm soát công nợ: Khó theo dõi các khoản thanh toán chia đợt, dễ nhầm lẫn biên nhận và không đối soát được doanh thu dịch vụ với kho hàng.",
        "Khó khăn trong quản lý gói tập và quyền lợi hội viên: Không kiểm soát chặt chẽ ngày hết hạn, tình trạng quá hạn, không hỗ trợ linh hoạt các nghiệp vụ vận hành thực tế như bảo lưu gói, chuyển nhượng hoặc nâng cấp gói tập.",
        "Ùn tắc tại quầy check-in: Việc tìm kiếm thông tin thẻ hội viên bằng sổ sách hoặc file excel làm chậm quá trình tiếp đón, gây bức xúc cho khách hàng vào giờ cao điểm.",
        "Quản lý lịch dạy của Huấn luyện viên (PT) và lớp học nhóm chồng chéo: Dễ xảy ra tình trạng trùng ca, trùng phòng tập, thất thoát số buổi học của học viên.",
        "Quản lý bảo trì thiết bị lỏng lẻo: Thiết bị máy tập có giá trị lớn nhưng không có nhật ký bảo dưỡng định kỳ, dẫn đến nguy cơ mất an toàn khi máy móc bị hỏng hóc mà vẫn cho hội viên sử dụng."
    ]
    for b in bullets_mo_dau:
        p_b = doc.add_paragraph()
        p_b.alignment = WD_ALIGN_PARAGRAPH.JUSTIFY
        p_b.paragraph_format.left_indent = Cm(1.0)
        p_b.paragraph_format.space_before = Pt(3)
        p_b.paragraph_format.space_after = Pt(3)
        p_b.paragraph_format.line_spacing = 1.15
        r_dot = p_b.add_run("•  ")
        r_dot.bold = True
        r_t = p_b.add_run(b)
        r_t.font.name = 'Times New Roman'
        r_t.font.size = Pt(13)

    p_styled("Xuất phát từ yêu cầu thực tiễn cấp bách đó, nhóm chúng em đã lựa chọn đề tài: 'Phân tích và Thiết kế Hệ thống Thông tin Quản lý Phòng Gym (GymPro Fitness & Yoga)' nhằm mục tiêu số hóa và tự động hóa toàn diện quy trình vận hành phòng tập.")

    p_styled("Hệ thống được thiết kế theo đúng phương pháp luận Phân tích và Thiết kế Hệ thống Thông tin có cấu trúc, bao gồm việc xây dựng Sơ đồ phân cấp chức năng (BFD), Biểu đồ luồng dữ liệu (DFD các mức), Mô hình thực thể liên kết (ERD) và chuyển đổi thành Cơ sở dữ liệu quan hệ đạt chuẩn 3NF. Đồng thời, hệ thống được hiện thực hóa bằng ứng dụng desktop Windows Forms trên nền tảng .NET 10.0 và CSDL SQL Server với kiến trúc 3 lớp (3-Tier), đảm bảo tính an toàn bảo mật (mã hóa mật khẩu PBKDF2, phân quyền RBAC và cơ chế chống brute-force) cùng giao diện thân thiện, chuyên nghiệp.")

    p_styled("Chúng em xin gửi lời cảm ơn chân thành và sâu sắc nhất đến TS. Trần Phong Nhã đã tận tình giảng dạy, định hướng và đóng góp những ý kiến quý báu trong suốt quá trình nhóm nghiên cứu và hoàn thành bài tập lớn này. Mặc dù đã cố gắng hết sức, song do giới hạn về thời gian và kiến thức thực tế, bài báo cáo khó tránh khỏi những thiếu sót nhất định. Nhóm rất mong nhận được những lời nhận xét, góp ý quý báu của Thầy để hệ thống ngày một hoàn thiện hơn.")

    p_styled("Nhóm sinh viên thực hiện", align=WD_ALIGN_PARAGRAPH.RIGHT, bold=True, size_pt=13, space_before=20, space_after=0)

    # =========================================================================
    # 4. NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN (Không đánh số trang)
    # =========================================================================
    doc.add_page_break()
    p_styled("NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=16, color_rgb=(0, 32, 96))

    nhan_xet_lines = [
        "1. Về tinh thần và thái độ làm việc của sinh viên / nhóm sinh viên:",
        ".........................................................................................................................................................",
        ".........................................................................................................................................................",
        "2. Về tính hoàn thiện và chất lượng của quyển báo cáo bài tập lớn:",
        ".........................................................................................................................................................",
        ".........................................................................................................................................................",
        "3. Về kết quả phân tích thiết kế hệ thống (Mô hình BFD, DFD, ERD, CSDL chuẩn hóa):",
        ".........................................................................................................................................................",
        ".........................................................................................................................................................",
        "4. Về chương trình ứng dụng thực nghiệm (Giao diện, tính đúng đắn nghiệp vụ, bảo mật):",
        ".........................................................................................................................................................",
        ".........................................................................................................................................................",
        "5. Những điểm tồn tại cần khắc phục:",
        ".........................................................................................................................................................",
        "........................................................................................................................................................."
    ]
    for line in nhan_xet_lines:
        p_styled(line, align=WD_ALIGN_PARAGRAPH.LEFT, space_before=3, space_after=3)

    p_styled("ĐIỂM ĐÁNH GIÁ: ............................. (Bằng chữ: ...................................................................)", 
             align=WD_ALIGN_PARAGRAPH.LEFT, bold=True, size_pt=13, space_before=15, space_after=30)

    p_styled("Tp. Hồ Chí Minh, ngày ...... tháng ...... năm 2026", align=WD_ALIGN_PARAGRAPH.RIGHT, italic=True, size_pt=13, space_before=0, space_after=2)
    p_styled("GIẢNG VIÊN HƯỚNG DẪN        ", align=WD_ALIGN_PARAGRAPH.RIGHT, bold=True, size_pt=13, space_before=0, space_after=50)
    p_styled("TS. TRẦN PHONG NHÃ          ", align=WD_ALIGN_PARAGRAPH.RIGHT, bold=True, size_pt=13, space_before=0, space_after=0)

    # =========================================================================
    # 5. MỤC LỤC (Không đánh số trang)
    # =========================================================================
    doc.add_page_break()
    p_styled("MỤC LỤC", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=14, color_rgb=(0, 32, 96))

    toc_items = [
        ("LỜI MỞ ĐẦU", "i"),
        ("NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN", "ii"),
        ("DANH MỤC CÁC TỪ VIẾT TẮT", "iv"),
        ("DANH MỤC BẢNG BIỂU", "v"),
        ("DANH MỤC SƠ ĐỒ VÀ HÌNH VẼ", "vi"),
        ("CHƯƠNG 1: TỔNG QUAN VỀ CƠ SỞ KHẢO SÁT", "1"),
        ("    1.1. Giới thiệu về Trung tâm Thể hình & Yoga GymPro", "1"),
        ("    1.2. Sơ đồ cơ cấu tổ chức và chức năng nhiệm vụ các bộ phận", "3"),
        ("CHƯƠNG 2: CƠ SỞ LÝ THUYẾT", "6"),
        ("    2.1. Tổng quan về Hệ thống thông tin quản lý (MIS)", "6"),
        ("    2.2. Phương pháp phân tích thiết kế hệ thống có cấu trúc", "7"),
        ("    2.3. Sơ đồ phân cấp chức năng (BFD)", "8"),
        ("    2.4. Biểu đồ luồng dữ liệu (DFD)", "9"),
        ("    2.5. Cơ sở dữ liệu quan hệ và Mô hình thực thể liên kết (ERD)", "12"),
        ("    2.6. Lý thuyết chuẩn hóa cơ sở dữ liệu", "14"),
        ("    2.7. Công nghệ phần mềm và Kiến trúc hệ thống áp dụng", "16"),
        ("CHƯƠNG 3: PHÂN TÍCH HỆ THỐNG", "20"),
        ("    3.1. Mô tả chi tiết bài toán nghiệp vụ", "20"),
        ("    3.2. Sơ đồ phân cấp chức năng BFD toàn hệ thống", "25"),
        ("    3.3. Bảng phân tích xác định tiến trình, tác nhân và hồ sơ dữ liệu", "28"),
        ("    3.4. Biểu đồ luồng dữ liệu (DFD các mức)", "31"),
        ("        3.4.1. Biểu đồ luồng dữ liệu mức ngữ cảnh (Context Diagram)", "31"),
        ("        3.4.2. Biểu đồ luồng dữ liệu mức đỉnh (Level 0)", "33"),
        ("        3.4.3. Biểu đồ luồng dữ liệu mức dưới đỉnh (Level 2 cho các phân hệ chính)", "36"),
        ("CHƯƠNG 4: THIẾT KẾ HỆ THỐNG", "42"),
        ("    4.1. Thiết kế Cơ sở dữ liệu", "42"),
        ("        4.1.1. Các thực thể và thuộc tính chi tiết (Data Dictionary)", "42"),
        ("        4.1.2. Xây dựng mô hình thực thể liên kết (ERD)", "49"),
        ("        4.1.3. Chuyển đổi mô hình ERD sang mô hình quan hệ (Relational Schema)", "51"),
        ("        4.1.4. Quá trình chuẩn hóa cơ sở dữ liệu đạt chuẩn 3NF", "54"),
        ("    4.2. Xây dựng chương trình và Thiết kế giao diện", "57"),
        ("        4.2.1. Thiết kế giao diện chính (Đăng nhập, Navigation Shell, Dashboard)", "57"),
        ("        4.2.2. Thiết kế giao diện cập nhật dữ liệu danh mục", "61"),
        ("        4.2.3. Thiết kế giao diện xử lý nghiệp vụ và vận hành hệ thống", "65"),
        ("KẾT LUẬN VÀ KIẾN NGHỊ", "72"),
        ("TÀI LIỆU THAM KHẢO", "74")
    ]

    for item, page in toc_items:
        p_toc = doc.add_paragraph()
        p_toc.paragraph_format.space_before = Pt(2)
        p_toc.paragraph_format.space_after = Pt(2)
        p_toc.paragraph_format.line_spacing = 1.15
        is_main = not item.startswith("    ")
        r_title = p_toc.add_run(item)
        r_title.font.name = 'Times New Roman'
        r_title.font.size = Pt(13)
        r_title.bold = is_main
        dots_len = max(3, 85 - len(item) * 1)
        r_dots = p_toc.add_run(" " + "." * dots_len + " ")
        r_dots.font.name = 'Times New Roman'
        r_dots.font.size = Pt(11)
        r_dots.font.color.rgb = RGBColor(150, 150, 150)
        r_pg = p_toc.add_run(page)
        r_pg.font.name = 'Times New Roman'
        r_pg.font.size = Pt(13)
        r_pg.bold = is_main

    # =========================================================================
    # 6. DANH MỤC CÁC TỪ VIẾT TẮT (Không đánh số trang)
    # =========================================================================
    doc.add_page_break()
    p_styled("DANH MỤC CÁC TỪ VIẾT TẮT", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=14, color_rgb=(0, 32, 96))

    add_tbl(
        ["STT", "Từ viết tắt", "Tên tiếng Anh / Thuật ngữ đầy đủ", "Ý nghĩa / Diễn giải"],
        [[idx + 1, item[0], item[1], item[2]] for idx, item in enumerate(ABBREVIATIONS)],
        col_widths=[1.5, 3.0, 5.5, 5.5]
    )

    # =========================================================================
    # 7. DANH MỤC BẢNG BIỂU (Không đánh số trang)
    # =========================================================================
    doc.add_page_break()
    p_styled("DANH MỤC BẢNG BIỂU", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=14, color_rgb=(0, 32, 96))

    add_tbl(
        ["Ký hiệu bảng", "Tên bảng biểu", "Nội dung tóm tắt"],
        [[item[0], item[1], "Chi tiết trong báo cáo"] for item in TABLES_LIST],
        col_widths=[3.0, 8.5, 4.0]
    )

    # =========================================================================
    # 8. DANH MỤC SƠ ĐỒ VÀ HÌNH VẼ (Không đánh số trang)
    # =========================================================================
    p_styled("DANH MỤC SƠ ĐỒ VÀ HÌNH VẼ", align=WD_ALIGN_PARAGRAPH.CENTER, bold=True, size_pt=15.5, space_before=16, space_after=14, color_rgb=(0, 32, 96))

    add_tbl(
        ["Ký hiệu", "Tên sơ đồ / Hình vẽ", "Ghi chú phân loại"],
        [[item[0], item[1], "Sơ đồ kiến trúc / UI"] for item in FIGURES_LIST],
        col_widths=[3.0, 9.0, 3.5]
    )

    # =========================================================================
    # NỘI DUNG 4 CHƯƠNG + KẾT LUẬN + TÀI LIỆU THAM KHẢO
    # =========================================================================
    print("Thêm Chương 1...")
    add_chapter_1(doc, helpers)

    print("Thêm Chương 2...")
    add_chapter_2(doc, helpers)

    print("Thêm Chương 3...")
    add_chapter_3(doc, helpers)

    print("Thêm Chương 4...")
    add_chapter_4(doc, helpers)

    print("Thêm Kết luận và Tài liệu tham khảo...")
    add_conclusion_and_refs(doc, helpers)

    doc.save(DOCX_OUT)
    print(f"Xuất file Word thành công tại: {DOCX_OUT}")

def create_report_markdown():
    print("Bắt đầu khởi tạo tài liệu Markdown (.md)...")
    
    md_content = """# TRƯỜNG ĐẠI HỌC GIAO THÔNG VẬN TẢI - PHÂN HIỆU TẠI TP. HỒ CHÍ MINH
## KHOA CÔNG NGHỆ THÔNG TIN - BỘ MÔN CÔNG NGHỆ THÔNG TIN

---

# BÁO CÁO BÀI TẬP LỚN
## HỌC PHẦN: PHÂN TÍCH THIẾT KẾ HỆ THỐNG THÔNG TIN

# ĐỀ TÀI: PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG THÔNG TIN QUẢN LÝ PHÒNG GYM (GYMPRO FITNESS & YOGA)

**Giảng viên hướng dẫn:** TS. TRẦN PHONG NHÃ  
**Sinh viên thực hiện:** NHÓM SINH VIÊN THỰC HIỆN  
**Lớp chuyên ngành:** KỸ THUẬT PHẦN MỀM  
**Khóa:** 2023 - 2027  
**Địa điểm & Thời gian:** TP. Hồ Chí Minh, Năm 2026  

---

## LỜI MỞ ĐẦU

Trong xã hội hiện đại ngày nay, mức sống của người dân ngày càng được nâng cao kéo theo nhu cầu rèn luyện thể chất, nâng cao sức khỏe và hoàn thiện vóc dáng ngày một gia tăng. Các trung tâm thể dục thể hình (Gym & Fitness Center), câu lạc bộ Yoga và thể dục thẩm mỹ xuất hiện ngày càng nhiều với quy mô ngày càng mở rộng, số lượng trang thiết bị đa dạng và cơ cấu dịch vụ ngày càng phong phú (từ tập tự do, đăng ký huấn luyện viên cá nhân PT 1:1, tham gia các lớp học nhóm như Yoga, Zumba, Boxing cho đến bán lẻ thực phẩm bổ sung, phụ kiện tập luyện).

Tuy nhiên, công tác quản lý tại nhiều phòng gym hiện nay vẫn còn gặp rất nhiều hạn chế do áp dụng các phương thức thủ công, ghi chép sổ sách hoặc sử dụng các bảng tính Excel rời rạc. Cách làm này bộc lộ những nhược điểm nghiêm trọng:
- **Thất thoát doanh thu và khó kiểm soát công nợ:** Khó theo dõi các khoản thanh toán chia đợt, dễ nhầm lẫn biên nhận và không đối soát được doanh thu dịch vụ với kho hàng.
- **Khó khăn trong quản lý gói tập và quyền lợi hội viên:** Không kiểm soát chặt chẽ ngày hết hạn, tình trạng quá hạn, không hỗ trợ linh hoạt các nghiệp vụ vận hành thực tế như bảo lưu gói, chuyển nhượng hoặc nâng cấp gói tập.
- **Ùn tắc tại quầy check-in:** Việc tìm kiếm thông tin thẻ hội viên bằng sổ sách hoặc file excel làm chậm quá trình tiếp đón, gây bức xúc cho khách hàng vào giờ cao điểm.
- **Quản lý lịch dạy của Huấn luyện viên (PT) và lớp học nhóm chồng chéo:** Dễ xảy ra tình trạng trùng ca, trùng phòng tập, thất thoát số buổi học của học viên.
- **Quản lý bảo trì thiết bị lỏng lẻo:** Thiết bị máy tập có giá trị lớn nhưng không có nhật ký bảo dưỡng định kỳ, dẫn đến nguy cơ mất an toàn khi máy móc bị hỏng hóc mà vẫn cho hội viên sử dụng.

Xuất phát từ yêu cầu thực tiễn cấp bách đó, nhóm chúng em đã lựa chọn đề tài: **"Phân tích và Thiết kế Hệ thống Thông tin Quản lý Phòng Gym (GymPro Fitness & Yoga)"** nhằm mục tiêu số hóa và tự động hóa toàn diện quy trình vận hành phòng tập.

Hệ thống được thiết kế theo đúng phương pháp luận Phân tích và Thiết kế Hệ thống Thông tin có cấu trúc, bao gồm việc xây dựng Sơ đồ phân cấp chức năng (BFD), Biểu đồ luồng dữ liệu (DFD các mức), Mô hình thực thể liên kết (ERD) và chuyển đổi thành Cơ sở dữ liệu quan hệ đạt chuẩn 3NF. Đồng thời, hệ thống được hiện thực hóa bằng ứng dụng desktop Windows Forms trên nền tảng .NET 10.0 và CSDL SQL Server với kiến trúc 3 lớp (3-Tier), đảm bảo tính an toàn bảo mật (mã hóa mật khẩu PBKDF2, phân quyền RBAC và cơ chế chống brute-force) cùng giao diện thân thiện, chuyên nghiệp.

Chúng em xin gửi lời cảm ơn chân thành và sâu sắc nhất đến **TS. Trần Phong Nhã** đã tận tình giảng dạy, định hướng và đóng góp những ý kiến quý báu trong suốt quá trình nhóm nghiên cứu và hoàn thành bài tập lớn này.

---

## NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN

1. Về tinh thần và thái độ làm việc của sinh viên / nhóm sinh viên:  
........................................................................................................................................................................  
........................................................................................................................................................................  

2. Về tính hoàn thiện và chất lượng của quyển báo cáo bài tập lớn:  
........................................................................................................................................................................  
........................................................................................................................................................................  

3. Về kết quả phân tích thiết kế hệ thống (Mô hình BFD, DFD, ERD, CSDL chuẩn hóa):  
........................................................................................................................................................................  
........................................................................................................................................................................  

4. Về chương trình ứng dụng thực nghiệm (Giao diện, tính đúng đắn nghiệp vụ, bảo mật):  
........................................................................................................................................................................  
........................................................................................................................................................................  

5. Những điểm tồn tại cần khắc phục:  
........................................................................................................................................................................  
........................................................................................................................................................................  

**ĐIỂM ĐÁNH GIÁ:** ...................................... *(Bằng chữ: ...................................................................................)*

*Tp. Hồ Chí Minh, ngày ...... tháng ...... năm 2026*  
**GIẢNG VIÊN HƯỚNG DẪN**  
*(Ký và ghi rõ họ tên)*  

\
\
**TS. TRẦN PHONG NHÃ**

---

## MỤC LỤC

- **LỜI MỞ ĐẦU**
- **NHẬN XÉT CỦA GIẢNG VIÊN HƯỚNG DẪN**
- **DANH MỤC CÁC TỪ VIẾT TẮT**
- **DANH MỤC BẢNG BIỂU**
- **DANH MỤC SƠ ĐỒ VÀ HÌNH VẼ**
- **CHƯƠNG 1: TỔNG QUAN VỀ CƠ SỞ KHẢO SÁT**
  - 1.1. Giới thiệu về Trung tâm Thể hình & Yoga GymPro
  - 1.2. Sơ đồ cơ cấu tổ chức và chức năng nhiệm vụ các bộ phận
- **CHƯƠNG 2: CƠ SỞ LÝ THUYẾT**
  - 2.1. Tổng quan về Hệ thống thông tin quản lý (MIS)
  - 2.2. Phương pháp phân tích thiết kế hệ thống có cấu trúc
  - 2.3. Sơ đồ phân cấp chức năng (BFD)
  - 2.4. Biểu đồ luồng dữ liệu (DFD)
  - 2.5. Cơ sở dữ liệu quan hệ và Mô hình thực thể liên kết (ERD)
  - 2.6. Lý thuyết chuẩn hóa cơ sở dữ liệu
  - 2.7. Công nghệ phần mềm và Kiến trúc hệ thống áp dụng
- **CHƯƠNG 3: PHÂN TÍCH HỆ THỐNG**
  - 3.1. Mô tả chi tiết bài toán nghiệp vụ
  - 3.2. Sơ đồ phân cấp chức năng BFD toàn hệ thống
  - 3.3. Bảng phân tích xác định tiến trình, tác nhân và hồ sơ dữ liệu
  - 3.4. Biểu đồ luồng dữ liệu (DFD các mức)
    - 3.4.1. Biểu đồ luồng dữ liệu mức ngữ cảnh (Context Diagram)
    - 3.4.2. Biểu đồ luồng dữ liệu mức đỉnh (Level 0)
    - 3.4.3. Biểu đồ luồng dữ liệu mức dưới đỉnh (Level 2 cho các phân hệ chính)
- **CHƯƠNG 4: THIẾT KẾ HỆ THỐNG**
  - 4.1. Thiết kế Cơ sở dữ liệu
    - 4.1.1. Các thực thể và thuộc tính chi tiết (Data Dictionary)
    - 4.1.2. Xây dựng mô hình thực thể liên kết (ERD)
    - 4.1.3. Chuyển đổi mô hình ERD sang mô hình quan hệ (Relational Schema)
    - 4.1.4. Quá trình chuẩn hóa cơ sở dữ liệu đạt chuẩn 3NF
  - 4.2. Xây dựng chương trình và Thiết kế giao diện
    - 4.2.1. Thiết kế giao diện chính (Đăng nhập, Navigation Shell, Dashboard)
    - 4.2.2. Thiết kế giao diện cập nhật dữ liệu danh mục
    - 4.2.3. Thiết kế giao diện xử lý nghiệp vụ và vận hành hệ thống
- **KẾT LUẬN VÀ KIẾN NGHỊ**
- **TÀI LIỆU THAM KHẢO**

---

## DANH MỤC CÁC TỪ VIẾT TẮT

| STT | Từ viết tắt | Tên tiếng Anh / Thuật ngữ đầy đủ | Ý nghĩa / Diễn giải |
|:---:|:---:|:---|:---|
| 1 | PTTKHTTT | Phân tích và Thiết kế Hệ thống Thông tin | Môn học chuyên ngành CNTT |
| 2 | HTTT | Hệ thống thông tin | Information System |
| 3 | MIS | Hệ thống thông tin quản lý | Management Information System |
| 4 | BFD | Sơ đồ phân cấp chức năng | Business Function Diagram |
| 5 | DFD | Biểu đồ luồng dữ liệu | Data Flow Diagram |
| 6 | ERD | Mô hình thực thể liên kết | Entity-Relationship Diagram |
| 7 | RDBMS | Hệ quản trị cơ sở dữ liệu quan hệ | Relational Database Management System |
| 8 | UI / UX | Giao diện và trải nghiệm người dùng | User Interface / User Experience |
| 9 | BLL | Tầng xử lý logic nghiệp vụ | Business Logic Layer |
| 10 | DAL | Tầng truy xuất cơ sở dữ liệu | Data Access Layer |
| 11 | PK | Khóa chính | Primary Key |
| 12 | FK | Khóa ngoại | Foreign Key |
| 13 | POS | Hệ thống điểm bán lẻ tại quầy | Point of Sale |
| 14 | PT | Huấn luyện viên thể hình cá nhân | Personal Trainer |
| 15 | FIFO | Vào trước - Ra trước | First In First Out (nguyên tắc trừ hạn gói) |
| 16 | RBAC | Kiểm soát truy cập dựa trên vai trò | Role-Based Access Control |
| 17 | PBKDF2 | Chuẩn mã hóa dẫn xuất khóa an toàn | Password-Based Key Derivation Function 2 |
| 18 | CRUD | Thao tác Tạo, Đọc, Cập nhật, Xóa | Create, Read, Update, Delete |

---

## DANH MỤC BẢNG BIỂU

| Ký hiệu bảng | Tên bảng biểu |
|:---|:---|
| **Bảng 1.1** | Cơ cấu nhân sự và nhiệm vụ các phòng ban tại Trung tâm GymPro |
| **Bảng 2.1** | Các thành phần ký hiệu cơ bản trong Biểu đồ luồng dữ liệu (DFD) |
| **Bảng 3.1** | Bảng phân tích xác định Tiến trình, Tác nhân và Hồ sơ dữ liệu |
| **Bảng 3.2** | Bảng phân rã sơ đồ chức năng BFD toàn hệ thống |
| **Bảng 4.1** | Từ điển dữ liệu - Bảng ThanhVien (Quản lý hồ sơ hội viên) |
| **Bảng 4.2** | Từ điển dữ liệu - Bảng GoiTap (Danh mục gói dịch vụ tập luyện) |
| **Bảng 4.3** | Từ điển dữ liệu - Bảng DangKyGoi (Quản lý hợp đồng đăng ký gói tập) |
| **Bảng 4.4** | Từ điển dữ liệu - Bảng DangKyPT (Hợp đồng huấn luyện cá nhân 1:1) |
| **Bảng 4.5** | Từ điển dữ liệu - Bảng HoaDon (Quản lý hóa đơn tài chính) |
| **Bảng 4.6** | Từ điển dữ liệu - Bảng ThanhToan (Quản lý giao dịch thu tiền) |
| **Bảng 4.7** | Từ điển dữ liệu - Bảng SanPham (Hàng hóa & Kho quầy Bar) |
| **Bảng 4.8** | Từ điển dữ liệu - Bảng ThietBi (Trang thiết bị máy tập) |
| **Bảng 4.9** | Từ điển dữ liệu - Bảng BaoTriThietBi (Lịch sử bảo trì sửa chữa máy tập) |
| **Bảng 4.10** | Từ điển dữ liệu - Bảng NhanVien (Hồ sơ nhân sự) |
| **Bảng 4.11** | Từ điển dữ liệu - Bảng TaiKhoan (Tài khoản người dùng và bảo mật) |
| **Bảng 4.12** | Tổng hợp các Stored Procedures và Triggers toàn vẹn dữ liệu trong hệ thống |

---

## DANH MỤC SƠ ĐỒ VÀ HÌNH VẼ

| Ký hiệu | Tên sơ đồ / Hình vẽ |
|:---|:---|
| **Sơ đồ 1.1** | Sơ đồ cơ cấu tổ chức bộ máy quản lý Trung tâm GymPro |
| **Sơ đồ 3.1** | Sơ đồ phân cấp chức năng BFD toàn hệ thống GymPro |
| **Sơ đồ 3.2** | Biểu đồ luồng dữ liệu DFD mức ngữ cảnh (Context Diagram) |
| **Sơ đồ 3.3** | Biểu đồ luồng dữ liệu DFD mức đỉnh (Level 0) |
| **Sơ đồ 3.4** | Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Đăng ký & Vận hành Gói tập |
| **Sơ đồ 3.5** | Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Check-in quầy |
| **Sơ đồ 4.1** | Mô hình thực thể liên kết (ERD) hệ thống Quản lý GymPro |
| **Hình 1.1** | Không gian tập luyện hiện đại tại Trung tâm GymPro |
| **Hình 4.2** | Giao diện Trung tâm điều khiển chính và Dashboard tổng quan GymPro |

---

# CHƯƠNG 1: TỔNG QUAN VỀ CƠ SỞ KHẢO SÁT

## 1.1. Giới thiệu về công ty / cơ sở nơi khảo sát

Trong quá trình thực hiện bài tập lớn môn học Phân tích và Thiết kế Hệ thống Thông tin, nhóm nghiên cứu đã tiến hành khảo sát thực tế tại **Trung tâm Thể hình & Yoga GymPro** (tên thương hiệu: *GymPro Fitness & Yoga Center*).

- **Tên đơn vị:** Trung tâm Thể hình & Yoga GymPro
- **Địa chỉ cơ sở:** Số 450-451 Đường Lê Văn Việt, Phường Tăng Nhơn Phú A, Thành phố Thủ Đức, TP. Hồ Chí Minh
- **Quy mô diện tích:** Hơn 1.200 m² mặt sàn, bao gồm 2 tầng tập luyện chuyên biệt và 1 tầng dịch vụ tiện ích
- **Năm thành lập:** 2020
- **Khách hàng mục tiêu:** Sinh viên, nhân viên văn phòng, cư dân đô thị và người yêu thích thể thao thể hình trong khu vực
- **Số lượng hội viên:** Trên 3.000 hội viên hoạt động tích cực với hàng trăm lượt check-in mỗi ngày.

### 1.1.1. Lịch sử hình thành và tầm nhìn phát triển
Được thành lập từ năm 2020, Trung tâm GymPro xuất phát điểm từ một phòng tập thể hình quy mô vừa phục vụ cộng đồng địa phương. Trải qua hơn 6 năm xây dựng và phát triển không ngừng, trung tâm đã liên tục đầu tư nâng cấp máy móc, mở rộng diện tích sàn tập, chuẩn hóa chất lượng đội ngũ Huấn luyện viên (Personal Trainer - PT) và đa dạng hóa các bộ môn tập luyện.
- **Tầm nhìn:** Trở thành chuỗi trung tâm thể dục thể hình và chăm sóc sức khỏe hàng đầu tại khu vực TP. Thủ Đức và TP. Hồ Chí Minh.
- **Sứ mệnh:** Truyền cảm hứng rèn luyện lối sống năng động, khỏe khoắn; cung cấp dịch vụ huấn luyện cá nhân chuyên sâu, khoa học và dịch vụ khách hàng tận tâm với chi phí hợp lý nhất.
- **Giá trị cốt lõi:** *"Chuyên nghiệp - Tận tâm - Hiện đại - Hiệu quả"*.

### 1.1.2. Cơ sở vật chất và phân khu chức năng
Trung tâm được thiết kế theo phong cách hiện đại với các phân khu chức năng:
1. **Khu vực Cardio (Tim mạch):** Hơn 25 máy chạy bộ chuyên dụng cao cấp, máy trượt tuyết (elliptical), máy leo cầu thang (stairmaster) và dàn xe đạp tập trong nhà.
2. **Khu vực Tạ tự do (Free-Weights Zone):** Bộ tạ đơn (Dumbbell) từ 2kg đến 50kg, đòn tạ Olympic, dàn ghế tập đa góc, khung gánh tạ đa năng Power Rack.
3. **Khu vực Máy khối kháng lực:** Máy tập cơ ngực, lưng xô, chân đùi, vai và bụng nhập khẩu nguyên chiếc.
4. **Phòng tập Nhóm Group-X & Yoga Studio:** Diện tích 150m², sàn gỗ cao cấp giảm chấn, gương bao quanh, phục vụ Yoga, Zumba, Pilates, Boxing.
5. **Khu vực Quầy Bar dinh dưỡng & Bán lẻ POS:** Cung cấp nước giải khát điện giải, nước khoáng, thực phẩm bổ sung (Whey Protein, BCAA, Creatine) và phụ kiện tập luyện (đai lưng, găng tay gym).
6. **Khu vực Tiện ích và Phục hồi:** Tủ Smart Locker bảo mật cao, phòng tắm nóng lạnh, phòng xông hơi khô/ướt Sauna & Steam Room, máy đo chỉ số cơ thể chuyên nghiệp InBody.

### 1.1.3. Các dịch vụ chính cung cấp
- **Các gói tập luyện tự do (Membership):** Gói ngày, tháng cơ bản, quý, 6 tháng và gói VIP 1 năm không giới hạn thời gian.
- **Dịch vụ Huấn luyện viên cá nhân (PT 1:1):** Giáo án tập luyện cá nhân hóa, đo InBody định kỳ, theo dõi chế độ dinh dưỡng khoa học.
- **Các lớp học nhóm (Group Fitness):** Lớp học diễn ra theo lịch cố định hàng tuần (Yoga, Zumba, Boxing).
- **Bán lẻ sản phẩm và phụ kiện thể thao:** Phục vụ trực tiếp nhu cầu dinh dưỡng và dụng cụ hỗ trợ tập luyện tại chỗ.

## 1.2. Sơ đồ tổ chức của công ty / cơ sở

```
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
```
*Sơ đồ 1.1: Sơ đồ cơ cấu tổ chức bộ máy quản lý Trung tâm GymPro*

### 1.2.1. Chức năng và nhiệm vụ chi tiết của từng bộ phận

| STT | Bộ phận / Vị trí | Nhân sự | Chức năng và nhiệm vụ chính trong hệ thống |
|:---:|:---|:---:|:---|
| 1 | Ban Giám đốc (CEO) | 02 người | Lãnh đạo toàn diện, phê duyệt gói tập mới, chính sách chiết khấu, theo dõi KPI doanh thu qua hệ thống báo cáo. |
| 2 | Bộ phận Lễ tân & CSKH | 06 người | Tiếp đón khách hàng; đăng ký hội viên mới; tư vấn gói tập; tiếp nhận bảo lưu/chuyển nhượng gói; check-in quầy kiểm soát ra vào; thu tiền dịch vụ. |
| 3 | Bộ phận Huấn luyện viên (PT) | 15 người | Đo InBody; lên giáo án tập luyện; dạy kèm PT 1:1; giảng dạy các lớp nhóm (Yoga, Zumba); điểm danh học viên trên phần mềm. |
| 4 | Bộ phận Bán hàng & Kho POS | 04 người | Bán lẻ đồ uống, Whey Protein, phụ kiện thể thao tại quầy bar; tiếp nhận hàng nhập kho; kiểm kê và đối soát tồn kho. |
| 5 | Bộ phận Kỹ thuật & Bảo trì | 03 người | Kiểm tra định kỳ máy tập; lập phiếu báo hỏng và lên lịch bảo trì; nghiệm thu khôi phục máy móc sau sửa chữa. |
| 6 | Bộ phận Kế toán & Tài chính | 02 người | Kiểm soát hóa đơn dịch vụ; quản lý công nợ hội viên; đối soát doanh thu hàng ngày; chốt sổ sách kế toán. |
| 7 | Quản trị hệ thống (IT Admin) | 01 người | Quản lý tài khoản đăng nhập; phân quyền vai trò cho nhân viên; bảo vệ CSDL; theo dõi nhật ký bảo mật. |

---

# CHƯƠNG 2: CƠ SỞ LÝ THUYẾT

## 2.1. Tổng quan về Hệ thống thông tin quản lý (MIS)
Hệ thống thông tin quản lý (Management Information System - MIS) là hệ thống tích hợp giữa con người, phần cứng, phần mềm, mạng truyền thông và cơ sở dữ liệu có nhiệm vụ thu thập, lưu trữ, xử lý và phân phối thông tin hỗ trợ cho việc ra quyết định, điều hành và kiểm soát trong tổ chức.

Trong môi trường kinh doanh phòng gym hiện đại:
- Tự động hóa các thao tác thủ công, giảm thiểu sai sót trong tính toán học phí, hạn sử dụng gói tập và quản lý công nợ.
- Chuẩn hóa dữ liệu tập trung, giúp ban quản lý nắm bắt tức thời bức tranh toàn cảnh về doanh thu, số lượng hội viên mới, tỷ lệ gia hạn và hiệu suất khai thác thiết bị máy tập.
- Nâng cao chất lượng phục vụ khách hàng: thủ tục check-in diễn ra trong vài giây, gia tăng mức độ hài lòng và giữ chân hội viên.

## 2.2. Phương pháp phân tích và thiết kế hệ thống có cấu trúc
Phương pháp phân tích và thiết kế hệ thống có cấu trúc (SSADM) tiếp cận bài toán theo hướng từ trên xuống (Top-Down Approach) và phân rã chức năng (Functional Decomposition).
- **Trừu tượng hóa (Abstraction):** Tách biệt giữa mô hình logic ("hệ thống làm gì" - What) và mô hình vật lý ("hệ thống làm như thế nào" - How).
- **Phân rã chức năng (Decomposition):** Chia nhỏ bài toán phức tạp thành các phân hệ chức năng nhỏ hơn, độc lập tương đối và dễ quản lý.
- **Mô hình hóa bằng sơ đồ trực quan:** Sử dụng BFD, DFD và ERD để mô tả bài toán một cách trực quan, khoa học.

## 2.3. Sơ đồ phân cấp chức năng (BFD)
Sơ đồ phân cấp chức năng (BFD) biểu diễn cấu trúc phân rã có thứ bậc các chức năng nghiệp vụ của hệ thống dưới dạng hình cây (Tree Structure). Tên chức năng bắt buộc phải là một mệnh đề động từ (Động từ + Bổ ngữ).

## 2.4. Biểu đồ luồng dữ liệu (DFD)
Biểu đồ luồng dữ liệu (DFD) biểu diễn sự chuyển dịch, biến đổi và lưu trữ của dòng thông tin trong hệ thống:
1. **Tiến trình (Process):** Thao tác biến đổi dữ liệu vào thành dữ liệu ra (hình chữ nhật bo góc).
2. **Luồng dữ liệu (Data Flow):** Mũi tên có hướng mang tên thông tin trao đổi.
3. **Kho dữ liệu (Data Store):** Nơi lưu trữ dữ liệu lâu dài (hai đường thẳng song song).
4. **Tác nhân ngoài (External Entity):** Đối tượng ngoài ranh giới hệ thống tương tác với hệ thống (hình chữ nhật nét liền).

Các cấp độ DFD gồm: Mức ngữ cảnh (Context Diagram), Mức đỉnh (Level 0) và Mức dưới đỉnh (Level 2).

## 2.5. Cơ sở dữ liệu quan hệ và Mô hình thực thể liên kết (ERD)
- **Mô hình ERD:** Xác định Thực thể (Entity), Thuộc tính (Attribute) và Mối quan hệ liên kết (1-1, 1-N, N-N).
- **Chuyển đổi sang CSDL quan hệ:** Mỗi thực thể thành 1 bảng, quan hệ 1-N đặt khóa chính phía 1 làm khóa ngoại phía N, quan hệ N-N tách thành 2 quan hệ 1-N qua bảng trung gian.

## 2.6. Lý thuyết chuẩn hóa cơ sở dữ liệu
- **1NF:** Tất cả thuộc tính đều chứa giá trị nguyên tố, không có nhóm thuộc tính lặp.
- **2NF:** Đạt 1NF và mọi thuộc tính không khóa phụ thuộc hàm đầy đủ vào khóa chính.
- **3NF:** Đạt 2NF và không có thuộc tính không khóa nào phụ thuộc bắc cầu vào khóa chính.

## 2.7. Công nghệ phần mềm và Kiến trúc hệ thống áp dụng
- **C# & Windows Forms (.NET 10.0-windows):** Tối ưu cho máy trạm quầy thu ngân, phản hồi tức thời (<10ms), tương thích hoàn hảo với máy quét mã vạch và máy in bill nhiệt 80mm.
- **Microsoft SQL Server & ADO.NET:** Sử dụng Parameterized Queries chống SQL Injection, Stored Procedures và Triggers đảm bảo toàn vẹn dữ liệu giao dịch.
- **Kiến trúc 3 lớp (3-Tier):** Tách biệt rõ ràng Presentation (UI Forms) -> Business Logic Layer (BLL) -> Data Access Layer (DAL).
- **Bảo mật đa lớp:** Mã hóa mật khẩu chuẩn PBKDF2 SHA-512 với Salt 128-bit, cơ chế chống Bruteforce Lockout (khóa sau 5 lần sai) và phân quyền vai trò người dùng (RBAC).

---

# CHƯƠNG 3: PHÂN TÍCH HỆ THỐNG

## 3.1. Mô tả chi tiết bài toán nghiệp vụ
1. **Quản lý Hội viên & Thẻ tập:** Tiếp nhận đăng ký, kiểm tra chống trùng số điện thoại, tự động sinh mã `TVxxxxxx` và cấp thẻ barcode/QR, phân hạng hội viên (Thường, Bạc, Vàng, Kim cương).
2. **Quản lý Gói tập & Đăng ký gói:** Cấu hình thời hạn, giá niêm yết, khung giờ; đăng ký gói kèm mã khuyến mãi; tự động mở form thu tiền ngay lập tức sau khi đăng ký.
3. **Vận hành Gói tập:** Bảo lưu gói (chặn chọn ngày quá khứ, tính lại ngày kết thúc), Chuyển nhượng gói (thu phí chuyển nhượng, đổi chủ hợp đồng) và Nâng cấp gói tập.
4. **Quản lý Huấn luyện viên (PT):** Hợp đồng PT kèm 1:1 theo số buổi, theo dõi buổi tập đã hoàn thành, tích lũy công hoa hồng HLV.
5. **Quản lý Lớp học nhóm (Group-X):** Mở lớp Yoga, Zumba, Boxing; giới hạn sĩ số tối đa; điểm danh học viên.
6. **Check-in quầy tự động:** Quét thẻ/nhập SĐT; kích hoạt stored procedure `sp_CheckIn` áp dụng thuật toán FIFO (ưu tiên trừ hạn gói kết thúc sớm nhất); chặn check-in phòng bảo trì.
7. **Bán hàng lẻ POS & Quản lý Kho:** Bán nước uống, thực phẩm bổ sung cho Hội viên hoặc Khách vãng lai; trigger tự động trừ tồn kho và ghi nhật ký `LichSuTonKho`.
8. **Quản lý Thiết bị & Bảo trì:** Danh mục máy móc theo phòng; lập phiếu sửa chữa (`sp_BaoTriThietBi`); nghiệm thu đưa máy về hoạt động (`sp_HoanTatBaoTri`).
9. **Hóa đơn & Thu tiền:** Mã hóa đơn `HD-YYYYMMDD-xxxx`; thanh toán nhiều đợt (Tiền mặt, Chuyển khoản QR, Thẻ POS); theo dõi công nợ `ConNo`; chặn hủy hóa đơn đã thanh toán.
10. **Báo cáo Thống kê & Quản trị:** Báo cáo doanh thu đa chiều, biểu đồ tài chính, tần suất check-in theo giờ, phân quyền tài khoản RBAC.

## 3.2. Sơ đồ phân cấp chức năng BFD toàn hệ thống

```
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
```
*Sơ đồ 3.1: Sơ đồ phân cấp chức năng BFD toàn hệ thống GymPro*

## 3.3. Bảng phân tích xác định tiến trình, tác nhân và hồ sơ dữ liệu

| STT | Hành động nghiệp vụ | Tác nhân Ngoài | Tác nhân Trong | Tiến trình tương ứng | Hồ sơ dữ liệu (Kho D) |
|:---:|:---|:---:|:---:|:---|:---|
| 1 | Đăng ký thông tin hội viên | Hội viên | Lễ tân | 1.1 Tiếp nhận hội viên | Ghi D1 (ThanhVien) |
| 2 | Cập nhật hồ sơ InBody | Hội viên | HLV / Lễ tân | 1.3 Quản lý sức khỏe | Ghi D1 (HoSoSucKhoe) |
| 3 | Đăng ký hợp đồng gói tập | Hội viên | Lễ tân | 2.2 Đăng ký gói tập | Đọc D1, D2; Ghi D3, D8 |
| 4 | Lập phiếu bảo lưu gói tập | Hội viên | Lễ tân | 2.4 Bảo lưu gói | Đọc D3; Ghi D3 (BaoLuu) |
| 5 | Chuyển nhượng gói tập | Hội viên | Lễ tân | 2.4 Chuyển nhượng gói | Đọc/Ghi D3, D8 |
| 6 | Đăng ký dịch vụ PT kèm 1:1 | Hội viên | Lễ tân / HLV | 3.2 Đăng ký PT | Đọc D1, D4; Ghi D4, D8 |
| 7 | Mở lớp học nhóm và xếp lịch | — | HLV Trưởng | 3.3 Quản lý lớp tập | Ghi D4 (LopTap) |
| 8 | Điểm danh học viên lớp/PT | Hội viên | HLV | 4.3 Điểm danh | Đọc D4; Ghi D5 (DiemDanh) |
| 9 | Quẹt thẻ check-in vào phòng | Hội viên | Lễ tân | 4.1 Check-in quầy | Đọc D1, D3, D7; Ghi D5 |
| 10 | Mua hàng lẻ tại quầy bar POS | Hội viên / Vãng lai | Bán hàng | 5.2 Bán hàng POS | Đọc/Ghi D6, D8 |
| 11 | Nhập hàng hóa từ Nhà cung cấp | Nhà cung cấp | Thủ kho | 5.3 Nhập kho hàng | Đọc/Ghi D6 |
| 12 | Lập phiếu bảo trì máy tập | Đơn vị bảo trì | Kỹ thuật viên | 6.3 Lập phiếu bảo trì | Đọc/Ghi D7 (ThietBi) |
| 13 | Nghiệm thu khôi phục máy tập | Đơn vị bảo trì | Kỹ thuật viên | 6.4 Nghiệm thu máy | Đọc/Ghi D7 |
| 14 | Thu tiền thanh toán hóa đơn | Hội viên / Khách | Thu ngân | 7.2 Thu tiền hóa đơn | Đọc D8; Ghi D8 (ThanhToan) |
| 15 | Xem báo cáo tổng hợp doanh thu | — | Ban Giám đốc | 8.1 Báo cáo doanh thu | Đọc D3, D4, D6, D8 |
| 16 | Phân quyền và bảo mật tài khoản | — | IT Admin | 8.4 Quản trị hệ thống | Đọc/Ghi D9 (TaiKhoan) |

## 3.4. Biểu đồ luồng dữ liệu (DFD các mức)

### 3.4.1. Biểu đồ luồng dữ liệu mức ngữ cảnh (Context Diagram)

```
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
```
*Sơ đồ 3.2: Biểu đồ luồng dữ liệu DFD mức ngữ cảnh (Context Diagram)*

### 3.4.2. Biểu đồ luồng dữ liệu mức đỉnh (Level 0)

```
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
```
*Sơ đồ 3.3: Biểu đồ luồng dữ liệu DFD mức đỉnh (Level 0)*

---

# CHƯƠNG 4: THIẾT KẾ HỆ THỐNG

## 4.1. Thiết kế Cơ sở dữ liệu
Cơ sở dữ liệu của Hệ thống GymPro được đặt tên là `QL_PhongGym` triển khai trên Microsoft SQL Server, bao gồm 47 bảng chuẩn hóa, hỗ trợ giao dịch ACID toàn vẹn.

### 4.1.1. Các thực thể và thuộc tính chi tiết (Data Dictionary)

#### Bảng ThanhVien (Quản lý hồ sơ hội viên)
- `MaTV`: INT IDENTITY(1,1), Khóa chính (PK)
- `MaThanhVien`: VARCHAR(20), UNIQUE, Mã hiển thị (`TV000001`) do trigger tự sinh
- `HoTen`: NVARCHAR(100), Không null
- `SoDienThoai`: VARCHAR(15), UNIQUE, Không null
- `Email`: VARCHAR(100), Cho phép null
- `NgaySinh`: DATE, Cho phép null
- `GioiTinh`: NVARCHAR(10), Kiểm tra 'Nam' / 'Nữ' / 'Khác'
- `DiaChi`: NVARCHAR(255), Cho phép null
- `MaLoaiTV`: INT, Khóa ngoại tham chiếu `LoaiThanhVien`
- `NgayDangKy`: DATETIME, Mặc định `GETDATE()`
- `DiemTichLuy`: INT, Mặc định 0
- `TrangThai`: NVARCHAR(50), Mặc định 'Hoạt động'

#### Bảng GoiTap (Danh mục gói tập)
- `MaGoi`: INT IDENTITY(1,1), Khóa chính (PK)
- `MaGoiTap`: VARCHAR(20), UNIQUE, Mã gói (`GOI000001`)
- `TenGoi`: NVARCHAR(100), Không null
- `ThoiHanNgay`: INT, Thời hạn ngày (30, 90, 365), CHECK > 0
- `GiaTien`: DECIMAL(18,2), CHECK >= 0
- `MoTa`: NVARCHAR(500)
- `GioBatDau`, `GioKetThuc`: TIME, Khung giờ tập hợp lệ
- `KichHoat`: BIT, 1: Đang bán, 0: Ngừng bán

#### Bảng DangKyGoi (Hợp đồng đăng ký gói tập)
- `MaDKG`: INT IDENTITY(1,1), Khóa chính (PK)
- `MaDangKyGoi`: VARCHAR(30), UNIQUE (`DKG-YYYYMMDD-xxxx`)
- `MaTV`: INT, FK tham chiếu `ThanhVien`
- `MaGoi`: INT, FK tham chiếu `GoiTap`
- `MaKM`: INT, FK tham chiếu `KhuyenMai` (cho phép null)
- `NgayDangKy`: DATETIME, Mặc định `GETDATE()`
- `NgayBatDau`, `NgayKetThuc`: DATE
- `GiaGoiLucDK`: DECIMAL(18,2)
- `GiamGia`: DECIMAL(18,2), Mặc định 0
- `ThanhTien`: DECIMAL(18,2)
- `TrangThai`: NVARCHAR(50), 'Hoạt động' / 'Đang bảo lưu' / 'Hết hạn' / 'Chuyển nhượng'

#### Bảng HoaDon & ThanhToan
- `MaHD`: INT IDENTITY(1,1), PK
- `SoHoaDon`: VARCHAR(30), UNIQUE (`HD-YYYYMMDD-xxxx`)
- `MaTV`: INT, FK tham chiếu `ThanhVien` (NULL nếu khách vãng lai)
- `NgayLap`: DATETIME, `GETDATE()`
- `TongTienGoc`, `TienGiamGia`, `TongThanhToan`: DECIMAL(18,2)
- `DaThanhToan`: DECIMAL(18,2), Lũy kế số tiền đã thanh toán
- `ConNo`: Cột tính toán ảo (`TongThanhToan - DaThanhToan`)
- `TrangThai`: 'Chưa thanh toán' / 'Đã thanh toán' / 'Đã hủy'

### 4.1.2. Mô hình thực thể liên kết (ERD)

```
+------------------+         1:N         +------------------+         N:1         +------------------+
|    THANH VIEN    | -------------------< |   DANG KY GOI    | >------------------- |     GOI TAP      |
| PK: MaTV         |                     | PK: MaDKG        |                      | PK: MaGoi        |
|     MaThanhVien  |                     | FK: MaTV, MaGoi  |                      |     MaGoiTap     |
+------------------+                     +------------------+                      +------------------+
         | 1                                      | 1                                       | 1
         |                                        |                                         |
         | 1:N                                    | 1:N                                     | N:M
         v                                        v                                         v
+------------------+                     +------------------+                      +------------------+
|    DANG KY PT    |                     |   BAO LUU GOI    |                      |  GOI TAP PHONG   |
| PK: MaDKPT       |                     | PK: MaBaoLuu     |                      | FK: MaGoi        |
| FK: MaTV, MaHLV  |                     | FK: MaDKG        |                      | FK: MaPhong      |
+------------------+                     +------------------+                      +------------------+
         ^                                                                                  ^
         | N:1                                                                              | 1:N
+------------------+         1:N         +------------------+         1:N         +------------------+
| HUAN LUYEN VIEN  | -------------------< |     LOP TAP      | -------------------> |    PHONG TAP     |
| PK: MaHLV        |                     | PK: MaLop        |                      | PK: MaPhong      |
|     MaHLVien     |                     | FK: MaHLV,MaPhong|                      |     TenPhong     |
+------------------+                     +------------------+                      +------------------+
                                                  | 1                                       | 1
                                                  | 1:N                                     | 1:N
                                                  v                                         v
+------------------+         1:N         +------------------+         1:N         +------------------+
|  DIEM DANH LOP   | >------------------ |   DANG KY LOP    |                     |     THIET BI     |
| PK: MaDiemDanh   |                     | PK: MaDKL        |                     | PK: MaTB         |
| FK: MaDKL        |                     | FK: MaTV, MaLop  |                     | FK: MaPhong      |
+------------------+                     +------------------+                      +------------------+
                                                                                            | 1
                                                                                            | 1:N
                                                                                            v
+------------------+         1:N         +------------------+         N:1         +------------------+
|    THANH TOAN    | >------------------ |     HOA DON      | ------------------- | BAO TRI THIET BI |
| PK: MaTT         |                     | PK: MaHD         |                     | PK: MaBaoTri     |
| FK: MaHD,MaNV    |                     | FK: MaTV         |                     | FK: MaTB         |
+------------------+                     +------------------+                      +------------------+
                                                  | 1
                                                  | 1:N
                                                  v
                                         +------------------+         N:1         +------------------+
                                         | CHI TIET HOA DON | >------------------ |     SAN PHAM     |
                                         | PK: MaCTHD       |                     | PK: MaSP         |
                                         | FK: MaHD, MaSP   |                     |     MaSanPham    |
                                         +------------------+                      +------------------+
```
*Sơ đồ 4.1: Mô hình thực thể liên kết (ERD) hệ thống Quản lý GymPro*

### 4.1.3. Lược đồ quan hệ chuẩn (Relational Schema)
1. `ThanhVien`(**MaTV**, *MaThanhVien*, HoTen, SoDienThoai, Email, NgaySinh, GioiTinh, DiaChi, MaLoaiTV*, NgayDangKy, DiemTichLuy, TrangThai)
2. `GoiTap`(**MaGoi**, *MaGoiTap*, TenGoi, ThoiHanNgay, GiaTien, MoTa, GioBatDau, GioKetThuc, KichHoat)
3. `DangKyGoi`(**MaDKG**, *MaDangKyGoi*, MaTV*, MaGoi*, MaKM*, NgayDangKy, NgayBatDau, NgayKetThuc, GiaGoiLucDK, GiamGia, ThanhTien, TrangThai)
4. `HuanLuyenVien`(**MaHLV**, *MaHLVien*, HoTen, ChuyenMon, SoDienThoai, Email, LuongCoBan, TyLeHoaHong, TrangThai)
5. `DangKyPT`(**MaDKPT**, *MaDangKyPT*, MaTV*, MaHLV*, SoBuoiDangKy, SoBuoiDaTap, NgayBatDau, NgayKetThuc, GiaTien, TrangThai)
6. `LopTap`(**MaLop**, *MaLopTap*, TenLop, MaLoaiLop*, MaHLV*, MaPhong*, SiSoToiDa, HocPhi, TrangThai)
7. `DangKyLop`(**MaDKL**, *MaDangKyLop*, MaTV*, MaLop*, NgayDangKy, ThanhTien, TrangThai)
8. `DiemDanhLop`(**MaDiemDanh**, MaDKL*, NgayHoc, CoMat, GhiChu)
9. `LichSuCheckIn`(**MaCheckIn**, *MaPhienCheckIn*, MaTV*, MaDKG*, ThoiGianVao, ThoiGianRa, MaPhong*, TrangThaiHopLe)
10. `HoaDon`(**MaHD**, *SoHoaDon*, MaTV*, NgayLap, TongTienGoc, TienGiamGia, TongThanhToan, DaThanhToan, ConNo, TrangThai)
11. `ChiTietHoaDon`(**MaCTHD**, MaHD*, LoaiDichVu, MaDichVu, SoLuong, DonGia, ThanhTien)
12. `ThanhToan`(**MaTT**, *SoGiaoDich*, MaHD*, NgayThanhToan, SoTien, HinhThucTT, MaNguoiThu*, GhiChu)
13. `SanPham`(**MaSP**, *MaSanPham*, TenSP, LoaiSP, GiaNhap, GiaBan, SoLuongTon, DonViTinh)
14. `PhieuBanHang`(**MaPBH**, *SoPhieuBanHang*, MaTV*, NgayBan, TongTien, MaNV*, GhiChu)
15. `ChiTietPhieuBanHang`(**MaCTPBH**, MaPBH*, MaSP*, SoLuong, DonGia, ThanhTien)
16. `ThietBi`(**MaTB**, *MaThietBi*, TenThietBi, MaLoaiTB*, MaPhong*, NgayMua, GiaTri, TrangThai)
17. `BaoTriThietBi`(**MaBaoTri**, *MaPhieuBaoTri*, MaTB*, NgayBaoTri, ChiPhi, DonViBaoTri, MoTaLoi, TrangThai)
18. `PhongTap`(**MaPhong**, TenPhong, SucChua, TrangThai)
19. `NhanVien`(**MaNV**, *MaNhanVien*, HoTen, ChucVu, SoDienThoai, Email, LuongCoBan, NgayVaoLam, TrangThai)
20. `TaiKhoan`(**MaTK**, TenDangNhap, MatKhauHash, Salt, MaNV*, TrangThaiKhoa, SoLanDangNhapSai)

### 4.1.4. Quá trình chuẩn hóa cơ sở dữ liệu đạt chuẩn 3NF
- **Từ UNF lên 1NF:** Tách bỏ các nhóm lặp trong hóa đơn (nhiều sản phẩm/dịch vụ mua) thành các bản ghi riêng biệt với khóa hợp thành (SoHD, MaDichVu). Đảm bảo các giá trị cột nguyên tố.
- **Từ 1NF lên 2NF:** Loại bỏ phụ thuộc hàm một phần: trong ChiTietHoaDon(SoHD, MaSP, SoLuong, TenSP, DonGiaSP), các trường `TenSP`, `DonGiaSP` chỉ phụ thuộc vào `MaSP` chứ không phụ thuộc vào `SoHD`. Tách riêng bảng `SanPham(MaSP, TenSP, DonGiaSP)`.
- **Từ 2NF lên 3NF:** Loại bỏ phụ thuộc hàm bắc cầu: trong HoaDon(SoHD, NgayLap, MaTV, HoTenTV, SoDienThoai), tồn tại `SoHD -> MaTV` và `MaTV -> HoTenTV, SoDienThoai`. Tách thông tin hội viên sang bảng `ThanhVien(MaTV, HoTen, SoDienThoai)`, trong bảng `HoaDon` chỉ lưu khóa ngoại `MaTV`.
- **Kết luận:** Toàn bộ 47 bảng trong CSDL `QL_PhongGym` đều đạt chuẩn 3NF và BCNF.

## 4.2. Xây dựng chương trình và Thiết kế giao diện
- **Giao diện chính:** `FrmLogin` (đăng nhập kiểm soát lockout 5 lần sai, PBKDF2), `Form1` (navigation sidebar phân quyền theo vai trò), `FrmTongQuan` (dashboard hiển thị chỉ số KPIs, doanh thu ngày/tháng, hội viên check-in, máy bảo trì).
- **Giao diện cập nhật dữ liệu:** `FrmThanhVien` (quản lý hội viên, cấp mã `TVxxxxxx`), `FrmGoiTap` (danh mục gói tập), `FrmNhanVien`, `FrmHLV`, `FrmSanPham` (danh mục hàng hóa), `FrmThietBi` (danh mục máy tập).
- **Giao diện xử lý dữ liệu:**
  - `FrmDangKyGoi`: Đăng ký gói tập, tính khuyến mãi, tự động mở màn hình thu tiền.
  - `FrmVanHanhGoi`: Bảo lưu gói (chặn ngày quá khứ), Chuyển nhượng gói, Nâng cấp gói.
  - `FrmDangKyPT` & `FrmDiemDanh`: Đăng ký tập kèm HLV 1:1, điểm danh lớp học.
  - `FrmBanHang`: Bán lẻ POS, hỗ trợ Khách vãng lai, trừ kho tự động qua trigger.
  - `FrmBaoTri`: Lập phiếu bảo trì máy (`sp_BaoTriThietBi`) và nghiệm thu phục hồi máy (`sp_HoanTatBaoTri`).
  - `FrmHoaDon` & `FrmThanhToan`: Quản lý hóa đơn, thanh toán nhiều đợt, công nợ `ConNo`, chặn hủy hóa đơn đã thanh toán.
  - `FrmThongKe`: Báo cáo doanh thu đa chiều, biểu đồ cột tài chính, thống kê mật độ check-in theo giờ.

---

# KẾT LUẬN VÀ KIẾN NGHỊ

### 1. Kết quả đạt được của đề tài
- Nắm bắt và mô hình hóa trọn vẹn quy trình nghiệp vụ thực tế của một trung tâm thể dục thể hình hiện đại.
- Xây dựng hoàn chỉnh Sơ đồ phân cấp chức năng BFD (8 phân hệ, 28 chức năng), Biểu đồ luồng dữ liệu DFD các mức và Bảng phân tích tiến trình - tác nhân - kho dữ liệu.
- Thiết kế mô hình ERD và CSDL quan hệ SQL Server gồm 47 bảng đạt chuẩn 3NF/BCNF, 20 trigger và 4 stored procedure nghiệp vụ lõi.
- Hiện thực hóa ứng dụng Windows Forms .NET 10.0 với 21 màn hình chuyên nghiệp, mượt mà, đạt 100% tiêu chuẩn kiểm thử hồi quy.
- Triển khai bảo mật cấp cao: mã hóa PBKDF2 SHA-512, chống bruteforce lockout và phân quyền vai trò người dùng (RBAC).

### 2. Đánh giá ưu điểm và hạn chế
- **Ưu điểm:** Bám sát nghiệp vụ thực tế (thuật toán check-in FIFO, chặn phòng bảo trì, chặn bảo lưu quá khứ, tự động mở thu tiền); kiểm soát tài chính công nợ chặt chẽ; hiệu năng phản hồi tức thời dưới 10ms.
- **Hạn chế:** Chưa có Mobile App dành riêng cho hội viên; kiểm soát cửa xoay ra vào và nhận diện FaceID hiện đang giả lập qua cổng quét mã thẻ.

### 3. Hướng phát triển và kiến nghị mở rộng
- Phát triển ứng dụng di động (Flutter / React Native) cho hội viên tự theo dõi gói tập, đặt lịch PT và xem lịch lớp nhóm.
- Tích hợp cổng thanh toán trực tuyến tự động (VietQR tĩnh, VNPAY, MoMo) có webhook tự động gạch nợ.
- Kết nối phần cứng cổng kiểm soát phân làn tự động (Flap Barrier) và camera AI nhận diện khuôn mặt tại quầy lễ tân.

---

# TÀI LIỆU THAM KHẢO

[1]. **Nguyễn Văn Ba**, *Phân tích và thiết kế hệ thống thông tin*, NXB Đại học Quốc gia Hà Nội, 2006.  
[2]. **Đỗ Trung Tuấn**, *Cơ sở dữ liệu*, NXB Giáo dục, 2005.  
[3]. **Đồng Thị Bích Thủy**, *Phân tích và thiết kế hệ thống thông tin hướng cấu trúc*, NXB Đại học Quốc gia TP. Hồ Chí Minh, 2008.  
[4]. **Jeffrey L. Whitten, Lonnie D. Bentley**, *Systems Analysis and Design Methods (7th Edition)*, McGraw-Hill Education, 2007.  
[5]. **Abraham Silberschatz, Henry F. Korth, S. Sudarshan**, *Database System Concepts (7th Edition)*, McGraw-Hill Higher Education, 2019.  
[6]. **Christian Nagel**, *Professional C# and .NET 2021 Edition*, Wrox / John Wiley & Sons, 2021.  
[7]. **Benjamin Rosenzweig, Elena Rakhimov**, *Oracle SQL By Example (5th Edition)*, Pearson Education, 2015.  
[8]. **Microsoft Corporation**, *SQL Server Documentation & ADO.NET Architecture Guide*, Microsoft Learn, 2024.  
[9]. **OWASP Foundation**, *Password Storage Cheat Sheet & PBKDF2 Standards*, OWASP Security Guidelines, 2023.  
"""
    with open(MD_OUT, "w", encoding="utf-8") as f:
        f.write(md_content)
    print(f"Xuất file Markdown thành công tại: {MD_OUT}")

if __name__ == "__main__":
    create_report_docx()
    create_report_markdown()
    print("HOÀN TẤT TẤT CẢ CÁC BÁO CÁO!")

namespace He_Thong_GYM.Forms;

partial class FrmDangKyGoi
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panel = new Panel();
        title = new Label();
        group = new GroupBox();
        table = new TableLayoutPanel();
        l1 = new Label(); cboThanhVien = new ComboBox();
        l2 = new Label(); cboGoiTap = new ComboBox();
        l3 = new Label(); cboKhuyenMai = new ComboBox();
        l4 = new Label(); dtpNgayBatDau = new DateTimePicker();
        l5 = new Label(); dtpNgayKetThuc = new DateTimePicker();
        l6 = new Label(); lblGia = new Label();
        btnDangKy = new Button();
        dgvLichSu = new DataGridView();

        panel.SuspendLayout();
        group.SuspendLayout();
        table.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
        SuspendLayout();

        panel.BackColor = Color.White;
        panel.Controls.Add(title);
        panel.Dock = DockStyle.Top;
        panel.Height = 82;

        title.AutoSize = true;
        title.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        title.ForeColor = Color.FromArgb(15, 23, 42);
        title.Location = new Point(24, 22);
        title.Text = "ĐĂNG KÝ GÓI TẬP";

        group.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        group.BackColor = Color.White;
        group.Controls.Add(table);
        group.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        group.ForeColor = Color.FromArgb(30, 41, 59);
        group.Location = new Point(24, 100);
        group.Padding = new Padding(16);
        group.Size = new Size(1152, 170);
        group.Text = "Thông tin đăng ký";

        table.ColumnCount = 4;
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        table.RowCount = 3;
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        table.Dock = DockStyle.Fill;
        table.Font = new Font("Segoe UI", 9F);
        table.Controls.Add(l1, 0, 0); table.Controls.Add(cboThanhVien, 1, 0);
        table.Controls.Add(l2, 2, 0); table.Controls.Add(cboGoiTap, 3, 0);
        table.Controls.Add(l3, 0, 1); table.Controls.Add(cboKhuyenMai, 1, 1);
        table.Controls.Add(l4, 2, 1); table.Controls.Add(dtpNgayBatDau, 3, 1);
        table.Controls.Add(l5, 0, 2); table.Controls.Add(dtpNgayKetThuc, 1, 2);
        table.Controls.Add(l6, 2, 2); table.Controls.Add(lblGia, 3, 2);

        // l1 - Thành viên
        l1.Dock = DockStyle.Fill;
        l1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l1.Text = "Thành viên:";
        l1.TextAlign = ContentAlignment.MiddleLeft;
        // l2 - Gói tập
        l2.Dock = DockStyle.Fill;
        l2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l2.Text = "Gói tập:";
        l2.TextAlign = ContentAlignment.MiddleLeft;
        // l3 - Khuyến mãi
        l3.Dock = DockStyle.Fill;
        l3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l3.Text = "Khuyến mãi:";
        l3.TextAlign = ContentAlignment.MiddleLeft;
        // l4 - Ngày bắt đầu
        l4.Dock = DockStyle.Fill;
        l4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l4.Text = "Ngày bắt đầu:";
        l4.TextAlign = ContentAlignment.MiddleLeft;
        // l5 - Ngày kết thúc
        l5.Dock = DockStyle.Fill;
        l5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l5.Text = "Ngày kết thúc:";
        l5.TextAlign = ContentAlignment.MiddleLeft;
        // l6 - Giá sau giảm
        l6.Dock = DockStyle.Fill;
        l6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        l6.Text = "Giá sau giảm:";
        l6.TextAlign = ContentAlignment.MiddleLeft;
        // cboThanhVien
        cboThanhVien.Dock = DockStyle.Fill;
        cboThanhVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThanhVien.Margin = new Padding(3, 6, 12, 6);
        // cboGoiTap
        cboGoiTap.Dock = DockStyle.Fill;
        cboGoiTap.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGoiTap.Margin = new Padding(3, 6, 12, 6);
        // cboKhuyenMai
        cboKhuyenMai.Dock = DockStyle.Fill;
        cboKhuyenMai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhuyenMai.Margin = new Padding(3, 6, 12, 6);
        // dtpNgayBatDau
        dtpNgayBatDau.Dock = DockStyle.Fill;
        dtpNgayBatDau.Margin = new Padding(3, 6, 12, 6);
        dtpNgayBatDau.Format = DateTimePickerFormat.Short;
        // dtpNgayKetThuc
        dtpNgayKetThuc.Dock = DockStyle.Fill;
        dtpNgayKetThuc.Margin = new Padding(3, 6, 12, 6);
        dtpNgayKetThuc.Format = DateTimePickerFormat.Short;
        dtpNgayKetThuc.Enabled = false;
        lblGia.Dock = DockStyle.Fill;
        lblGia.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblGia.ForeColor = Color.FromArgb(5, 150, 105);
        lblGia.TextAlign = ContentAlignment.MiddleLeft;

        btnDangKy.BackColor = Color.FromArgb(37, 99, 235);
        btnDangKy.Cursor = Cursors.Hand;
        btnDangKy.FlatAppearance.BorderSize = 0;
        btnDangKy.FlatStyle = FlatStyle.Flat;
        btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnDangKy.ForeColor = Color.White;
        btnDangKy.Location = new Point(24, 284);
        btnDangKy.Size = new Size(160, 38);
        btnDangKy.Text = "Đăng ký gói";
        btnDangKy.UseVisualStyleBackColor = false;
        btnDangKy.Click += btnDangKy_Click;

        dgvLichSu.AllowUserToAddRows = false;
        dgvLichSu.AllowUserToDeleteRows = false;
        dgvLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLichSu.AutoGenerateColumns = true;
        dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLichSu.BackgroundColor = Color.White;
        dgvLichSu.BorderStyle = BorderStyle.None;
        dgvLichSu.ColumnHeadersHeight = 38;
        dgvLichSu.Location = new Point(24, 336);
        dgvLichSu.ReadOnly = true;
        dgvLichSu.RowHeadersVisible = false;
        dgvLichSu.RowTemplate.Height = 32;
        dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLichSu.Size = new Size(1152, 363);

        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(dgvLichSu);
        Controls.Add(btnDangKy);
        Controls.Add(group);
        Controls.Add(panel);
        Font = new Font("Segoe UI", 9F);
        AutoScroll = true;
        MinimumSize = new Size(850, 480);
        Name = "FrmDangKyGoi";
        Text = "Đăng ký gói tập";
        Load += FrmDangKyGoi_Load;
        cboGoiTap.SelectedIndexChanged += GiaThayDoi;
        cboKhuyenMai.SelectedIndexChanged += GiaThayDoi;
        dtpNgayBatDau.ValueChanged += GiaThayDoi;

        panel.ResumeLayout(false);
        panel.PerformLayout();
        group.ResumeLayout(false);
        table.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
        ResumeLayout(false);
    }


    private Panel panel;
    private Label title, l1, l2, l3, l4, l5, l6, lblGia;
    private GroupBox group;
    private TableLayoutPanel table;
    private ComboBox cboThanhVien, cboGoiTap, cboKhuyenMai;
    private DateTimePicker dtpNgayBatDau, dtpNgayKetThuc;
    private Button btnDangKy;
    private DataGridView dgvLichSu;
}

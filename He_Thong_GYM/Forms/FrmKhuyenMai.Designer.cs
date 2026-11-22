namespace He_Thong_GYM.Forms;

partial class FrmKhuyenMai
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelTieuDe = new Panel();
        lblMoTa = new Label();
        lblTieuDe = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTen = new Label();
        txtTen = new TextBox();
        lblGiam = new Label();
        nudGiam = new NumericUpDown();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnLamMoi = new Button();
        lblThongKe = new Label();
        dgvKhuyenMai = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudGiam).BeginInit();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).BeginInit();
        SuspendLayout();
        // 
        // panelTieuDe
        // 
        panelTieuDe.BackColor = Color.White;
        panelTieuDe.Controls.Add(lblMoTa);
        panelTieuDe.Controls.Add(lblTieuDe);
        panelTieuDe.Dock = DockStyle.Top;
        panelTieuDe.Location = new Point(0, 0);
        panelTieuDe.Name = "panelTieuDe";
        panelTieuDe.Padding = new Padding(24, 16, 24, 16);
        panelTieuDe.Size = new Size(1000, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(339, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Các chương trình ưu đãi, giảm giá gói tập và thời hạn áp dụng.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(251, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ KHUYẾN MÃI";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThongTin.ForeColor = Color.FromArgb(30, 41, 59);
        groupThongTin.Location = new Point(24, 96);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(16, 12, 16, 12);
        groupThongTin.Size = new Size(952, 105);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Bộ lọc chương trình khuyến mãi";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTen, 0, 0);
        tableThongTin.Controls.Add(txtTen, 1, 0);
        tableThongTin.Controls.Add(lblGiam, 2, 0);
        tableThongTin.Controls.Add(nudGiam, 3, 0);
        tableThongTin.Controls.Add(lblTrangThai, 0, 1);
        tableThongTin.Controls.Add(cboTrangThai, 1, 1);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 30);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.Size = new Size(920, 63);
        tableThongTin.TabIndex = 0;
        // 
        // lblTen
        // 
        lblTen.AutoSize = true;
        lblTen.Dock = DockStyle.Fill;
        lblTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTen.Location = new Point(3, 0);
        lblTen.Name = "lblTen";
        lblTen.Size = new Size(124, 31);
        lblTen.TabIndex = 0;
        lblTen.Text = "Tên khuyến mãi:";
        lblTen.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTen
        // 
        txtTen.Dock = DockStyle.Fill;
        txtTen.Location = new Point(133, 3);
        txtTen.Name = "txtTen";
        txtTen.PlaceholderText = "Nhập mã khuyến mãi hoặc tên chương trình...";
        txtTen.Size = new Size(324, 23);
        txtTen.TabIndex = 1;
        txtTen.TextChanged += BoLocThayDoi;
        // 
        // lblGiam
        // 
        lblGiam.AutoSize = true;
        lblGiam.Dock = DockStyle.Fill;
        lblGiam.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGiam.Location = new Point(463, 0);
        lblGiam.Name = "lblGiam";
        lblGiam.Size = new Size(124, 31);
        lblGiam.TabIndex = 2;
        lblGiam.Text = "Giảm tối thiểu (%):";
        lblGiam.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudGiam
        // 
        nudGiam.Dock = DockStyle.Fill;
        nudGiam.Location = new Point(593, 3);
        nudGiam.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        nudGiam.Name = "nudGiam";
        nudGiam.Size = new Size(324, 23);
        nudGiam.TabIndex = 3;
        nudGiam.ValueChanged += BoLocThayDoi;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.Location = new Point(3, 31);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(124, 32);
        lblTrangThai.TabIndex = 4;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "-- Tất cả trạng thái --", "Hoạt động", "Hết hạn" });
        cboTrangThai.Location = new Point(133, 34);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(324, 23);
        cboTrangThai.TabIndex = 5;
        cboTrangThai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnSua);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Controls.Add(lblThongKe);
        flowThaoTac.Location = new Point(24, 212);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(952, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(37, 99, 235);
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(0, 0);
        btnThem.Margin = new Padding(0, 0, 8, 0);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(130, 36);
        btnThem.TabIndex = 0;
        btnThem.Text = "Tạo khuyến mãi";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(16, 185, 129);
        btnSua.Cursor = Cursors.Hand;
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(138, 0);
        btnSua.Margin = new Padding(0, 0, 8, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 36);
        btnSua.TabIndex = 1;
        btnSua.Text = "Cập nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(246, 0);
        btnLamMoi.Margin = new Padding(0, 0, 16, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(120, 36);
        btnLamMoi.TabIndex = 2;
        btnLamMoi.Text = "Làm mới / Bỏ lọc";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblThongKe
        // 
        lblThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKe.ForeColor = Color.FromArgb(71, 85, 105);
        lblThongKe.Location = new Point(382, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Padding = new Padding(0, 8, 0, 0);
        lblThongKe.Size = new Size(350, 36);
        lblThongKe.TabIndex = 3;
        lblThongKe.Text = "Đang tải dữ liệu...";
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvKhuyenMai
        // 
        dgvKhuyenMai.AllowUserToAddRows = false;
        dgvKhuyenMai.AllowUserToDeleteRows = false;
        dgvKhuyenMai.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvKhuyenMai.BackgroundColor = Color.White;
        dgvKhuyenMai.BorderStyle = BorderStyle.None;
        dgvKhuyenMai.ColumnHeadersHeight = 36;
        dgvKhuyenMai.Location = new Point(24, 260);
        dgvKhuyenMai.MultiSelect = false;
        dgvKhuyenMai.Name = "dgvKhuyenMai";
        dgvKhuyenMai.ReadOnly = true;
        dgvKhuyenMai.RowHeadersVisible = false;
        dgvKhuyenMai.RowTemplate.Height = 32;
        dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKhuyenMai.Size = new Size(952, 435);
        dgvKhuyenMai.TabIndex = 3;
        dgvKhuyenMai.SelectionChanged += dgvKhuyenMai_SelectionChanged;
        // 
        // FrmKhuyenMai
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 710);
        Controls.Add(dgvKhuyenMai);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmKhuyenMai";
        Text = "Quản lý khuyến mãi";
        Load += FrmKhuyenMai_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudGiam).EndInit();
        flowThaoTac.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblTen;
    private TextBox txtTen;
    private Label lblGiam;
    private NumericUpDown nudGiam;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnSua;
    private Button btnLamMoi;
    private Label lblThongKe;
    private DataGridView dgvKhuyenMai;
}

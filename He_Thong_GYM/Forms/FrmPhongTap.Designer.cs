namespace He_Thong_GYM.Forms;

partial class FrmPhongTap
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
        panelThaoTac = new Panel();
        lblThongKe = new Label();
        btnLamMoi = new Button();
        tableThongTin = new TableLayoutPanel();
        lblTen = new Label();
        txtTen = new TextBox();
        lblLoai = new Label();
        cboLoai = new ComboBox();
        lblSucChua = new Label();
        nudSucChua = new NumericUpDown();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        dgv = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        panelThaoTac.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSucChua).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
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
        lblMoTa.Size = new Size(303, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Danh mục phòng tập, sức chứa và trạng thái hoạt động.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(242, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ PHÒNG TẬP";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(panelThaoTac);
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThongTin.ForeColor = Color.FromArgb(30, 41, 59);
        groupThongTin.Location = new Point(24, 96);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(16, 12, 16, 12);
        groupThongTin.Size = new Size(952, 134);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Bộ lọc & Tra cứu phòng tập";
        // 
        // panelThaoTac
        // 
        panelThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelThaoTac.Controls.Add(lblThongKe);
        panelThaoTac.Controls.Add(btnLamMoi);
        panelThaoTac.Location = new Point(16, 90);
        panelThaoTac.Name = "panelThaoTac";
        panelThaoTac.Size = new Size(920, 34);
        panelThaoTac.TabIndex = 1;
        // 
        // lblThongKe
        // 
        lblThongKe.AutoSize = true;
        lblThongKe.Dock = DockStyle.Left;
        lblThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKe.ForeColor = Color.FromArgb(71, 85, 105);
        lblThongKe.Location = new Point(0, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Padding = new Padding(0, 8, 0, 0);
        lblThongKe.Size = new Size(98, 23);
        lblThongKe.TabIndex = 0;
        lblThongKe.Text = "Đang tải dữ liệu...";
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.Dock = DockStyle.Right;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(780, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(140, 34);
        btnLamMoi.TabIndex = 1;
        btnLamMoi.Text = "Làm mới / Bỏ lọc";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // tableThongTin
        // 
        tableThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTen, 0, 0);
        tableThongTin.Controls.Add(txtTen, 1, 0);
        tableThongTin.Controls.Add(lblLoai, 2, 0);
        tableThongTin.Controls.Add(cboLoai, 3, 0);
        tableThongTin.Controls.Add(lblSucChua, 0, 1);
        tableThongTin.Controls.Add(nudSucChua, 1, 1);
        tableThongTin.Controls.Add(lblTrangThai, 2, 1);
        tableThongTin.Controls.Add(cboTrangThai, 3, 1);
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 26);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.Size = new Size(920, 58);
        tableThongTin.TabIndex = 0;
        // 
        // lblTen
        // 
        lblTen.AutoSize = true;
        lblTen.Dock = DockStyle.Fill;
        lblTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTen.Location = new Point(3, 0);
        lblTen.Name = "lblTen";
        lblTen.Size = new Size(104, 29);
        lblTen.TabIndex = 0;
        lblTen.Text = "Tên phòng:";
        lblTen.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTen
        // 
        txtTen.Dock = DockStyle.Fill;
        txtTen.Location = new Point(113, 3);
        txtTen.Name = "txtTen";
        txtTen.PlaceholderText = "Nhập tên hoặc mã phòng...";
        txtTen.Size = new Size(344, 23);
        txtTen.TabIndex = 1;
        txtTen.TextChanged += BoLocThayDoi;
        // 
        // lblLoai
        // 
        lblLoai.AutoSize = true;
        lblLoai.Dock = DockStyle.Fill;
        lblLoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLoai.Location = new Point(463, 0);
        lblLoai.Name = "lblLoai";
        lblLoai.Size = new Size(104, 29);
        lblLoai.TabIndex = 2;
        lblLoai.Text = "Loại phòng:";
        lblLoai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboLoai
        // 
        cboLoai.Dock = DockStyle.Fill;
        cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoai.FormattingEnabled = true;
        cboLoai.Items.AddRange(new object[] { "-- Tất cả loại --", "Gym thường", "Yoga", "Boxing", "Cardio", "VIP" });
        cboLoai.Location = new Point(573, 3);
        cboLoai.Name = "cboLoai";
        cboLoai.Size = new Size(344, 23);
        cboLoai.TabIndex = 3;
        cboLoai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // lblSucChua
        // 
        lblSucChua.AutoSize = true;
        lblSucChua.Dock = DockStyle.Fill;
        lblSucChua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSucChua.Location = new Point(3, 29);
        lblSucChua.Name = "lblSucChua";
        lblSucChua.Size = new Size(104, 29);
        lblSucChua.TabIndex = 4;
        lblSucChua.Text = "Sức chứa tối đa:";
        lblSucChua.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSucChua
        // 
        nudSucChua.Dock = DockStyle.Fill;
        nudSucChua.Location = new Point(113, 32);
        nudSucChua.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudSucChua.Name = "nudSucChua";
        nudSucChua.Size = new Size(344, 23);
        nudSucChua.TabIndex = 5;
        nudSucChua.ValueChanged += BoLocThayDoi;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.Location = new Point(463, 29);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(104, 29);
        lblTrangThai.TabIndex = 6;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "-- Tất cả trạng thái --", "Hoạt động", "Đang bảo trì", "Tạm đóng" });
        cboTrangThai.Location = new Point(573, 32);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(344, 23);
        cboTrangThai.TabIndex = 7;
        cboTrangThai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // dgv
        // 
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.None;
        dgv.ColumnHeadersHeight = 36;
        dgv.Location = new Point(24, 242);
        dgv.MultiSelect = false;
        dgv.Name = "dgv";
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.RowTemplate.Height = 32;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.Size = new Size(952, 423);
        dgv.TabIndex = 2;
        // 
        // FrmPhongTap
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 690);
        Controls.Add(dgv);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmPhongTap";
        Text = "Quản lý phòng tập";
        Load += FrmPhongTap_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        panelThaoTac.ResumeLayout(false);
        panelThaoTac.PerformLayout();
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSucChua).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private Panel panelThaoTac;
    private Label lblThongKe;
    private Button btnLamMoi;
    private TableLayoutPanel tableThongTin;
    private Label lblTen;
    private TextBox txtTen;
    private Label lblLoai;
    private ComboBox cboLoai;
    private Label lblSucChua;
    private NumericUpDown nudSucChua;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private DataGridView dgv;
}

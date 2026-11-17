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
        lblTu = new Label();
        dtpTu = new DateTimePicker();
        lblDen = new Label();
        dtpDen = new DateTimePicker();
        flowThaoTac = new FlowLayoutPanel();
        btnLamMoi = new Button();
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
        groupThongTin.Text = "Thông tin khuyến mãi";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTen, 0, 0);
        tableThongTin.Controls.Add(txtTen, 1, 0);
        tableThongTin.Controls.Add(lblGiam, 2, 0);
        tableThongTin.Controls.Add(nudGiam, 3, 0);
        tableThongTin.Controls.Add(lblTu, 0, 1);
        tableThongTin.Controls.Add(dtpTu, 1, 1);
        tableThongTin.Controls.Add(lblDen, 2, 1);
        tableThongTin.Controls.Add(dtpDen, 3, 1);
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
        txtTen.PlaceholderText = "Nhập tên chương trình...";
        txtTen.Size = new Size(329, 23);
        txtTen.TabIndex = 1;
        // 
        // lblGiam
        // 
        lblGiam.AutoSize = true;
        lblGiam.Dock = DockStyle.Fill;
        lblGiam.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGiam.Location = new Point(468, 0);
        lblGiam.Name = "lblGiam";
        lblGiam.Size = new Size(114, 31);
        lblGiam.TabIndex = 2;
        lblGiam.Text = "Giảm giá (%):";
        lblGiam.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudGiam
        // 
        nudGiam.Dock = DockStyle.Fill;
        nudGiam.Location = new Point(588, 3);
        nudGiam.Name = "nudGiam";
        nudGiam.Size = new Size(329, 23);
        nudGiam.TabIndex = 3;
        // 
        // lblTu
        // 
        lblTu.AutoSize = true;
        lblTu.Dock = DockStyle.Fill;
        lblTu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTu.Location = new Point(3, 31);
        lblTu.Name = "lblTu";
        lblTu.Size = new Size(124, 32);
        lblTu.TabIndex = 4;
        lblTu.Text = "Từ ngày:";
        lblTu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpTu
        // 
        dtpTu.Dock = DockStyle.Fill;
        dtpTu.Format = DateTimePickerFormat.Short;
        dtpTu.Location = new Point(133, 34);
        dtpTu.Name = "dtpTu";
        dtpTu.Size = new Size(329, 23);
        dtpTu.TabIndex = 5;
        // 
        // lblDen
        // 
        lblDen.AutoSize = true;
        lblDen.Dock = DockStyle.Fill;
        lblDen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDen.Location = new Point(468, 31);
        lblDen.Name = "lblDen";
        lblDen.Size = new Size(114, 32);
        lblDen.TabIndex = 6;
        lblDen.Text = "Đến ngày:";
        lblDen.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpDen
        // 
        dtpDen.Dock = DockStyle.Fill;
        dtpDen.Format = DateTimePickerFormat.Short;
        dtpDen.Location = new Point(588, 34);
        dtpDen.Name = "dtpDen";
        dtpDen.Size = new Size(329, 23);
        dtpDen.TabIndex = 7;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Location = new Point(24, 212);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(952, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(0, 0);
        btnLamMoi.Margin = new Padding(0, 0, 8, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(120, 36);
        btnLamMoi.TabIndex = 0;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
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
    private Label lblTu;
    private DateTimePicker dtpTu;
    private Label lblDen;
    private DateTimePicker dtpDen;
    private FlowLayoutPanel flowThaoTac;
    private Button btnLamMoi;
    private DataGridView dgvKhuyenMai;
}

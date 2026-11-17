namespace He_Thong_GYM.Forms;

partial class FrmThietBi
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
        lblTieuDe = new Label();
        lblMoTa = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTen = new Label();
        txtTen = new TextBox();
        lblLoai = new Label();
        cboLoai = new ComboBox();
        lblPhong = new Label();
        cboPhong = new ComboBox();
        lblTinhTrang = new Label();
        cboTinhTrang = new ComboBox();
        dgv = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
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
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(230, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ THIẾT BỊ";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(320, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Danh mục thiết bị phòng gym, vị trí đặt và tình trạng sử dụng.";
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
        groupThongTin.Size = new Size(952, 90);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin thiết bị";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTen, 0, 0);
        tableThongTin.Controls.Add(txtTen, 1, 0);
        tableThongTin.Controls.Add(lblLoai, 2, 0);
        tableThongTin.Controls.Add(cboLoai, 3, 0);
        tableThongTin.Controls.Add(lblPhong, 0, 1);
        tableThongTin.Controls.Add(cboPhong, 1, 1);
        tableThongTin.Controls.Add(lblTinhTrang, 2, 1);
        tableThongTin.Controls.Add(cboTinhTrang, 3, 1);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 30);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.Size = new Size(920, 52);
        tableThongTin.TabIndex = 0;
        // 
        // lblTen
        // 
        lblTen.AutoSize = true;
        lblTen.Dock = DockStyle.Fill;
        lblTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTen.Location = new Point(3, 0);
        lblTen.Name = "lblTen";
        lblTen.Size = new Size(104, 26);
        lblTen.TabIndex = 0;
        lblTen.Text = "Tên thiết bị:";
        lblTen.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTen
        // 
        txtTen.Dock = DockStyle.Fill;
        txtTen.Location = new Point(113, 3);
        txtTen.Name = "txtTen";
        txtTen.PlaceholderText = "Nhập tên thiết bị...";
        txtTen.Size = new Size(344, 23);
        txtTen.TabIndex = 1;
        // 
        // lblLoai
        // 
        lblLoai.AutoSize = true;
        lblLoai.Dock = DockStyle.Fill;
        lblLoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLoai.Location = new Point(463, 0);
        lblLoai.Name = "lblLoai";
        lblLoai.Size = new Size(104, 26);
        lblLoai.TabIndex = 2;
        lblLoai.Text = "Loại thiết bị:";
        lblLoai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboLoai
        // 
        cboLoai.Dock = DockStyle.Fill;
        cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoai.FormattingEnabled = true;
        cboLoai.Items.AddRange(new object[] { "Máy chạy bộ", "Xe đạp tập", "Giàn tạ đa năng", "Ghế tập ngực", "Tạ đơn", "Máy kéo xô" });
        cboLoai.Location = new Point(573, 3);
        cboLoai.Name = "cboLoai";
        cboLoai.Size = new Size(344, 23);
        cboLoai.TabIndex = 3;
        // 
        // lblPhong
        // 
        lblPhong.AutoSize = true;
        lblPhong.Dock = DockStyle.Fill;
        lblPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPhong.Location = new Point(3, 26);
        lblPhong.Name = "lblPhong";
        lblPhong.Size = new Size(104, 26);
        lblPhong.TabIndex = 4;
        lblPhong.Text = "Phòng tập:";
        lblPhong.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboPhong
        // 
        cboPhong.Dock = DockStyle.Fill;
        cboPhong.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPhong.FormattingEnabled = true;
        cboPhong.Location = new Point(113, 29);
        cboPhong.Name = "cboPhong";
        cboPhong.Size = new Size(344, 23);
        cboPhong.TabIndex = 5;
        // 
        // lblTinhTrang
        // 
        lblTinhTrang.AutoSize = true;
        lblTinhTrang.Dock = DockStyle.Fill;
        lblTinhTrang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTinhTrang.Location = new Point(463, 26);
        lblTinhTrang.Name = "lblTinhTrang";
        lblTinhTrang.Size = new Size(104, 26);
        lblTinhTrang.TabIndex = 6;
        lblTinhTrang.Text = "Tình trạng:";
        lblTinhTrang.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTinhTrang
        // 
        cboTinhTrang.Dock = DockStyle.Fill;
        cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTinhTrang.FormattingEnabled = true;
        cboTinhTrang.Items.AddRange(new object[] { "Hoạt động", "Bảo trì", "Hư hỏng", "Thanh lý" });
        cboTinhTrang.Location = new Point(573, 29);
        cboTinhTrang.Name = "cboTinhTrang";
        cboTinhTrang.Size = new Size(344, 23);
        cboTinhTrang.TabIndex = 7;
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
        dgv.Location = new Point(24, 200);
        dgv.MultiSelect = false;
        dgv.Name = "dgv";
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.RowTemplate.Height = 32;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.Size = new Size(952, 465);
        dgv.TabIndex = 2;
        // 
        // FrmThietBi
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
        Name = "FrmThietBi";
        Text = "Quản lý thiết bị";
        Load += FrmThietBi_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblTen;
    private TextBox txtTen;
    private Label lblLoai;
    private ComboBox cboLoai;
    private Label lblPhong;
    private ComboBox cboPhong;
    private Label lblTinhTrang;
    private ComboBox cboTinhTrang;
    private DataGridView dgv;
}

namespace He_Thong_GYM.Forms;

partial class FrmBaoTri
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
        lblThietBi = new Label();
        cboThietBi = new ComboBox();
        lblNhanVien = new Label();
        cboNhanVien = new ComboBox();
        lblNoiDung = new Label();
        txtNoiDung = new TextBox();
        lblChiPhi = new Label();
        nudChiPhi = new NumericUpDown();
        flowThaoTac = new FlowLayoutPanel();
        btnTao = new Button();
        dgv = new DataGridView();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudChiPhi).BeginInit();
        flowThaoTac.SuspendLayout();
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
        lblMoTa.Size = new Size(317, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Lập phiếu bảo dưỡng, sửa chữa thiết bị và theo dõi chi phí.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(194, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BẢO TRÌ THIẾT BỊ";
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
        groupThongTin.Text = "Thông tin phiếu bảo trì";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblThietBi, 0, 0);
        tableThongTin.Controls.Add(cboThietBi, 1, 0);
        tableThongTin.Controls.Add(lblNhanVien, 2, 0);
        tableThongTin.Controls.Add(cboNhanVien, 3, 0);
        tableThongTin.Controls.Add(lblNoiDung, 0, 1);
        tableThongTin.Controls.Add(txtNoiDung, 1, 1);
        tableThongTin.Controls.Add(lblChiPhi, 2, 1);
        tableThongTin.Controls.Add(nudChiPhi, 3, 1);
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
        // lblThietBi
        // 
        lblThietBi.AutoSize = true;
        lblThietBi.Dock = DockStyle.Fill;
        lblThietBi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblThietBi.Location = new Point(3, 0);
        lblThietBi.Name = "lblThietBi";
        lblThietBi.Size = new Size(104, 31);
        lblThietBi.TabIndex = 0;
        lblThietBi.Text = "Thiết bị:";
        lblThietBi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboThietBi
        // 
        cboThietBi.Dock = DockStyle.Fill;
        cboThietBi.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThietBi.FormattingEnabled = true;
        cboThietBi.Location = new Point(113, 3);
        cboThietBi.Name = "cboThietBi";
        cboThietBi.Size = new Size(339, 23);
        cboThietBi.TabIndex = 1;
        // 
        // lblNhanVien
        // 
        lblNhanVien.AutoSize = true;
        lblNhanVien.Dock = DockStyle.Fill;
        lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhanVien.Location = new Point(458, 0);
        lblNhanVien.Name = "lblNhanVien";
        lblNhanVien.Size = new Size(114, 31);
        lblNhanVien.TabIndex = 2;
        lblNhanVien.Text = "NV phụ trách:";
        lblNhanVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboNhanVien
        // 
        cboNhanVien.Dock = DockStyle.Fill;
        cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhanVien.FormattingEnabled = true;
        cboNhanVien.Location = new Point(578, 3);
        cboNhanVien.Name = "cboNhanVien";
        cboNhanVien.Size = new Size(339, 23);
        cboNhanVien.TabIndex = 3;
        // 
        // lblNoiDung
        // 
        lblNoiDung.AutoSize = true;
        lblNoiDung.Dock = DockStyle.Fill;
        lblNoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNoiDung.Location = new Point(3, 31);
        lblNoiDung.Name = "lblNoiDung";
        lblNoiDung.Size = new Size(104, 32);
        lblNoiDung.TabIndex = 4;
        lblNoiDung.Text = "Nội dung:";
        lblNoiDung.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtNoiDung
        // 
        txtNoiDung.Dock = DockStyle.Fill;
        txtNoiDung.Location = new Point(113, 34);
        txtNoiDung.Name = "txtNoiDung";
        txtNoiDung.PlaceholderText = "Mô tả nội dung bảo trì, linh kiện thay thế...";
        txtNoiDung.Size = new Size(339, 23);
        txtNoiDung.TabIndex = 5;
        // 
        // lblChiPhi
        // 
        lblChiPhi.AutoSize = true;
        lblChiPhi.Dock = DockStyle.Fill;
        lblChiPhi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblChiPhi.Location = new Point(458, 31);
        lblChiPhi.Name = "lblChiPhi";
        lblChiPhi.Size = new Size(114, 32);
        lblChiPhi.TabIndex = 6;
        lblChiPhi.Text = "Chi phí (VNĐ):";
        lblChiPhi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudChiPhi
        // 
        nudChiPhi.Dock = DockStyle.Fill;
        nudChiPhi.Location = new Point(578, 34);
        nudChiPhi.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudChiPhi.Name = "nudChiPhi";
        nudChiPhi.Size = new Size(339, 23);
        nudChiPhi.TabIndex = 7;
        nudChiPhi.ThousandsSeparator = true;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnTao);
        flowThaoTac.Location = new Point(24, 212);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(952, 42);
        flowThaoTac.TabIndex = 2;
        // 
        // btnTao
        // 
        btnTao.BackColor = Color.FromArgb(5, 150, 105);
        btnTao.Cursor = Cursors.Hand;
        btnTao.FlatAppearance.BorderSize = 0;
        btnTao.FlatStyle = FlatStyle.Flat;
        btnTao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTao.ForeColor = Color.White;
        btnTao.Location = new Point(0, 0);
        btnTao.Margin = new Padding(0, 0, 8, 0);
        btnTao.Name = "btnTao";
        btnTao.Size = new Size(150, 36);
        btnTao.TabIndex = 0;
        btnTao.Text = "Tạo phiếu bảo trì";
        btnTao.UseVisualStyleBackColor = false;
        btnTao.Click += btnTao_Click;
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
        dgv.Location = new Point(24, 260);
        dgv.MultiSelect = false;
        dgv.Name = "dgv";
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.RowTemplate.Height = 32;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.Size = new Size(952, 405);
        dgv.TabIndex = 3;
        // 
        // FrmBaoTri
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 690);
        Controls.Add(dgv);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmBaoTri";
        Text = "Quản lý bảo trì";
        Load += FrmBaoTri_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudChiPhi).EndInit();
        flowThaoTac.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private Label lblThietBi;
    private ComboBox cboThietBi;
    private Label lblNhanVien;
    private ComboBox cboNhanVien;
    private Label lblNoiDung;
    private TextBox txtNoiDung;
    private Label lblChiPhi;
    private NumericUpDown nudChiPhi;
    private FlowLayoutPanel flowThaoTac;
    private Button btnTao;
    private DataGridView dgv;
}

namespace He_Thong_GYM.Forms;

partial class FrmGoiTap
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        panelTieuDe = new Panel();
        lblMoTa = new Label();
        lblTieuDe = new Label();
        groupThongTin = new GroupBox();
        tableThongTin = new TableLayoutPanel();
        lblTenGoi = new Label();
        txtTenGoi = new TextBox();
        lblThoiHan = new Label();
        nudThoiHan = new NumericUpDown();
        lblGia = new Label();
        nudGia = new NumericUpDown();
        lblTrangThaiForm = new Label();
        cboTrangThai = new ComboBox();
        lblMoTaForm = new Label();
        txtMoTa = new TextBox();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnLuu = new Button();
        btnNgungApDung = new Button();
        panelTimKiem = new Panel();
        btnTimKiem = new Button();
        txtTimKiem = new TextBox();
        lblTimKiem = new Label();
        dgvGoiTap = new DataGridView();
        colMaGoi = new DataGridViewTextBoxColumn();
        colTenGoi = new DataGridViewTextBoxColumn();
        colThoiHan = new DataGridViewTextBoxColumn();
        colGia = new DataGridViewTextBoxColumn();
        colMoTa = new DataGridViewTextBoxColumn();
        colTrangThai = new DataGridViewTextBoxColumn();
        panelTrangThai = new Panel();
        lblTrangThai = new Label();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudThoiHan).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudGia).BeginInit();
        flowThaoTac.SuspendLayout();
        panelTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoiTap).BeginInit();
        panelTrangThai.SuspendLayout();
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
        panelTieuDe.Size = new Size(1200, 82);
        panelTieuDe.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(255, 15);
        lblMoTa.TabIndex = 0;
        lblMoTa.Text = "Thiết lập thời hạn, giá và trạng thái các gói tập.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(201, 30);
        lblTieuDe.TabIndex = 1;
        lblTieuDe.Text = "QUẢN LÝ GÓI TẬP";
        // 
        // groupThongTin
        // 
        groupThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupThongTin.BackColor = Color.White;
        groupThongTin.Controls.Add(tableThongTin);
        groupThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupThongTin.Location = new Point(24, 100);
        groupThongTin.Name = "groupThongTin";
        groupThongTin.Padding = new Padding(16);
        groupThongTin.Size = new Size(1152, 150);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Thông tin gói tập";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblTenGoi, 0, 0);
        tableThongTin.Controls.Add(txtTenGoi, 1, 0);
        tableThongTin.Controls.Add(lblThoiHan, 2, 0);
        tableThongTin.Controls.Add(nudThoiHan, 3, 0);
        tableThongTin.Controls.Add(lblGia, 0, 1);
        tableThongTin.Controls.Add(nudGia, 1, 1);
        tableThongTin.Controls.Add(lblTrangThaiForm, 2, 1);
        tableThongTin.Controls.Add(cboTrangThai, 3, 1);
        tableThongTin.Controls.Add(lblMoTaForm, 0, 2);
        tableThongTin.Controls.Add(txtMoTa, 1, 2);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 34);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 3;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
        tableThongTin.Size = new Size(1120, 100);
        tableThongTin.TabIndex = 0;
        // 
        // lblTenGoi
        // 
        lblTenGoi.AutoSize = true;
        lblTenGoi.Dock = DockStyle.Fill;
        lblTenGoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTenGoi.Location = new Point(3, 0);
        lblTenGoi.Name = "lblTenGoi";
        lblTenGoi.Size = new Size(94, 33);
        lblTenGoi.TabIndex = 0;
        lblTenGoi.Text = "Tên gói:";
        lblTenGoi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTenGoi
        // 
        txtTenGoi.Dock = DockStyle.Fill;
        txtTenGoi.Location = new Point(103, 3);
        txtTenGoi.Margin = new Padding(3, 6, 12, 6);
        txtTenGoi.Name = "txtTenGoi";
        txtTenGoi.Size = new Size(445, 23);
        txtTenGoi.TabIndex = 1;
        // 
        // lblThoiHan
        // 
        lblThoiHan.AutoSize = true;
        lblThoiHan.Dock = DockStyle.Fill;
        lblThoiHan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblThoiHan.Location = new Point(563, 0);
        lblThoiHan.Name = "lblThoiHan";
        lblThoiHan.Size = new Size(94, 33);
        lblThoiHan.TabIndex = 2;
        lblThoiHan.Text = "Thời hạn (ngày):";
        lblThoiHan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudThoiHan
        // 
        nudThoiHan.Dock = DockStyle.Fill;
        nudThoiHan.Location = new Point(663, 3);
        nudThoiHan.Margin = new Padding(3, 6, 12, 6);
        nudThoiHan.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        nudThoiHan.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudThoiHan.Name = "nudThoiHan";
        nudThoiHan.Size = new Size(454, 23);
        nudThoiHan.TabIndex = 3;
        nudThoiHan.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblGia
        // 
        lblGia.AutoSize = true;
        lblGia.Dock = DockStyle.Fill;
        lblGia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGia.Location = new Point(3, 33);
        lblGia.Name = "lblGia";
        lblGia.Size = new Size(94, 33);
        lblGia.TabIndex = 4;
        lblGia.Text = "Giá (VNĐ):";
        lblGia.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudGia
        // 
        nudGia.DecimalPlaces = 0;
        nudGia.Dock = DockStyle.Fill;
        nudGia.Location = new Point(103, 36);
        nudGia.Margin = new Padding(3, 6, 12, 6);
        nudGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudGia.Name = "nudGia";
        nudGia.Size = new Size(445, 23);
        nudGia.TabIndex = 5;
        nudGia.ThousandsSeparator = true;
        // 
        // lblTrangThaiForm
        // 
        lblTrangThaiForm.AutoSize = true;
        lblTrangThaiForm.Dock = DockStyle.Fill;
        lblTrangThaiForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThaiForm.Location = new Point(563, 33);
        lblTrangThaiForm.Name = "lblTrangThaiForm";
        lblTrangThaiForm.Size = new Size(94, 33);
        lblTrangThaiForm.TabIndex = 6;
        lblTrangThaiForm.Text = "Trạng thái:";
        lblTrangThaiForm.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.Items.AddRange(new object[] { "Đang áp dụng", "Ngừng áp dụng" });
        cboTrangThai.Location = new Point(663, 36);
        cboTrangThai.Margin = new Padding(3, 6, 12, 6);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(454, 23);
        cboTrangThai.TabIndex = 7;
        // 
        // lblMoTaForm
        // 
        lblMoTaForm.AutoSize = true;
        lblMoTaForm.Dock = DockStyle.Fill;
        lblMoTaForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMoTaForm.Location = new Point(3, 66);
        lblMoTaForm.Name = "lblMoTaForm";
        lblMoTaForm.Size = new Size(94, 34);
        lblMoTaForm.TabIndex = 8;
        lblMoTaForm.Text = "Mô tả:";
        lblMoTaForm.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtMoTa
        // 
        tableThongTin.SetColumnSpan(txtMoTa, 3);
        txtMoTa.Dock = DockStyle.Fill;
        txtMoTa.Location = new Point(103, 69);
        txtMoTa.Margin = new Padding(3, 6, 12, 6);
        txtMoTa.Name = "txtMoTa";
        txtMoTa.Size = new Size(1004, 23);
        txtMoTa.TabIndex = 9;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnLuu);
        flowThaoTac.Controls.Add(btnNgungApDung);
        flowThaoTac.Location = new Point(24, 260);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1152, 42);
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
        btnThem.Size = new Size(128, 36);
        btnThem.TabIndex = 0;
        btnThem.Text = "Thêm mới";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(5, 150, 105);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(136, 0);
        btnLuu.Margin = new Padding(0, 0, 8, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(128, 36);
        btnLuu.TabIndex = 1;
        btnLuu.Text = "Lưu gói tập";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnNgungApDung
        // 
        btnNgungApDung.BackColor = Color.FromArgb(185, 28, 28);
        btnNgungApDung.Cursor = Cursors.Hand;
        btnNgungApDung.FlatAppearance.BorderSize = 0;
        btnNgungApDung.FlatStyle = FlatStyle.Flat;
        btnNgungApDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNgungApDung.ForeColor = Color.White;
        btnNgungApDung.Location = new Point(272, 0);
        btnNgungApDung.Margin = new Padding(0, 0, 8, 0);
        btnNgungApDung.Name = "btnNgungApDung";
        btnNgungApDung.Size = new Size(128, 36);
        btnNgungApDung.TabIndex = 2;
        btnNgungApDung.Text = "Ngừng áp dụng";
        btnNgungApDung.UseVisualStyleBackColor = false;
        btnNgungApDung.Click += btnNgungApDung_Click;
        // 
        // panelTimKiem
        // 
        panelTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTimKiem.BackColor = Color.White;
        panelTimKiem.Controls.Add(btnTimKiem);
        panelTimKiem.Controls.Add(txtTimKiem);
        panelTimKiem.Controls.Add(lblTimKiem);
        panelTimKiem.Location = new Point(24, 313);
        panelTimKiem.Name = "panelTimKiem";
        panelTimKiem.Size = new Size(1152, 58);
        panelTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(30, 64, 175);
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(1020, 13);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(112, 30);
        btnTimKiem.TabIndex = 0;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTimKiem.Location = new Point(144, 16);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập tên gói...";
        txtTimKiem.Size = new Size(862, 23);
        txtTimKiem.TabIndex = 1;
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.Location = new Point(18, 20);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(103, 15);
        lblTimKiem.TabIndex = 2;
        lblTimKiem.Text = "Tìm kiếm gói tập:";
        // 
        // dgvGoiTap
        // 
        dgvGoiTap.AllowUserToAddRows = false;
        dgvGoiTap.AllowUserToDeleteRows = false;
        dgvGoiTap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGoiTap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGoiTap.BackgroundColor = Color.White;
        dgvGoiTap.BorderStyle = BorderStyle.None;
        dgvGoiTap.ColumnHeadersHeight = 38;
        dgvGoiTap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvGoiTap.Columns.AddRange(new DataGridViewColumn[] { colMaGoi, colTenGoi, colThoiHan, colGia, colMoTa, colTrangThai });
        dgvGoiTap.Location = new Point(24, 381);
        dgvGoiTap.Name = "dgvGoiTap";
        dgvGoiTap.ReadOnly = true;
        dgvGoiTap.RowHeadersVisible = false;
        dgvGoiTap.RowTemplate.Height = 32;
        dgvGoiTap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGoiTap.Size = new Size(1152, 318);
        dgvGoiTap.TabIndex = 4;
        dgvGoiTap.SelectionChanged += dgvGoiTap_SelectionChanged;
        // 
        // colMaGoi
        // 
        colMaGoi.DataPropertyName = "MaGoi";
        colMaGoi.FillWeight = 50F;
        colMaGoi.HeaderText = "Mã gói";
        colMaGoi.Name = "colMaGoi";
        colMaGoi.ReadOnly = true;
        // 
        // colTenGoi
        // 
        colTenGoi.DataPropertyName = "TenGoi";
        colTenGoi.FillWeight = 140F;
        colTenGoi.HeaderText = "Tên gói tập";
        colTenGoi.Name = "colTenGoi";
        colTenGoi.ReadOnly = true;
        // 
        // colThoiHan
        // 
        colThoiHan.DataPropertyName = "ThoiHanNgay";
        colThoiHan.FillWeight = 80F;
        colThoiHan.HeaderText = "Thời hạn (ngày)";
        colThoiHan.Name = "colThoiHan";
        colThoiHan.ReadOnly = true;
        // 
        // colGia
        // 
        colGia.DataPropertyName = "Gia";
        dataGridViewCellStyle1.Format = "N0";
        colGia.DefaultCellStyle = dataGridViewCellStyle1;
        colGia.FillWeight = 90F;
        colGia.HeaderText = "Giá (VNĐ)";
        colGia.Name = "colGia";
        colGia.ReadOnly = true;
        // 
        // colMoTa
        // 
        colMoTa.DataPropertyName = "MoTa";
        colMoTa.FillWeight = 160F;
        colMoTa.HeaderText = "Mô tả";
        colMoTa.Name = "colMoTa";
        colMoTa.ReadOnly = true;
        // 
        // colTrangThai
        // 
        colTrangThai.DataPropertyName = "TrangThai";
        colTrangThai.FillWeight = 90F;
        colTrangThai.HeaderText = "Trạng thái";
        colTrangThai.Name = "colTrangThai";
        colTrangThai.ReadOnly = true;
        // 
        // panelTrangThai
        // 
        panelTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelTrangThai.BackColor = Color.White;
        panelTrangThai.Controls.Add(lblTrangThai);
        panelTrangThai.Location = new Point(24, 709);
        panelTrangThai.Name = "panelTrangThai";
        panelTrangThai.Size = new Size(1152, 30);
        panelTrangThai.TabIndex = 5;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139);
        lblTrangThai.Location = new Point(10, 8);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(136, 15);
        lblTrangThai.TabIndex = 0;
        lblTrangThai.Text = "Hiển thị toàn bộ gói tập.";
        // 
        // FrmGoiTap
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvGoiTap);
        Controls.Add(panelTimKiem);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        AutoScroll = true;
        MinimumSize = new Size(850, 480);
        Name = "FrmGoiTap";
        Text = "Quản lý gói tập";
        Load += FrmGoiTap_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudThoiHan).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudGia).EndInit();
        flowThaoTac.ResumeLayout(false);
        panelTimKiem.ResumeLayout(false);
        panelTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGoiTap).EndInit();
        panelTrangThai.ResumeLayout(false);
        panelTrangThai.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelTieuDe;
    private Panel panelTimKiem;
    private Panel panelTrangThai;
    private Label lblTieuDe;
    private Label lblMoTa;
    private Label lblTenGoi;
    private Label lblThoiHan;
    private Label lblGia;
    private Label lblTrangThaiForm;
    private Label lblMoTaForm;
    private Label lblTimKiem;
    private Label lblTrangThai;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private TextBox txtTenGoi;
    private TextBox txtMoTa;
    private TextBox txtTimKiem;
    private NumericUpDown nudThoiHan;
    private NumericUpDown nudGia;
    private ComboBox cboTrangThai;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnLuu;
    private Button btnNgungApDung;
    private Button btnTimKiem;
    private DataGridView dgvGoiTap;
    private DataGridViewTextBoxColumn colMaGoi;
    private DataGridViewTextBoxColumn colTenGoi;
    private DataGridViewTextBoxColumn colThoiHan;
    private DataGridViewTextBoxColumn colGia;
    private DataGridViewTextBoxColumn colMoTa;
    private DataGridViewTextBoxColumn colTrangThai;
}

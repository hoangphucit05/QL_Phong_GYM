namespace He_Thong_GYM.Forms;

partial class FrmHLV
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
        lblNhanVien = new Label();
        cboNhanVien = new ComboBox();
        lblChuyenMon = new Label();
        txtChuyenMon = new TextBox();
        lblKinhNghiem = new Label();
        nudKinhNghiem = new NumericUpDown();
        lblPhiThue = new Label();
        nudPhiThue = new NumericUpDown();
        flowThaoTac = new FlowLayoutPanel();
        btnThem = new Button();
        btnLuu = new Button();
        panelTimKiem = new Panel();
        btnTimKiem = new Button();
        txtTimKiem = new TextBox();
        lblTimKiem = new Label();
        dgvHLV = new DataGridView();
        colMaHLV = new DataGridViewTextBoxColumn();
        colHoTen = new DataGridViewTextBoxColumn();
        colChuyenMon = new DataGridViewTextBoxColumn();
        colKinhNghiem = new DataGridViewTextBoxColumn();
        colPhiThue = new DataGridViewTextBoxColumn();
        colTrangThai = new DataGridViewTextBoxColumn();
        panelTrangThai = new Panel();
        lblTrangThai = new Label();
        panelTieuDe.SuspendLayout();
        groupThongTin.SuspendLayout();
        tableThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudKinhNghiem).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudPhiThue).BeginInit();
        flowThaoTac.SuspendLayout();
        panelTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHLV).BeginInit();
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
        lblMoTa.Size = new Size(365, 15);
        lblMoTa.TabIndex = 0;
        lblMoTa.Text = "Quản lý chuyên môn, kinh nghiệm và phí thuê của huấn luyện viên.";
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 12);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(310, 30);
        lblTieuDe.TabIndex = 1;
        lblTieuDe.Text = "QUẢN LÝ HUẤN LUYỆN VIÊN";
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
        groupThongTin.Size = new Size(1152, 120);
        groupThongTin.TabIndex = 1;
        groupThongTin.TabStop = false;
        groupThongTin.Text = "Hồ sơ huấn luyện viên";
        // 
        // tableThongTin
        // 
        tableThongTin.ColumnCount = 4;
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableThongTin.Controls.Add(lblNhanVien, 0, 0);
        tableThongTin.Controls.Add(cboNhanVien, 1, 0);
        tableThongTin.Controls.Add(lblChuyenMon, 2, 0);
        tableThongTin.Controls.Add(txtChuyenMon, 3, 0);
        tableThongTin.Controls.Add(lblKinhNghiem, 0, 1);
        tableThongTin.Controls.Add(nudKinhNghiem, 1, 1);
        tableThongTin.Controls.Add(lblPhiThue, 2, 1);
        tableThongTin.Controls.Add(nudPhiThue, 3, 1);
        tableThongTin.Dock = DockStyle.Fill;
        tableThongTin.Font = new Font("Segoe UI", 9F);
        tableThongTin.Location = new Point(16, 34);
        tableThongTin.Name = "tableThongTin";
        tableThongTin.RowCount = 2;
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableThongTin.Size = new Size(1120, 70);
        tableThongTin.TabIndex = 0;
        // 
        // lblNhanVien
        // 
        lblNhanVien.AutoSize = true;
        lblNhanVien.Dock = DockStyle.Fill;
        lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhanVien.Location = new Point(3, 0);
        lblNhanVien.Name = "lblNhanVien";
        lblNhanVien.Size = new Size(114, 35);
        lblNhanVien.TabIndex = 0;
        lblNhanVien.Text = "Nhân viên:";
        lblNhanVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboNhanVien
        // 
        cboNhanVien.Dock = DockStyle.Fill;
        cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhanVien.Location = new Point(123, 3);
        cboNhanVien.Margin = new Padding(3, 6, 12, 6);
        cboNhanVien.Name = "cboNhanVien";
        cboNhanVien.Size = new Size(425, 23);
        cboNhanVien.TabIndex = 1;
        // 
        // lblChuyenMon
        // 
        lblChuyenMon.AutoSize = true;
        lblChuyenMon.Dock = DockStyle.Fill;
        lblChuyenMon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblChuyenMon.Location = new Point(563, 0);
        lblChuyenMon.Name = "lblChuyenMon";
        lblChuyenMon.Size = new Size(114, 35);
        lblChuyenMon.TabIndex = 2;
        lblChuyenMon.Text = "Chuyên môn:";
        lblChuyenMon.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtChuyenMon
        // 
        txtChuyenMon.Dock = DockStyle.Fill;
        txtChuyenMon.Location = new Point(683, 3);
        txtChuyenMon.Margin = new Padding(3, 6, 12, 6);
        txtChuyenMon.Name = "txtChuyenMon";
        txtChuyenMon.Size = new Size(434, 23);
        txtChuyenMon.TabIndex = 3;
        // 
        // lblKinhNghiem
        // 
        lblKinhNghiem.AutoSize = true;
        lblKinhNghiem.Dock = DockStyle.Fill;
        lblKinhNghiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblKinhNghiem.Location = new Point(3, 35);
        lblKinhNghiem.Name = "lblKinhNghiem";
        lblKinhNghiem.Size = new Size(114, 35);
        lblKinhNghiem.TabIndex = 4;
        lblKinhNghiem.Text = "Kinh nghiệm (năm):";
        lblKinhNghiem.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudKinhNghiem
        // 
        nudKinhNghiem.Dock = DockStyle.Fill;
        nudKinhNghiem.Location = new Point(123, 38);
        nudKinhNghiem.Margin = new Padding(3, 6, 12, 6);
        nudKinhNghiem.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
        nudKinhNghiem.Name = "nudKinhNghiem";
        nudKinhNghiem.Size = new Size(425, 23);
        nudKinhNghiem.TabIndex = 5;
        // 
        // lblPhiThue
        // 
        lblPhiThue.AutoSize = true;
        lblPhiThue.Dock = DockStyle.Fill;
        lblPhiThue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPhiThue.Location = new Point(563, 35);
        lblPhiThue.Name = "lblPhiThue";
        lblPhiThue.Size = new Size(114, 35);
        lblPhiThue.TabIndex = 6;
        lblPhiThue.Text = "Phí thuê / buổi:";
        lblPhiThue.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudPhiThue
        // 
        nudPhiThue.DecimalPlaces = 0;
        nudPhiThue.Dock = DockStyle.Fill;
        nudPhiThue.Location = new Point(683, 38);
        nudPhiThue.Margin = new Padding(3, 6, 12, 6);
        nudPhiThue.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudPhiThue.Name = "nudPhiThue";
        nudPhiThue.Size = new Size(434, 23);
        nudPhiThue.TabIndex = 7;
        nudPhiThue.ThousandsSeparator = true;
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnThem);
        flowThaoTac.Controls.Add(btnLuu);
        flowThaoTac.Location = new Point(24, 230);
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
        btnLuu.Text = "Lưu thông tin";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // panelTimKiem
        // 
        panelTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTimKiem.BackColor = Color.White;
        panelTimKiem.Controls.Add(btnTimKiem);
        panelTimKiem.Controls.Add(txtTimKiem);
        panelTimKiem.Controls.Add(lblTimKiem);
        panelTimKiem.Location = new Point(24, 283);
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
        txtTimKiem.Location = new Point(162, 16);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập từ khóa...";
        txtTimKiem.Size = new Size(844, 23);
        txtTimKiem.TabIndex = 1;
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.Location = new Point(18, 20);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(132, 15);
        lblTimKiem.TabIndex = 2;
        lblTimKiem.Text = "Tìm tên / chuyên môn:";
        // 
        // dgvHLV
        // 
        dgvHLV.AllowUserToAddRows = false;
        dgvHLV.AllowUserToDeleteRows = false;
        dgvHLV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvHLV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHLV.BackgroundColor = Color.White;
        dgvHLV.BorderStyle = BorderStyle.None;
        dgvHLV.ColumnHeadersHeight = 38;
        dgvHLV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvHLV.Columns.AddRange(new DataGridViewColumn[] { colMaHLV, colHoTen, colChuyenMon, colKinhNghiem, colPhiThue, colTrangThai });
        dgvHLV.Location = new Point(24, 351);
        dgvHLV.Name = "dgvHLV";
        dgvHLV.ReadOnly = true;
        dgvHLV.RowHeadersVisible = false;
        dgvHLV.RowTemplate.Height = 32;
        dgvHLV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHLV.Size = new Size(1152, 348);
        dgvHLV.TabIndex = 4;
        dgvHLV.SelectionChanged += dgvHLV_SelectionChanged;
        // 
        // colMaHLV
        // 
        colMaHLV.DataPropertyName = "MaHLV";
        colMaHLV.FillWeight = 50F;
        colMaHLV.HeaderText = "Mã HLV";
        colMaHLV.Name = "colMaHLV";
        colMaHLV.ReadOnly = true;
        // 
        // colHoTen
        // 
        colHoTen.DataPropertyName = "HoTen";
        colHoTen.FillWeight = 140F;
        colHoTen.HeaderText = "Họ tên HLV";
        colHoTen.Name = "colHoTen";
        colHoTen.ReadOnly = true;
        // 
        // colChuyenMon
        // 
        colChuyenMon.DataPropertyName = "ChuyenMon";
        colChuyenMon.FillWeight = 140F;
        colChuyenMon.HeaderText = "Chuyên môn";
        colChuyenMon.Name = "colChuyenMon";
        colChuyenMon.ReadOnly = true;
        // 
        // colKinhNghiem
        // 
        colKinhNghiem.DataPropertyName = "KinhNghiem";
        colKinhNghiem.FillWeight = 80F;
        colKinhNghiem.HeaderText = "Kinh nghiệm (năm)";
        colKinhNghiem.Name = "colKinhNghiem";
        colKinhNghiem.ReadOnly = true;
        // 
        // colPhiThue
        // 
        colPhiThue.DataPropertyName = "PhiThue";
        dataGridViewCellStyle1.Format = "N0";
        colPhiThue.DefaultCellStyle = dataGridViewCellStyle1;
        colPhiThue.FillWeight = 90F;
        colPhiThue.HeaderText = "Phí thuê (VNĐ)";
        colPhiThue.Name = "colPhiThue";
        colPhiThue.ReadOnly = true;
        // 
        // colTrangThai
        // 
        colTrangThai.DataPropertyName = "TrangThaiNhanVien";
        colTrangThai.FillWeight = 80F;
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
        lblTrangThai.Size = new Size(183, 15);
        lblTrangThai.TabIndex = 0;
        lblTrangThai.Text = "Hiển thị toàn bộ huấn luyện viên.";
        // 
        // FrmHLV
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvHLV);
        Controls.Add(panelTimKiem);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        AutoScroll = true;
        MinimumSize = new Size(850, 480);
        Name = "FrmHLV";
        Text = "Quản lý huấn luyện viên";
        Load += FrmHLV_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        groupThongTin.ResumeLayout(false);
        tableThongTin.ResumeLayout(false);
        tableThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudKinhNghiem).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudPhiThue).EndInit();
        flowThaoTac.ResumeLayout(false);
        panelTimKiem.ResumeLayout(false);
        panelTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHLV).EndInit();
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
    private Label lblNhanVien;
    private Label lblChuyenMon;
    private Label lblKinhNghiem;
    private Label lblPhiThue;
    private Label lblTimKiem;
    private Label lblTrangThai;
    private GroupBox groupThongTin;
    private TableLayoutPanel tableThongTin;
    private ComboBox cboNhanVien;
    private TextBox txtChuyenMon;
    private TextBox txtTimKiem;
    private NumericUpDown nudKinhNghiem;
    private NumericUpDown nudPhiThue;
    private FlowLayoutPanel flowThaoTac;
    private Button btnThem;
    private Button btnLuu;
    private Button btnTimKiem;
    private DataGridView dgvHLV;
    private DataGridViewTextBoxColumn colMaHLV;
    private DataGridViewTextBoxColumn colHoTen;
    private DataGridViewTextBoxColumn colChuyenMon;
    private DataGridViewTextBoxColumn colKinhNghiem;
    private DataGridViewTextBoxColumn colPhiThue;
    private DataGridViewTextBoxColumn colTrangThai;
}

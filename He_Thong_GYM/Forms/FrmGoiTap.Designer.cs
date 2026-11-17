namespace He_Thong_GYM.Forms;
partial class FrmGoiTap
{
 private System.ComponentModel.IContainer components=null!;
 protected override void Dispose(bool disposing){if(disposing&&components is not null)components.Dispose();base.Dispose(disposing);}
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
        panelTieuDe.TabIndex = 5;
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
        groupThongTin.TabIndex = 4;
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
        lblTenGoi.Location = new Point(3, 0);
        lblTenGoi.Name = "lblTenGoi";
        lblTenGoi.Size = new Size(94, 23);
        lblTenGoi.TabIndex = 0;
        // 
        // txtTenGoi
        // 
        txtTenGoi.Location = new Point(103, 3);
        txtTenGoi.Name = "txtTenGoi";
        txtTenGoi.Size = new Size(100, 23);
        txtTenGoi.TabIndex = 1;
        // 
        // lblThoiHan
        // 
        lblThoiHan.Location = new Point(563, 0);
        lblThoiHan.Name = "lblThoiHan";
        lblThoiHan.Size = new Size(94, 23);
        lblThoiHan.TabIndex = 2;
        // 
        // nudThoiHan
        // 
        nudThoiHan.Dock = DockStyle.Fill;
        nudThoiHan.Location = new Point(663, 3);
        nudThoiHan.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        nudThoiHan.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudThoiHan.Name = "nudThoiHan";
        nudThoiHan.Size = new Size(454, 23);
        nudThoiHan.TabIndex = 3;
        nudThoiHan.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblGia
        // 
        lblGia.Location = new Point(3, 33);
        lblGia.Name = "lblGia";
        lblGia.Size = new Size(94, 23);
        lblGia.TabIndex = 4;
        // 
        // nudGia
        // 
        nudGia.DecimalPlaces = 2;
        nudGia.Dock = DockStyle.Fill;
        nudGia.Location = new Point(103, 36);
        nudGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        nudGia.Name = "nudGia";
        nudGia.Size = new Size(454, 23);
        nudGia.TabIndex = 5;
        // 
        // lblTrangThaiForm
        // 
        lblTrangThaiForm.Location = new Point(563, 33);
        lblTrangThaiForm.Name = "lblTrangThaiForm";
        lblTrangThaiForm.Size = new Size(94, 23);
        lblTrangThaiForm.TabIndex = 6;
        // 
        // cboTrangThai
        // 
        cboTrangThai.Dock = DockStyle.Fill;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.Items.AddRange(new object[] { "Đang áp dụng", "Ngừng áp dụng" });
        cboTrangThai.Location = new Point(663, 36);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(454, 23);
        cboTrangThai.TabIndex = 7;
        // 
        // lblMoTaForm
        // 
        lblMoTaForm.Location = new Point(3, 66);
        lblMoTaForm.Name = "lblMoTaForm";
        lblMoTaForm.Size = new Size(94, 23);
        lblMoTaForm.TabIndex = 8;
        // 
        // txtMoTa
        // 
        tableThongTin.SetColumnSpan(txtMoTa, 3);
        txtMoTa.Location = new Point(103, 69);
        txtMoTa.Name = "txtMoTa";
        txtMoTa.Size = new Size(100, 23);
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
        flowThaoTac.TabIndex = 3;
        // 
        // btnThem
        // 
        btnThem.Location = new Point(3, 3);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(75, 23);
        btnThem.TabIndex = 0;
        btnThem.Click += btnThem_Click;
        // 
        // btnLuu
        // 
        btnLuu.Location = new Point(84, 3);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(75, 23);
        btnLuu.TabIndex = 1;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnNgungApDung
        // 
        btnNgungApDung.Location = new Point(165, 3);
        btnNgungApDung.Name = "btnNgungApDung";
        btnNgungApDung.Size = new Size(75, 23);
        btnNgungApDung.TabIndex = 2;
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
        panelTimKiem.TabIndex = 2;
        // 
        // btnTimKiem
        // 
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(30, 64, 175);
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
        dgvGoiTap.Columns.AddRange(new DataGridViewColumn[] { colMaGoi, colTenGoi, colThoiHan, colGia, colMoTa, colTrangThai });
        dgvGoiTap.Location = new Point(24, 381);
        dgvGoiTap.Name = "dgvGoiTap";
        dgvGoiTap.ReadOnly = true;
        dgvGoiTap.RowHeadersVisible = false;
        dgvGoiTap.RowTemplate.Height = 32;
        dgvGoiTap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGoiTap.Size = new Size(1152, 318);
        dgvGoiTap.TabIndex = 1;
        dgvGoiTap.SelectionChanged += dgvGoiTap_SelectionChanged;
        // 
        // colMaGoi
        // 
        colMaGoi.Name = "colMaGoi";
        colMaGoi.ReadOnly = true;
        // 
        // colTenGoi
        // 
        colTenGoi.Name = "colTenGoi";
        colTenGoi.ReadOnly = true;
        // 
        // colThoiHan
        // 
        colThoiHan.Name = "colThoiHan";
        colThoiHan.ReadOnly = true;
        // 
        // colGia
        // 
        dataGridViewCellStyle1.Format = "N0";
        colGia.DefaultCellStyle = dataGridViewCellStyle1;
        colGia.Name = "colGia";
        colGia.ReadOnly = true;
        // 
        // colMoTa
        // 
        colMoTa.Name = "colMoTa";
        colMoTa.ReadOnly = true;
        // 
        // colTrangThai
        // 
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
        panelTrangThai.TabIndex = 0;
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
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1200, 760);
        Controls.Add(panelTrangThai);
        Controls.Add(dgvGoiTap);
        Controls.Add(panelTimKiem);
        Controls.Add(flowThaoTac);
        Controls.Add(groupThongTin);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
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
    private static void CauHinhNhan(Label l,string t){l.AutoSize=true;l.Dock=DockStyle.Fill;l.Font=new Font("Segoe UI",9F,FontStyle.Bold);l.Text=t;l.TextAlign=ContentAlignment.MiddleLeft;}private static void CauHinhNhap(TextBox t){t.Dock=DockStyle.Fill;t.Margin=new Padding(3,6,12,6);}private static void CauHinhNut(Button b,string t,Color c){b.BackColor=c;b.FlatStyle=FlatStyle.Flat;b.FlatAppearance.BorderSize=0;b.ForeColor=Color.White;b.Font=new Font("Segoe UI",9F,FontStyle.Bold);b.Margin=new Padding(0,0,8,0);b.Size=new Size(128,36);b.Text=t;}private static void CauHinhCot(DataGridViewTextBoxColumn c,string p,string h,float w){c.DataPropertyName=p;c.HeaderText=h;c.Name="col"+p;c.FillWeight=w;c.ReadOnly=true;}
 private Panel panelTieuDe,panelTimKiem,panelTrangThai;private Label lblTieuDe,lblMoTa,lblTenGoi,lblThoiHan,lblGia,lblTrangThaiForm,lblMoTaForm,lblTimKiem,lblTrangThai;private GroupBox groupThongTin;private TableLayoutPanel tableThongTin;private TextBox txtTenGoi,txtMoTa,txtTimKiem;private NumericUpDown nudThoiHan,nudGia;private ComboBox cboTrangThai;private FlowLayoutPanel flowThaoTac;private Button btnThem,btnLuu,btnNgungApDung,btnTimKiem;private DataGridView dgvGoiTap;private DataGridViewTextBoxColumn colMaGoi,colTenGoi,colThoiHan,colGia,colMoTa,colTrangThai;
}

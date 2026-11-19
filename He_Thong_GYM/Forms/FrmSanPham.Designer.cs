namespace He_Thong_GYM.Forms;

partial class FrmSanPham
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
        flowThaoTac = new FlowLayoutPanel();
        btnBanHang = new Button();
        btnLamMoi = new Button();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        lblThongKe = new Label();
        dgvSanPham = new DataGridView();
        panelTieuDe.SuspendLayout();
        flowThaoTac.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
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
        panelTieuDe.Size = new Size(1050, 80);
        panelTieuDe.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDe.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new Point(24, 14);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(390, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "QUẢN LÝ SẢN PHẨM VÀ TỒN KHO";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(360, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Danh mục nước uống, whey protein, phụ kiện và theo dõi tồn kho.";
        // 
        // flowThaoTac
        // 
        flowThaoTac.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowThaoTac.Controls.Add(btnBanHang);
        flowThaoTac.Controls.Add(btnLamMoi);
        flowThaoTac.Controls.Add(lblTimKiem);
        flowThaoTac.Controls.Add(txtTimKiem);
        flowThaoTac.Controls.Add(lblTrangThai);
        flowThaoTac.Controls.Add(cboTrangThai);
        flowThaoTac.Controls.Add(lblThongKe);
        flowThaoTac.Location = new Point(24, 96);
        flowThaoTac.Name = "flowThaoTac";
        flowThaoTac.Size = new Size(1002, 42);
        flowThaoTac.TabIndex = 1;
        // 
        // btnBanHang
        // 
        btnBanHang.BackColor = Color.FromArgb(5, 150, 105);
        btnBanHang.Cursor = Cursors.Hand;
        btnBanHang.FlatAppearance.BorderSize = 0;
        btnBanHang.FlatStyle = FlatStyle.Flat;
        btnBanHang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBanHang.ForeColor = Color.White;
        btnBanHang.Location = new Point(0, 0);
        btnBanHang.Margin = new Padding(0, 0, 8, 0);
        btnBanHang.Name = "btnBanHang";
        btnBanHang.Size = new Size(140, 36);
        btnBanHang.TabIndex = 0;
        btnBanHang.Text = "Bán sản phẩm";
        btnBanHang.UseVisualStyleBackColor = false;
        btnBanHang.Click += btnBanHang_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(71, 85, 105);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(148, 0);
        btnLamMoi.Margin = new Padding(0, 0, 16, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(100, 36);
        btnLamMoi.TabIndex = 1;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblTimKiem
        // 
        lblTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTimKiem.ForeColor = Color.FromArgb(30, 41, 59);
        lblTimKiem.Location = new Point(267, 0);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new Size(70, 36);
        lblTimKiem.TabIndex = 2;
        lblTimKiem.Text = "Tìm kiếm:";
        lblTimKiem.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Location = new Point(343, 6);
        txtTimKiem.Margin = new Padding(3, 6, 12, 3);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tên, mã sản phẩm...";
        txtTimKiem.Size = new Size(180, 23);
        txtTimKiem.TabIndex = 3;
        txtTimKiem.TextChanged += BoLocThayDoi;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
        lblTrangThai.Location = new Point(541, 0);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(72, 36);
        lblTrangThai.TabIndex = 4;
        lblTrangThai.Text = "Trạng thái:";
        lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cboTrangThai
        // 
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Đang bán", "Hết hàng", "Ngừng kinh doanh" });
        cboTrangThai.Location = new Point(619, 6);
        cboTrangThai.Margin = new Padding(3, 6, 12, 3);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(140, 23);
        cboTrangThai.TabIndex = 5;
        cboTrangThai.SelectedIndexChanged += BoLocThayDoi;
        // 
        // lblThongKe
        // 
        lblThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblThongKe.ForeColor = Color.FromArgb(100, 116, 139);
        lblThongKe.Location = new Point(774, 0);
        lblThongKe.Name = "lblThongKe";
        lblThongKe.Size = new Size(200, 36);
        lblThongKe.TabIndex = 6;
        lblThongKe.Text = "Hiển thị: 0 sản phẩm";
        lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvSanPham
        // 
        dgvSanPham.AllowUserToAddRows = false;
        dgvSanPham.AllowUserToDeleteRows = false;
        dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSanPham.BackgroundColor = Color.White;
        dgvSanPham.BorderStyle = BorderStyle.None;
        dgvSanPham.ColumnHeadersHeight = 36;
        dgvSanPham.Location = new Point(24, 146);
        dgvSanPham.MultiSelect = false;
        dgvSanPham.Name = "dgvSanPham";
        dgvSanPham.ReadOnly = true;
        dgvSanPham.RowHeadersVisible = false;
        dgvSanPham.RowTemplate.Height = 32;
        dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSanPham.Size = new Size(1002, 530);
        dgvSanPham.TabIndex = 2;
        // 
        // FrmSanPham
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1050, 700);
        Controls.Add(dgvSanPham);
        Controls.Add(flowThaoTac);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmSanPham";
        Text = "Quản lý sản phẩm";
        Load += FrmSanPham_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        flowThaoTac.ResumeLayout(false);
        flowThaoTac.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private FlowLayoutPanel flowThaoTac;
    private Button btnBanHang;
    private Button btnLamMoi;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private Label lblThongKe;
    private DataGridView dgvSanPham;
}

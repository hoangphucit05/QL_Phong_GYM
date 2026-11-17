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
        btnBanHang.Size = new Size(150, 36);
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
        btnLamMoi.Location = new Point(158, 0);
        btnLamMoi.Margin = new Padding(0, 0, 8, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(120, 36);
        btnLamMoi.TabIndex = 1;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
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
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private FlowLayoutPanel flowThaoTac;
    private Button btnBanHang;
    private Button btnLamMoi;
    private DataGridView dgvSanPham;
}

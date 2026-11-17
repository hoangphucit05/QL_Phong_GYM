namespace He_Thong_GYM.Forms;

partial class FrmThongKe
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelTieuDe = new Panel();
        lblTieuDe = new Label();
        lblMoTa = new Label();
        tableChiSo = new TableLayoutPanel();
        panelTheThanhVien = new Panel();
        lblTieuDeThanhVien = new Label();
        lblThanhVien = new Label();
        panelTheDoanhThu = new Panel();
        lblTieuDeDoanhThu = new Label();
        lblDoanhThu = new Label();
        panelTheCongNo = new Panel();
        lblTieuDeCongNo = new Label();
        lblCongNo = new Label();
        panelTheTonKho = new Panel();
        lblTieuDeTonKho = new Label();
        lblTonKho = new Label();
        panelTheBaoTri = new Panel();
        lblTieuDeBaoTri = new Label();
        lblBaoTri = new Label();
        panelTieuDe.SuspendLayout();
        tableChiSo.SuspendLayout();
        panelTheThanhVien.SuspendLayout();
        panelTheDoanhThu.SuspendLayout();
        panelTheCongNo.SuspendLayout();
        panelTheTonKho.SuspendLayout();
        panelTheBaoTri.SuspendLayout();
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
        lblTieuDe.Size = new Size(245, 30);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BÁO CÁO THỐNG KÊ";
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.ForeColor = Color.FromArgb(100, 116, 139);
        lblMoTa.Location = new Point(26, 48);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(335, 15);
        lblMoTa.TabIndex = 1;
        lblMoTa.Text = "Tổng hợp các chỉ số KPI vận hành, tài chính và tài sản thiết bị.";
        // 
        // tableChiSo
        // 
        tableChiSo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableChiSo.ColumnCount = 3;
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tableChiSo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tableChiSo.Controls.Add(panelTheThanhVien, 0, 0);
        tableChiSo.Controls.Add(panelTheDoanhThu, 1, 0);
        tableChiSo.Controls.Add(panelTheCongNo, 2, 0);
        tableChiSo.Controls.Add(panelTheTonKho, 0, 1);
        tableChiSo.Controls.Add(panelTheBaoTri, 1, 1);
        tableChiSo.Location = new Point(24, 96);
        tableChiSo.Name = "tableChiSo";
        tableChiSo.RowCount = 2;
        tableChiSo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableChiSo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableChiSo.Size = new Size(952, 455);
        tableChiSo.TabIndex = 1;
        // 
        // panelTheThanhVien
        // 
        panelTheThanhVien.BackColor = Color.FromArgb(30, 64, 175);
        panelTheThanhVien.Controls.Add(lblThanhVien);
        panelTheThanhVien.Controls.Add(lblTieuDeThanhVien);
        panelTheThanhVien.Dock = DockStyle.Fill;
        panelTheThanhVien.Location = new Point(10, 10);
        panelTheThanhVien.Margin = new Padding(10);
        panelTheThanhVien.Name = "panelTheThanhVien";
        panelTheThanhVien.Padding = new Padding(22, 18, 22, 18);
        panelTheThanhVien.Size = new Size(297, 207);
        panelTheThanhVien.TabIndex = 0;
        // 
        // lblTieuDeThanhVien
        // 
        lblTieuDeThanhVien.AutoSize = true;
        lblTieuDeThanhVien.Dock = DockStyle.Top;
        lblTieuDeThanhVien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTieuDeThanhVien.ForeColor = Color.FromArgb(241, 245, 249);
        lblTieuDeThanhVien.Location = new Point(22, 18);
        lblTieuDeThanhVien.Name = "lblTieuDeThanhVien";
        lblTieuDeThanhVien.Size = new Size(133, 19);
        lblTieuDeThanhVien.TabIndex = 0;
        lblTieuDeThanhVien.Text = "TỔNG THÀNH VIÊN";
        // 
        // lblThanhVien
        // 
        lblThanhVien.Dock = DockStyle.Fill;
        lblThanhVien.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblThanhVien.ForeColor = Color.White;
        lblThanhVien.Location = new Point(22, 37);
        lblThanhVien.Name = "lblThanhVien";
        lblThanhVien.Size = new Size(253, 152);
        lblThanhVien.TabIndex = 1;
        lblThanhVien.Text = "--";
        lblThanhVien.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelTheDoanhThu
        // 
        panelTheDoanhThu.BackColor = Color.FromArgb(5, 150, 105);
        panelTheDoanhThu.Controls.Add(lblDoanhThu);
        panelTheDoanhThu.Controls.Add(lblTieuDeDoanhThu);
        panelTheDoanhThu.Dock = DockStyle.Fill;
        panelTheDoanhThu.Location = new Point(327, 10);
        panelTheDoanhThu.Margin = new Padding(10);
        panelTheDoanhThu.Name = "panelTheDoanhThu";
        panelTheDoanhThu.Padding = new Padding(22, 18, 22, 18);
        panelTheDoanhThu.Size = new Size(297, 207);
        panelTheDoanhThu.TabIndex = 1;
        // 
        // lblTieuDeDoanhThu
        // 
        lblTieuDeDoanhThu.AutoSize = true;
        lblTieuDeDoanhThu.Dock = DockStyle.Top;
        lblTieuDeDoanhThu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTieuDeDoanhThu.ForeColor = Color.FromArgb(241, 245, 249);
        lblTieuDeDoanhThu.Location = new Point(22, 18);
        lblTieuDeDoanhThu.Name = "lblTieuDeDoanhThu";
        lblTieuDeDoanhThu.Size = new Size(173, 19);
        lblTieuDeDoanhThu.TabIndex = 0;
        lblTieuDeDoanhThu.Text = "DOANH THU THỰC NHẬN";
        // 
        // lblDoanhThu
        // 
        lblDoanhThu.Dock = DockStyle.Fill;
        lblDoanhThu.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblDoanhThu.ForeColor = Color.White;
        lblDoanhThu.Location = new Point(22, 37);
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Size = new Size(253, 152);
        lblDoanhThu.TabIndex = 1;
        lblDoanhThu.Text = "--";
        lblDoanhThu.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelTheCongNo
        // 
        panelTheCongNo.BackColor = Color.FromArgb(220, 38, 38);
        panelTheCongNo.Controls.Add(lblCongNo);
        panelTheCongNo.Controls.Add(lblTieuDeCongNo);
        panelTheCongNo.Dock = DockStyle.Fill;
        panelTheCongNo.Location = new Point(644, 10);
        panelTheCongNo.Margin = new Padding(10);
        panelTheCongNo.Name = "panelTheCongNo";
        panelTheCongNo.Padding = new Padding(22, 18, 22, 18);
        panelTheCongNo.Size = new Size(298, 207);
        panelTheCongNo.TabIndex = 2;
        // 
        // lblTieuDeCongNo
        // 
        lblTieuDeCongNo.AutoSize = true;
        lblTieuDeCongNo.Dock = DockStyle.Top;
        lblTieuDeCongNo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTieuDeCongNo.ForeColor = Color.FromArgb(241, 245, 249);
        lblTieuDeCongNo.Location = new Point(22, 18);
        lblTieuDeCongNo.Name = "lblTieuDeCongNo";
        lblTieuDeCongNo.Size = new Size(115, 19);
        lblTieuDeCongNo.TabIndex = 0;
        lblTieuDeCongNo.Text = "TỔNG CÔNG NỢ";
        // 
        // lblCongNo
        // 
        lblCongNo.Dock = DockStyle.Fill;
        lblCongNo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblCongNo.ForeColor = Color.White;
        lblCongNo.Location = new Point(22, 37);
        lblCongNo.Name = "lblCongNo";
        lblCongNo.Size = new Size(254, 152);
        lblCongNo.TabIndex = 1;
        lblCongNo.Text = "--";
        lblCongNo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelTheTonKho
        // 
        panelTheTonKho.BackColor = Color.FromArgb(217, 119, 6);
        panelTheTonKho.Controls.Add(lblTonKho);
        panelTheTonKho.Controls.Add(lblTieuDeTonKho);
        panelTheTonKho.Dock = DockStyle.Fill;
        panelTheTonKho.Location = new Point(10, 237);
        panelTheTonKho.Margin = new Padding(10);
        panelTheTonKho.Name = "panelTheTonKho";
        panelTheTonKho.Padding = new Padding(22, 18, 22, 18);
        panelTheTonKho.Size = new Size(297, 208);
        panelTheTonKho.TabIndex = 3;
        // 
        // lblTieuDeTonKho
        // 
        lblTieuDeTonKho.AutoSize = true;
        lblTieuDeTonKho.Dock = DockStyle.Top;
        lblTieuDeTonKho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTieuDeTonKho.ForeColor = Color.FromArgb(241, 245, 249);
        lblTieuDeTonKho.Location = new Point(22, 18);
        lblTieuDeTonKho.Name = "lblTieuDeTonKho";
        lblTieuDeTonKho.Size = new Size(126, 19);
        lblTieuDeTonKho.TabIndex = 0;
        lblTieuDeTonKho.Text = "GIÁ TRỊ TỒN KHO";
        // 
        // lblTonKho
        // 
        lblTonKho.Dock = DockStyle.Fill;
        lblTonKho.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblTonKho.ForeColor = Color.White;
        lblTonKho.Location = new Point(22, 37);
        lblTonKho.Name = "lblTonKho";
        lblTonKho.Size = new Size(253, 153);
        lblTonKho.TabIndex = 1;
        lblTonKho.Text = "--";
        lblTonKho.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelTheBaoTri
        // 
        panelTheBaoTri.BackColor = Color.FromArgb(124, 58, 237);
        panelTheBaoTri.Controls.Add(lblBaoTri);
        panelTheBaoTri.Controls.Add(lblTieuDeBaoTri);
        panelTheBaoTri.Dock = DockStyle.Fill;
        panelTheBaoTri.Location = new Point(327, 237);
        panelTheBaoTri.Margin = new Padding(10);
        panelTheBaoTri.Name = "panelTheBaoTri";
        panelTheBaoTri.Padding = new Padding(22, 18, 22, 18);
        panelTheBaoTri.Size = new Size(297, 208);
        panelTheBaoTri.TabIndex = 4;
        // 
        // lblTieuDeBaoTri
        // 
        lblTieuDeBaoTri.AutoSize = true;
        lblTieuDeBaoTri.Dock = DockStyle.Top;
        lblTieuDeBaoTri.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTieuDeBaoTri.ForeColor = Color.FromArgb(241, 245, 249);
        lblTieuDeBaoTri.Location = new Point(22, 18);
        lblTieuDeBaoTri.Name = "lblTieuDeBaoTri";
        lblTieuDeBaoTri.Size = new Size(126, 19);
        lblTieuDeBaoTri.TabIndex = 0;
        lblTieuDeBaoTri.Text = "CHI PHÍ BẢO TRÌ";
        // 
        // lblBaoTri
        // 
        lblBaoTri.Dock = DockStyle.Fill;
        lblBaoTri.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblBaoTri.ForeColor = Color.White;
        lblBaoTri.Location = new Point(22, 37);
        lblBaoTri.Name = "lblBaoTri";
        lblBaoTri.Size = new Size(253, 153);
        lblBaoTri.TabIndex = 1;
        lblBaoTri.Text = "--";
        lblBaoTri.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // FrmThongKe
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(241, 245, 249);
        ClientSize = new Size(1000, 580);
        Controls.Add(tableChiSo);
        Controls.Add(panelTieuDe);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(800, 480);
        Name = "FrmThongKe";
        Text = "Báo cáo thống kê";
        Load += FrmThongKe_Load;
        panelTieuDe.ResumeLayout(false);
        panelTieuDe.PerformLayout();
        tableChiSo.ResumeLayout(false);
        panelTheThanhVien.ResumeLayout(false);
        panelTheThanhVien.PerformLayout();
        panelTheDoanhThu.ResumeLayout(false);
        panelTheDoanhThu.PerformLayout();
        panelTheCongNo.ResumeLayout(false);
        panelTheCongNo.PerformLayout();
        panelTheTonKho.ResumeLayout(false);
        panelTheTonKho.PerformLayout();
        panelTheBaoTri.ResumeLayout(false);
        panelTheBaoTri.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelTieuDe;
    private Label lblTieuDe;
    private Label lblMoTa;
    private TableLayoutPanel tableChiSo;
    private Panel panelTheThanhVien;
    private Label lblTieuDeThanhVien;
    private Panel panelTheDoanhThu;
    private Label lblTieuDeDoanhThu;
    private Panel panelTheCongNo;
    private Label lblTieuDeCongNo;
    private Panel panelTheTonKho;
    private Label lblTieuDeTonKho;
    private Panel panelTheBaoTri;
    private Label lblTieuDeBaoTri;
    private Label lblThanhVien;
    private Label lblDoanhThu;
    private Label lblCongNo;
    private Label lblTonKho;
    private Label lblBaoTri;
}

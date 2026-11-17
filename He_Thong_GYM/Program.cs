namespace He_Thong_GYM
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            if (args.Length > 0 && args[0] == "--check-ui")
            {
                Helpers.UiInspector.Run();
                return;
            }

            using var frmLogin = new Forms.FrmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK && Helpers.SessionManager.NguoiDungHienTai is not null)
            {
                Application.Run(new Form1(Helpers.SessionManager.NguoiDungHienTai));
            }
        }
    }
}

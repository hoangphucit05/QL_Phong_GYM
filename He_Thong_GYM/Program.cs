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

            while (true)
            {
                using var frmLogin = new Forms.FrmLogin();
                if (frmLogin.ShowDialog() != DialogResult.OK || Helpers.SessionManager.NguoiDungHienTai is null)
                {
                    break;
                }

                using var formChinh = new Form1(Helpers.SessionManager.NguoiDungHienTai);
                Application.Run(formChinh);

                if (Helpers.SessionManager.NguoiDungHienTai is not null)
                {
                    break;
                }
            }
        }
    }
}

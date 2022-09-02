namespace QuanLyPhongNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using QuanLyPhongNet.Common;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            QuanLyPhongNet.Common.Properties.Settings.Default.strConnect = "";
            QuanLyPhongNet.Common.Properties.Settings.Default.Save();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (string.IsNullOrEmpty(QuanLyPhongNet.Common.Properties.Settings.Default.strConnect) || !new SqlData().TestConnection())
            {
                Application.Run(new DatabaseConfigurationGUI());
            }
            else
            {
                Application.Run(new OptionGUI());
            }
        }
    }
}

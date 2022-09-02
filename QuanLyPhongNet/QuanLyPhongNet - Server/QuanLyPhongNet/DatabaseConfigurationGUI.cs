using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.Common;


namespace QuanLyPhongNet
{
    public partial class DatabaseConfigurationGUI : Form
    {
        public DatabaseConfigurationGUI()
        {
            InitializeComponent();
        }

        private void ExitEventHandler(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChkValidateWindowCheckedChangedEventHandler(object sender, EventArgs e)
        {
            if (chkValidateWindow.Checked)
            {
                txtUserID.Enabled = false;
                txtPass.Enabled = false;
                QuanLyPhongNet.Common.Properties.Settings.Default.ConnectType = true;
            }
            else
            {
                txtPass.Enabled = true;
                txtUserID.Enabled = true;
                QuanLyPhongNet.Common.Properties.Settings.Default.ConnectType = false;
            }
        }

        private void DatabaseConfigurationGUILoadEventHandler(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            QuanLyPhongNet.Common.Properties.Settings.Default.ConnectType = true;
            MessageBox.Show("Bạn hãy chắc chắn đã bật Server Browser của SQL trước thì mới tải được Server lên","Nhắc Nhở",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void ConnectEventHandler(object sender, EventArgs e)
        {
            string strConnect = "";
            if (chkValidateWindow.Checked)
            {
                strConnect = string.Format(@"Server={0};Database=QuanLyPhongNet;Trusted_Connection=True;", cboServer.Text);
            }
            else
            {
                strConnect = string.Format(@"Server={0};Database=QuanLyPhongNet;User ID={1};Password={2};", cboServer.Text, txtUserID.Text, txtPass.Text);
            }
            QuanLyPhongNet.Common.Properties.Settings.Default.strConnect = strConnect;
            if (!new SqlData().TestConnection())
            {
                MessageBox.Show("Kết nối thất bại!\r\nHãy thử lại!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Kết nối thành công!");
                QuanLyPhongNet.Common.Properties.Settings.Default.severConnect = cboServer.SelectedItem.ToString();
                if (QuanLyPhongNet.Common.Properties.Settings.Default.ConnectType==false)
                {
                    QuanLyPhongNet.Common.Properties.Settings.Default.userConnect = txtUserID.Text;
                    QuanLyPhongNet.Common.Properties.Settings.Default.passConnect = txtPass.Text;
                }
                QuanLyPhongNet.Common.Properties.Settings.Default.Save();
                LoginGUI frmLogin = new LoginGUI();
                this.Hide();
                frmLogin.ShowDialog();

            }
        }

        private void CboServerDropDownEventHandler(object sender, EventArgs e)
        {
            if (cboServer.Items.Count==0)
            {
                LoadingGUI frmLoading = new LoadingGUI();
                frmLoading.ShowDialog();
                while (!frmLoading.IsFinish) { }
                cboServer.Items.AddRange(frmLoading.Servers.ToArray());

            }
        }
    }
}

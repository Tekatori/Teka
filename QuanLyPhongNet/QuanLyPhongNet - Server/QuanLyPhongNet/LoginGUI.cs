using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.DTO;
using QuanLyPhongNet.DAO;

namespace QuanLyPhongNet
{
    public partial class LoginGUI : Form
    {
        public LoginGUI()
        {
            InitializeComponent();
            loadcb();
        }
        QuanLyPhongNETDataContext qlpn = new QuanLyPhongNETDataContext();
        private void LoginEventHandler(object sender, EventArgs e)
        {

            if (cboUser.Text.Equals("") || cboUser.Text.Equals("--Lựa Chọn--"))
            {
                MessageBox.Show("Chưa chọn loại tài khoản! Vui lòng chọn!");
                cboUser.Select();
            }
            else if (txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Chưa nhập mật khẩu! Vui lòng nhập vào");
                txtPassword.Select();
            }
            else
            {
                var findname = qlpn.TheUsers.Where(t=>t.UserName == cboUser.Text).FirstOrDefault();
                if ( findname.Password == txtPassword.Text && findname.Type =="Staff" )
                {
                    HomeGUI frmHome = new HomeGUI();
                    this.Hide();
                    frmHome.ShowDialog();
                }
                else if (findname.Password == txtPassword.Text && findname.Type == "Manager")
                {
                    OptionGUI frmOption = new OptionGUI();
                    this.Hide();
                    frmOption.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }


        }

        private void LoginGUILoadEventHandler(object sender, EventArgs e)
        {
            //this.Location = new Point(100, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
      
        }
        private void loadcb()
        {
            var user = from p in qlpn.TheUsers select p;
            cboUser.DataSource = user;
            cboUser.DisplayMember = "UserName";
        }
        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

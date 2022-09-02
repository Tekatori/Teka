using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.DAO;
namespace QuanLyPhongNet
{
    public partial class AddMemberGUI : Form
    {
        public AddMemberGUI()
        {
            InitializeComponent();
        }

        private void AddMemberGUILoadEventHandler(object sender, EventArgs e)
        {
            //this.Location = new Point(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void CancelClickEventHandler(object sender, EventArgs e)
        {
            HomeGUI frmHome = new HomeGUI();
            this.Close();
            frmHome.ShowDialog();
        }

        private void OKClickEventHandler(object sender, EventArgs e)
        {
            txtAddMoney.ReadOnly = false;

            int h = int.Parse(txtAddMoney.Text) / 5000;
            int du = int.Parse(txtAddMoney.Text) % 5000;
            int p = 0;
            p = (du / 5000)*60;
            TimeSpan time  = new TimeSpan(h, p, 0);
            ProcessMember mb = new ProcessMember();
            mb.InsertMember(txtName.Text, txtPass.Text, "Hội Viên", time,float.Parse(txtAddMoney.Text), "Cho Phép");

            HomeGUI frmHome = new HomeGUI();
            this.Hide();
            frmHome.ShowDialog();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

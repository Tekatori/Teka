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
    public partial class AddTimeMemberGUI : Form
    {
        public delegate void SendMessage(string Message);
        public SendMessage Sender;
        QuanLyPhongNETDataContext ql = new QuanLyPhongNETDataContext();
        public AddTimeMemberGUI()
        {
            InitializeComponent();
            Sender = new SendMessage(GetMessage);
        }
        string name = "";
        private void GetMessage(string Message)
        {
            name = Message;
        }
        private void AddTimeMemberGUILoadEventHandler(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            ProcessMember   member = new ProcessMember();
            int id = member.findidMember(name);
            
            var find = ql.Members.Single(x => x.MemberID == id );

            txtName.Text = find.AccountName.ToString();
            txtPass.Text = find.Password.ToString();
            txtCon.Text = find.CurrentMoney.ToString();

        }

        private void CancelClickEventHandler(object sender, EventArgs e)
        {
            HomeGUI frmHome = new HomeGUI();
            this.Close();
            frmHome.ShowDialog();
        }

        private void OKClickEventHandler(object sender, EventArgs e)
        {
            ProcessMember processMember = new ProcessMember();
            int id = processMember.findidMember(name);
            var find = ql.Members.Single(x => x.MemberID == id);
            find.Password = txtPass.Text;
            
            find.CurrentMoney = int.Parse(txtCon.Text)+ int.Parse(txtAddMoney.Text);
            int sum = int.Parse(txtCon.Text) + int.Parse(txtAddMoney.Text);

            int h = sum / 5000;
            int du = sum % 5000;
            int p;
            p = (du / 5000) * 60;
            TimeSpan time = new TimeSpan(h, p, 0);
            find.CurrentTime = time;
            ql.SubmitChanges();
           

            HomeGUI frmHome = new HomeGUI();
            this.Close();
            frmHome.ShowDialog();
        }
    }
}

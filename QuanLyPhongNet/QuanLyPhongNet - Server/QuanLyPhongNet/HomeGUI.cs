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
using System.Threading;
using QuanLyPhongNet.DAO;
namespace QuanLyPhongNet
{
    public partial class HomeGUI : Form
    {
        

        QuanLyPhongNETDataContext qlpn = new QuanLyPhongNETDataContext();
        public HomeGUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            timerHome.Interval = 1000;
            timerHome.Enabled = true;
            timerHome.Start();
           
        }

        private void HomeGUILoadEventHandler(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet1.TransacDiary' table. You can move, or remove it, as needed.
            this.transacDiaryTableAdapter.Fill(this.quanLyPhongNetDataSet1.TransacDiary);
            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet.SystemDiary' table. You can move, or remove it, as needed.
            this.systemDiaryTableAdapter.Fill(this.quanLyPhongNetDataSet.SystemDiary);
            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet3.TransacDiary' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet1.SystemDiary' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet.PayClient' table. You can move, or remove it, as needed.
            //this.payClientTableAdapter.Fill(this.quanLyPhongNetDataSet.PayClient);
            //this.Location = new Point(550, 500);
            LoadSourceToDRGV();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }
        private void loadnk()
        {
            var pay = from p in qlpn.TransacDiaries select p;
            DgvNK.DataSource = pay;
            DgvNK.Columns[0].HeaderText = "Máy";
            DgvNK.Columns[1].HeaderText = "Thời Gian";
            DgvNK.Columns[2].HeaderText = "Thời Gian Bắt đầu";
            DgvNK.Columns[3].HeaderText = "Thời Gian Sử dụng";
            DgvNK.Columns[4].HeaderText = "Tiền sử dụng";
            DgvNK.Columns[5].HeaderText = "Tổng Tiền phải trả";

          
            DgvNK.Columns[4].DefaultCellStyle.Format = "c";
            DgvNK.Columns[5].DefaultCellStyle.Format = "c";
        }
        private void loadnksystem()
        {
            var sysnk = from p in qlpn.SystemDiaries select p;
            DGVSYS.DataSource = sysnk;
            DGVSYS.Columns[0].HeaderText = "Máy Sử Dụng";
            DGVSYS.Columns[1].HeaderText = "Thời Gian";
            DGVSYS.Columns[2].HeaderText = "Thời Gian thêm";
            DGVSYS.Columns[3].HeaderText = "Thêm Tiền";
            DGVSYS.Columns[4].HeaderText = "Ghi Chú";
            DgvNK.Columns[3].DefaultCellStyle.Format = "c";
        }
        private void LinkLabelLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI frmLogin = new LoginGUI();
            this.Hide();
            frmLogin.ShowDialog();
        }
        public void LoadClient()
        {
            ProcessClient processClient = new ProcessClient();
            var may = processClient.LoadAllClients();
            drgvClient.DataSource = may;

        }

        private void TimeHomeTickEventHandler(object sender, EventArgs e)
        {
          
          
        }

        private void LoadSourceToDRGV()
        {
            ProcessFood f = new ProcessFood();
            var food = f.LoadAllFoods();

            drgvFood.DataSource = food;
            drgvFood.Columns[0].HeaderText = "Mã Định Danh";
            drgvFood.Columns[1].HeaderText = "Tên Món Ăn";
            drgvFood.Columns[2].HeaderText = "Thuộc Loại";
            drgvFood.Columns[3].HeaderText = "Đơn Giá";
            drgvFood.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvFood.Columns[5].HeaderText = "Số Lượng Tồn";

            ProcessDrink d = new ProcessDrink();
            var drink = d.LoadAllDrinks();
            drgvDrink.DataSource = drink;
            drgvDrink.Columns[0].HeaderText = "Mã Định Danh";
            drgvDrink.Columns[1].HeaderText = "Tên Món Ăn";
            drgvDrink.Columns[2].HeaderText = "Thuộc Loại";
            drgvDrink.Columns[3].HeaderText = "Đơn Giá";
            drgvDrink.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvDrink.Columns[5].HeaderText = "Số Lượng Tồn";

            ProcessCard c = new ProcessCard();
            var card = c.LoadAllCards();
            drgvCard.DataSource = card;
            drgvCard.Columns[0].HeaderText = "Mã Định Danh";
            drgvCard.Columns[1].HeaderText = "Tên Món Ăn";
            drgvCard.Columns[2].HeaderText = "Thuộc Loại";
            drgvCard.Columns[3].HeaderText = "Đơn Giá";
            drgvCard.Columns[4].HeaderText = "Đơn Vị Tính";
            drgvCard.Columns[5].HeaderText = "Số Lượng Tồn";

            ProcessMember mb = new ProcessMember();
            var member = mb.LoadAllMembers();
            drgvMember.DataSource = member;
            //drgvMember.Columns[0].HeaderText = "STT";
            drgvMember.Columns[0].HeaderText = "Tên Tài Khoản";
            drgvMember.Columns[1].HeaderText = "Mật Khẩu";
            drgvMember.Columns[2].HeaderText = "Thuộc Nhóm";
            drgvMember.Columns[3].HeaderText = "Thời Gian Hiện Có";
            drgvMember.Columns[4].HeaderText = "Số Tiền Hiện Có";
            drgvMember.Columns[5].HeaderText = "Trạng Thái";


            ProcessUser processUser = new ProcessUser();
            var us = processUser.LoadAllUsers();
            drgvStaff.DataSource = us;

          

            ProcessClient processClient = new ProcessClient();
            var cl = processClient.LoadAllClients();
            drgvClient.DataSource = cl;

            ProcessGroupClient processGroupClient = new ProcessGroupClient();
            var grc = processGroupClient.LoadAllGroupClients();
            drgvClientGroup.DataSource = grc;

            ProcessGroupUser grcUser = new ProcessGroupUser();
            var gu = grcUser.LoadAllGroupUsers();
            drgvUserGroup.DataSource = gu;
           
            loadnk();
            loadnksystem();

        }
  
        private void PicCalculateMoneyMouseHoverEventHandler(object sender, EventArgs e)
        {
            picCalculateMoney.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PicCalculateMoneyMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picCalculateMoney.BorderStyle = BorderStyle.None;
        }

        private void PicOpenClientMouseHoverEventHandler(object sender, EventArgs e)
        {
            picOpenClient.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PicOpenClientMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picOpenClient.BorderStyle = BorderStyle.None;
        }

        private void PicOpenClientEventHandler(object sender, EventArgs e)
        {

            string tt = drgvClient.SelectedCells[2].Value.ToString();
            

            TrangThai = tt;
            if (TrangThai == "Disconnect"|| TrangThai == "DISCONNECT")
            {
                MessageBox.Show("làm ơn hãy kết nối máy với máy chủ");

                TrangThai = "Disconnect";
            }
            else
            if (TrangThai == "Online")
            {
                MessageBox.Show("máy đang được sử dụng");

                TrangThai = "Online";
            }
            else
            {
                string may = drgvClient.SelectedCells[0].Value.ToString();
                string grcl = drgvClient.SelectedCells[1].Value.ToString();
                string note = drgvClient.SelectedCells[3].Value.ToString();

                ProcessClient cle = new ProcessClient();
                cle.UpdateClient(may, grcl, "Online", note);

             
              



                MessageBox.Show("đã Mở máy " + may + " thành công!!");

                ProcessBill processBill = new ProcessBill();
                DateTime d1 = DateTime.Now;
                TimeSpan d2 = new TimeSpan(d1.Hour, d1.Minute, d1.Second);
                processBill.CreateNewBill(may, d1,d2, 2000);
                


                // nhat ky

                SystemDiary nk = new SystemDiary();
                nk.ClientName = may;
                nk.Note = "Mở Máy";
                nk.TransacDate = DateTime.Now;
                //nk.TimeDay = d2;
                qlpn.SystemDiaries.InsertOnSubmit(nk);
                qlpn.SubmitChanges();
                LoadSourceToDRGV();
            }
            LoadSourceToDRGV();

            TrangThai = "Online";
        }
        static string TrangThai = "";
        private void PicCalculateMoneyEventhandler(object sender, EventArgs e)
        {
            string loaim = drgvClient.SelectedCells[1].Value.ToString();


            string cl = drgvClient.SelectedCells[0].Value.ToString();
            var tt = drgvClient.SelectedCells[2].Value.ToString();

            TrangThai = tt;

            if (TrangThai == "Online")
            {

                ProcessBill processBill = new ProcessBill();
                int idbill = processBill.findidBill(cl);

                var bill = qlpn.Bills.Single(t => t.BillID == idbill);

                float money = float.Parse(bill.PriceTotal.ToString());
                DateTime d1 = DateTime.Now;

                TimeSpan d2 =bill.StartTime.Value;

                DateTime timeuse = d1 - d2;


                int h = timeuse.Hour;
                int p = timeuse.Minute;
                int m = timeuse.Second;

                int tongs = h * 60 * 60 + p * 60 + m;
                int tongs1h = 3600;//1h
                float tien = 0;
                if (tongs > 0)
                {
                    tien = (float)tongs / tongs1h;
                }
                float full = 0;

                if (loaim== "Thi Đấu")
                {
                    full = tien * 10000;
                }
                else if (loaim == "Máy Lạnh")
                {
                    full = tien * 6000;
                }
                else if(loaim == "VIP")
                {
                    full = tien * 7000;
                }
                else
                {
                    full = tien * 5000;
                }
                    


                float tongtien = full + money;
                int lamtron = (int)tongtien;

                txtTotalPrice.Text = tongtien.ToString();
                MessageBox.Show("Tong Tien may " + cl + " la: " + lamtron.ToString("#,#") + " VND tong thoi gian su dung: " + h + " :" + p + " :" + h + "");

                //update order
                try
                {
                    var timorfood = qlpn.OrderFoods.SingleOrDefault(t => t.ClientName == cl.ToString());
                    qlpn.OrderFoods.DeleteOnSubmit(timorfood);
                    qlpn.SubmitChanges();
                }
                catch
                {
                    try {
                        var timordrinks = qlpn.OrderDrinks.SingleOrDefault(t => t.ClientName == cl.ToString());
                        qlpn.OrderDrinks.DeleteOnSubmit(timordrinks);
                        qlpn.SubmitChanges();
                    }
                    catch
                    {
                        try
                        {
                            var timorcard = qlpn.OrderCards.SingleOrDefault(t => t.ClientName == cl.ToString());
                            qlpn.OrderCards.DeleteOnSubmit(timorcard);
                            qlpn.SubmitChanges();
                        }
                        catch
                        {

                        }
                    }
                }

                //update pay
                TransacDiary transacDiary = new TransacDiary();
                transacDiary.ClientName = cl;
                transacDiary.StartTime = d2;
                transacDiary.TransacTime = d1;


                TimeSpan d3 = new TimeSpan(h, p, m);
                transacDiary.UseTime = d3;
                transacDiary.PriceUnit = full;
                transacDiary.TotalMoney = float.Parse(txtTotalPrice.Text);
                qlpn.TransacDiaries.InsertOnSubmit(transacDiary);
                qlpn.SubmitChanges();

                //update lai may
                string may = drgvClient.SelectedCells[0].Value.ToString();
                string grcl = drgvClient.SelectedCells[1].Value.ToString();
                string note = drgvClient.SelectedCells[3].Value.ToString();
                ProcessClient client = new ProcessClient();
                client.UpdateClient(may, grcl, "Offline", note);

                //xoa bill
                processBill.DeleteBill(idbill);

                //

                SystemDiary nk = new SystemDiary();
                nk.ClientName = cl;
                nk.Note = "Thanh Toán";

                TimeSpan timea = new TimeSpan(d1.Hour, d1.Minute, d1.Second);
                nk.TransacDate = DateTime.Now;
                //nk.TimeDay = timea;
                qlpn.SystemDiaries.InsertOnSubmit(nk);
                qlpn.SubmitChanges();
                LoadSourceToDRGV();
                TrangThai = "Offline";


            }
            else if (TrangThai == "Offline")
            {
                MessageBox.Show("Máy Đang offline");
                TrangThai = "Offline";
            }
            else
            {
                MessageBox.Show("Máy đang disconnect");
                TrangThai = "Disconnect";
            }
        }

         private void picOrder_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void picOrder_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void picOrderEventHandler(object sender, EventArgs e)
        {
            
        }
		
		 private void drgvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string iddv = drgvFood.SelectedCells[0].Value.ToString();
            string loai = "Foods";
            string may =  drgvClient.SelectedCells[0].Value.ToString();
            OrderServiceGUI frmOrder = new OrderServiceGUI();
            frmOrder.Sender(iddv,loai,may);
            this.Hide();
            frmOrder.ShowDialog();
        }
		
		private void drgvCard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderServiceGUI frmOrder = new OrderServiceGUI();
            frmOrder.ShowDialog();
        }
		
		private void picAddMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddMemberGUI frmAddMember = new AddMemberGUI();
            frmAddMember.ShowDialog();
            
        }
		
		private void drgvMember_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddTimeMemberGUI frmAddTime = new AddTimeMemberGUI();
            frmAddTime.ShowDialog();
        }
		
		private void picUpdateMember_Click(object sender, EventArgs e)
        {
            try
            {
                string mb = drgvMember.SelectedCells[0].Value.ToString();
                this.Hide();
                AddTimeMemberGUI frmAddTime = new AddTimeMemberGUI();
                frmAddTime.Sender(mb);
                frmAddTime.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vì " + ex.Message);
            }
        }
		
		private void picAddMember_MouseHover(object sender, EventArgs e)
        {
            picAddMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picAddMember_MouseLeave(object sender, EventArgs e)
        {
            picAddMember.BorderStyle = BorderStyle.None;
        }

        private void picUpdateMember_MouseHover(object sender, EventArgs e)
        {
            picUpdateMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picUpdateMember_MouseLeave(object sender, EventArgs e)
        {
            picUpdateMember.BorderStyle = BorderStyle.None;
        }

        private void picDeleteMember_MouseHover(object sender, EventArgs e)
        {
            picDeleteMember.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picDeleteMember_MouseLeave(object sender, EventArgs e)
        {
            picDeleteMember.BorderStyle = BorderStyle.None;
        }

        private void PicLockClientClickEventHandler(object sender, EventArgs e)
        {

            string may = drgvClient.SelectedCells[0].Value.ToString();
            var tt = drgvClient.SelectedCells[2].Value.ToString();

            TrangThai = tt;
            if (TrangThai == "Offline")
            {
                string grcl = drgvClient.SelectedCells[1].Value.ToString();
                string note = drgvClient.SelectedCells[3].Value.ToString();
                ProcessClient client2 = new ProcessClient();
                client2.UpdateClient(may, grcl, "Disconnect", note);

                MessageBox.Show("đã tắt máy " + may + " thành công!!");


                SystemDiary nk = new SystemDiary();
                nk.ClientName = may;
                nk.Note = "Tắt Máy";
                nk.TransacDate = DateTime.Now;
                DateTime d1 = DateTime.Now;
                TimeSpan d2 = new TimeSpan(d1.Hour, d1.Minute, d1.Second);
                //nk.TimeDay = d2;
                qlpn.SystemDiaries.InsertOnSubmit(nk);
                qlpn.SubmitChanges();

                LoadSourceToDRGV();
                TrangThai = "Disconnect";
            }
            else if (TrangThai == "Offline")
            {
                MessageBox.Show("máy Đang ở offline");
                TrangThai = "Offline";
            }
            else
            {
                MessageBox.Show("máy Đang ở trang thái disconnect");
                TrangThai = "Disconnect";
            }    
        }

        private void drgvCard_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string iddv = drgvCard.SelectedCells[0].Value.ToString();
            string loai = "Cards";
            string may = drgvClient.SelectedCells[0].Value.ToString();
            OrderServiceGUI frmOrder = new OrderServiceGUI();
            frmOrder.Sender(iddv, loai,may);
            this.Hide();
            frmOrder.ShowDialog();
        }

        private void drgvDrink_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string iddv = drgvDrink.SelectedCells[0].Value.ToString();
            string loai = "Drinks";
            string may = drgvClient.SelectedCells[0].Value.ToString();
            OrderServiceGUI frmOrder = new OrderServiceGUI();
            frmOrder.Sender(iddv,loai,may);
            this.Hide();
            frmOrder.ShowDialog();
        }

        private void picDeletememberEventHandler(object sender, EventArgs e)
        {
            try
            {
                int curen = drgvMember.CurrentCell.RowIndex;
                string name = Convert.ToString(drgvMember.Rows[curen].Cells[0].Value.ToString());

                ProcessMember processMember = new ProcessMember();
                int id = processMember.findidMember(name);
                processMember.DeleteMember(id);

                LoadSourceToDRGV();
                MessageBox.Show("Xoá thành công", "Xoá Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vì " + ex.Message);
            }

        }

        private void drgvMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picShutdownClient_Click(object sender, EventArgs e)
        {
            string may = drgvClient.SelectedCells[0].Value.ToString();
            var tt = drgvClient.SelectedCells[2].Value.ToString();

            TrangThai = tt;

            if (TrangThai == "Disconnect")
            {

                string grcl = drgvClient.SelectedCells[1].Value.ToString();
                string note = drgvClient.SelectedCells[3].Value.ToString();

                ProcessClient client = new ProcessClient();
                client.UpdateClient(may, grcl, "Offline", note);




                MessageBox.Show("đã kết nối máy " + may + " thành công!!");


                SystemDiary nk = new SystemDiary();
                nk.ClientName = may;
                nk.Note = "kết nối Máy";
                nk.TransacDate = DateTime.Now;
                DateTime d1 = DateTime.Now;
                TimeSpan d2 = new TimeSpan(d1.Hour, d1.Minute, d1.Second);
                //nk.TimeDay = d2;
                qlpn.SystemDiaries.InsertOnSubmit(nk);
                qlpn.SubmitChanges();


                LoadSourceToDRGV();
                TrangThai = "Offline";
            }
            else if(TrangThai == "Offline")
            {
                MessageBox.Show("máy đang offline");
                TrangThai = "Offline";
            }
            else
            {
                MessageBox.Show("máy đang disconnect");
                TrangThai = "Disconnect";
            }    



        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void drgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void drgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void drgvDrink_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }

        
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ProcessGroupClient processGroupClient = new ProcessGroupClient();
            string clientName = drgvClientGroup.CurrentRow.Cells[0].Value.ToString();
            processGroupClient.DeleteGroupClient(clientName);
            LoadSourceToDRGV();
        }
        private void drgvClientGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            
        }

        private void drgvUserGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabClient_Click(object sender, EventArgs e)
        {

        }

        private void tabTransactionDiary_Click(object sender, EventArgs e)
        {

        }

        private void dtp_Date_sys_ValueChanged(object sender, EventArgs e)
        {
            DateTime day = dtp_Date_sys.Value.Date;
       
            var sysnk = from p in qlpn.SystemDiaries select p;
            var ds = sysnk.Where(p => p.TransacDate == day).ToList();


            DGVSYS.DataSource = ds;
        }

        private void dateTimePicker_trans_ValueChanged(object sender, EventArgs e)
        {
            DateTime day = dateTimePicker_trans.Value.Date;

            var sysnk = from p in qlpn.TransacDiaries select p;
            var ds = sysnk.Where(p => p.TransacTime == day).ToList();
            DgvNK.DataSource = ds;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadSourceToDRGV();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LoadSourceToDRGV();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
          
            pictureBox1.BorderStyle = BorderStyle.None;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            
            pictureBox2.BorderStyle = BorderStyle.None;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            
           
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
        }
    }
}

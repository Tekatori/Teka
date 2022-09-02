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
    public partial class OrderServiceGUI : Form
    {
        public delegate void SendMessage(string Message,string loai,string may);
        public SendMessage Sender;
        QuanLyPhongNETDataContext qlpn = new QuanLyPhongNETDataContext();
        public OrderServiceGUI()
        {
            InitializeComponent();
            
            Sender = new SendMessage(GetMessage);
        }    
        int iddv=0;
        string loaidv="";
        
        private void OrderServiceGUILoadEventHandler(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            txtclient.Text = mayv;
            LoadSourceToAllControls();
            if(loaidv == "Foods")
            {
                loaddatafood();
            }
            else if(loaidv =="Drinks")
            {
                loaddataDrink();
            }
            else
            {
                loaddataCard();
            }
                    
        }
        string mayv = "";
        private void GetMessage(string Message,string loai,string may)
        {
            iddv =int.Parse( Message);
            loaidv =loai;
            mayv = may;
        }
        private void LoadSourceToAllControls()
        {
           
        }
        private void loaddatafood()
        {

            var find = qlpn.Foods.Single(x => x.FoodID == iddv);
            txtName.Text = find.FoodName;
            txtPrice.Text = find.PriceUnit.ToString();
            txtUnit.Text = find.UnitLot;
            txtTonKho.Text = find.InventoryNumber.ToString();
            txtGroupName.Text = find.CategoryName;

        }
        private void loaddataDrink()
        {

            var find = qlpn.Drinks.Single(x => x.DrinkID == iddv);
            txtName.Text = find.DrinkName;
            txtPrice.Text = find.PriceUnit.ToString();
            txtUnit.Text = find.UnitLot;
            txtTonKho.Text = find.InventoryNumber.ToString();
            txtGroupName.Text = find.CategoryName;

        }
        private void loaddataCard()
        {

            var find = qlpn.TheCards.Single(x => x.CardID == iddv);
            txtName.Text = find.CardName;
            txtPrice.Text = find.PriceUnit.ToString();
            txtUnit.Text = find.UnitLot;
            txtTonKho.Text = find.InventoryNumber.ToString();
            txtGroupName.Text = find.CategoryName;

        }
        private void loadclient()
        {
            try
            {
               

            
            }
            catch
            {
                MessageBox.Show("Khong có máy nào mở");
            }

        }
        private void CancelClickEventHandler(object sender, EventArgs e)
        {
            HomeGUI frmHome = new HomeGUI();
            this.Hide();
            frmHome.ShowDialog();
        }

        private void OKClickEventHandler(object sender, EventArgs e)
        {


            Client client = new Client();
            string may =txtclient.Text;


            var find = qlpn.Clients.Single(x => x.ClientName == may);
            if(find.StatusClient == "Online")
            {
                if ((int)numericUpDown1.Value > 0)
                {
                    if (loaidv == "Foods")
                    {
                        try
                        {
                            var timdv = qlpn.OrderFoods.Single(x => x.ClientName == may);
                            if (timdv != null)
                            {
                                timdv.Quantity = timdv.Quantity + (int)numericUpDown1.Value;
                                timdv.PriceTotal = timdv.PriceTotal + (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                                qlpn.SubmitChanges();

                            }

                        }
                        catch
                        {
                            OrderFood orFood = new OrderFood();
                            orFood.ClientName = may;
                            orFood.FoodID = iddv;
                            orFood.Quantity = (int)numericUpDown1.Value;
                            orFood.PriceTotal = (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                            qlpn.OrderFoods.InsertOnSubmit(orFood);

                            ProcessFood processFood = new ProcessFood();
                            processFood.UpdateFoods(iddv, txtName.Text, txtGroupName.Text, float.Parse(txtPrice.Text), txtUnit.Text, int.Parse(txtTonKho.Text) - (int)numericUpDown1.Value);

                            qlpn.SubmitChanges();
                        }

                    }
                    else if (loaidv == "Drinks")
                    {
                        try
                        {
                            var timdv = qlpn.OrderDrinks.Single(x => x.ClientName == may);
                            if (timdv != null)
                            {
                                timdv.Quantity = timdv.Quantity + (int)numericUpDown1.Value;
                                timdv.PriceTotal = timdv.PriceTotal + (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                                qlpn.SubmitChanges();

                            }
                        }
                        catch
                        { 
                      
                            OrderDrink Dr = new OrderDrink();
                            Dr.ClientName = may;
                            Dr.DrinkID = iddv;
                            Dr.Quantity = (int)numericUpDown1.Value;
                            Dr.PriceTotal = (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                            qlpn.OrderDrinks.InsertOnSubmit(Dr);
                            qlpn.SubmitChanges();
                        
                        }
                    }
                    else
                    {
                        try
                        {
                            var timdv = qlpn.OrderCards.Single(x => x.ClientName == may);
                            if (timdv != null)
                            {
                                timdv.Quantity = timdv.Quantity + (int)numericUpDown1.Value;
                                timdv.PriceTotal = timdv.PriceTotal + (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                                qlpn.SubmitChanges();

                            }
                        }
                        catch
                        {
                            OrderCard orderCard = new OrderCard();
                            orderCard.ClientName = may;
                            orderCard.CardID = iddv;
                            orderCard.Quantity = (int)numericUpDown1.Value;
                            orderCard.PriceTotal = ((double)numericUpDown1.Value) * (int)numericUpDown1.Value;
                            qlpn.OrderCards.InsertOnSubmit(orderCard);
                            qlpn.SubmitChanges();
                        }
              
                    }
                    Bill bill = new Bill();
                    var timbill = qlpn.Bills.Single(t => t.ClientName == may);
                    timbill.PriceTotal = timbill.PriceTotal + (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                    qlpn.SubmitChanges();

                    SystemDiary nk = new SystemDiary();
                    nk.ClientName = may;
                    nk.Note = "Order " + (int)numericUpDown1.Value + " " + txtName.Text;
                    nk.TransacDate = DateTime.Now;
                    nk.AddMoney = (double.Parse(txtPrice.Text) * (int)numericUpDown1.Value);
                    //nk.TimeDay = d2;
                    qlpn.SystemDiaries.InsertOnSubmit(nk);
                    qlpn.SubmitChanges();

                    HomeGUI frmHome = new HomeGUI();
                    this.Hide();
                    frmHome.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Hãy Thêm Số Lượng");
                }
                

            }
            else
            {
                MessageBox.Show("Máy chưa được sử dụng");
            }



           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

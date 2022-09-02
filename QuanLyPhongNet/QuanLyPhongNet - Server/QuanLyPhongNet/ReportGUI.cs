using QuanLyPhongNet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.DAO;

namespace QuanLyPhongNet
{
    public partial class ReportGUI : Form
    {

        public ReportGUI()
        {
            InitializeComponent();
           
        }
        int i = 1;
        QuanLyPhongNETDataContext qlpn = new QuanLyPhongNETDataContext();
        private void btn_Report_Click(object sender, EventArgs e)
        {
            

                
            DateTime day = dtp_batdau.Value.Date;
            //var timngay = qlpn.PayClients.SingleOrDefault(t => t.DateDay == day.Date);
            DateTime day2 = dtp_kt.Value.Date;
            if (day > day2)
            {
                MessageBox.Show("ngay thu nhat phai nho hon ngay thu 2");
            }
            else
            {
                int lap = day2.Day - day.Day;
                DateTime daybd = day;
                for (int j = 0; j <= lap; j++)
                {

                    var pri = from o in qlpn.TransacDiaries
                              where o.TransacTime == daybd.Date
                              select o.TotalMoney;




                    var sum = (int)pri.ToList().Sum();


                    string[] arr = new string[3];
                    ListViewItem itm;
                    arr[0] = i.ToString();
                    if (sum == 0)
                    {
                        arr[1] = "0";
                    }
                    else
                    {
                        arr[1] = sum.ToString("#,#"); //ToString("#,#")
                    }

                    arr[2] = daybd.Date.Date.ToString("dd/MM/yyyy");
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                    i++;
                    daybd = daybd.AddDays(1);
                }
            }

        }

        private void dtp_Date_ValueChanged(object sender, EventArgs e)
        {
          
            



        }
       

        private void ReportGUI_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyPhongNetDataSet2.PayClient' table. You can move, or remove it, as needed.
            dtp_batdau.Format = DateTimePickerFormat.Custom;
            dtp_batdau.CustomFormat = "yyyy-MM-dd";
            dtp_kt.CustomFormat = "yyyy-MM-dd";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            //loadbc();

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            OptionGUI frmOption = new OptionGUI();
            this.Hide();
            frmOption.ShowDialog();
            
        }




        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 30)
            {
                this.Close();

                FormDuDoan form1 = new FormDuDoan();
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa thống kê đủ 30 ngày");
                ReportGUI formgui = new ReportGUI();
                this.Close();
                formgui.ShowDialog();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.Filter = "(*.txt)|*.txt";

            //string FileName = "..//Report//data.txt";
            //saveFile.FileName = FileName;
            //string path = saveFile.FileName;
            //for (int i = 0; i < listView1.Items.Count; i++)
            //{
            //    File.AppendAllText(path, listView1.Items[i].SubItems[1].Text + Environment.NewLine);
            //}
            //MessageBox.Show("Xuat Thanh cong!");

        }
        //private void loadbc()
        //{
        //    SaveFileDialog saveFile = new SaveFileDialog();
        //    saveFile.Filter = "(*.txt)|*.txt";

        //    string FileName = "..//Report//data.txt";
        //    saveFile.FileName = FileName;
        //    string path = saveFile.FileName;

        //    FileInfo filetodelete = new FileInfo(path);
        //    filetodelete.Delete();

        //}
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 30)
            {

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "(*.txt)|*.txt";

                string FileName = "..//Report//data.txt";
                saveFile.FileName = FileName;
                string path = saveFile.FileName;
                File.Delete(path);
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    File.AppendAllText(path, listView1.Items[i].SubItems[1].Text + Environment.NewLine);
                }
                
                MessageBox.Show("Xuat Thanh cong!");
            }
            else
            {
                MessageBox.Show("Dữ liệu đưa vào chỉ được 30 ngày");
                ReportGUI frmrp = new ReportGUI();
                //this.Hide();
                this.Close();
                frmrp.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel|*.xls", ValidateNames = true })
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
                int cell = 2;
                int row = 1;
                ws.Cells[row, 1] = "STT";
                ws.Cells[row, 2] = "Doanh thu";
                ws.Cells[row, 3] = "Ngày";
                foreach (ListViewItem lvi in listView1.Items)
                {
                    row = 1;
                    foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                    {
                        ws.Cells[cell, row] = lvs.Text;
                        row++;
                    }
                    cell++;
                }
            }
        }
    }
}

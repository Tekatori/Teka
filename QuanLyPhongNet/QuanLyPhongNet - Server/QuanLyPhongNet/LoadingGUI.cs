using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace QuanLyPhongNet
{
    public partial class LoadingGUI : Form
    {
        private string loadText;
        private BackgroundWorker bgWorker;
        
        public List<string> Servers { get; set; }

        public bool IsFinish { get; set; }

        public LoadingGUI()
        {
            InitializeComponent();
            Servers = new List<string>();
            IsFinish = false;
            loadText = "Đang lấy thông tin Server";
        }

        private void DoWordHandler(object sender,DoWorkEventArgs e)
        {
            bgWorker.ReportProgress(25);
            DataTable instance = SqlDataSourceEnumerator.Instance.GetDataSources();
            int rowCount = instance.Rows.Count;
            int currentRow = 0;
            foreach (DataRow row in instance.Rows)
            {
                string name = row["ServerName"].ToString();
                if (row["InstanceName"] != null && row["InstanceName"].ToString() != string.Empty)
                    name += string.Format(@"\{0}", row["InstanceName"]);
                Servers.Add(name);
                currentRow++;
                bgWorker.ReportProgress((currentRow / rowCount) * 75 + 25);
            }
        }

        private void ProgressChangedHandler(object sender,ProgressChangedEventArgs e)
        {
            prbLoading.Value = e.ProgressPercentage;
            lblPercent.Text = prbLoading.Value + "%";
            lblPercent.ForeColor = Color.Red;
            Application.DoEvents();
        }

        private void RunWorkerCompletedHandler(object sender,RunWorkerCompletedEventArgs e)
        {
            IsFinish = true;
            bgWorker.CancelAsync();
            timerLoading.Stop();
            MessageBox.Show("Kết nối thành công!");
            this.Close();
        }


        private void LoadingGUI_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(150, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += DoWordHandler;
            bgWorker.ProgressChanged += ProgressChangedHandler;
            bgWorker.RunWorkerCompleted += RunWorkerCompletedHandler;
            bgWorker.RunWorkerAsync();
            timerLoading.Start();
        }



        private void timerLoading_Tick(object sender, EventArgs e)
        {
            int length = loadText.Length;
            if (length == lblLoading.Text.Length)
                lblLoading.Text = loadText + "...";
            else if (length + 3 == lblLoading.Text.Length)
                lblLoading.Text = loadText + "......";
            else if (length + 6 == lblLoading.Text.Length)
                lblLoading.Text = loadText + ".........";
            else
                lblLoading.Text = loadText;
            lblLoading.ForeColor = Color.Blue;
        }
    }
}

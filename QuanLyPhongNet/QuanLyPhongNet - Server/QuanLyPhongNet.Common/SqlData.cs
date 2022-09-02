namespace QuanLyPhongNet.Common 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using System.Data.Sql;

    public class SqlData
    {
        private string strConnect;
        private SqlConnection cnn;

        public SqlData()
        {
            strConnect = QuanLyPhongNet.Common.Properties.Settings.Default.strConnect;
        }

        /// <summary>
        /// Kiem tra ket noi de su dung LINQ.
        /// </summary>
        /// <returns>Tra ve true neu mo ket noi thanh cong, false neu that bai.</returns>
        public bool TestConnection()
        {
            cnn = new SqlConnection(strConnect);
            try
            {
                cnn.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// ket noi de su dung ADO.net.
        /// </summary>
        /// <returns>Tra ve doi tuong SqlConnection.</returns>
        public SqlConnection Connect()
        {
            cnn = new SqlConnection(strConnect);
            cnn.Open();

            return cnn;
        }

        /// <summary>
        /// Dong ket noi.
        /// </summary>
        public SqlConnection DisConnect()
        {
            try
            {
                if (cnn != null)
                    cnn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
                cnn = null; 
            }
            return cnn;
        }

        public string StrConnect
        {
            get
            {
                return strConnect;
            }

            set
            {
                strConnect = value;
            }
        }

    }
}

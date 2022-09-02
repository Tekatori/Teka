namespace QuanLyPhongNet.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Member
    {
        private string accountName;
        private string password;
        private string groupUserName;
        private TimeSpan timeInAccount;
        private float currentMoney;
        private string status;
        //private int id;

        //public int Id { get => id; set => id = value; }

        public string AccountName
        {
            get
            {
                return accountName;
            }

            set
            {
                accountName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string GroupUserName
        {
            get
            {
                return groupUserName;
            }

            set
            {
                groupUserName = value;
            }
        }

        public TimeSpan TimeInAccount
        {
            get
            {
                return timeInAccount;
            }

            set
            {
                timeInAccount = value;
            }
        }

        public float CurrentMoney
        {
            get
            {
                return currentMoney;
            }

            set
            {
                currentMoney = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

       
    }
}

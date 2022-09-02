namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessMember
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;

        public ProcessMember()
        {
            objReader = new QuanLyPhongNETDataContext();
            objWriter = new QuanLyPhongNETDataContext();
        }

        public List<QuanLyPhongNet.DTO.Member> LoadAllMembers()
        {
            return (from member in objReader.Members
                    select new QuanLyPhongNet.DTO.Member
                    {                  
                        AccountName =member.AccountName,
                        Password=member.Password,
                        GroupUserName=member.GroupUser,
                        TimeInAccount=member.CurrentTime.Value,
                        CurrentMoney=(float)member.CurrentMoney,
                        Status=member.StatusAccount
                    }).ToList();             
        }

        public void InsertMember(string account, string pass, string groupUser, TimeSpan time, float money, string status)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                objWriter.Members.InsertOnSubmit(new Member
                {
                    AccountName = account,
                    Password = pass,
                    GroupUser = groupUser,
                    CurrentTime = time,
                    CurrentMoney = money,
                    StatusAccount = status
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateMember(int memberID,string account,string pass,string groupUser,TimeSpan time,float money,string status)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                Member objUpdate;
                objUpdate = objWriter.Members.FirstOrDefault(x => x.MemberID == memberID);
                objUpdate.AccountName = account;
                objUpdate.Password = pass;
                objUpdate.GroupUser = groupUser;
                objUpdate.CurrentTime = time;
                objUpdate.CurrentMoney = money;
                objUpdate.StatusAccount = status;
                objWriter.SubmitChanges();
            }
        }
        
        public void DeleteMember(int memberID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.Members.Single(x => x.MemberID == memberID);
                objWriter.Members.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
        public int findidMember(string account)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.Members.Single(x => x.AccountName == account);
                int id = int.Parse(objDelete.MemberID.ToString()); 
                return id;
               
            }
        }
    }
}

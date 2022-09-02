namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessUser
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;

        public ProcessUser()
        {
            objReader = new QuanLyPhongNETDataContext();
            objWriter = new QuanLyPhongNETDataContext();

        }

        public List<QuanLyPhongNet.DTO.User> LoadAllUsers()
        {
            return (from user in objReader.TheUsers
                    select new QuanLyPhongNet.DTO.User
                    {
                        UserName=user.UserName,
                        Type=user.Type,
                        GroupUserName=user.GroupUser,
                        PhoneNumber=user.PhoneNumber,
                        Email=user.Email
                    }).ToList();
        }

        public void UpdateUser(string name, string type, string groupUser, string phone, string email)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                TheUser objUpdate;
                if ((objUpdate = objWriter.TheUsers.FirstOrDefault(x => x.UserName.Equals(name))) == null)
                {
                    objWriter.TheUsers.InsertOnSubmit(new TheUser
                    {
                        UserName = name,
                        Type = type,
                        GroupUser = groupUser,
                        PhoneNumber = phone,
                        Email = email
                    });
                    objWriter.SubmitChanges();
                }
                else
                {
                    objUpdate.UserName = name;
                    objUpdate.Type = type;
                    objUpdate.GroupUser = groupUser;
                    objUpdate.PhoneNumber = phone;
                    objUpdate.Email = email;
                    objWriter.SubmitChanges();
                }
            }
        }

        public void DeleteUser(string userName)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.TheUsers.Single(x => x.UserName.Equals(userName));
                objWriter.TheUsers.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}

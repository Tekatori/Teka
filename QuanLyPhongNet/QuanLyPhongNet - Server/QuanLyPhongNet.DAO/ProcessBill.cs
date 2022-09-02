namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessBill
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        
        public ProcessBill()
        {
            objReader = new QuanLyPhongNETDataContext();
            objWriter = new QuanLyPhongNETDataContext();
        }

        public void CreateNewBill(/*int billID, */string userName,DateTime foundedDate,TimeSpan startTime,float priceTotal)
        {
            Bill bill = new Bill();
            //bill.BillID = billID;
            bill.ClientName = userName;
            bill.FoundedDate = foundedDate;
            bill.StartTime = startTime;
            bill.PriceTotal = priceTotal;
            objReader.Bills.InsertOnSubmit(bill);
            objReader.SubmitChanges();
        }
        public List<QuanLyPhongNet.DTO.Bill> LoadAllBills()
        {
            return (from bill in objReader.Bills
                    select new QuanLyPhongNet.DTO.Bill
                    {
                        //BillID =bill.BillID,
                        UserName=bill.ClientName,
                        FoundedDate=bill.FoundedDate.Value,
                        PriceTotal=(float)bill.PriceTotal
                    }).ToList();
        }
        public int findidBill(string name)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var bill = objWriter.Bills.Single(x => x.ClientName == name);
                int id = int.Parse(bill.BillID.ToString());
                return id;

            }
        }
        public void DeleteBill(int billID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.Bills.Single(x => x.BillID == billID);
                objWriter.Bills.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }


    }
}

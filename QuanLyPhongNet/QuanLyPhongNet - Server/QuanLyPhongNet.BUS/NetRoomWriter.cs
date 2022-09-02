namespace QuanLyPhongNet.BUS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.DTO;
    using QuanLyPhongNet.DAO;

    public class NetRoomWriter
    {
        private ProcessFood foodObjectWriter;
        private ProcessDrink drinkObjectWriter;
        private ProcessCard cardObjectWriter;
        private ProcessCategory categoryObjectWriter;
        private ProcessClient clientObjectWriter;
        private ProcessMember memberObjectWriter;
        private ProcessGroupClient groupClientObjectWriter;
        private ProcessGroupUser groupUserObjectWriter;
        private ProcessUser userObjectWriter;
        private ProcessBill billObjectWriter;

        public NetRoomWriter()
        {
            foodObjectWriter = new ProcessFood();
            drinkObjectWriter = new ProcessDrink();
            cardObjectWriter = new ProcessCard();
            categoryObjectWriter = new ProcessCategory();
            clientObjectWriter = new ProcessClient();
            memberObjectWriter = new ProcessMember();
            groupClientObjectWriter = new ProcessGroupClient();
            groupUserObjectWriter = new ProcessGroupUser();
            userObjectWriter = new ProcessUser();
            billObjectWriter = new ProcessBill();
        }

        public void InsertMember(string account, string pass, string groupUser, TimeSpan time, float money, string status)
        {
            memberObjectWriter.InsertMember(account, pass, groupUser, time, money, status);
        }

        public void InsertFood(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.InsertFood(name, categoryName, priceUnit, unitLot, inventoryNumber);
            
        }

        public void InsertDrink(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.InsertDrink(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void InsertCard(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.InsertCard(name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void InsertCategory(string name)
        {
            categoryObjectWriter.InsertCatergory(name);
        }

        public void UpdateMember(int memberID, string account, string pass, string groupUser, TimeSpan time, float money, string status)
        {
            memberObjectWriter.UpdateMember(memberID, account, pass, groupUser, time, money, status);
        }

        public void UpdateFood(int foodID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            foodObjectWriter.UpdateFoods(foodID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateDrink(int drinkID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            drinkObjectWriter.UpdateDrink(drinkID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateCard(int cardID, string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            cardObjectWriter.UpdateCard(cardID, name, categoryName, priceUnit, unitLot, inventoryNumber);
        }

        public void UpdateCategory(string categoryName)
        {
            categoryObjectWriter.UpdateCategory(categoryName);
        }

        public void UpdateClient(string name, string groupClientName, string status, string note)
        {
            clientObjectWriter.UpdateClient(name, groupClientName, status, note);
        }

        public void UpdateGroupClient(string groupClientName, string description)
        {
            groupClientObjectWriter.UpdateGroupClient(groupClientName, description);
        }

        public void UpdateGroupUser(string groupUserName, string typeName)
        {
            groupUserObjectWriter.UpdateGroupUser(groupUserName, typeName);
        }

        public void UpdateUser(string name, string type, string groupUser, string phone, string email)
        {
            userObjectWriter.UpdateUser(name, type, groupUser, phone, email);
        }

        public void DeleteMember(int memberID)
        {
            memberObjectWriter.DeleteMember(memberID);
        }

        public void DeleteFood(int foodID)
        {
            foodObjectWriter.DeleteFood(foodID);
        }

        public void DeleteDrink(int drinkID)
        {
            drinkObjectWriter.DeleteDrink(drinkID);
        }

        public void DeleteCard(int cardID)
        {
            cardObjectWriter.DeleteCard(cardID);
        }

        public void DeleteCategory(string categoryName)
        {
            categoryObjectWriter.DeleteCategory(categoryName);
        }

        public void DeleteClient(string clientName)
        {
            clientObjectWriter.DeleteClient(clientName);
        }

        public void DeleteGroupClient(string groupName)
        {
            groupClientObjectWriter.DeleteGroupClient(groupName);
        }

        public void DeleteGroupUser(string groupName)
        {
            groupUserObjectWriter.DeleteGroupUser(groupName);
        }

        public void DeleteUser(string userName)
        {
            userObjectWriter.DeleteUser(userName);
        }       
    }
}

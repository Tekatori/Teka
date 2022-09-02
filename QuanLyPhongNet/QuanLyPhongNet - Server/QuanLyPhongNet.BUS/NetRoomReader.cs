namespace QuanLyPhongNet.BUS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.DAO;

    public class NetRoomReader
    {
        private ProcessFood foodObjectReader;
        private ProcessDrink drinkObjectReader;
        private ProcessCard cardObjectReader;
        private ProcessCategory categoryObjectReader;
        private ProcessClient clientObjectReader;
        private ProcessMember memberObjectReader;
        private ProcessGroupClient groupClientObjectReader;
        private ProcessGroupUser groupUserObjectReader;
        private ProcessUser userObjectReader;
        private ProcessBill billObjectReader;

        public NetRoomReader()
        {
            foodObjectReader = new ProcessFood();
            drinkObjectReader = new ProcessDrink();
            cardObjectReader = new ProcessCard();
            categoryObjectReader = new ProcessCategory();
            clientObjectReader = new ProcessClient();
            memberObjectReader = new ProcessMember();
            groupClientObjectReader = new ProcessGroupClient();
            groupUserObjectReader = new ProcessGroupUser();
            userObjectReader = new ProcessUser();
            billObjectReader = new ProcessBill();
        }

        public List<QuanLyPhongNet.DTO.Food> GetAllFoods()
        {
            return foodObjectReader.LoadAllFoods();
        }

        public List<QuanLyPhongNet.DTO.Drink> GetAllDrinks()
        {
            return drinkObjectReader.LoadAllDrinks();
        }

        public List<QuanLyPhongNet.DTO.Card> GetAllCards()
        {
            return cardObjectReader.LoadAllCards();
        }

        public List<QuanLyPhongNet.DTO.Client> GetAllClient()
        {
            return clientObjectReader.LoadAllClients();
        }

        public List<QuanLyPhongNet.DTO.Category> GetAllCategorys()
        {
            return categoryObjectReader.LoadAllCategorys();
        }

        public List<QuanLyPhongNet.DTO.GroupClient> GetAllGroupClients()
        {
            return groupClientObjectReader.LoadAllGroupClients();
        }

        public List<QuanLyPhongNet.DTO.GroupUser> GetAllGroupUsers()
        {
            return groupUserObjectReader.LoadAllGroupUsers();
        }

        public List<QuanLyPhongNet.DTO.Member> GetAllMembers()
        {
            return memberObjectReader.LoadAllMembers();
        }

        public List<QuanLyPhongNet.DTO.User> GetAllUsers()
        {
            return userObjectReader.LoadAllUsers();
        }

        public List<QuanLyPhongNet.DTO.Bill> GetAllBills()
        {
            return billObjectReader.LoadAllBills();
        }
    }
}

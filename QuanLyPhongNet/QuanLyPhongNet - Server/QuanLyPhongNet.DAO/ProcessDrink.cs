namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessDrink
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;

        public ProcessDrink()
        {
            objReader = new QuanLyPhongNETDataContext();
            objWriter = new QuanLyPhongNETDataContext();
        }
        public List<QuanLyPhongNet.DTO.Drink> LoadAllDrinks()
        {
            return (from drink in objReader.Drinks
                    select new QuanLyPhongNet.DTO.Drink
                    {
                        DrinkID = drink.DrinkID,
                        Name = drink.DrinkName,
                        CategoryName = drink.CategoryName,
                        PriceUnit = (float)drink.PriceUnit,
                        UnitLot = drink.UnitLot,
                        InventoryNumber = (int)drink.InventoryNumber,
                    }).ToList();
        }

        public void InsertDrink(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                objWriter.Drinks.InsertOnSubmit(new Drink
                {
                    DrinkName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateDrink(int drinkID,string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                Drink objUpdate;
                objUpdate = objWriter.Drinks.FirstOrDefault(x => x.DrinkID == drinkID);
                objUpdate.DrinkName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SubmitChanges();
            }          
        }
        public void DeleteDrink(int drinkID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.Drinks.Single(x => x.DrinkID == drinkID);
                objWriter.Drinks.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}

namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.Common;
    using QuanLyPhongNet.DTO;

    public class ProcessFood
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;

        public ProcessFood()
        {
            objReader = new QuanLyPhongNETDataContext();
            objWriter = new QuanLyPhongNETDataContext();
        }

        public List<QuanLyPhongNet.DTO.Food> LoadAllFoods()
        {
            return (from food in objReader.Foods
                    select new QuanLyPhongNet.DTO.Food
                    {
                        FoodID = food.FoodID,
                        Name = food.FoodName,
                        CategoryName=food.CategoryName,
                        PriceUnit=(float)food.PriceUnit,
                        UnitLot=food.UnitLot,
                        InventoryNumber=(int)food.InventoryNumber,
                    }).ToList();
        }

        public void InsertFood(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                objWriter.Foods.InsertOnSubmit(new Food
                {
                    FoodName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateFoods(int foodID,string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                Food objUpdate;
                objUpdate = objWriter.Foods.FirstOrDefault(x => x.FoodID == foodID);
                objUpdate.FoodName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SubmitChanges();
            }
        }

        public void DeleteFood(int foodID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext())
            {
                var objDelete = objWriter.Foods.Single(x => x.FoodID == foodID);
                objWriter.Foods.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }     
    }
}

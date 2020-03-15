using AbstractFoodListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Food> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Kit> Products { get; set; }
        public List<KitFood> ProductComponents { get; set; }
        private DataListSingleton()
        {
            Components = new List<Food>();
            Orders = new List<Order>();
            Products = new List<Kit>();
            ProductComponents = new List<KitFood>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}

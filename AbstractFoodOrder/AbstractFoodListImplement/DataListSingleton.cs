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
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> MessageInfoes { get; set; }
        private DataListSingleton()
        {
            Components = new List<Food>();
            Orders = new List<Order>();
            Products = new List<Kit>();
            ProductComponents = new List<KitFood>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            MessageInfoes = new List<MessageInfo>();
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

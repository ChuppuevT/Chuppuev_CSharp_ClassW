using AbstractFoodFileImplement.Models;
using AbstractFoodOrderBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AbstractFoodFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string FoodFileName = "Food.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string KitFileName = "Kit.xml";
        private readonly string KitFoodFileName = "KitFood.xml";
        public List<Food> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Kit> Products { get; set; }
        public List<KitFood> ProductComponents { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Products = LoadProducts();
            ProductComponents = LoadProductComponents();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveProducts();
            SaveProductComponents();
        }
        private List<Food> LoadComponents()
        {
            var list = new List<Food>();
            if (File.Exists(FoodFileName))
            {
                XDocument xDocument = XDocument.Load(FoodFileName);
                var xElements = xDocument.Root.Elements("Food").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Food
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FoodName = elem.Element("FoodName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        KitId = Convert.ToInt32(elem.Element("KitId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Kit> LoadProducts()
        {
            var list = new List<Kit>();
            if (File.Exists(KitFileName))
            {
                XDocument xDocument = XDocument.Load(KitFileName);
                var xElements = xDocument.Root.Elements("Kit").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Kit
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        KitName = elem.Element("KitName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<KitFood> LoadProductComponents()
        {
            var list = new List<KitFood>();
            if (File.Exists(KitFoodFileName))
            {
                XDocument xDocument = XDocument.Load(KitFoodFileName);
                var xElements = xDocument.Root.Elements("KitFood").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new KitFood
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        KitId = Convert.ToInt32(elem.Element("KitId").Value),
                        FoodId = Convert.ToInt32(elem.Element("FoodId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Foods");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Food",
                    new XAttribute("Id", component.Id),
                    new XElement("FoodName", component.FoodName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(FoodFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("KitId", order.KitId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveProducts()
        {
            if (Products != null)
            {
                var xElement = new XElement("Kits");
                foreach (var product in Products)
                {
                    xElement.Add(new XElement("Kit",
                    new XAttribute("Id", product.Id),
                    new XElement("KitName", product.KitName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(KitFileName);
            }
        }
        private void SaveProductComponents()
        {
            if (ProductComponents != null)
            {
                var xElement = new XElement("KitComponents");
                foreach (var productComponent in ProductComponents)
                {
                    xElement.Add(new XElement("KitComponent",
                    new XAttribute("Id", productComponent.Id),
                    new XElement("KitId", productComponent.KitId),
                    new XElement("FoodId", productComponent.FoodId),
                    new XElement("Count", productComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(KitFoodFileName);
            }
        }
    }
}

using AbstractFoodFileImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractFoodFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order element;
            if (model.Id.HasValue)
            {
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec =>
               rec.Id) : 0;
                element = new Order { Id = maxId + 1 };
                source.Orders.Add(element);
            }
            element.KitId = model.KitId == 0 ? element.KitId : model.KitId;
            element.Count = model.Count;
            element.Sum = model.Sum;
            element.Status = model.Status;
            element.DateCreate = model.DateCreate;
            element.DateImplement = model.DateImplement;
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
           .Where(rec => model == null || rec.Id == model.Id || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
           .Select(rec => new OrderViewModel
           {
               Id = rec.Id,
               KitName = GetProductName(rec.KitId),
               Count = rec.Count,
               Sum = rec.Sum,
               Status = rec.Status,
               DateCreate = rec.DateCreate,
               DateImplement = rec.DateImplement
           })
            .ToList();
        }
        private string GetProductName(int id)
        {
            string name = "";
            var product = source.Products.FirstOrDefault(x => x.Id == id);
            name = product != null ? product.KitName : "";
            return name;
        }
    }
}

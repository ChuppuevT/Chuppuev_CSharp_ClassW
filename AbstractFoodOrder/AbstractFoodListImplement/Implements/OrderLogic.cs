using AbstractFoodListImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Enums;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;


namespace AbstractFoodListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;
        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };

            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }

            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }

                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.KitId = model.KitId;
            order.Count = model.Count;
            order.ClientId = model.ClientId;
            order.ImplementerId = model.ImplementerId;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.Sum = model.Sum;
            order.Status = model.Status;

            return order;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            foreach (var order in source.Orders)
            {
                if (model != null && order.Id == model.Id
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                    || (model.ClientId == model.ClientId)
                    || (model.FreeOrders.HasValue && model.FreeOrders.Value)
                    || (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется))
                {
                    result.Add(CreateViewModel(order));
                    break;
                }

                result.Add(CreateViewModel(order));
            }

            return result;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            string productName = null;

            foreach (var product in source.Products)
            {
                if (product.Id == order.KitId)
                {
                    productName = product.KitName;
                }
            }

            if (productName == null)
            {
                throw new Exception("Продукт не найден");
            }
            string clientFio = null;

            foreach (var client in source.Clients)
            {
                if (client.Id == order.ClientId)
                {
                    clientFio = client.FIO;
                }
            }

            if (clientFio == null)
            {
                throw new Exception("Клиент не найден");
            }

            string implementerFio = null;
            if (order.ImplementerId.HasValue)
            {
                foreach (var implement in source.Implementers)
                {
                    if (implement.Id == order.ImplementerId)
                    {
                        clientFio = implement.ImplementerFIO;
                    }
                }
            }

            if (implementerFio == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                KitId = order.KitId,
                ClientFIO = clientFio,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = implementerFio,
                KitName = productName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };

        }
    }
}

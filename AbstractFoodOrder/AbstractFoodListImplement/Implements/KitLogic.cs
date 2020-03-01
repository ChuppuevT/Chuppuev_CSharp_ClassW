using AbstractFoodListImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodListImplement.Implements
{
    public class KitLogic : IKitLogic
    {
        private readonly DataListSingleton source;
        public KitLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(KitBindingModel model)
        {
            Kit tempProduct = model.Id.HasValue ? null : new Kit { Id = 1 };
            foreach (var product in source.Products)
            {
                if (product.KitName == model.KitName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Products.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(KitBindingModel model)
        {
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].KitId == model.Id)
                {
                    source.ProductComponents.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Kit CreateModel(KitBindingModel model, Kit product)
        {
            product.KitName = model.KitName;
            product.Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].Id > maxPCId)
                {
                    maxPCId = source.ProductComponents[i].Id;
                }
                if (source.ProductComponents[i].KitId == product.Id)
                {
                    if
                    (model.KitComponents.ContainsKey(source.ProductComponents[i].FoodId))
                    {
                        source.ProductComponents[i].Count =
                        model.KitComponents[source.ProductComponents[i].KitId].Item2;
                        model.KitComponents.Remove(source.ProductComponents[i].KitId);
                    }
                    else
                    {
                        source.ProductComponents.RemoveAt(i--);
                    }
                }
            }
            foreach (var pc in model.KitComponents)
            {
                source.ProductComponents.Add(new KitFood
                {
                    Id = ++maxPCId,
                    KitId = product.Id,
                    FoodId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return product;
        }
        public List<KitViewModel> Read(KitBindingModel model)
        {
            List<KitViewModel> result = new List<KitViewModel>();
            foreach (var component in source.Products)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private KitViewModel CreateViewModel(Kit product)
        {
            Dictionary<int, (string, int)> productComponents = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.ProductComponents)
            {
                if (pc.KitId == product.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Components)
                    {
                        if (pc.FoodId == component.Id)
                        {
                            componentName = component.FoodName;
                            break;
                        }
                    }
                    productComponents.Add(pc.FoodId, (componentName, pc.Count));
                }
            }
            return new KitViewModel
            {
                Id = product.Id,
                KitName = product.KitName,
                Price = product.Price,
                KitComponents = productComponents
            };
        }
    }
}

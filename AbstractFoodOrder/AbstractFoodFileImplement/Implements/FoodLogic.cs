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
    public class FoodLogic : IFoodLogic
    {
        private readonly FileDataListSingleton source;
        public FoodLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(FoodBindingModel model)
        {
            Food element = source.Components.FirstOrDefault(rec => rec.FoodName
           == model.FoodName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Components.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Components.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new Food { Id = maxId + 1 };
                source.Components.Add(element);
            }
            element.FoodName = model.FoodName;
        }
        public void Delete(FoodBindingModel model)
        {
            Food element = source.Components.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Components.Remove(element);
            }
            else
            {
            throw new Exception("Элемент не найден");
            }
        }
        public List<FoodViewModel> Read(FoodBindingModel model)
        {
            return source.Components
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new FoodViewModel
            {
                Id = rec.Id,
                FoodName = rec.FoodName
            })
            .ToList();
        }
    }
}

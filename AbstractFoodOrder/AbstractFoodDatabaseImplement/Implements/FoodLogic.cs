using AbstractFoodDatabaseImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractFoodDatabaseImplement.Implements
{
    public class FoodLogic : IFoodLogic
    {
        public void CreateOrUpdate(FoodBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                Food element = context.Foods.FirstOrDefault(rec =>
               rec.ComponentName == model.FoodName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Foods.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Блюдо не найдено");
                    }
                }
                else
                {
                    element = new Food();
                    context.Foods.Add(element);
                }
                element.ComponentName = model.FoodName;
                context.SaveChanges();
            }
        }
        public void Delete(FoodBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                Food element = context.Foods.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Foods.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Блюдо не найдено");
                }
            }
        }
        public List<FoodViewModel> Read(FoodBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                return context.Foods
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new FoodViewModel
                {
                    Id = rec.Id,
                    FoodName = rec.ComponentName
                })
                .ToList();
            }
        }
    }
}

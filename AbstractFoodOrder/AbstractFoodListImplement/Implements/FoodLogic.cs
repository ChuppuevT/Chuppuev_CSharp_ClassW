using AbstractFoodListImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodListImplement.Implements
{
    public class FoodLogic: IFoodLogic
    {
        private readonly DataListSingleton source;
        public FoodLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(FoodBindingModel model)
        {
            Food tempComponent = model.Id.HasValue ? null : new Food
            {
                Id = 1
            };
            foreach (var component in source.Components)
            {
                if (component.FoodName == model.FoodName && component.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
                if (!model.Id.HasValue && component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
                else if (model.Id.HasValue && component.Id == model.Id)

                {
                    tempComponent = component;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempComponent == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempComponent);
            }
            else
            {
                source.Components.Add(CreateModel(model, tempComponent));
            }
        }
        public void CreateOrUpdate(KitBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(FoodBindingModel model)
        {
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id == model.Id.Value)
                {
                    source.Components.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void Delete(KitBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<FoodViewModel> Read(FoodBindingModel model)
        {
            List<FoodViewModel> result = new List<FoodViewModel>();
            foreach (var component in source.Components)
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

        public List<KitViewModel> Read(KitBindingModel model)
        {
            throw new NotImplementedException();
        }

        private Food CreateModel(FoodBindingModel model, Food component)
        {
            component.FoodName = model.FoodName;
            return component;
        }
        private FoodViewModel CreateViewModel(Food component)
        {
            return new FoodViewModel
            {
                Id = component.Id,
                FoodName = component.FoodName
            };
        }
    }
}

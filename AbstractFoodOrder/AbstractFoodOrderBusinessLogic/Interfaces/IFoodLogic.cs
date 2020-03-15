using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.Interfaces
{
    public interface IFoodLogic
    {
        List<FoodViewModel> Read(FoodBindingModel model);
        void CreateOrUpdate(FoodBindingModel model);
        void Delete(FoodBindingModel model);
    }
}

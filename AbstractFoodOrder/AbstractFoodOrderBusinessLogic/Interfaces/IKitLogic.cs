using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.Interfaces
{
    public interface IKitLogic
    {
        List<KitViewModel> Read(KitBindingModel model);
        void CreateOrUpdate(KitBindingModel model);
        void Delete(KitBindingModel model);
    }
}

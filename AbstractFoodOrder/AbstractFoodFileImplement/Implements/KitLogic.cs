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
    public class KitLogic : IKitLogic
    {
        private readonly FileDataListSingleton source;
        public KitLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(KitBindingModel model)
        {
            Kit element = source.Kits.FirstOrDefault(rec => rec.KitName ==
           model.KitName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть набор с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Kits.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Kits.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new Kit { Id = maxId + 1 };
                source.Kits.Add(element);
            }
            element.KitName = model.KitName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.ProductComponents.RemoveAll(rec => rec.KitId == model.Id &&
           !model.KitComponents.ContainsKey(rec.KitId));
            // обновили количество у существующих записей
            var updateComponents = source.ProductComponents.Where(rec => rec.KitId ==
           model.Id && model.KitComponents.ContainsKey(rec.KitId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.KitComponents[updateComponent.KitId].Item2;
                model.KitComponents.Remove(updateComponent.KitId);
            }
            // добавили новые
            int maxPCId = source.ProductComponents.Count > 0 ?
           source.ProductComponents.Max(rec => rec.Id) : 0;

            foreach (var pc in model.KitComponents)
            {
                source.ProductComponents.Add(new KitFood
                {
                    Id = ++maxPCId,
                    KitId = element.Id,
                    FoodId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(KitBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.ProductComponents.RemoveAll(rec => rec.KitId == model.Id);
            Kit element = source.Kits.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Kits.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<KitViewModel> Read(KitBindingModel model)
        {
            return source.Kits
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new KitViewModel
            {
                Id = rec.Id,
                KitName = rec.KitName,
                Price = rec.Price,
                KitComponents = source.ProductComponents
            .Where(recPC => recPC.KitId == rec.Id)
           .ToDictionary(recPC => recPC.KitId, recPC =>
            (source.Components.FirstOrDefault(recC => recC.Id ==
           recPC.KitId)?.FoodName, recPC.Count))
            })
            .ToList();
        }
    }
}

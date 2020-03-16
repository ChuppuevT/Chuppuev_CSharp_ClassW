using AbstractFoodDatabaseImplement.Models;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractFoodDatabaseImplement.Implements
{
    public class KitLogic : IKitLogic
    {
        public void CreateOrUpdate(KitBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Kit element = context.Kits.FirstOrDefault(rec =>
                       rec.KitName == model.KitName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть набор с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Kits.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Kit();
                            context.Kits.Add(element);
                        }
                        element.KitName = model.KitName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.KitComponents.Where(rec
                           => rec.KitId == model.Id.Value).ToList();
                            context.KitComponents.RemoveRange(productComponents.Where(rec =>
                            !model.KitComponents.ContainsKey(rec.FoodId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count =
                               model.KitComponents[updateComponent.KitId].Item2;

                                model.KitComponents.Remove(updateComponent.FoodId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.KitComponents)
                        {
                            context.KitComponents.Add(new KitFood
                            {
                                KitId = element.Id,
                                FoodId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(KitBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.KitComponents.RemoveRange(context.KitComponents.Where(rec =>
                        rec.KitId == model.Id));
                        Kit element = context.Kits.FirstOrDefault(rec => rec.Id== model.Id);
                        if (element != null)
                        {
                            context.Kits.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<KitViewModel> Read(KitBindingModel model)
        {
            using (var context = new AbstractFoodDatabase())
            {
                return context.Kits
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new KitViewModel
               {
                   Id = rec.Id,
                   KitName = rec.KitName,
                   Price = rec.Price,
                   KitComponents = context.KitComponents
                .Include(recPC => recPC.Food)
               .Where(recPC => recPC.KitId == rec.Id)
               .ToDictionary(recPC => recPC.FoodId, recPC =>
                (recPC.Food?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}

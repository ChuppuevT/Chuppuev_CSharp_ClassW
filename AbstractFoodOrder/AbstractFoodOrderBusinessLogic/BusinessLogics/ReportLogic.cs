using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.HelperModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IFoodLogic foodLogic;
        private readonly IKitLogic kitLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IKitLogic kitLogic, IFoodLogic foodLogic,
       IOrderLogic orderLLogic)
        {
            this.kitLogic = kitLogic;
            this.foodLogic = foodLogic;
            this.orderLogic = orderLLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportKitFoodViewModel> GetProductComponent()
        {
            var foods = foodLogic.Read(null);
            var products = kitLogic.Read(null);
            var list = new List<ReportKitFoodViewModel>();
            foreach (var component in foods)
            {
                var record = new ReportKitFoodViewModel
                {
                    FoodName = component.FoodName,
                    Kits = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var product in products)
                {
                    if (product.KitComponents.ContainsKey(component.Id))
                    {
                        record.Kits.Add(new Tuple<string, int>(product.KitName,
                       product.KitComponents[component.Id].Item2));
                        record.TotalCount +=
                       product.KitComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                KitName = x.KitName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                //Foods = foodLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                KitComponents = GetProductComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}

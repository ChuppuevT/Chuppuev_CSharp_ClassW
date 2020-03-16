using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    public class ReportKitFoodViewModel
    {
        public string FoodName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Kits { get; set; }
    }
}

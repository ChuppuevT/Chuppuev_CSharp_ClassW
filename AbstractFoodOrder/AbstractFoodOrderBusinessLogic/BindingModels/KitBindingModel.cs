using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.BindingModels
{
    public class KitBindingModel
    {
        public int? Id { get; set; }
        public string KitName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> KitComponents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    public class KitViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название набора")]
        public string KitName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> KitComponents { get; set; }
    }
}

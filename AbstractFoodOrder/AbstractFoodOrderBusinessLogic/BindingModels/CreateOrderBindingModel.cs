using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int KitId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}

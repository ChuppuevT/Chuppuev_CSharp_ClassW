using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    public class FoodViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название блюда")]
        public string FoodName { get; set; }
    }
}

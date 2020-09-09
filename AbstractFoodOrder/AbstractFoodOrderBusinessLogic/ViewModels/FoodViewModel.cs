using AbstractFoodOrderBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        [Column(title: "Блюдо", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FoodName { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FoodName"
        };
    }
}

using AbstractFoodOrderBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class KitViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Название набора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string KitName { get; set; }

        [DataMember]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> KitComponents { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "KitName",
            "Price"
        };
    }
}

using AbstractFoodOrderBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("Клиент")]
        public string FIO { get; set; }

        [Column(title: "Почта", width: 150)]
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FIO",
            "Email"
        };
    }
}

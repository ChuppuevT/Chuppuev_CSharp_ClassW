using AbstractFoodOrderBusinessLogic.Attributes;
using AbstractFoodOrderBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int KitId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        [Column(title: "Исполнитель", width: 70)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        [Column(title: "Набор", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string KitName { get; set; }

        [Column(title: "Количество", width: 100)]
        [DataMember]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 80)]
        [DataMember]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        [DataMember]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        [DataMember]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        [DataMember]
        public DateTime? DateImplement { get; set; }

        public override List<string> Properties() => new List<string> { "Id",
"ClientFIO", "KitName", "ImplementerFIO", "Count", "Sum", "Status", "DateCreate",
"DateImplement" };
    }
}


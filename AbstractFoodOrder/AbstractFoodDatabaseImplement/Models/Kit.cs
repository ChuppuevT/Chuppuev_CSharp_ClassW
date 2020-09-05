using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractFoodDatabaseImplement.Models
{
    public class Kit
    {
        public int Id { get; set; }
        [Required]
        public string KitName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<KitFood> KitComponents { get; set; }
    }
}

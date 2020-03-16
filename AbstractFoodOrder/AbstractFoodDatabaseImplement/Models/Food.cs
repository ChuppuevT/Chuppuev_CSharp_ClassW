using AbstractFoodFileImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbstractFoodDatabaseImplement.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        public string ComponentName { get; set; }
        [ForeignKey("FoodId")]
        public virtual List<KitFood> KitComponents { get; set; }  //nneen
    }

}

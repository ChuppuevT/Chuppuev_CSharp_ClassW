using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractFoodDatabaseImplement.Models
{
    public class KitFood
    {
        public int Id { get; set; }
        public int KitId { get; set; }
        public int FoodId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Food Food { get; set; }
        public virtual Kit Kit { get; set; }
    }
}

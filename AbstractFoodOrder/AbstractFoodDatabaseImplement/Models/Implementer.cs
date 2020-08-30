using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFoodDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }

        public string ImplementerFIO { get; set; }

        public int WorkingTime { get; set; }

        public int PauseTime { get; set; }

        public List<Order> Orders { get; set; }
    }
}

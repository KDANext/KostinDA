using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }
        [Required] 
        public string ImplementerFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

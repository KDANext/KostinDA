using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class Billet
    {
        public int Id { get; set; }
        [Required]
        public string BilletName { get; set; }
        [ForeignKey("BilletId")]
        public virtual List<ProductBillet> ProductBillets { get; set; }
    }
}

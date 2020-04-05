using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<ProductBillet> ForgeProductBillets { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

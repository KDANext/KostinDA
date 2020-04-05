using System.ComponentModel.DataAnnotations;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class ProductBillet
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BilletId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Billet Billet { get; set; }
        public virtual Product Product { get; set; }
    }
}

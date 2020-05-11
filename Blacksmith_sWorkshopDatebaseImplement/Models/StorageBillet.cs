using Blacksmith_sWorkshopDatebaseImplement.Models;
using System.ComponentModel.DataAnnotations;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class StorageBillet
    {
        public int Id { set; get; }
        [Required]
        public int StorageId { set; get; }
        [Required]
        public int BilletId { set; get; }
        [Required]
        public int Count { set; get; }
        public virtual Billet Billet { get; set; }
        public virtual Storage Storage { get; set; } 
    }
}

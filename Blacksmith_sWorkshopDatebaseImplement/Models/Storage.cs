using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class Storage
    {
        public int Id { set; get; }
        [Required]
        public string StorageName { set; get; }
        public virtual List<StorageBillet> StorageBillets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blacksmith_sWorkshopDatebaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public string Email { get; set; }
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

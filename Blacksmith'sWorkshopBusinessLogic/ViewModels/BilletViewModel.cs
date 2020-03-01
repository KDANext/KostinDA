using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class BilletViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название заготовки")]
        public string BilletName { get; set; }
    }
}

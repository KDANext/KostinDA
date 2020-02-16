using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class ProductBilletViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BilletId { get; set; }
        [DisplayName("Заготовка")]
        public string BilletName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}

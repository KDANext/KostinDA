using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.BindingModels
{
    public class ProductBilletBindingModel
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public int BilletId { get; set; }
        public int Count { get; set; }
    }
}

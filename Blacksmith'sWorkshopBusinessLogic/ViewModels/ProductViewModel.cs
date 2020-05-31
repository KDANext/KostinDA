using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public class ProductViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> ProductBillets { get; set; }
    }
}

using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public class ProductViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Название изделия", width: 100)]
        public string ProductName { get; set; }
        [DataMember]
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> ProductBillets { get; set; }
        public override List<string> Properties() => new List<string> { "Id","ProductName","Price" };
    }
}

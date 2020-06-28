using System.Runtime.Serialization;

namespace Blacksmith_sWorkshopBusinessLogic.BindingModels
{
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public int ClientId { get; set; }
    }
}

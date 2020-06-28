using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        public override List<string> Properties() => new List<string> { "Id","ClientFIO" };
    }
}

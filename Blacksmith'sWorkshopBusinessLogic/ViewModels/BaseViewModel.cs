using System;
using System.Collections.Generic;
using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System.Runtime.Serialization;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public abstract class BaseViewModel
    {
        [Column(visible: false)]
        [DataMember]
        public int Id { get; set; }
        public abstract List<string> Properties();
    }
}

using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember] public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 100)]
        [DataMember] 
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 100)]
        [DataMember]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 100)]
        [DataMember] 
        public string Subject { get; set; }

        [Column(title: "Текст", width: 100)]
        [DataMember] 
        public string Body { get; set; }
        public override List<string> Properties() => new List<string> { "Id", "SenderName","DateDelivery","Subject","Body" };
    }
}

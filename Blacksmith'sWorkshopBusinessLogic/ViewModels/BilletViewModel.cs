using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class BilletViewModel : BaseViewModel
    {
        [Column(title: "Заготовки", width: 100)]
        public string BilletName { get; set; }
        public override List<string> Properties() => new List<string> { "Id","BilletName" };
    }
}

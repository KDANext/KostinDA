using Blacksmith_sWorkshopBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class ImplementerViewModel : BaseViewModel
    {
        [Column(title: "ФИО исполнителя", width: 150)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Время на заказ", width: 100)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 100)]
        public int PauseTime { get; set; }
        public override List<string> Properties() => new List<string> { "Id","ImplementerFIO","WorkingTime","PauseTime" };
    }
}

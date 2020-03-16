using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class ReportProductBilletViewModel
    {
        public string BilletName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Products { get; set; }
    }
}

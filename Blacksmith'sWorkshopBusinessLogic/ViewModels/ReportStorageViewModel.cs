using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class ReportStorageViewModel
    {
        public string StorageName { get; set; }
        public List<Tuple<string, int>> Billets { get; set; }
        public int TotalCount { get; set; }
    }
}

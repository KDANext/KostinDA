using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportProductBilletViewModel> ProductBillets { get; set; }
        public IEnumerable<IGrouping<DateTime, ReportOrdersViewModel>> Orders { get; set; }
    }
}

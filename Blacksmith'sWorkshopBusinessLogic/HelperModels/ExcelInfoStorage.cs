using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.HelperModels
{
    public class ExcelInfoStorage
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportStorageViewModel> Storages { get; set; }
    }
}

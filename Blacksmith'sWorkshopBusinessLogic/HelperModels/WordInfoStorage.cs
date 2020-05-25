using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.HelperModels
{
    public class WordInfoStorage
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<StorageViewModel> Storages { get; set; }
    }
}

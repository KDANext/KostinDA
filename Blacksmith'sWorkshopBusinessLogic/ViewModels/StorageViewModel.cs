using System.Collections.Generic;
using System.ComponentModel;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { set; get; }
        [DisplayName("Склад")]
        public string StorageName { set; get; }
        public Dictionary<int, (string, int)> StoragedBillets { get; set; }
    }
}

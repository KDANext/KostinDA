using System.Collections.Generic;

namespace Blacksmith_sWorkshopBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int? Id { set; get; }
        public string StorageName { set; get; }
        public Dictionary<int, (string, int)> StoragedBillets { get; set; } //(name, count)
    }
}

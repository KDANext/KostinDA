using System.Collections.Generic;

namespace Blacksmith_sWorkshopBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int? Id { get; set; }
        public string StorageName { get; set; }
        public Dictionary<int, (string, int)> StorageBillets { get; set; }
    }
}

using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace Blacksmith_sWorkshopBusinessLogic.Intefaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> Read(StorageBindingModel model);
        void CreateOrUpdate(StorageBindingModel model);
        void Delete(StorageBindingModel model);
        void FillStorage(StorageBilletBindingModel model);
    }
}

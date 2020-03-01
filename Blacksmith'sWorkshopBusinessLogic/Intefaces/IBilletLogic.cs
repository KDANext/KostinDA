using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.Intefaces
{
    public interface IBilletLogic
    {
        List<BilletViewModel> Read(BilletBindingModel model);
        void CreateOrUpdate(BilletBindingModel model);
        void Delete(BilletBindingModel model);
    }
}

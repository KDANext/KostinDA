using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.Intefaces
{
    public interface IBilletLogic
    {
        List<BilletViewModel> GetList();
        BilletViewModel GetElement(int id);
        void AddElement(BilletBindingModel model);
        void UpdElement(BilletBindingModel model);
        void DelElement(int id);
    }
}

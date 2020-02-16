using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.Intefaces
{
    public interface IProductLogic
    {
        List<ProductViewModel> GetList();
        ProductViewModel GetElement(int id);
        void AddElement(ProductBindingModel model);
        void UpdElement(ProductBindingModel model);
        void DelElement(int id);
    }
}

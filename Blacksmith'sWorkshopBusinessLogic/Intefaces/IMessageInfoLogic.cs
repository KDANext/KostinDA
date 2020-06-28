using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.Intefaces
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);
        void Create(MessageInfoBindingModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.BindingModels
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string ClientFIO { get; set; }
        public string Login { get; set; }
        public string Password{ get; set; }

    }
}

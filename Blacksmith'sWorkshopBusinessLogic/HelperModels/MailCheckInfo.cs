using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopBusinessLogic.HelperModels
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }

        public int PopPort { get; set; }

        public IMessageInfoLogic Logic { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopFileImplement.Models
{
    public class StorageBillet
    {
        public int Id { set; get; }
        public int StorageId { set; get; }
        public int BilletId { set; get; }
        public int Count { set; get; }
    }
}

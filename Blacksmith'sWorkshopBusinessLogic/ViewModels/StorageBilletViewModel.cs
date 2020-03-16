using System.ComponentModel;

namespace Blacksmith_sWorkshopBusinessLogic.ViewModels
{
    public class StorageBilletViewModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int BilletId { get; set; }
        [DisplayName("Название компонента")]
        public string BilletName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}

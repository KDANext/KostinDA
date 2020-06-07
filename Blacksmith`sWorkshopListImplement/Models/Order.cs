using Blacksmith_sWorkshopBusinessLogic.Enums;
using System;

namespace Blacksmith_sWorkshopListImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int? ImplementerId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public bool? FreeOrders { get; set; }
    }
}

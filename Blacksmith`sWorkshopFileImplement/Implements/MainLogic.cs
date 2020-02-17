using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Enums;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Blacksmith_sWorkshopFileImplement.Implements
{
    public class MainLogic : IMainLogic
    {
        private readonly FileDataListSingleton source;
        public MainLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                ProductId = model.ProductId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order order = source.Orders.FirstOrDefault(ord => ord.Id == model.Id);
            if (order == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            order.Status = OrderStatus.Готов;
        }

        public List<OrderViewModel> GetOrders()
        {
            List<OrderViewModel> orders = source.Orders.Select(ord => new OrderViewModel
            {
                Id = ord.Id,
                ProductId = ord.ProductId,
                ProductName = source.Products.FirstOrDefault(rec => rec.Id == ord.ProductId).ProductName,
                Count = ord.Count,
                Sum = ord.Sum,
                DateCreate = ord.DateCreate,
                DateImplement = ord.DateImplement,
                Status = ord.Status
            }).ToList();           
            return orders;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order order = source.Orders.FirstOrDefault(ord => ord.Id == model.Id);
            if (order == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            order.Status = OrderStatus.Оплачен;
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order order = source.Orders.FirstOrDefault(ord => ord.Id == model.Id); 
            if (order == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            order.DateImplement = DateTime.Now;
            order.Status = OrderStatus.Выполняется;
        }
    }
}

using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Enums;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using System;
using System.Collections.Generic;

namespace Blacksmith_sWorkshopBusinessLogic.BusinessLogics
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic storageLogic;
        private readonly IProductLogic productLogic;
        public MainLogic(IOrderLogic orderLogic, IStorageLogic storageLogic, IProductLogic productLogic)
        {
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
            this.productLogic = productLogic;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                ProductId = model.ProductId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (storageLogic.CheckingStoragedBillet(order.ProductId, order.Count))
            {
                if (order.Status != OrderStatus.Принят)
                {
                    throw new Exception("Заказ не в статусе \"Принят\"");
                }
                orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = DateTime.Now,
                    Status = OrderStatus.Выполняется
                });
                storageLogic.RemoveBillet(order.ProductId, order.Count);
            }
            else
                throw new Exception("Не хватает заготовок на складах!");
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }      
    }
}

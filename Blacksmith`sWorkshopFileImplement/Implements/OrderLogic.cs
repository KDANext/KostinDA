using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Enums;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopFileImplement
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;

        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order order;
            if (model.Id.HasValue)
            {
                order = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Элемент не найден");
                }
                order.Status = model.Status;
                order.ProductId = model.ProductId;
                order.ClientId = model.ClientId;
                order.ImplementerId = model.ImplementerId;
                order.Count = model.Count;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;
                order.Status = model.Status;
                order.Sum = model.Sum;
                order.FreeOrders = model.FreeOrders;
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
                order = new Order { Id = maxId + 1 };
                order.ProductId = model.ProductId;
                order.ClientId = model.ClientId;
                order.ImplementerId = model.ImplementerId;
                order.Count = model.Count;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;
                order.Status = model.Status;
                order.Sum = model.Sum;
                order.FreeOrders = model.FreeOrders;

                source.Orders.Add(order);
            }
        }

        public void Delete(OrderBindingModel model)
        {
            Order order = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (order != null)
            {
                source.Orders.Remove(order);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
            .Where(
                    rec => model == null
                    || (rec.Id == model.Id && model.Id.HasValue)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                    || (rec.ClientId == model.ClientId)
                    || (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue)
                    || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется)
                )
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ProductId = rec.ProductId,
                ClientId = rec.ClientId,
                ImplementerId = rec.ImplementerId,
                ProductName = source.Products.FirstOrDefault(r => r.Id == rec.ProductId).ProductName,
                ClientFIO = source.Clients.FirstOrDefault(r => r.Id == rec.ClientId).ClientFIO,
                ImplementerFIO = rec.ImplementerId.HasValue 
                                    ?
                                    source.Implementers.FirstOrDefault((im) => im.Id == rec.ImplementerId).ImplementerFIO
                                    : 
                                    string.Empty,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
            })
            .ToList();
        }
    }
}

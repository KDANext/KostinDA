﻿using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Enums;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;

        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order
            {
                Id = 1
            };
            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
                else if (model.FreeOrders.HasValue && model.FreeOrders.Value && !order.ImplementerId.HasValue)
                {
                    tempOrder = order;
                }
                else if (model.ImplementerId.HasValue && model.ImplementerId.Value == order.ImplementerId && order.Status == Blacksmith_sWorkshopBusinessLogic.Enums.OrderStatus.Выполняется)
                {
                    tempOrder = order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (model != null)
                {
                    if (order.Id == model.Id)
                    {
                        if (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                        {
                            result.Add(CreateViewModel(order));
                            continue;
                        }
                        if (model.ClientId == order.ClientId)
                        {
                            result.Add(CreateViewModel(order));
                            continue;
                        }
                        if (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется)
                        {
                            result.Add(CreateViewModel(order));
                            continue;
                        }
                        if (model.FreeOrders.HasValue && model.FreeOrders.Value && !order.ImplementerId.HasValue)
                        {
                            result.Add(CreateViewModel(order));
                            continue;
                        }
                        result.Add(CreateViewModel(order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(order));
            }
            return result;
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.Count = model.Count;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.ProductId = model.ProductId;
            order.ClientId = model.ClientId;
            order.ImplementerId = model.ImplementerId;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.FreeOrders = model.FreeOrders;
            return order;
        }
        private OrderViewModel CreateViewModel(Order order)
        {
            var productName = source.Products.FirstOrDefault((n) => n.Id == order.ProductId).ProductName;
            var clientFIO = source.Clients.FirstOrDefault((n) => n.Id == order.ClientId).ClientFIO;
            var implementerFIO = source.Implementers.FirstOrDefault((n) => n.Id == order.ImplementerId).ImplementerFIO;
            return new OrderViewModel
            {
                Id = order.Id,
                Count = order.Count,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                ProductName = productName,
                ClientFIO = clientFIO,
                ImplementerFIO = implementerFIO,
                ProductId = order.ProductId,
                Status = order.Status,
                Sum = order.Sum
            };
        }
    }
}

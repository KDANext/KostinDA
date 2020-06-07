using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blacksmith_sWorkshopBusinessLogic.BusinessLogics
{
    public class WorkModeling
    {
        private readonly IImplementerLogic implementerLogic;
        private readonly IOrderLogic orderLogic;
        private readonly MainLogic mainLogic;
        private readonly Random rnd;

        public WorkModeling(IImplementerLogic implementerLogic, IOrderLogic orderLogic, MainLogic mainLogic)
        {
            this.implementerLogic = implementerLogic; 
            this.orderLogic = orderLogic; 
            this.mainLogic = mainLogic;
            rnd = new Random(1000);
        }
        public void DoWork()
        {
            var implementers = implementerLogic.Read(null);

            var orders = orderLogic.Read(new OrderBindingModel { FreeOrders = true });

            foreach (var implementer in implementers) { 
                WorkerWorkAsync(implementer, orders); 
            }
        }
        private async void WorkerWorkAsync(ImplementerViewModel implementer, List<OrderViewModel> orders)
        {
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel { 
                ImplementerId = implementer.Id 
            }));
            foreach (var order in runOrders)
            {
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);

                mainLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });
                Thread.Sleep(implementer.PauseTime);
            }
            await Task.Run(() => {
            foreach (var order in orders)
            {
                try
                {
                    mainLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });
                    Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                    mainLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id, ImplementerId = implementer.Id });
                    Thread.Sleep(implementer.PauseTime);
                    }
                    catch (Exception) { }
                }
            });
        }
    }
}
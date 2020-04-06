using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.HelperModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IBilletLogic billetLogic;
        private readonly IProductLogic productLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IProductLogic productLogic, IBilletLogic BilletLogic,
       IOrderLogic orderLLogic)
        {
            this.productLogic = productLogic;
            this.billetLogic = BilletLogic;
            this.orderLogic = orderLLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportProductBilletViewModel> GetProductBillet()
        {
            var billets = billetLogic.Read(null);
            var products = productLogic.Read(null);
            var list = new List<ReportProductBilletViewModel>();
            foreach (var billet in billets)
            {
                foreach (var product in products)
                {
                    if (product.ProductBillets.ContainsKey(billet.Id))
                    {
                        var record = new ReportProductBilletViewModel
                        {
                            ProductName = product.ProductName,
                            BilletName = billet.BilletName,
                            Count = product.ProductBillets[billet.Id].Item2
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                ProductName = x.ProductName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveBilletsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Products = productLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductBilletToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                FileName = model.FileName,
                Title = "Заказы",
                Orders = GetOrders(model)
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductBilletToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список нужных заготовок на продукты",
                ProductBillets = GetProductBillet()
            });
        }
    }
}
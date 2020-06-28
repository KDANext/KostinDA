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
        private readonly IStorageLogic storageLogic;
        public ReportLogic(IProductLogic productLogic, IBilletLogic BilletLogic,IOrderLogic orderLLogic, IStorageLogic storageLogic)
        {
            this.productLogic = productLogic;
            this.billetLogic = BilletLogic;
            this.orderLogic = orderLLogic;
            this.storageLogic = storageLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportProductBilletViewModel> GetProductBillet()
        {
            var products = productLogic.Read(null);
            var list = new List<ReportProductBilletViewModel>();
                foreach (var product in products)
                {
                    foreach (var billet in product.ProductBillets)
                    {
                        list.Add(new ReportProductBilletViewModel
                        {
                            ProductName = product.ProductName,
                            BilletName = billet.Value.Item1,
                            Count = billet.Value.Item2
                        }) ;
                    }
                }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<DateTime, ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
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
           .GroupBy(x => x.DateCreate.Date);
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
        public List<ReportStorageBilletViewModel> GetStorageBillets()
        {
            return storageLogic.Read(null)
                .Select(x => x.StoragedBillets
                        .Select(sc => new ReportStorageBilletViewModel
                        {
                            StorageName = x.StorageName,
                            BilletName = sc.Value.Item1,
                            Count = sc.Value.Item2
                        }))
                .SelectMany(scl => scl)
                .ToList();
        }
        public List<ReportStorageViewModel> GetStorageWithBillets()
        {
            return storageLogic.Read(null)
                .Select(x => new ReportStorageViewModel
                {
                    StorageName = x.StorageName,
                    Billets = x.StoragedBillets
                        .Select(
                            sc => new Tuple<string, int>(sc.Value.Item1, sc.Value.Item2)
                        ).ToList(),
                    TotalCount = x.StoragedBillets.Sum(sc => sc.Value.Item2)
                })
                .ToList();
        }
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocStorages(new WordInfoStorage
            {
                FileName = model.FileName,
                Title = "Список складов",
                Storages = storageLogic.Read(null)
            });
        }
        public void SaveStoragesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocStorages(new ExcelInfoStorage
            {
                FileName = model.FileName,
                Title = "Список складов с заготовками",
                Storages = GetStorageWithBillets()
            });
        }
        public void SaveStorageComponentsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocStorage(new PdfInfoStorage
            {
                FileName = model.FileName,
                Title = "Список заготовок на складах",
                StorageBillets = GetStorageBillets()
            });
        }
    }
}
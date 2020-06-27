using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopDatebaseImplement;
using Blacksmith_sWorkshopDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlacksmithsWorkshopDatebaseImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly IProductLogic productLogic;
        public StorageLogic(IProductLogic productLogic)
        {
            this.productLogic = productLogic;
        }
        public void CreateOrUpdate(StorageBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Storage();
                    context.Storages.Add(element);
                }
                element.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }

        public void Delete(StorageBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                context.StorageBillets.RemoveRange(context.StorageBillets.Where(rec => rec.StorageId == model.Id));
                Storage element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Storages.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                return context.Storages.Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    StoragedBillets = context.StorageBillets.Include(x => x.Billet)
                                                           .Where(x => x.StorageId == rec.Id)
                                                           .ToDictionary(x => x.BilletId,  x => (x.Billet.BilletName, x.Count))
                }).ToList();
            }
        }

        public void AddBilletToStorage(StorageAddBilletBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                var storageBillet = context.StorageBillets
                    .FirstOrDefault(sm => sm.BilletId == model.BilletId && sm.StorageId == model.StorageId);
                if (storageBillet != null)
                    storageBillet.Count += model.BilletCount;
                else
                    context.StorageBillets.Add(new StorageBillet()
                    {
                        BilletId = model.BilletId,
                        StorageId = model.StorageId,
                        Count = model.BilletCount
                    });
                context.SaveChanges();
            }
        }

        public void RemoveBillet(int ProductId, int ProductCount)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var ProductBillets = context.ProductBillets.Where(q => q.ProductId == ProductId);
                    foreach (var q in ProductBillets)
                    {
                        int BilletCount = q.Count * ProductCount;
                        foreach (var w in context.StorageBillets)
                        {
                            if (w.BilletId == q.BilletId && w.Count >= BilletCount)
                            {
                                w.Count -= BilletCount;
                                break;
                            }
                            else if (w.BilletId == q.BilletId && w.Count < BilletCount)
                            {
                                BilletCount -= w.Count;
                                w.Count = 0;
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public bool CheckingStoragedBillet(int ProductId, int ProductCount)
        {
            var storages = Read(null);
            var ProductBillet = productLogic.Read(new ProductBindingModel() { Id = ProductId })[0].ProductBillets;
            var BilletStorages = new Dictionary<int, int>(); // Ключ,Количество
            foreach (var storage in storages)
            {
                foreach (var sm in storage.StoragedBillets)
                {
                    if (BilletStorages.ContainsKey(sm.Key))
                        BilletStorages[sm.Key] += sm.Value.Item2;
                    else
                        BilletStorages.Add(sm.Key, sm.Value.Item2);
                }
            }

            foreach (var dm in ProductBillet)
            {
                if (!BilletStorages.ContainsKey(dm.Key) || BilletStorages[dm.Key] < dm.Value.Item2 * ProductCount)
                    return false;
            }
            return true;
        }
    }
    }
}

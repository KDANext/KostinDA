using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopFileImplement;
using Blacksmith_sWorkshopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith_sWorkshopFileImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly FileDataListSingleton source;
        private readonly IProductLogic productLogic;

        public StorageLogic(IProductLogic productLogic)
        {
            this.productLogic = productLogic;
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(q => q.StorageName == model.StorageName && q.Id != model.Id);
            if (element != null)
                throw new Exception("Уже есть компонент с таким названием");
            if (model.Id.HasValue)
            {
                element = source.Storages.FirstOrDefault(q => q.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                var storageBillets = source.StorageBillets.Where(q => q.StorageId == element.Id).ToList();
                //обновление материалов, связанных с текущим хранилищем
                for (int i = 0; i < storageBillets.Count; i++)
                {
                    if (model.StoragedBillets.ContainsKey(storageBillets[i].BilletId))
                        storageBillets[i].Count = model.StoragedBillets[storageBillets[i].BilletId].Item2;
                    else
                        storageBillets.RemoveAt(i);
                }
                //добавление новых материалов
                var keysBillets = model.StoragedBillets.Keys;
                int maxId = source.StorageBillets.Count > 0 ? source.StorageBillets.Count : 0;
                foreach (var k in keysBillets)
                {
                    if (!source.StorageBillets.Where(q => q.StorageId == element.Id).Select(q => q.BilletId).Contains(k))
                        source.StorageBillets.Add(new StorageBillet()
                        {
                            Id = ++maxId,
                            BilletId = k,
                            StorageId = element.Id,
                            Count = model.StoragedBillets[k].Item2
                        });
                }
            }
            else
            {
                int maxId = source.Storages.Count > 0 ? source.Storages.Max(q => q.Id) : 0;
                element = new Storage { Id = maxId + 1 };
                source.Storages.Add(element);
            }
            element.StorageName = model.StorageName;
        }

        public void Delete(StorageBindingModel model)
        {
            Storage storage = source.Storages.FirstOrDefault(q => q.Id == model.Id);
            if (storage != null)
                source.Storages.Remove(storage);
            else
                throw new Exception("Склад ненайден");
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            return source.Storages
            .Where(q => model == null || q.Id == model.Id)
            .Select(q => new StorageViewModel
            {
                Id = q.Id,
                StorageName = q.StorageName,
                StoragedBillets = source.StorageBillets
                    .Where(w => w.StorageId == q.Id)
                    .Select(w => (source.Billets.FirstOrDefault(e => e.Id == w.BilletId).BilletName, w.Count))
                    .ToDictionary(r => source.Billets.FirstOrDefault(e => e.BilletName == r.BilletName).Id)
            })
            .ToList();
        }

        public void AddBilletToStorage(StorageAddBilletBindingModel model)
        {
            if (source.StorageBillets.Count == 0)
            {
                source.StorageBillets.Add(new StorageBillet()
                {
                    Id = 1,
                    BilletId = model.BilletId,
                    StorageId = model.StorageId,
                    Count = model.BilletCount
                });
            }
            else
            {
                var Billet = source.StorageBillets.FirstOrDefault(q => q.StorageId == model.StorageId && q.BilletId == model.BilletId);
                if (Billet == null)
                {
                    source.StorageBillets.Add(new StorageBillet()
                    {
                        Id = source.StorageBillets.Max(q => q.Id) + 1,
                        BilletId = model.BilletId,
                        StorageId = model.StorageId,
                        Count = model.BilletCount
                    });
                }
                else
                    Billet.Count += model.BilletCount;
            }
        }

        public void RemoveBillet(int ProductId, int countProduct)
        {
            var ProductBillets = source.ProductBillets.Where(q => q.ProductId == ProductId);
            foreach (var q in ProductBillets)
            {
                int BilletCount = q.Count * countProduct;
                foreach (var w in source.StorageBillets)
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

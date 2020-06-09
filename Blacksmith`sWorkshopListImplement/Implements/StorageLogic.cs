using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;
        private readonly ProductLogic productLogic;

        public StorageLogic(ProductLogic productLogic)
        {
            this.productLogic = productLogic;
        }

        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(StorageBindingModel storage)
        {
            Storage tempStorage = storage.Id.HasValue ? null : new Storage
            {
                Id = 1
            };
            foreach (var s in source.Storages)
            {
                if (s.StorageName == storage.StorageName && s.Id != storage.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                if (!storage.Id.HasValue && s.Id >= tempStorage.Id)
                {
                    tempStorage.Id = s.Id + 1;
                }
                else if (storage.Id.HasValue && s.Id == storage.Id)
                {
                    tempStorage = s;
                }
            }
            if (storage.Id.HasValue)
            {
                if (tempStorage == null)
                {
                    throw new Exception("Элемент не найден");
                }
                storage.StorageName = tempStorage.StorageName;
            }
            else
            {
                source.Storages.Add(new Storage
                {
                    StorageName = storage.StorageName
                });
            }
        }

        public void Delete(StorageBindingModel model)
        {
            // удаляем записи по компонентам при удалении хранилища
            for (int i = 0; i < source.StorageBillets.Count; ++i)
            {
                if (source.StorageBillets[i].StorageId == model.Id)
                {
                    source.StorageBillets.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            foreach (var storage in source.Storages)
            {
                if (model != null)
                {
                    if (storage.Id == model.Id)
                    {
                        result.Add(CreateViewModel(storage));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(storage));
            }
            return result;
        }
        private StorageViewModel CreateViewModel(Storage storage)
        {
            // требуется дополнительно получить список компонентов для хранилища с
            // названиями и их количество
            Dictionary<int, (string, int)> storageBillets = new Dictionary<int, (string, int)>();
            foreach (var sm in source.StorageBillets)
            {
                if (sm.StorageId == storage.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Billets)
                    {
                        if (sm.BilletId == component.Id)
                        {
                            componentName = component.BilletName;
                            break;
                        }
                    }
                    storageBillets.Add(sm.BilletId, (componentName, sm.Count));
                }
            }
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StoragedBillets = storageBillets
            };
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
                var Billet = source.StorageBillets.FirstOrDefault(sm => sm.StorageId == model.StorageId && sm.BilletId == model.BilletId);
                if (Billet == null)
                {
                    source.StorageBillets.Add(new StorageBillet()
                    {
                        Id = source.StorageBillets.Max(sm => sm.Id) + 1,
                        BilletId = model.BilletId,
                        StorageId = model.StorageId,
                        Count = model.BilletCount
                    });
                }
                else
                    Billet.Count += model.BilletCount;
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

        public void RemoveBillet(int ProductId, int ProductCount)
        {
            throw new NotImplementedException();
        }
    }
}

using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;
        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageBilletViewModel> StorageBillets = new List<StorageBilletViewModel>();
                for (int j = 0; j < source.StorageBillets.Count; ++j)
                {
                    if (source.StorageBillets[j].StorageId == source.Storages[i].Id)
                    {
                        string BilletName = string.Empty;
                        for (int k = 0; k < source.Billets.Count; ++k)
                        {
                            if (source.StorageBillets[j].BilletId ==
                           source.Billets[k].Id)
                            {
                                BilletName = source.Billets[k].BilletName;
                                break;
                            }
                        }
                        StorageBillets.Add(new StorageBilletViewModel
                        {
                            Id = source.StorageBillets[j].Id,
                            StorageId = source.StorageBillets[j].StorageId,
                            BilletId = source.StorageBillets[j].BilletId,
                            BilletName = BilletName,
                            Count = source.StorageBillets[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageBillets = StorageBillets
                });
            }
            return result;
        }
        public StorageViewModel GetElement(int? id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageBilletViewModel> StorageBillets = new List<StorageBilletViewModel>();
                for (int j = 0; j < source.StorageBillets.Count; ++j)
                {
                    if (source.StorageBillets[j].StorageId == source.Storages[i].Id)
                    {
                        string BilletName = string.Empty;
                        for (int k = 0; k < source.Billets.Count; ++k)
                        {
                            if (source.StorageBillets[j].BilletId == source.Billets[k].Id)
                            {
                                BilletName = source.Billets[k].BilletName;
                                break;
                            }
                        }
                        StorageBillets.Add(new StorageBilletViewModel
                        {
                            Id = source.StorageBillets[j].Id,
                            StorageId = source.StorageBillets[j].StorageId,
                            BilletId = source.StorageBillets[j].BilletId,
                            BilletName = BilletName,
                            Count = source.StorageBillets[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageBillets = StorageBillets
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void CreateOrUpdate(StorageBindingModel model)
        {
            Storage tempStorage = model.Id.HasValue ? null : new Storage
            {
                Id = 1
            };
            foreach (var s in source.Storages)
            {
                if (s.StorageName == model.StorageName && s.Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                if (!model.Id.HasValue && s.Id >= tempStorage.Id)
                {
                    tempStorage.Id = s.Id + 1;
                }
                else if (model.Id.HasValue && s.Id == model.Id)
                {
                    tempStorage = s;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempStorage == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempStorage);
            }
            else
            {
                source.Storages.Add(CreateModel(model, tempStorage));
            }
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            int maxSMId = 0;
            for (int i = 0; i < source.StorageBillets.Count; ++i)
            {
                if (source.StorageBillets[i].Id > maxSMId)
                {
                    maxSMId = source.StorageBillets[i].Id;
                }
                if (source.StorageBillets[i].StorageId == storage.Id)
                {
                    if (model.StorageBillets.ContainsKey(source.StorageBillets[i].BilletId))
                    {
                        source.StorageBillets[i].Count = model.StorageBillets[source.StorageBillets[i].BilletId].Count;
                        model.StorageBillets.Remove(source.StorageBillets[i].MaterialId);
                    }
                    else
                    {
                        source.StorageBillets.RemoveAt(i--);
                    }
                }
            }
            foreach (var sb in model.StorageBillets)
            {
                source.StorageBillets.Add(new StorageBillet
                {
                    Id = ++maxSMId,
                    StorageId = storage.Id,
                    MaterialId = sb.Key,
                    Count = sb.Value.Item2
                });
            }
            return storage;
        }
        public void FillStorage(StorageBilletBindingModel model)
        {
            int foundItemIndex = -1;
            for (int i = 0; i < source.StorageBillets.Count; ++i)
            {
                if (source.StorageBillets[i].BilletId == model.BilletId
                    && source.StorageBillets[i].StorageId == model.StorageId)
                {
                    foundItemIndex = i;
                    break;
                }
            }
            if (foundItemIndex != -1)
            {
                source.StorageBillets[foundItemIndex].Count =
                    source.StorageBillets[foundItemIndex].Count + model.Count;
            }
            else
            {
                int maxId = 0;
                for (int i = 0; i < source.StorageBillets.Count; ++i)
                {
                    if (source.StorageBillets[i].Id > maxId)
                    {
                        maxId = source.StorageBillets[i].Id;
                    }
                }
                source.StorageBillets.Add(new StorageBillet
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    BilletId = model.BilletId,
                    Count = model.Count
                });
            }
        }
    }
}

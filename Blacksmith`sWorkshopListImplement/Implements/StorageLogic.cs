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
                CreateModel(storage, tempStorage);
            }
            else
            {
                source.Storages.Add(CreateModel(storage, tempStorage));
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
        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxSMId = 0;
            for (int i = 0; i < source.StorageBillets.Count; ++i)
            {
                if (source.StorageBillets[i].Id > maxSMId)
                {
                    maxSMId = source.StorageBillets[i].Id;
                }
                if (source.StorageBillets[i].StorageId == storage.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if (model.StorageBillets.ContainsKey(source.StorageBillets[i].BilletId))
                    {
                        // обновляем количество
                        source.StorageBillets[i].Count = model.StorageBillets[source.StorageBillets[i].BilletId].Item2;
                        // из модели убираем эту запись, чтобы остались только не
                        //просмотренные
                        model.StorageBillets.Remove(source.StorageBillets[i].BilletId);
                    }
                    else
                    {
                        source.StorageBillets.RemoveAt(i--);
                    }
                }
            }
            if (model.StorageBillets == null) model.StorageBillets = new Dictionary<int, (string, int)>();
            foreach (var sm in model.StorageBillets)
            {
                source.StorageBillets.Add(new StorageBillet
                {
                    Id = ++maxSMId,
                    StorageId = storage.Id,
                    BilletId = sm.Key,
                    Count = sm.Value.Item2
                });
            }
            return storage;
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
                StorageBillets = storageBillets
            };
        }
    }
}

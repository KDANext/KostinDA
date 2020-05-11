using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly DataListSingleton source;
        public ImplementerLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            Implementer tempImplementer = model.Id.HasValue ? null : new Implementer
            {
                Id = 1
            };
            foreach (var Implementer in source.Implementers)
            {
                if (!model.Id.HasValue && Implementer.Id >= tempImplementer.Id)
                {
                    tempImplementer.Id = Implementer.Id + 1;
                }
                else if (model.Id.HasValue && Implementer.Id == model.Id)
                {
                    tempImplementer = Implementer;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempImplementer == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempImplementer);
            }
            else
            {
                source.Implementers.Add(CreateModel(model, tempImplementer));
            }

        }
        public void Delete(ImplementerBindingModel model)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id.Value)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var Implementer in source.Implementers)
            {
                if (model != null)
                {
                    if (Implementer.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Implementer));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Implementer));
            }
            return result;
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer Implementer)
        {
            Implementer.ImplementerFIO = model.ImplementerFIO;
            Implementer.PauseTime = model.PauseTime;
            Implementer.WorkingTime = model.WorkingTime;
            return Implementer;
        }
        private ImplementerViewModel CreateViewModel(Implementer Implementer)
        {
            return new ImplementerViewModel
            {
                Id = Implementer.Id,
                ImplementerFIO = Implementer.ImplementerFIO,
                WorkingTime = Implementer.WorkingTime,
                PauseTime = Implementer.PauseTime
            };
        }
    }
}
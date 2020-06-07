using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith_sWorkshopFileImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly FileDataListSingleton source;
        public ImplementerLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть работник с таким именем");
            }
            if (model.Id.HasValue)
            {
                element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int? maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
                element = new Implementer { Id = maxId + 1 };
                source.Implementers.Add(element);
            }
            element.ImplementerFIO = model.ImplementerFIO;
            element.PauseTime = model.PauseTime;
            element.WorkingTime = model.WorkingTime;
        }
        public void Delete(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Implementers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            return source.Implementers
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ImplementerViewModel
            {
                Id = rec.Id.Value,
                ImplementerFIO = rec.ImplementerFIO,
                WorkingTime = rec.WorkingTime,
                PauseTime = rec.PauseTime
            })
            .ToList();
        }
    }
}

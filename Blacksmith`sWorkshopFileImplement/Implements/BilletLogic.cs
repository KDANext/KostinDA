using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopFileImplement.Implements
{
    public class BilletLogic : IBilletLogic
    {
        private readonly FileDataListSingleton source;
        public BilletLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(BilletBindingModel model)
        {
            Billet element = source.Billets.FirstOrDefault(rec => rec.BilletName == model.BilletName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Billets.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Billets.Count > 0 ? source.Billets.Max(rec =>
               rec.Id) : 0;
                element = new Billet { Id = maxId + 1 };
                source.Billets.Add(element);
            }
            element.BilletName = model.BilletName;
        }
        public void Delete(BilletBindingModel model)
        {
            Billet element = source.Billets.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Billets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<BilletViewModel> Read(BilletBindingModel model)
        {
            return source.Billets
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new BilletViewModel
            {
                Id = rec.Id,
                BilletName = rec.BilletName
            })
            .ToList();
        }
    }
}

using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class BilletLogic : IBilletLogic
    {
        private readonly DataListSingleton source;
        public BilletLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(BilletBindingModel model)
        {
            Billet tempComponent = model.Id.HasValue ? null : new Billet
            {
                Id = 1
            };
            foreach (var billet in source.Billets)
            {
                if (billet.BilletName == model.BilletName && billet.Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (!model.Id.HasValue && billet.Id >= tempComponent.Id)
                {
                    tempComponent.Id = billet.Id + 1;
                }
                else if (model.Id.HasValue && billet.Id == model.Id)
                {
                    tempComponent = billet;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempComponent == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempComponent);
            }
            else
            {
                source.Billets.Add(CreateModel(model, tempComponent));
            }

        }
        public void Delete(BilletBindingModel model)
        {
            for (int i = 0; i < source.Billets.Count; ++i)
            {
                if (source.Billets[i].Id == model.Id.Value)
                {
                    source.Billets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<BilletViewModel> Read(BilletBindingModel model)
        {
            List<BilletViewModel> result = new List<BilletViewModel>();
            foreach (var component in source.Billets)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private Billet CreateModel(BilletBindingModel model, Billet billet)
        {
            billet.BilletName = model.BilletName;
            return billet;
        }
        private BilletViewModel CreateViewModel(Billet component)
        {
            return new BilletViewModel
            {
                Id = component.Id,
                BilletName = component.BilletName
            };
        }
    }
}

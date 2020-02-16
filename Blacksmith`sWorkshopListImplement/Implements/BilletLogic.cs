using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class BilletLogic : IBilletLogic
    {
        private readonly DataListSingleton source;

        public BilletLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<BilletViewModel> GetList()
        {
            List<BilletViewModel> result = new List<BilletViewModel>();
            for (int i = 0; i < source.Billets.Count; ++i)
            {
                result.Add(new BilletViewModel
                {
                    Id = source.Billets[i].Id,
                    BilletName = source.Billets[i].BilletName
                });
            }
            return result;
        }

        public BilletViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Billets.Count; ++i)
            {
                if (source.Billets[i].Id == id)
                {
                    return new BilletViewModel
                    {
                        Id = source.Billets[i].Id,
                        BilletName = source.Billets[i].BilletName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BilletBindingModel model)
        {
            int maxId = 0; for (int i = 0; i < source.Billets.Count; ++i)
            {
                if (source.Billets[i].Id > maxId)
                {
                    maxId = source.Billets[i].Id;
                }
                if (source.Billets[i].BilletName == model.BilletName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Billets.Add(new Billet
            {
                Id = maxId + 1,
                BilletName = model.BilletName
            });
        }

        public void UpdElement(BilletBindingModel model)
        {
            int index = -1; for (int i = 0; i < source.Billets.Count; ++i)
            {
                if (source.Billets[i].Id == model.Id) { index = i; }
                if (source.Billets[i].BilletName == model.BilletName && source.Billets[i].Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Billets[index].BilletName = model.BilletName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Billets.Count; ++i)
            {
                if (source.Billets[i].Id == id)
                {
                    source.Billets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}

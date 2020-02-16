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
    class BilletLogic : IBilletLogic
    {
        private readonly FileDataListSingleton source;

        public BilletLogic() 
        { 
            source = FileDataListSingleton.GetInstance(); 
        }

        public List<BilletViewModel> GetList() 
        {
            List<BilletViewModel> result = source.Billets.Select(rec => new BilletViewModel 
                { 
                    Id = rec.Id, 
                    BilletName = rec.BilletName 
                }).ToList(); 
            return result; 
        }


        public BilletViewModel GetElement(int id) 
        { 
            Billet element = source.Billets.FirstOrDefault(rec => rec.Id == id); 
            if (element != null) 
            { 
                return new BilletViewModel 
                { 
                    Id = element.Id, 
                    BilletName = element.BilletName 
                }; 
            } 
            throw new Exception("Элемент не найден"); 
        }

        public void AddElement(BilletBindingModel model)
        {
            Billet element = source.Billets.FirstOrDefault(rec => rec.BilletName == model.BilletName); 
            if (element != null) 
            { 
                throw new Exception("Уже есть компонент с таким названием"); 
            }
            int maxId = source.Billets.Count > 0 ? source.Billets.Max(rec => rec.Id) : 0;
            source.Billets.Add(new Billet            
            {
                 Id = maxId + 1, 
                 BilletName = model.BilletName
            });
        }

        public void UpdElement(BilletBindingModel model) 
        { 
            Billet element = source.Billets.FirstOrDefault(rec => rec.BilletName == model.BilletName && rec.Id != model.Id); 
            if (element != null) 
            { 
                throw new Exception("Уже есть компонент с таким названием"); 
            } 
            element = source.Billets.FirstOrDefault(rec => rec.Id == model.Id); 
            if (element == null) 
            { 
                throw new Exception("Элемент не найден"); 
            } 
            element.BilletName = model.BilletName; 
        }

        public void DelElement(int id) 
        { 
            Billet element = source.Billets.FirstOrDefault(rec => rec.Id == id); 
            if (element != null) 
            { 
                source.Billets.Remove(element); 
            } 
            else 
            { 
                throw new Exception("Элемент не найден"); 
            } 
        }
    }
}

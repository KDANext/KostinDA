using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopFileImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly FileDataListSingleton source;
        public ProductLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ProductBindingModel model)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.ProductName ==
           model.ProductName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Products.Count > 0 ? source.Billets.Max(rec =>
               rec.Id) : 0;
                element = new Product { Id = maxId + 1 };
                source.Products.Add(element);
            }
            element.ProductName = model.ProductName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.ProductBillets.RemoveAll(rec => rec.ProductId == model.Id &&
           !model.ProductBillets.ContainsKey(rec.BilletId));
            // обновили количество у существующих записей
            var updateBillets = source.ProductBillets.Where(rec => rec.ProductId ==
            model.Id && model.ProductBillets.ContainsKey(rec.BilletId));
            foreach (var updateBillet in updateBillets)
            {
                updateBillet.Count =
               model.ProductBillets[updateBillet.BilletId].Item2;
                model.ProductBillets.Remove(updateBillet.BilletId);
            }
            // добавили новые
            int maxPCId = source.ProductBillets.Count > 0 ?
           source.ProductBillets.Max(rec => rec.Id) : 0;
            foreach (var pc in model.ProductBillets)
            {
                source.ProductBillets.Add(new ProductBillet
                {
                    Id = ++maxPCId,
                    ProductId = element.Id,
                    BilletId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(ProductBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.ProductBillets.RemoveAll(rec => rec.ProductId == model.Id);
            Product element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Products.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            return source.Products
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ProductViewModel
            {
                Id = rec.Id,
                ProductName = rec.ProductName,
                Price = rec.Price,
                ProductBillets = source.ProductBillets
            .Where(recPC => recPC.ProductId == rec.Id)
           .ToDictionary(recPC => recPC.BilletId, recPC =>
            (source.Billets.FirstOrDefault(recC => recC.Id ==
           recPC.BilletId)?.BilletName, recPC.Count))
            })
            .ToList();
        }
    }
}


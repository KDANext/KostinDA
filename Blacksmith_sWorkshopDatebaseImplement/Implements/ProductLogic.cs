using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopDatebaseImplement;
using Blacksmith_sWorkshopDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithsWorkshopDatebaseImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Product element = context.Products.FirstOrDefault(rec =>
                       rec.ProductName == model.ProductName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Products.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Product();
                            context.Products.Add(element);
                        }
                        element.ProductName = model.ProductName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var ProductBillets = context.ProductBillets.Where(rec
                           => rec.ProductId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.ProductBillets.RemoveRange(ProductBillets.Where(rec =>
                            !model.ProductBillets.ContainsKey(rec.BilletId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateBillet in ProductBillets)
                            {
                                updateBillet.Count =
                               model.ProductBillets[updateBillet.BilletId].Item2;

                                model.ProductBillets.Remove(updateBillet.BilletId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.ProductBillets)
                        {
                            context.ProductBillets.Add(new ProductBillet
                            {
                                ProductId = element.Id,
                                BilletId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ProductBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.ProductBillets.RemoveRange(context.ProductBillets.Where(rec =>
                        rec.ProductId == model.Id));
                        Product element = context.Products.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Products.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new BlacksmithsWorkshopDatebase())
            {
                return context.Products
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new ProductViewModel
               {
                   Id = rec.Id,
                   ProductName = rec.ProductName,
                   Price = rec.Price,
                   ProductBillets = context.ProductBillets
                .Include(recPC => recPC.Billet)
               .Where(recPC => recPC.ProductId == rec.Id)
               .ToDictionary(recPC => recPC.BilletId, recPC =>
                (recPC.Billet?.BilletName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
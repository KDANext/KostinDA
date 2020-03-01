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
    public class ProductLogic : IProductLogic
    {
        private readonly FileDataListSingleton source;

        public ProductLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<ProductViewModel> GetList()
        {
            List<ProductViewModel> result = source.Products.Select(rec => new ProductViewModel
            {
                Id = rec.Id,
                ProductName = rec.ProductName,
                Price = rec.Price,
                ProductBillets = source.ProductBillets.Where
                (recPC => recPC.ProductId == rec.Id).Select
                (recPC => new ProductBilletViewModel
                {
                    Id = recPC.Id,
                    ProductId = recPC.ProductId,
                    BilletId = recPC.BilletId,
                    BilletName = source.Billets.FirstOrDefault
                    (
                        recC => recC.Id == recPC.BilletId)?.BilletName,
                    Count = recPC.Count
                }).ToList()
            }).ToList(); return result;
        }
        public ProductViewModel GetElement(int id)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ProductViewModel
                {
                    Id = element.Id,
                    ProductName = element.ProductName,
                    Price = element.Price,
                    ProductBillets = source.ProductBillets.Where
                    (recPC => recPC.ProductId == element.Id)
                    .Select
                    (recPC => new ProductBilletViewModel
                    {
                        Id = recPC.Id,
                        ProductId = recPC.ProductId,
                        BilletId = recPC.BilletId,
                        BilletName = source.Billets.FirstOrDefault(recC => recC.Id == recPC.BilletId)?.BilletName,
                        Count = recPC.Count
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProductBindingModel model)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.ProductName == model.ProductName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Products.Count > 0 ? source.Products.Max(rec => rec.Id) : 0;
            source.Products.Add(new Product
            {
                Id = maxId + 1,
                ProductName = model.ProductName,
                Price = model.Price
            });
            // компоненты для изделия 
            int maxPCId = source.ProductBillets.Count > 0 ? source.ProductBillets.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам 
            var groupComponents = model.ProductBillets.GroupBy(rec => rec.BilletId).Select(rec => new
            {
                ComponentId = rec.Key,
                Count = rec.Sum(r => r.Count)
            });
            // добавляем компоненты     
            foreach (var groupComponent in groupComponents)
            {
                source.ProductBillets.Add(new ProductBillet
                {
                    Id = ++maxPCId,
                    ProductId = maxId + 1,
                    BilletId = groupComponent.ComponentId,
                    Count = groupComponent.Count
                });
            }
        }

        public void UpdElement(ProductBindingModel model)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.ProductName == model.ProductName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ProductName = model.ProductName;
            element.Price = model.Price;
            int maxPCId = source.ProductBillets.Count > 0 ? source.ProductBillets.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты 
            var compIds = model.ProductBillets.Select(rec => rec.BilletId).Distinct();
            var updateComponents = source.ProductBillets.Where(rec => rec.ProductId == model.Id && compIds.Contains(rec.BilletId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.ProductBillets.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
            }
            source.ProductBillets.RemoveAll(rec => rec.ProductId == model.Id && !compIds.Contains(rec.BilletId));
            // новые записи       
            var groupComponents = model.ProductBillets
                .Where(rec => rec.Id == 0)
                .GroupBy(rec => rec.BilletId)
                .Select(rec => new
                {
                    ComponentId = rec.Key,
                    Count = rec.Sum(r => r.Count)
                });
            foreach (var groupComponent in groupComponents)
            {
                ProductBillet elementPC = source.ProductBillets.FirstOrDefault(rec => rec.ProductId == model.Id && rec.BilletId == groupComponent.ComponentId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.ProductBillets.Add(new ProductBillet
                    {
                        Id = ++maxPCId,
                        ProductId = model.Id,
                        BilletId = groupComponent.ComponentId,
                        Count = groupComponent.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {             
            Product element = source.Products.FirstOrDefault(rec => rec.Id == id);   
            if (element != null)     
            {             
                // удаяем записи по компонентам при удалении изделия       
                source.ProductBillets.RemoveAll(rec => rec.ProductId == id);       
                source.Products.Remove(element);  
            }         
            else      
            {            
                throw new Exception("Элемент не найден");    
            }    
        }

    }
}
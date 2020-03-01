using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly DataListSingleton source;
        public ProductLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ProductBindingModel model)
        {
            Product tempProduct = model.Id.HasValue ? null : new Product { Id = 1 };
            foreach (var product in source.Products)
            {
                if (product.ProductName == model.ProductName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Products.Add(CreateModel(model, tempProduct));
            }
        }

        public void Delete(ProductBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].ProductId == model.Id)
                {
                    source.ProductBillets.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Product CreateModel(ProductBindingModel model, Product product)
        {
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].Id > maxPCId)
                {
                    maxPCId = source.ProductBillets[i].Id;
                }
                if (source.ProductBillets[i].ProductId == product.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.ProductBillets.ContainsKey(source.ProductBillets[i].BilletId))
                    {
                        // обновляем количество
                        source.ProductBillets[i].Count = model.ProductBillets[source.ProductBillets[i].BilletId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные

                        model.ProductBillets.Remove(source.ProductBillets[i].BilletId);
                    }
                    else
                    {
                        source.ProductBillets.RemoveAt(i--);
                    }
                }
            }
            foreach (var pc in model.ProductBillets)
            {
                source.ProductBillets.Add(new ProductBillet
                {
                    Id = ++maxPCId,
                    ProductId = product.Id,
                    BilletId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return product;
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            foreach (var component in source.Products)
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
        private ProductViewModel CreateViewModel(Product product)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> productComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in source.ProductBillets)
            {
                if (pc.ProductId == product.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Billets)
                    {
                        if (pc.BilletId == component.Id)
                        {
                            componentName = component.BilletName;
                            break;
                        }
                    }
                    productComponents.Add(pc.BilletId, (componentName, pc.Count));
                }
            }
            return new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductBillets = productComponents
            };
        }
    }
}


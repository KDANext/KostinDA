using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly DataListSingleton source;
        public ProductLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ProductViewModel> GetList()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            for (int i = 0; i < source.Products.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<ProductBilletViewModel> productBillets = new List<ProductBilletViewModel>();
                for (int j = 0; j < source.ProductBillets.Count; ++j)
                {
                    if (source.ProductBillets[j].ProductId == source.Products[i].Id)
                    {
                        string billetName = string.Empty;
                        for (int k = 0; k < source.Billets.Count; ++k)
                        {
                            if (source.ProductBillets[j].BilletId == source.Billets[k].Id)
                            {
                                billetName = source.Billets[k].BilletName;
                                break;
                            }
                        }
                        productBillets.Add(new ProductBilletViewModel
                        {
                             Id = source.ProductBillets[j].Id,
                             ProductId = source.ProductBillets[j].ProductId,
                             BilletId = source.ProductBillets[j].BilletId,
                             BilletName = billetName,
                             Count = source.ProductBillets[j].Count
                         });
                    }
                }
                result.Add(new ProductViewModel
                {
                    Id = source.Products[i].Id,
                    ProductName = source.Products[i].ProductName,
                    Price = source.Products[i].Price,
                    ProductBillets = productBillets
                });
            }
            return result;
        }
        public ProductViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<ProductBilletViewModel> productBillets = new List<ProductBilletViewModel>();
                for (int j = 0; j < source.ProductBillets.Count; ++j)
                {
                    if (source.ProductBillets[j].ProductId == source.Products[i].Id)
                    {
                        string billetName = string.Empty;
                        for (int k = 0; k < source.Billets.Count; ++k)
                        {
                            if (source.ProductBillets[j].BilletId ==
                           source.Billets[k].Id)
                            {
                                billetName = source.Billets[k].BilletName;
                                break;
                            }
                        }
                        productBillets.Add(new ProductBilletViewModel
                        {
                            Id = source.ProductBillets[j].Id,
                            ProductId = source.ProductBillets[j].ProductId,
                            BilletId = source.ProductBillets[j].BilletId,
                            BilletName = billetName,
                            Count = source.ProductBillets[j].Count
                        });
                    }
                }
                if (source.Products[i].Id == id)
                {
                    return new ProductViewModel
                    {
                        Id = source.Products[i].Id,
                        ProductName = source.Products[i].ProductName,
                        Price = source.Products[i].Price,
                        ProductBillets = productBillets
                    };
                }
            }

        throw new Exception("Элемент не найден");
        }
        public void AddElement(ProductBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id > maxId)
                {
                    maxId = source.Products[i].Id;
                }
                if (source.Products[i].ProductName == model.ProductName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Products.Add(new Product
            {
                Id = maxId + 1,
                ProductName = model.ProductName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].Id > maxPCId)
                {
                    maxPCId = source.ProductBillets[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.ProductBillets.Count; ++i)
            {
                for (int j = 1; j < model.ProductBillets.Count; ++j)
                {
                    if (model.ProductBillets[i].BilletId ==
                    model.ProductBillets[j].BilletId)
                    {
                        model.ProductBillets[i].Count +=
                        model.ProductBillets[j].Count;
                        model.ProductBillets.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.ProductBillets.Count; ++i)
            {
                source.ProductBillets.Add(new ProductBillet
                {
                    Id = ++maxPCId,
                    ProductId = maxId + 1,
                    BilletId = model.ProductBillets[i].BilletId,
                    Count = model.ProductBillets[i].Count
                });
            }
        }
        public void UpdElement(ProductBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Products[i].ProductName == model.ProductName &&
                source.Products[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Products[index].ProductName = model.ProductName;
            source.Products[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].Id > maxPCId)
                {
                    maxPCId = source.ProductBillets[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].ProductId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ProductBillets.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.ProductBillets[i].Id ==
                        model.ProductBillets[j].Id)
                        {
                            source.ProductBillets[i].Count =
                           model.ProductBillets[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.ProductBillets.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.ProductBillets.Count; ++i)
            {
                if (model.ProductBillets[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.ProductBillets.Count; ++j)
                    {
                        if (source.ProductBillets[j].ProductId == model.Id &&
                        source.ProductBillets[j].BilletId ==
                       model.ProductBillets[i].BilletId)
                        {
                            source.ProductBillets[j].Count += model.ProductBillets[i].Count;
                            model.ProductBillets[i].Id = source.ProductBillets[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.ProductBillets[i].Id == 0)
                    {
                        source.ProductBillets.Add(new ProductBillet
                        {
                            Id = ++maxPCId,
                            ProductId = model.Id,
                            BilletId = model.ProductBillets[i].BilletId,
                            Count = model.ProductBillets[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ProductBillets.Count; ++i)
            {
                if (source.ProductBillets[i].ProductId == id)
                {
                    source.ProductBillets.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}

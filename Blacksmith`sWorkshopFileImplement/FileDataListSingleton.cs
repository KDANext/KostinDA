using Blacksmith_sWorkshopBusinessLogic.Enums;
using Blacksmith_sWorkshopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Blacksmith_sWorkshopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string BilletFileName = "Billet.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ProductFileName = "Product.xml";
        private readonly string ProductBilletFileName = "ProductBillet.xml";
        public List<Billet> Billets { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductBillet> ProductBillets { get; set; }
        private FileDataListSingleton()
        {
            Billets = LoadBillets();
            Orders = LoadOrders();
            Products = LoadProducts();
            ProductBillets = LoadProductBillets();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveBillets();
            SaveOrders();
            SaveProducts();
            SaveProductBillets();
        }
        private List<Billet> LoadBillets()
        {
            var list = new List<Billet>();
            if (File.Exists(BilletFileName))
            {
                XDocument xDocument = XDocument.Load(BilletFileName);
                var xElements = xDocument.Root.Elements("Billet").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Billet
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        BilletName = elem.Element("BilletName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Product> LoadProducts()
        {
            var list = new List<Product>();
            if (File.Exists(ProductFileName))
            {
                XDocument xDocument = XDocument.Load(ProductFileName);
                var xElements = xDocument.Root.Elements("Product").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Product
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductName = elem.Element("ProductName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<ProductBillet> LoadProductBillets()
        {
            var list = new List<ProductBillet>();
            if (File.Exists(ProductBilletFileName))
            {
                XDocument xDocument = XDocument.Load(ProductBilletFileName);
                var xElements = xDocument.Root.Elements("ProductBillet").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new ProductBillet
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
                        BilletId = Convert.ToInt32(elem.Element("BilletId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveBillets()
        {
            if (Billets != null)
            {
                var xElement = new XElement("Billets");
                foreach (var Billet in Billets)
                {
                    xElement.Add(new XElement("Billet",
                    new XAttribute("Id", Billet.Id),
                    new XElement("BilletName", Billet.BilletName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(BilletFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("ProductId", order.ProductId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveProducts()
        {
            if (Products != null)
            {
                var xElement = new XElement("Products");
                foreach (var product in Products)
                {
                    xElement.Add(new XElement("Product",
                    new XAttribute("Id", product.Id),
                    new XElement("ProductName", product.ProductName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductFileName);
            }
        }
        private void SaveProductBillets()
        {
            if (ProductBillets != null)
            {
                var xElement = new XElement("ProductBillets");
                foreach (var productBillet in ProductBillets)
                {
                    xElement.Add(new XElement("ProductBillet",
                    new XAttribute("Id", productBillet.Id),
                    new XElement("ProductId", productBillet.ProductId),
                    new XElement("BilletId", productBillet.BilletId),
                    new XElement("Count", productBillet.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductBilletFileName);
            }
        }
    }
}

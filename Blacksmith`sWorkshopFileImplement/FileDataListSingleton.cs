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
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";
        public List<Billet> Billets { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductBillet> ProductBillets { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> MessageInfos { get; set; }
        private FileDataListSingleton()
        {
            Billets = LoadBillets();
            Orders = LoadOrders();
            Products = LoadProducts();
            ProductBillets = LoadProductBillets();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessageInfos = LoadMessageInfos();
        }

        private List<MessageInfo> LoadMessageInfos()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MessageInfoFileName))
            {
                XDocument xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("MessageInfos").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        Body = elem.Element("Body").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value),
                        MessageId = elem.Element("MessageId").Value,
                        SenderName = elem.Element("SenderName").Value,
                        Subject = elem.Element("Subject").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var Client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", Client.Id),
                    new XElement("ClientFIO", Client.ClientFIO),
                    new XElement("Login",Client.Login),
                    new XElement("Password", Client.Password)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
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
            SaveClients();
            SaveImplementers();
            SaveMessageInfos();
        }

        private void SaveMessageInfos()
        {
            if(MessageInfos != null)
            {
                var xElement = new XElement("MessageInfos");
                foreach (var message in MessageInfos)
                {
                    xElement.Add(
                        new XElement("MessageInfo",
                        new XElement("Body",message.Body),
                        new XElement("ClientId", message.ClientId),
                        new XElement("DateDelivery", message.DateDelivery),
                        new XElement("MessageId", message.MessageId),
                        new XElement("SenderName", message.SenderName),
                        new XElement("Subject", message.Subject)
                        ));
                }
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");

                foreach (var implementer in Implementers)
                {
                    xElement.Add(
                        new XElement("Implementer",
                        new XAttribute("Id", implementer.Id),
                        new XElement("ImplementerFIO", implementer.ImplementerFIO),
                        new XElement("PauseTime", implementer.PauseTime),
                        new XElement("WorkingTime", implementer.WorkingTime)
                        ));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
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
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
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
                    new XElement("ClientId", order.ClientId),
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

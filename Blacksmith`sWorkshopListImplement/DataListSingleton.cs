using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Billet> Billets { get; set; }

        public List<Order> Orders { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductBillet> ProductBillets { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        private DataListSingleton() {
            Billets = new List<Billet>();
            Orders = new List<Order>();
            Products = new List<Product>();
            ProductBillets = new List<ProductBillet>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null) { instance = new DataListSingleton(); }

            return instance;
        }
    }
}

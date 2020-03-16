using Blacksmith_sWorkshopListImplement.Models;
using System.Collections.Generic;

namespace Blacksmith_sWorkshopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Billet> Billets { get; set; }

        public List<Order> Orders { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductBillet> ProductBillets { get; set; }
        public List<Storage> Storages { get; set; }
        public List<StorageBillet> StorageBillets { get; set; }
        private DataListSingleton()
        {
            Billets = new List<Billet>();
            Orders = new List<Order>();
            Products = new List<Product>();
            ProductBillets = new List<ProductBillet>();
            Storages = new List<Storage>();
            StorageBillets = new List<StorageBillet>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null) { instance = new DataListSingleton(); }

            return instance;
        }
    }
}

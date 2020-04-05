using Blacksmith_sWorkshopDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace Blacksmith_sWorkshopDatebaseImplement
{
    public class BlacksmithsWorkshopDatebase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Blacksmith_sWorkshopDatebase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Billet> Billets { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<ProductBillet> ProductBillets { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}

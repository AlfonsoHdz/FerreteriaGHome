using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data
{
    using FerreteriaGHome.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext: IdentityDbContext<User>
    {
      

        public DbSet<Client> Clients { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleAgent> SalesAgents { get; set; }

        public DbSet<Shopping> Shoppings { get; set; }
        public DbSet<ShoppingDetail> ShoppingDetails { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }

        public DbSet<SaleF> SaleFs { get; set; }

        public DbSet<SaleFDetail> SaleFDetails { get; set; }

        public DbSet<SaleFDetailTemp> SaleFDetailTemps { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }

        //TODO: sobreescribir el metodo onmodelcreating para la eliminacion de cascada


    }
}

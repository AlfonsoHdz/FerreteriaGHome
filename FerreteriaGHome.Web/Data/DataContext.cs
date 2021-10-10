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
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesAgent> SalesAgents { get; set; }

        public DbSet<Shopping> Shoppings { get; set; }
        public DbSet<ShoppingDetail> ShoppingDetails { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }

        //TODO: sobreescribir el metodo onmodelcreating para la eliminacion de cascada


    }
}

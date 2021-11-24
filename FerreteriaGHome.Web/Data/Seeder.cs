using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaGHome.Web.Data
{
    using FerreteriaGHome.Web.Data.Entities;
    using System.Threading.Tasks;
    using System.Linq;
    using FerreteriaGHome.Web.Helper;
    using Microsoft.AspNetCore.Identity;


    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();

            //Roles de usuario
           

            await userHelper.CheckRoleAsync("Client");
            await userHelper.CheckRoleAsync("SalesAgent");
            await userHelper.CheckRoleAsync("Admin");


          


            //Alta de usuarios
            if (!this.dataContext.Admin.Any())
            {

                var user = await CheckUser("Jesse", "Cerezo", "22262824", "admin@gmail.com", "543211");
                await CheckAdmin(user, "Admin");

            }


            if (!this.dataContext.Clients.Any())
            {

                var user = await CheckUser("Pedro", "Palacios", "22262824", "palacios@gmail.com", "543211");
                await CheckClients(user, "Client");

            }

            if (!this.dataContext.SalesAgents.Any())
            {
                var user = await CheckUser("Juan", "Mendez", "222526287", "juan@gmail.com", "123456");
                await CheckSalesAgent(user, "SalesAgent");

            }


            //
            if (!this.dataContext.Products.Any())
            {
                await CheckProducts("Pinzas", "Pinzas de tipo Presión", 25, "~/images/products/Pinzas.png", 5);
            }

            if (!this.dataContext.Providers.Any())
            {
                await CheckProviders("Trupper", "Puebla,Pue Col Margaritas No 23", 2225634582, "trupper@hotmail.com");
                await CheckProviders("Voltek", "Puebla,Pue Col Carmen No 425", 5569423584, "voltek@hotmail.com");
                await CheckProviders("JabonesPro", "Puebla,Pue Col Santiago Momoxpan No 40", 5566842487, "jabonespro2021@outlook.com");
                await CheckProviders("TuercasMartinez", "Puebla,Pue Infonavit Las Margaritas", 2223475861, "tuercassuper@gmail.com");
            }

            if (!this.dataContext.Sales.Any())
            {
                await CheckSales(DateTime.Now, "Venta de Manguera de Gas 40cm", 89);
                await CheckSales(DateTime.Now, "Venta de paquete de tuercas", 500);
            }


            if (!this.dataContext.SaleDetails.Any())
            {
                await CheckSaleDetail("Jabon Zote", DateTime.Now, 25);
                await CheckSaleDetail("Llave de cruz", DateTime.Now, 50);
            }

           

            if (!this.dataContext.Shoppings.Any())
            {
                await CheckShopping("22069", "Paquete de taquetes de Madera", 16, 55);
                await CheckShopping("54871", "Paquete de tornillo", 16, 584);
               
            }
            if (!this.dataContext.ShoppingDetails.Any())
            {
                await CheckShoppingDetais("Taquetes de medida 4mm", 86, 5, DateTime.Now);
                await CheckShoppingDetais("Tornillos de tablaroca de media pulgada", 50, 5, DateTime.Now);
            }

            if (!this.dataContext.Brands.Any())
            {
                await CheckBrandDetais("Truper");
                await CheckBrandDetais("Voltek");
            }

        }


        private async Task CheckClients(User user, string rol)
        {
            this.dataContext.Clients.Add(new Client { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckAdmin(User user, string rol)
        {
            this.dataContext.Admin.Add(new Admin { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckSalesAgent(User user, string rol)
        {
            this.dataContext.SalesAgents.Add(new SaleAgent { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }



        //

        //Metodos
        //
        private async Task CheckProducts(string name, string descripcion, decimal price, string imagenUrl, double stock)
        {
            this.dataContext.Products.Add(new Product 
            { 
                Name = name, 
                Descripcion = descripcion, 
                Price = price,
                ImagenUrl = imagenUrl,
                Stock = stock,
                

                
                
            });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProviders(string name, string addres, long telephone, string email)
        {
            this.dataContext.Providers.Add(new Provider { Name = name, Address = addres, Telephone = telephone, Email = email });

            await this.dataContext.SaveChangesAsync();
        }


        private async Task CheckSaleDetail(string name, DateTime date, decimal price)
        {
            this.dataContext.SaleDetails.Add(new SaleDetail { Name = name, Date = date, Price = price });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSales(DateTime date, string description, decimal cost)
        {
            this.dataContext.Sales.Add(new Sale { Date = date, Description = description, Cost = cost });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckShopping( string folio, string dato, decimal iva, decimal total)
        {
            this.dataContext.Shoppings.Add(new Shopping { Folio = folio, DatoShopping = dato, IVA = iva, Total = total });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckShoppingDetais(string descripcion, decimal cost, int quantity, DateTime date)
        {
            this.dataContext.ShoppingDetails.Add(new ShoppingDetail { Descripcion = descripcion, Cost = cost, Quantity = quantity, Date = date });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckBrandDetais(string name)
        {
            this.dataContext.Brands.Add(new Brand { Name = name});

            await this.dataContext.SaveChangesAsync();
        }



        private async Task<User> CheckUser(string firstName, string lastName, string phoneNumber, string email, string password)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    UserName = email
                };
                var result = await userHelper.AddUserAsync(user, password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Error no se pudo crear el usuario");
                }
            }
            return user;
        }
    }
}
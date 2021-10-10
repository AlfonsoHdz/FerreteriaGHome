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


            //Alta de usuarios


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
                await CheckProducts("Pinzas",  "Pinzas de tipo Presión",  164.25);
                await CheckProducts("Tornillos", "Negros tipo tabla roca", 12.63);
                await CheckProducts("Chapa", "Chapa de color Mate con seguro", 155.80);
                await CheckProducts("Llave de Cruz", "Llave tipo de cruz", 90.5);
                await CheckProducts("Martillo", "Clavo grueso", 100.55);

            }
            if (!this.dataContext.Providers.Any())
            {
                await CheckProviders("Trupper",  "Puebla,Pue Col Margaritas No 23", 2225634582, "trupper@hotmail.com");
                await CheckProviders("Voltek", "Puebla,Pue Col Carmen No 425", 5569423584, "voltek@hotmail.com");
                await CheckProviders("JabonesPro", "Puebla,Pue Col Santiago Momoxpan No 40", 5566842487, "jabonespro2021@outlook.com");
                await CheckProviders("TuercasMartinez", "Puebla,Pue Infonavit Las Margaritas", 2223475861, "tuercassuper@gmail.com");
            }

            if (!this.dataContext.SaleDetails.Any())
            {
                await CheckSaleDetail("Jabon Zote",  "09/06/21",  25.41);
                await CheckSaleDetail("Llave de cruz", "04/06/21", 50.63);
            }

            if (!this.dataContext.Sales.Any())
            {
                await CheckSales("22/02/21",  "Venta de Manguera de Gas 40cm",  89.45);
                await CheckSales("14/02/2021", "Venta de paquete de tuercas", 500);
            }

            if (!this.dataContext.Shoppings.Any())
            {
                await CheckShopping(22069,  "Paquete de taquetes de Madera",  55.42*.16,  55.42);
                await CheckShopping(54871, "Paquete de tornillo",584.25*.16, 584.25);
            }
            if (!this.dataContext.ShoppingDetails.Any())
            {
                await CheckShoppingDetais("Taquetes de medida 4mm",  86.5,  5,  "15/04/21");
                await CheckShoppingDetais("Tornillos de tablaroca de media pulgada", 50, 5, "08/04/2021" );
            }
        }


        private async Task CheckClients(User user, string rol)
        {
            this.dataContext.Clients.Add(new Clients { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckSalesAgent(User user, string rol)
        {
            this.dataContext.SalesAgents.Add(new SalesAgent { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

       

        //

        //Metodos
        //
        private async Task CheckProducts(string name, string descripcionP, double priceP)
        {
            this.dataContext.Products.Add(new Products { nameP = name, descripcionP = descripcionP, priceP = priceP });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckProviders(string NamePro, string AddresPro, long Telephone, string EmailPro)
        {
            this.dataContext.Providers.Add(new Provider { namePro = NamePro, addressPro = AddresPro, telephone = Telephone, emailPro = EmailPro });

            await this.dataContext.SaveChangesAsync();
        }


        private async Task CheckSaleDetail(string NameV, string date, double PriceV)
        {
            this.dataContext.SaleDetails.Add(new SaleDetail { nameV =NameV, Date = date, priceV = PriceV});

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSales(string DateSale, string DescriptionS, double CostV)
        {
            this.dataContext.Sales.Add(new Sales { dateSale = DateSale, descriptionS = DescriptionS, costV = CostV });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckShopping(int folio, string datoShopping, double iVA, double total)
        {
            this.dataContext.Shoppings.Add(new Shopping { Folio= folio, DatoShopping=datoShopping,IVA=iVA, Total = total });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckShoppingDetais(string descripciondc, double costc, int  quantity, string dateShoppingDC)
        {
            this.dataContext.ShoppingDetails.Add(new ShoppingDetail { descripcionDC = descripciondc, costC = costc, Quantity = quantity, DateShoppingDC=dateShoppingDC });

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

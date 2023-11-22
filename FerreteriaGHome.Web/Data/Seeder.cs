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
    using System.Runtime.ConstrainedExecution;


    public class Seeder
    {

        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;
        private readonly RoleManager<IdentityRole> roleManager;

        public Seeder(DataContext dataContext, IUserHelper userHelper, RoleManager<IdentityRole> roleManager)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();

            await userHelper.CheckRoleAsync("Student");
            await userHelper.CheckRoleAsync("Teacher");

            if (!this.dataContext.Teachers.Any())
            {
                var role = await roleManager.FindByNameAsync("Teacher");
                var user = await CheckUser("Jesse", "Cerezo", "Honorato", "student@gmail.com", "123456","Teacher", role);
                await CheckStudents(user, "Teacher");
            }

            if (!this.dataContext.Students.Any())
            {
                var role = await roleManager.FindByNameAsync("Student");
                var user = await CheckUser("Yael", "Palace", "Sola", "teacher@gmail.com", "123456","Student",role);
                await CheckTeachers(user, "Student");
            }

            if (!this.dataContext.Priorities.Any())
            {
                await CheckPriorities("Alta");
                await CheckPriorities("Media");
                await CheckPriorities("Baja");
            }

            if (!this.dataContext.Statuses.Any())
            {
                await CheckStatuses("Pendiente");
                await CheckStatuses("Proceso");
                await CheckStatuses("Finalizada");
            }


        }

        private async Task CheckTeachers(User user, string rol)
        {
            this.dataContext.Teachers.Add(new Teacher { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckStudents(User user, string rol)
        {
            this.dataContext.Students.Add(new Student { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }


        private async Task CheckPriorities(string name)
        {
            this.dataContext.Priorities.Add(new Priority { Description = name });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckStatuses(string name)
        {
            this.dataContext.Statuses.Add(new Status { Description = name });

            await this.dataContext.SaveChangesAsync();
        }

        private async Task<User> CheckUser(string firstName, string fathersName,string maternalName, string email, string password, string roleName, IdentityRole role)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    FathersName = fathersName,
                    MaternalName = maternalName,
                    Email = email,
                    UserName = email,
                    Role = role
                };
                var result = await userHelper.AddUserAsync(user, password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Error no se pudo crear el usuario");
                }
                await userHelper.AddUserToRoleAsync(user, roleName);
            }
            return user;
        }
    }
}
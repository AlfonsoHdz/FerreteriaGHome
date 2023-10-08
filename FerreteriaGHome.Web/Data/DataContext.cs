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
        public DbSet<Proyect> Proyects { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //TODO: sobreescribir el metodo onmodelcreating para la eliminacion de cascada


    }
}

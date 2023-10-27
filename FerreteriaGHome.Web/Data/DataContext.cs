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
        public DbSet<ProyectStudent> ProyectStudents { get; set; }
        public DbSet<ProyectUser> ProyectUsers { get; set; }
        public DbSet<ProyectActivity> ProyectActivities { get; set; }
        public DbSet<ActivityUser> ActivityUsers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProyectStudent>()
                .HasKey(ps => new { ps.ProyectId, ps.StudentId });

            modelBuilder.Entity<ProyectStudent>()
                .HasOne(ps => ps.Proyect)
                .WithMany(p => p.ProyectStudents)
                .HasForeignKey(ps => ps.ProyectId);

            modelBuilder.Entity<ProyectStudent>()
                .HasOne(ps => ps.Student)
                .WithMany(s => s.ProyectStudents)
                .HasForeignKey(ps => ps.StudentId);

            //Clase intermedia Poryect/ProyectUser/User

            modelBuilder.Entity<ProyectUser>()
                .HasKey(pu => new { pu.ProyectId, pu.UserId });

            modelBuilder.Entity<ProyectUser>()
                .HasOne(pu => pu.Proyect)
                .WithMany(p => p.ProyectUsers)
                .HasForeignKey(pu => pu.ProyectId);

            modelBuilder.Entity<ProyectUser>()
                .HasOne(pu => pu.User)
                .WithMany(s => s.ProyectUsers)
                .HasForeignKey(pu => pu.UserId);

            //Clase intermedia Poryect/ProyectActivity/Activity

            modelBuilder.Entity<ProyectActivity>()
                .HasKey(pa => new {pa.ProyectId, pa.ActivityId});

            modelBuilder.Entity<ProyectActivity>()
                .HasOne(pa => pa.Proyect)
                .WithMany(p => p.ProyectActivities)
                .HasForeignKey(pa => pa.ProyectId);

            modelBuilder.Entity<ProyectActivity>()
                .HasOne(pa => pa.Activity)
                .WithMany(s => s.ProyectActivities)
                .HasForeignKey(pa => pa.ActivityId);

            //Clase intermedia Activity/ActivityUser/User

            modelBuilder.Entity<ActivityUser>()
                .HasKey(au => new { au.ActivityId, au.UserId });

            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.Activity)
                .WithMany(s => s.ActivityUsers)
                .HasForeignKey(au => au.ActivityId);

            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.User)
                .WithMany(s => s.ActivityUsers)
                .HasForeignKey(au => au.UserId);

        }
    }
}

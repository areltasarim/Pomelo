using EticaretWebCoreEntity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace EticaretWebCoreEntity
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public AppDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            Assembly
                   .GetExecutingAssembly()
                   .GetTypes()
                   .Where(p => !p.IsAbstract && p.GetInterfaces().Any(q => q.Name == nameof(IBaseEntity)))
                   .ToList()
                   .ForEach(p => p.GetMethod(nameof(IBaseEntity.Build)).Invoke(Activator.CreateInstance(p), new[] { builder }));

            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            var provider = configuration["Application:DatabaseProvider"];


            optionsBuilder.UseMySql(configuration.GetConnectionString("Mysql"), ServerVersion.AutoDetect(configuration.GetConnectionString("Mysql"))).LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            optionsBuilder.UseLazyLoadingProxies();

        }

       
        public virtual DbSet<Markalar> Markalar { get; set; }


    }
}
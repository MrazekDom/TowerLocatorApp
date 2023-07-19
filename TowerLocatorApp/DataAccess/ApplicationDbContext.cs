using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TowerLocatorApp.Models;
using TowerLocatorApp.Utility;

namespace TowerLocatorApp.DataAccess {
    public class ApplicationDbContext:DbContext {
        private readonly IConfiguration configuration;

        public DbSet<DetailedMapPoint> MapPoints { get; set; }
        public DbSet<BTS> BTSSet { get; set; }
        public DbSet<RouteWithData> Routes { get; set; }

        public ApplicationDbContext(IConfiguration configuration) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MapDb"),
                options => options.UseNetTopologySuite());  /*metoda musi byt tady i v Program.cs*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasPostgresExtension("postgis");   /* pro postgis */
            modelBuilder.Entity<DetailedMapPoint>()    /*one-to-many relationship*/
                .HasOne(point => point.Route)               /* kazdy bod ma prirazenou jednu cestu/Route */
                .WithMany(route => route.RoutePoints)       /*kazda cesta/Route ma mnoho bodu*/
                .HasForeignKey(point => point.RouteId);

            modelBuilder.Entity<BTS>()                 /*stejny princip jako vyse*/
                .HasOne(tower =>tower.Route)
                .WithMany(route =>route.AssociatedTowers)
                .HasForeignKey(tower => tower.RouteId);

            modelBuilder.ApplyUtcDateTimeConverter(); /* extension trida ze stackoverflow, built-in metody na konverzi data na UTC nefunguji */
        }

    
    }
    
}

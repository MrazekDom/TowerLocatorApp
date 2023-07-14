using Microsoft.EntityFrameworkCore;
using TowerLocatorApp.Models;

namespace TowerLocatorApp.DataAccess {
    public class ApplicationDbContext:DbContext {
        private readonly IConfiguration configuration;

        public DbSet<DetailedMapPointModel> MapPoints { get; set; }
        public DbSet<BTSModel> BTSSet { get; set; }
        public DbSet<RouteModel> Routes { get; set; }

        public ApplicationDbContext(IConfiguration configuration) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MapDb"),
                options => options.UseNetTopologySuite());  /*metoda musi byt tady i v Program.cs*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasPostgresExtension("postgis");   /* pro postgis */
            modelBuilder.Entity<DetailedMapPointModel>()    /*one-to-many relationship*/
                .HasOne(point => point.Route)               /* kazdy bod ma prirazenou jednu cestu/Route */
                .WithMany(route => route.RoutePoints)       /*kazda cesta/Route ma mnoho bodu*/
                .HasForeignKey(point => point.RouteId);

            modelBuilder.Entity<BTSModel>()                 /*stejny princip jako vyse*/
                .HasOne(tower =>tower.Route)
                .WithMany(route =>route.AssociatedTowers)
                .HasForeignKey(tower => tower.RouteId);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using TowerLocatorApp.Models;
using TowerLocatorApp.Utility;
using TowerLocatorApp.ViewModels;

namespace TowerLocatorApp.DataAccess.Services
{
    public class RouteService
    {
        ApplicationDbContext dbContext { get; set; }
        public RouteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SaveRouteAsync(IFormFile gpxFile, IFormFile csvFile, string routeName)
        {
            StreamReader? csvReader = null;
            StreamReader? gpxReader = null;
            string csvFileContents;
            string gpxFileContents;
            try
            {
                csvReader = new StreamReader(csvFile.OpenReadStream());
                gpxReader = new StreamReader(gpxFile.OpenReadStream());

                csvFileContents = await csvReader.ReadToEndAsync(); /*prectu soubory*/
                gpxFileContents = await gpxReader.ReadToEndAsync();

                Task<List<BTS>> towersTask = UtilityMethods.ExtractCSV(csvFileContents);    /*zpracuju data*/
                List<BTS> towers = await towersTask;
                List<DetailedMapPoint> points = UtilityMethods.ParseGPXFile(gpxFileContents);

                RouteWithData newRoute = new RouteWithData{
                    Name = routeName,
                    AssociatedTowers = towers,
                    RoutePoints = points
                };

                Coordinate[] coordinates = new Coordinate[points.Count]; /*pole souradnic, protoze LineString bere jako parametr pole*/

                int i = 0;
                foreach (var point in points)
                {
                    point.Route = newRoute;
                    dbContext.MapPoints.Add(point);
                    coordinates[i] = point.Coordinates.Coordinate;
                    i++;
                }
                foreach (var tower in towers)
                {
                    tower.Route = newRoute;
                    dbContext.BTSSet.Add(tower);
                }
                newRoute.Line = new LineString(coordinates);
                dbContext.Routes.Add(newRoute);
                await dbContext.SaveChangesAsync();
            }
            finally
            {
                csvReader?.Dispose();
                gpxReader?.Dispose();
            }

        }

        public async Task<IEnumerable<RouteViewModel>> getAllRoutesAsync() {
             var allRoutes =  await dbContext.Routes.ToListAsync();
            List<RouteViewModel> routeNamesAndIds = new List<RouteViewModel>();
            foreach(var route in allRoutes) {
                RouteViewModel routeViewModel = new RouteViewModel {
                    Name = route.Name,
                    Id = route.Id,
                };
                routeNamesAndIds.Add(routeViewModel);
            }
            return routeNamesAndIds;
        }

        public async Task deleteRouteAsync(int id) {
            var routeToDelete = await dbContext.Routes.Include(r=>r.RoutePoints).Include(r=>r.AssociatedTowers).FirstOrDefaultAsync(r => r.Id == id);
            dbContext.Routes.Remove(routeToDelete);
            
            await dbContext.SaveChangesAsync();
        }
    }
}


using NetTopologySuite.Geometries;
using TowerLocatorApp.Models;
using TowerLocatorApp.Utility;


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

                RouteWithData newRoute = new RouteWithData();
                newRoute.Name = routeName;
                newRoute.AssociatedTowers = towers;
                newRoute.RoutePoints = points;

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
    }
}

using NetTopologySuite.Geometries;

namespace TowerLocatorApp.Models {
    public class DetailedMapPointModel {
            public int Id { get; set; }
            public Point Coordinates { get; set; }
            public double Elevation { get; set; }
            public DateTime Timestamp { get; set; }

            public int RouteId { get; set; }
            public RouteModel Route { get; set; }
        
    }
}

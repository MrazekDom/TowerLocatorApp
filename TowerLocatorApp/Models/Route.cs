using NetTopologySuite.Geometries;

namespace TowerLocatorApp.Models {
    public class Route {
        public int Id { get; set; }
        public string Name { get; set; }
        public LineString? Line { get; set; }
        public List<DetailedMapPoint> RoutePoints { get; set; }
        public List<BTS> AssociatedTowers { get; set; }
    }
}

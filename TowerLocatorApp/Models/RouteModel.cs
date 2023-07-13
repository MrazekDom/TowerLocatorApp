using NetTopologySuite.Geometries;

namespace TowerLocatorApp.Models {
    public class RouteModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public LineString? Line { get; set; }
        public List<DetailedMapPointModel> RoutePoints { get; set; }
        public List<BTSModel> AssociatedTowers { get; set; }
    }
}

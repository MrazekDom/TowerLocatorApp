using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace TowerLocatorApp.Models {
    public class RouteWithData {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public LineString? Line { get; set; }
        public List<DetailedMapPoint> RoutePoints { get; set; }
        public List<BTS> AssociatedTowers { get; set; }
    }
}

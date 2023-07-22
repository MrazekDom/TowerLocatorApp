using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using TowerLocatorApp.Models;

namespace TowerLocatorApp.Utility {
    public class GeoJSONConverter {
        public static string ToGeoJson(RouteWithData route) {
            var featureCollection = new JObject();
            featureCollection["type"] = "FeatureCollection";

            var features = new JArray();
       
                var lineFeature = CreateFeature("LineString", route.Line, new JObject{
                    { "Id", route.Id },
                    { "Name", route.Name }
                });
                features.Add(lineFeature);

            /*feature pro BTS na mape*/
            foreach (var tower in route.AssociatedTowers) {
                var towerFeature = CreateFeature("Point", tower.ActualTowerLocation, new JObject{
                    { "Id", tower.Id },
                    { "MCC", tower.mcc },
                    { "MNC", tower.mnc },
                    { "LAC", tower.lac },
                    { "Cell_Id", tower.cell_id },
                    { "Short-cell Id", tower.short_cell_id },
                    { "RNC", tower.rnc },
                    { "PSC", tower.psc },
                    { "ASU", tower.asu },
                    { "Síla_signálu", tower.dbm },
                    { "TA", tower.ta },
                    { "Accuracy", tower.accuracy },
                    { "Speed", tower.speed },
                    { "Bearing", tower.bearing },
                    { "Altitude", tower.altitude },
                    { "Measured_At", tower.measured_at.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                    { "Net_Type", tower.net_type },
                    { "Discovered_At", tower.discovered_at.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                    { "Device", tower.device },
                    { "RSSI", tower.rssi },
                    { "RSSNR", tower.rssnr },
                    { "RSCP", tower.rscp }
                });
                features.Add(towerFeature);
            }
            /*Feature ktera zobrazuje bod na trase v momente, kdy jsem se pripojil k nove BTS*/
            foreach (var myLocation in route.AssociatedTowers) {
                var myLocationFeature = CreateFeature("Point", myLocation.MyLocationAtMeasurement, new JObject {
                    {"Cell_Id", myLocation.cell_id },
                    {"Síla_signálu", myLocation.dbm }
                });
                features.Add(myLocationFeature);
            }

            featureCollection["features"] = features;
            return featureCollection.ToString();
        }

        private static JObject CreateFeature(string geometryType, Geometry geometry, JObject properties) {
            var feature = new JObject();
            feature["type"] = "Feature";

            var geometryObject = new JObject();
            geometryObject["type"] = geometryType;
            if (geometryType == "Point") {
                var coordinatesArray = new JArray(geometry.Coordinate.X, geometry.Coordinate.Y);
                geometryObject["coordinates"] = coordinatesArray;
            }
            else if (geometryType == "LineString") {
                var coordinatesArray = new JArray();
                foreach (var coordinate in ((LineString)geometry).Coordinates) {
                    coordinatesArray.Add(new JArray(coordinate.X, coordinate.Y));
                }
                geometryObject["coordinates"] = coordinatesArray;
            }

            feature["geometry"] = geometryObject;
            feature["properties"] = properties;

            return feature;
        }
    }
}

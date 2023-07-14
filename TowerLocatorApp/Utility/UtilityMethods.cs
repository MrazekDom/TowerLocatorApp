using NetTopologySuite.Geometries;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TowerLocatorApp.Models;

namespace TowerLocatorApp.Utility {
    public class UtilityMethods {


        /* Metoda pro zjisteni souradnic BTS, 
          data z aplikaci mi daji vsechny informace krome teto, 
          lokalitu mi reknu pouze mou */
        public static async Task<string> GetCellTowerLocationAsync(double CellId, double MCC, double MNC, double LAC) {
            string ApiKey = "pk.c1e5692645c47c8196fdf24fbc76f5cc"; /*vygenerovany pro muj ucet na OpenCellId.org*/
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync($"http://opencellid.org/cell/get?key={ApiKey}&mcc={MCC}&mnc={MNC}&lac={LAC}&cellid={CellId}&format=at");
            return response;
        }
        /* Z volani OpenCellId API dostanu souradnice ve formatu stringu
         +Location:42.46,-73.245,1483228800, musim je parsovat do Point */
        public static Point StringCoordinatesToPoint(string StringCoordinates) {
            double X;
            double Y;

            int lastCommaIndex = StringCoordinates.LastIndexOf(',');                /* dostanu index posledni carky */
            StringCoordinates = StringCoordinates.Substring(0, lastCommaIndex);     /* +Location:42.46,-73.245 */
            StringCoordinates = Regex.Replace(StringCoordinates, "[A-Za-z: ]", ""); /* 42.46,-73.245 */
            string[] coordinateValues = StringCoordinates.Split(',');
            double.TryParse(coordinateValues[0], out X);
            double.TryParse(coordinateValues[1], out Y);

            return new Point(X, Y);
        }

        /* metoda pro zpracovani GPX soubory ze Stravy, Garmin atd..., */
        public static List<DetailedMapPointModel> ParseGPXFile(string GPXFile) {
            XDocument gpxDocument = XDocument.Parse(GPXFile);
            XNamespace xNamespace = "http://www.topografix.com/GPX/1/1";    /*XML namespace pro GPX*/
            var DetailedPoints = gpxDocument.Root.Elements(xNamespace + "trk")
                                                 .Elements(xNamespace + "trkseg")
                                                 .Elements(xNamespace + "trkpt")
                                                 .Select(x => new DetailedMapPointModel {
                                                     Coordinates = new Point((double)x.Attribute("lon"), (double)x.Attribute("lat")),
                                                     Elevation = (double)x.Element(xNamespace + "ele"),
                                                     Timestamp = (DateTime)x.Element(xNamespace + "time"),

                                                 }).ToList();
            return DetailedPoints;
        }
    }
}

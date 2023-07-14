using NetTopologySuite.Geometries;
using System.Text.RegularExpressions;


namespace TowerLocatorApp.Utility {
    public class UtilityMethods {


        /* Metoda pro zjisteni koordinaci BTS, 
          data z aplikaci mi daji vsechny informace krome teto, 
          lokalitu mi reknu pouze mou */
        public static async Task<string> GetCellTowerLocationAsync(double CellId, double MCC, double MNC, double LAC) {
            string ApiKey = "pk.c1e5692645c47c8196fdf24fbc76f5cc"; /*vygenerovany pro muj ucet na OpenCellId.org*/
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync($"http://opencellid.org/cell/get?key={ApiKey}&mcc={MCC}&mnc={MNC}&lac={LAC}&cellid={CellId}&format=at");
            return response;
        }
        /* Z volani OpenCellId API dostanu koordinace ve formatu stringu
         +Location:42.46,-73.245,1483228800, musim je parsovat do Point */
        public static Point StringCoordinatesToPoint(string StringCoordinates) {
            double X;
            double Y;

            int lastCommaIndex = StringCoordinates.LastIndexOf(',');                /* dostanu index posledni carky */
            StringCoordinates = StringCoordinates.Substring(0, lastCommaIndex);     /* +Location:42.46,-73.245 */
            StringCoordinates = Regex.Replace(StringCoordinates, "[A-Za-z: ]", ""); /* 42.46,-73.245 */
            string[] coordinateValues = StringCoordinates.Split(',');
            double.TryParse(coordinateValues[0], out X);
            double.TryParse(coordinateValues[1],out Y);

            return new Point (X, Y);
        }
    }
}

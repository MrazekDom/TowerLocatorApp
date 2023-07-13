﻿using CsvHelper.Configuration.Attributes;

namespace TowerLocatorApp.Models {
    /*Model na zaklade Headeru CSV souboru z "Tower Collector" appky*/
    public class BTSModel {
        public int Id { get; set; }
        public double mcc { get; set; }
        public double mnc { get; set; }
        public double lac { get; set; }
        public double cell_id { get; set; }
        public double? short_cell_id { get; set; }
        public double? rnc { get; set; }
        public double? psc { get; set; }
        public double asu { get; set; }
        public double dbm { get; set; }
        public double ta { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public double accuracy { get; set; }
        public double speed { get; set; }
        public double bearing { get; set; }
        public double altitude { get; set; }
        public DateTime measured_at { get; set; }
        public string net_type { get; set; }
        public bool neighboring { get; set; }
        public DateTime discovered_at { get; set; }
        public string device { get; set; }
        public double? rsrp { get; set; }
        public double? rsrq { get; set; }
        public double rssi { get; set; }
        public string rssnr { get; set; }
        public double? cqi { get; set; }
        public string rscp { get; set; }
        public string? csi_rsrp { get; set; }
        public string? csi_rsrq { get; set; }
        public string? csi_sinr { get; set; }
        public string? ss_rsrp { get; set; }
        public string? ss_rsrq { get; set; }
        public string? ss_sinr { get; set; }
        public string? cdma_dbm { get; set; }
        public string? cdma_ecio { get; set; }
        public string? evdo_dbm { get; set; }
        public string? evdo_ecio { get; set; }
        public string? evdo_snr { get; set; }
        public string? ec_no { get; set; }
        public string? arfcn { get; set; }

        public int? RouteId { get; set; } 
        public RouteModel? Route { get; set; }
    }
}


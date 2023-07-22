import { Component, OnInit, AfterViewInit, OnChanges } from '@angular/core';
import * as L from 'leaflet';
import { MapService } from 'src/app/services/map.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  private geoData: any;
  private showMapDataFlag: boolean = false;
  
  constructor(public mapService: MapService) { } /*injectuji Map service*/

  ngOnInit() { }

  ngAfterViewInit() {
    this.initializeMap();
    this.mapService.GeoJsonData$.subscribe(data => this.geoData = data)   /*subscribe na service*/
    this.mapService.showMapDataFlag$.subscribe((flag) => {                /*flag ktery se hodi na true, kdyz se klikne tlaciko v route select componentu*/
      this.showMapDataFlag = flag;
      if (this.showMapDataFlag) {
        this.showMapData();
      }
    }); 
    
    
  }

  

  private initializeMap() {
    const baseMapURl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    this.map = L.map('map');
    L.tileLayer(baseMapURl).addTo(this.map);
    this.map.setView(new L.LatLng(49.820923, 18.262524), 10);
  }

  showMapData() {
    const geoJsonLayer = L.geoJSON(this.geoData, {
      pointToLayer: (feature, latlng) => {  /*leaflet option na zmeneni marker ikony*/
        let color = 'blue';           /*zakladni barva*/
        if (feature.properties.Id && feature.properties.Cell_Id) {  /*jestlize ma feature Id i Cell Id, tak se jedna o BTS a barva bude cervena */ 
          color = 'red'; 
        }
        const markerOptions = {     /*objekt markerOptions ve kterem jsou options pro circleMarker*/
          radius: 6,
          fillColor: color,
          color: 'white',
          weight: 1,
          opacity: 1,
          fillOpacity: 0.8
        };
        return L.circleMarker(latlng, markerOptions); /*circle marker je Leaflet metoda pro vytvoreni kruhove ikony*/
      },
      onEachFeature: (feature, layer) => {
        if (feature.properties.Id && feature.geometry.type === 'Point') { /*zjisteni, jestli je feature BTS*/
          const popupContent = `
            <b>BTS Data</b><br>     
            Cell Id: ${feature.properties.Cell_Id}<br>    
            MCC: ${feature.properties.MCC}<br>
            MNC: ${feature.properties.MNC}<br>
            LAC: ${feature.properties.LAC}<br>
            Připojil se v: ${feature.properties.Measured_At}<br>
          `;    /*k vezi pridavam popup kde jsou data*/
          layer.bindPopup(popupContent);
        }
        else if (feature.geometry.type === 'Point') {
          const popupContent = `
          <b>Moje lokace pri měření</b><br>
          ID BTS ke které jsem byl v tento moment připojen: ${feature.properties.Cell_Id}<br>
          Síla signálu: ${feature.properties.Síla_signálu}<br>
        `;
        layer.bindPopup(popupContent);
        }
      },
    });
      geoJsonLayer.addTo(this.map);
      this.map.fitBounds(geoJsonLayer.getBounds());
  }
}

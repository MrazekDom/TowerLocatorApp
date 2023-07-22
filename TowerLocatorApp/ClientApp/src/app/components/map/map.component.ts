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
  showMapDataFlag: boolean = false;
  routeLoadInProcess: boolean = false;
  routeLoaded: boolean = false;
  GeoJsonLayers = L.layerGroup();
  
  
  
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

  clearMap() {            /*reset mapy pri kazdem nacteni nove trasy*/
    this.GeoJsonLayers.clearLayers();
    
  }

  private initializeMap() {
    const baseMapURl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    this.map = L.map('map');
    L.tileLayer(baseMapURl).addTo(this.map);
    this.map.setView(new L.LatLng(49.820923, 18.262524), 10);
  }

  showMapData() {
    this.clearMap();
    const markers:any = [];
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

        /*sebrana metoda z netu na highlight markeru pri kliknuti*/
        const marker = L.circleMarker(latlng, markerOptions); /*circle marker je Leaflet metoda pro vytvoreni kruhove ikony*/

        marker.on('click', () => {        
          const clickedCellId = feature.properties.Cell_Id;
          const relatedMarkers = markers.filter((m:any) => m.feature.properties.Cell_Id === clickedCellId);   /*pri kliknuti se projede pole markers, ktere ma v sobe vsechny cellId,*/ 
          relatedMarkers.forEach((m:any) => {                                                                 // vyfiltruje se na relatedMarkers, kterym se pak da zluta barva,
            m.marker.setStyle({ fillColor: 'yellow' });
          });

          markers.forEach((m:any) => {                                    /* naopak, vsem cellId ktere nejsou stejne, se da defaultni brava*/
            if (m.feature.properties.Cell_Id !== clickedCellId) {
              m.marker.setStyle({ fillColor: m.defaultColor });
            }
          });
        });
        markers.push({ feature, marker, defaultColor: color });
        return marker;
        // return L.circleMarker(latlng, markerOptions); /*circle marker je Leaflet metoda pro vytvoreni kruhove ikony*/
      },
      onEachFeature: (feature, layer) => {
        if (feature.properties.Id && feature.geometry.type === 'Point') { /*zjisteni, jestli je feature BTS*/
          const popupContent = `
            <b>|BTS Data|</b><br>     
            <b>Cell Id:</b> ${feature.properties.Cell_Id}<br>    
            <b>MCC:</b> ${feature.properties.MCC}<br>
            <b>MNC:</b> ${feature.properties.MNC}<br>
            <b>LAC:</b> ${feature.properties.LAC}<br>
            <b>Připojil se v:</b> ${feature.properties.Measured_At}<br>
            <b>Technologie:</b> ${feature.properties.Net_Type}<br>
          `;    /*k vezi pridavam popup kde jsou data*/
          layer.bindPopup(popupContent);
        }
        else if (feature.geometry.type === 'Point') {
          const popupContent = `
          <b>|Moje lokace při připojení k nové BTS|</b><br>
          <b>ID připojené BTS:</b> ${feature.properties.Cell_Id}<br>
          <b>Připojil se v:</b> ${feature.properties.Measured_At}<br>
          <b>Síla signálu:</b> ${feature.properties.Síla_signálu} dbm<br>
          <b>Zařízení:</b> ${feature.properties.Device}<br>
        `;
        layer.bindPopup(popupContent);
        }
      },
    });
    this.GeoJsonLayers.addLayer(geoJsonLayer);
      geoJsonLayer.addTo(this.map);
      this.map.fitBounds(geoJsonLayer.getBounds());
  }
}

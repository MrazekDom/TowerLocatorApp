import { Component } from '@angular/core';
import { MapService } from 'src/app/services/map.service';
import { RouteService } from 'src/app/services/route.service';

@Component({
  selector: 'app-route-select',
  templateUrl: './route-select.component.html',
  styleUrls: ['./route-select.component.css']
})
export class RouteSelectComponent { 
  routeList: any[] = []; /*pole do ktereho vlozim seznam cest*/
  returnRouteName: string = '';
  routeDeleted: boolean = false;
  deleteInProcess: boolean = false;
  listRefreshed: boolean = false;
  geoData: string = '';
  routeLoadInProcess: boolean = false;
  
  constructor(public routeService: RouteService, public mapService: MapService) {   /*injectuju RouteService a MapService */
    
  }

  ngOnInit(): void{
    this.routeService.getRouteNames().subscribe((response: any[]) => {
      this.routeList = response;
    });
  }


  onDelete(selectedRouteId: string) {
    this.deleteInProcess = true;
    this.routeService.deleteRoute(Number(selectedRouteId)).subscribe((response: any) => {
      this.deleteInProcess = false;
      this.returnRouteName = response.deletedRoute;
      this.routeDeleted = true;
      setTimeout(() => {
        this.routeDeleted = false;
      },10000)
      this.ngOnInit() /*kdyz smazu cestu, tak hned nactu seznam znovu */
    })
  }

  /*znovunacteni seznamu*/
  onRefresh(): void{
    this.ngOnInit();
    this.listRefreshed = true;
    setTimeout(() => {
      this.listRefreshed = false;
    },5000)
  }

  onView(selectedRouteId: string): void{
    this.routeLoadInProcess = true;
    setTimeout(() => {
      this.routeLoadInProcess = false;
    },7000)
    this.routeService.getSingleRoute(Number(selectedRouteId)).subscribe((response: any) => {
      this.geoData = response;
      this.mapService.sendMapData(this.geoData); /*posilam data z backendu do servicky */
      this.mapService.setShowMapDataFlag(true);
    })
  }
  
}

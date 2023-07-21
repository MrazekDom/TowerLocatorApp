import { Component } from '@angular/core';
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
  
  
  constructor(public routeService: RouteService) {
    
  }

  ngOnInit(): void{
    this.routeService.getRouteNames().subscribe((response: any[]) => {
      this.routeList = response;
    });
  }


  onDelete(selectedRouteId: string) {
    this.deleteInProcess = true;
    this.routeService.deleteRoute(Number(selectedRouteId)).subscribe((response: any) => {
      this.routeDeleted = true;
      this.deleteInProcess = false;
      this.returnRouteName = response.deletedRoute;
      this.ngOnInit() /*kdyz smazu cestu, tak hned nactu seznam znovu */
    })
  }


  onRefresh(): void{
    this.ngOnInit();
  }

  onView(): void{
    
  }
  
}

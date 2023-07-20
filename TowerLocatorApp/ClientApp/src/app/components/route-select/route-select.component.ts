import { Component } from '@angular/core';
import { RouteService } from 'src/app/services/route.service';

@Component({
  selector: 'app-route-select',
  templateUrl: './route-select.component.html',
  styleUrls: ['./route-select.component.css']
})
export class RouteSelectComponent { 
  routeList: any[] = []; /*pole do ktereho vlozim seznam cest*/
  
  constructor(private routeService:RouteService) {
  }

  ngOnInit(): void{
    this.routeService.getRouteNames().subscribe((response: any[]) => {
      this.routeList = response;
    });
  }


  onDelete(selectedRouteId:string) {
    this.routeService.deleteRoute(Number(selectedRouteId)).subscribe((response: any[]) => {
      this.routeList = response; /*kdyz smazu cestu, tak hned nactu seznam znovu */
    })
  }
  
}

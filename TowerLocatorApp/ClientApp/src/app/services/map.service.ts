import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn:'root',
})
    /*servicka pro sdielni dat z RouteSelect componentu do mapy*/
export class MapService{
    private GeoJsonDataSubject = new Subject<string>();
    GeoJsonData$ = this.GeoJsonDataSubject.asObservable();
    private showMapDataFlagSubject = new Subject<boolean>();
    showMapDataFlag$ = this.showMapDataFlagSubject.asObservable();

    constructor() { 
    }
    setShowMapDataFlag(flag: boolean) {
        this.showMapDataFlagSubject.next(flag);
      }

    sendMapData(mapData: string) {
        this.GeoJsonDataSubject.next(mapData);
    }
}
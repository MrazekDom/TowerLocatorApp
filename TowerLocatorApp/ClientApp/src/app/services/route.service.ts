import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RouteService {
  private url = 'Route';
  constructor(private http: HttpClient) {}

  public uploadFiles(formData: FormData) {
    return this.http.post(`${environment.apiUrl}/${this.url}`, formData); // POST metoda pro ulozeni dat
  }

  public getRouteNames(): Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}/${this.url}`);
  }
}

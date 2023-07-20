import { Component } from '@angular/core';
import { RouteService } from './services/route.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'app';
  result: string = '';

  constructor() {}
  ngOnInit(): void {
    
  }
}

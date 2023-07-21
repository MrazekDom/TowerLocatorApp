import { Component } from '@angular/core';
import { RouteService } from 'src/app/services/route.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent {
  routeName: string = '';
  isInvalidFileFormat: boolean = true;
  gpxFile!: File;
  csvFile!: File;
  routeService: RouteService;
  fileUploaded: boolean = false;
  uploadInProcess: boolean = false;
  routeNameReturn: string = '';
  

  constructor(routeService: RouteService) {
    this.routeService = routeService;
    
  }

  checkFileFormat(file: File, format: string): void {
    if (file) {/*jestlize je vybran nejaky soubor, tak otestuju jeho priponu*/
      const fileName = file.name; /*vytahnu si jmeno */
      const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase(); /*odseparuji priponu*/
      if (fileExtension !== format) {/*jestlize je pripona spravna, tak vratim, FALSE*/
        this.isInvalidFileFormat = true;
      } else {
        this.isInvalidFileFormat = false;
      }
    }
    else {
      this.isInvalidFileFormat =true; /*kdyz neni vybran soubor, tak je to FALSE */
    }
  }
  onGpxFileChange(event: any): void {
    this.gpxFile = event.target.files?.[0];   /*prirazeni souboru z inputu do promenne*/
  }

  onCsvFileChange(event: any): void {
    this.csvFile = event.target.files?.[0];
  }

  public onSubmit() {
    const formData = new FormData();
    formData.append('gpxFile', this.gpxFile);
    formData.append('csvFile', this.csvFile);
    formData.append('routeName', this.routeName);
    this.uploadInProcess = true;
    this.routeService.uploadFiles(formData).subscribe((response: any) => {
      this.routeNameReturn = response.routeName;      /*zobrazim jmeno ulozene cesty*/
      this.uploadInProcess = false;
      this.fileUploaded = true;
      
    }); /*zavolani RouteService kde se pak uplodanuji data na backend*/
    
  }
}

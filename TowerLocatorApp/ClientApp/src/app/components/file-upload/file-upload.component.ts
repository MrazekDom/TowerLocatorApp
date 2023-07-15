import { Component } from '@angular/core';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent {
  routeName: string = '';
  isInvalidFileFormat: boolean = true;

  checkFileFormat(file: File, format: string): void {
    if (file) {                                       /*jestlize je vybran nejaky soubor, tak otestuju jeho priponu*/
      const fileName = file.name;                     /*vytahnu si jmeno */
      const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();                               /*odseparuji priponu*/
      if (fileExtension !== format) {                 /*jestlize je pripona spravna, tak vratim, FALSE*/
        this.isInvalidFileFormat = true;
      } else {
        this.isInvalidFileFormat = false;
      }
    } else {
      this.isInvalidFileFormat =true; /*kdyz neni vybran soubor, tak je to FALSE */
    }
  }
}

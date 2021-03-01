import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FileUpload } from '../models/file-upload';

@Injectable({
  providedIn: 'root'
})
export class ImageDataService {

  constructor(private http: HttpClient) { }


 public upload(images: FileUpload[])  {
   let fd = new FormData();

   for (const image of images) {
     if (!image.isUploaded)
     {
      fd.append('images', image.file, image.file.name);
     }
   }

    return this.http.post("https://localhost:5001/image", fd,
    {
      reportProgress: true,
      observe: 'events'
    });

 }
}

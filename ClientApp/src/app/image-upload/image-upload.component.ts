import { TranslationWidth } from '@angular/common';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FileUpload } from '../models/file-upload';
import { ImageDataService } from '../services/image-data.service';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {

  constructor(private imageDataService: ImageDataService) { }

  ngOnInit() {
  }

  selectedFiles: FileUpload[] = [];
  imageFilesPasted: boolean = false;

  public onFileSelected(files: FileList) {
    console.log(files);
    this.addFiles(files);
  }

  public onFileDropped(event: FileList) {
    this.addFiles(event)
  }

  public onImageFilesPasted(imageFiles: File[]) {
    for (const image of imageFiles) {
      this.selectedFiles.push(new FileUpload(image))
    }

    this.imageFilesPasted = false;
    
  }

  public onUpload() {

    this.imageDataService.upload(this.selectedFiles)
    .subscribe((event) => {
      if (event.type === HttpEventType.UploadProgress) {
        // Upload progress
        // 100% means that all data has been send, but api will not neccessarily return
        // immediately.
        // TODO: Show "Creating image message" if upload reached 100%.
        this.setFilesUploadProgress(Math.round(event.loaded/event.total*100));
        console.log(event.loaded);
        console.log(event.total);

      } else if (event.type === HttpEventType.Response) {
        // Image succefully created (API returned)
        this.setFilesAsUpload();
      }
    });
  }

  public onDeleteSelectedFile(index: number) {
    this.selectedFiles.splice(index,1);
  }

  public formatBytes(bytes: number, decimals = 2) {
    if (bytes === 0) {
      return "0 Bytes";
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals;
    const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
  }
  
  //----------- TODO: Create UploadFiles class ---------------
  private setFilesAsUpload() {
    for (let file of this.selectedFiles) {
      file.isUploaded = true;
    }
  }

  private addFiles(files: FileList) {
    for (let index = 0; index < files.length; index++) {
      this.selectedFiles.push(new FileUpload(files[index]));
    }
  }

  private setFilesUploadProgress(uploadProgress: number) {
    for (var file of this.selectedFiles) {
      file.uploadProgress = uploadProgress;
    }

  }
  //--------------------------------------------------------------
}

import { Component, Input, OnChanges, OnInit, Output, EventEmitter } from '@angular/core';
import { FileUpload } from 'src/app/models/file-upload';

@Component({
  selector: 'app-image-file',
  templateUrl: './image-file.component.html',
  styleUrls: ['./image-file.component.css']
})
export class ImageFileComponent implements OnInit, OnChanges {

  constructor() { 
    
  }

  ngOnChanges() {

  }

  ngOnInit() {
  }

  @Output()
  public deleteFile = new EventEmitter<number>()
  
  @Input()
  public uploadFile: FileUpload;

  public uploadMessage: string;

  public deleteSelectedFile() {
    // TODO: Add delete logic.
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


}

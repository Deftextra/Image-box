import { FileUpload } from "./file-upload"

export class UploadFiles {
    public uploadFiles: FileUpload[]

    constructor(uploadFiles: FileUpload[]) {
        this.uploadFiles = uploadFiles;
    }

    public static Create(): UploadFiles {
        return new UploadFiles([]);
    }

    public addFile(file: File) {
        // this.uploadFiles.push(new FileUpload(file,))
    }

  public setFilesAsUpload() {
    for (let file of this.uploadFiles) {
      if (!file.isUploaded)
      {
        file.isUploaded = true;
      }
    }
  }

  public addFiles(files: FileList) {
    for (let index = 0; index < files.length; index++) {
      this.uploadFiles.push(new FileUpload(files[index]));
    }
    console.log(this.uploadFiles);
  }

  public setFilesUploadProgress(uploadProgress: number) {
    for (var file of this.uploadFiles) {
      file.uploadProgress = uploadProgress;
    }
  }
}

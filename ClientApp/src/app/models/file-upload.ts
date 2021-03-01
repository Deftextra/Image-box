
export class FileUpload {
    public file: File;
    public isUploaded: boolean;
    public uploadProgress: number;
    constructor(
        file: File,
        isUploaded: boolean = false,
        uploadProgress: number = 0
    ) {
        this.isUploaded = isUploaded;
        this.file = file;
        this.uploadProgress = uploadProgress;
    }
}

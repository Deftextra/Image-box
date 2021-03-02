
export class FileUpload {
    public index: number;
    public file: File;
    public isUploaded: boolean;
    public uploadProgress: number;
    public progressMessage: string;
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

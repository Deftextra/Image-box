import { Directive, EventEmitter, HostBinding, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appDnd]'
})
export class DndDirective {

  constructor() { }

  @HostBinding('class.fileover')
  public fileOver: boolean;

  @Output() fileDropped = new EventEmitter<FileList>();

  // Dragover listener
  @HostListener('dragover', ['$event'])
  public onDragOver(event: DragEvent) {
    console.log(event);
    event.preventDefault();
    event.stopPropagation();
    this.fileOver = true;

  }

  @HostListener('dragleave', ['$event'])
  public onDragLeave(event: DragEvent) {
    console.log(event);
    event.preventDefault();
    event.stopPropagation();
    this.fileOver = false;

  }

  @HostListener('drop', ['$event'])
  public onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();

    this.fileOver = false;
    const files = event.dataTransfer.files;

    if (files.length > 0) {
      this.fileDropped.emit(files);
    }
  }


}

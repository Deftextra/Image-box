import { Directive, EventEmitter, HostBinding, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appCnp]'
})
export class CnpDirective {

  constructor() { }

  @HostBinding('class.filesPasted')
  public filesPasted: boolean;

  @Output() imageFilesPasted  = new EventEmitter<File[]>();

  @HostListener('paste', ['$event'])
  public onPaste(event: any) {
    event.preventDefault();
    event.stopPropagation();

    const fileList = [];
    
    this.filesPasted = true;

    const items = (event.clipboardData || event.originalEvent.clipboardData).items;
    console.log(event);
    for (const item of items) {
      if (item.type.indexOf('image') === 0) {
        fileList.push(item.getAsFile());
      }
    }

    this.imageFilesPasted.emit(fileList);
  }

}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';
import { DndDirective } from './directives/dnd.directive';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CnpDirective } from './directives/cnp.directive';
import { ImageFileComponent } from "./image-upload/image-file/image-file.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ImageUploadComponent,
    DndDirective,
    CnpDirective,
    ImageFileComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ImageUploadComponent, pathMatch: 'full' },
      { path: 'upload-image', component: ImageUploadComponent },
    ])
    ,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

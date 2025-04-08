import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GetVaocherComponent } from './get-vaocher/get-vaocher.component';
import { AddVaocherComponent } from './add-vaocher/add-vaocher.component';
import { EditVaocherComponent } from './edit-vaocher/edit-vaocher.component';

@NgModule({
  declarations: [
    AppComponent,
    GetVaocherComponent,
    AddVaocherComponent,
    EditVaocherComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

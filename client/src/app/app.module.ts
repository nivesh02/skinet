import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { HomeModule } from './home/home.module';
import { ProviderAst } from '@angular/compiler';
import { ErrorInterceptors } from './core/interceptors/error.interceptors';
import { NgxSpinnerModule } from 'ngx-spinner';
import { Loadingintercepter } from './core/interceptors/loading.interceptors';



@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    NgxSpinnerModule
    // ShopModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass : ErrorInterceptors, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass : Loadingintercepter, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

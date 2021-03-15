import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { pathToFileURL } from 'url';
import { ShopComponent } from './shop.component';

import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule, Routes } from '@angular/router';

const route: Routes = [
  { path: '', component: ShopComponent },
  { path: ':id', component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productdetail'}} }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(route)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }

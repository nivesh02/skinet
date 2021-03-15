import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { env } from 'process';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  productTypes: IType[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOption = [
    {name : 'Alphabetical' , value : 'name'},
    {name : 'Price Low to High' , value : 'priceAsc'},
    {name : 'Price High to Low' , value : 'priceDesc'},
  ];

  constructor(private shopservice: ShopService) { }

  ngOnInit(): void {

    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopservice.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageSize = response.pageSize;
      this.shopParams.pageNumber = response.pageIndex;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }

  getBrands() {
    this.shopservice.getBrands().subscribe(response => {
      this.brands = [{id : 0, name : 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  getProductTypes() {
    this.shopservice.getProductType().subscribe(response => {
      this.productTypes = [{id : 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();

  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(Sort: string) {
    this.shopParams.sort = Sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    // this.shopParams.pageNumber = event.page;
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}

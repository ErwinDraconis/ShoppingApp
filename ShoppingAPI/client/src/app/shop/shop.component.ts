import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IProduct } from '../shared/models/IProduct';
import { ShopService } from '../shop/shop.service';
import { IBrand } from '../shared/models/ProductBrand';
import { IType } from '../shared/models/ProductType';
import { ShopParam } from '../shared/models/ShopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  constructor(private shopservice: ShopService) { }

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParam = new ShopParam();
  totalItem = 0;
  @ViewChild('search') searchTerm: ElementRef;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price ascending', value: 'pricedesc' },
    { name: 'Price descending', value: 'priceasc' },
  ];

  ngOnInit(): void {
    this.getproducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getproducts() {
    this.shopservice.getProducts(this.shopParam).subscribe(response => {
      this.products = response.data;
      this.shopParam.pageNumber = response.pageIndex;
      this.shopParam.pageSize = response.pageSize;
      this.totalItem = response.count;
    }, error => {
      console.log(error);
    });
  }

  getProductBrands() {
    this.shopservice.getProductBrands().subscribe(response => {
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, error => {
      console.log(error);
    });
  }

  getProductTypes() {
    this.shopservice.getProductTypes().subscribe(response => {
      this.types = [{ id: 0, name: 'All' }, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParam.branId = brandId;
    this.shopParam.pageNumber = 1;
    this.getproducts();
  }

  onTypeSelected(TypeId: number) {
    this.shopParam.typeId = TypeId;
    this.shopParam.pageNumber = 1;
    this.getproducts();
  }

  onSortSelected(Sort: string) {
    this.shopParam.sort = Sort;
    this.getproducts();
  }

  onPageChanged(event: any) {
    if(this.shopParam.pageNumber !== event){
      this.shopParam.pageNumber = event;
      this.getproducts();
    }
    
  }

  onSearch() {
    this.shopParam.search = this.searchTerm.nativeElement.value;
    this.getproducts();
  }
  onReset() {
    this.shopParam.search = '';
    this.shopParam = new ShopParam();
    this.getproducts();
  }

}



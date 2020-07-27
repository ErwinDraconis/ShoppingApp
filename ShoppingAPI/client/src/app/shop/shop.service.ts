import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {IPagination} from '../shared/models/IPagination';
import {IType} from '../shared/models/ProductType';
import {IBrand} from '../shared/models/ProductBrand';
import { map } from 'rxjs/operators';
import { ShopParam } from '../shared/models/ShopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  
  baseUrl = 'http://localhost:53137/';

  constructor(private http: HttpClient) { }

  getProducts(shopParam: ShopParam) {

    let params = new HttpParams();

    if(shopParam.branId !== 0)
    {
      params = params.append('brandId',shopParam.branId.toString());
    }
      

    if(shopParam.typeId !== 0)
    {
      params = params.append('typeId',shopParam.typeId.toString());
    }
      
    
    params = params.append('sort',shopParam.sort);
    params = params.append('pageIndex',shopParam.pageNumber.toString());
    params = params.append('PageSize',shopParam.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'Products',{ observe: 'response', params}).
    pipe(
      map(response => {
        return response.body;
      })
    );
  }

  getProductTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'Products/types');
  }

  getProductBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'Products/brands');
  }
}

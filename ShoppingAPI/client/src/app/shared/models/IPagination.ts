import {IProduct } from './IProduct';

export interface IPagination {
        pageIndex: number;
        pageSize: number;
        count: number;
        data: IProduct[];
    
}
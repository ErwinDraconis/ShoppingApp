using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Core.Entities;
using ShoppingAPI.Core.Interfaces;
using ShoppingAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ShoppingAPI.Infrastructure.Data
{
    public class ProductRepositroy : IProductRepository
    {
        private StoreContext _context;
        public ProductRepositroy(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.
                Include(p => p.ProductBrand).
                Include(p => p.ProductType).
                FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductFilterParams filterParam)
        
        {
            List<Product> products;
            if (filterParam.sort == null) filterParam.sort = "name";
            
            switch (filterParam.sort.ToLower())
            {
                case "priceasc":
                    products = await _context.Products.
                    Include(p => p.ProductBrand).
                    Include(p => p.ProductType).
                    OrderBy(p => p.Price).
                    ToListAsync();
                    break;
                case "pricedesc":
                    products = await _context.Products.
                    Include(p => p.ProductBrand).
                    Include(p => p.ProductType).
                    OrderByDescending(p => p.Price).
                    ToListAsync();
                    break;
                default:
                    products = await _context.Products.
                    Include(p => p.ProductBrand).
                    Include(p => p.ProductType).
                    OrderBy(p => p.Name).
                    ToListAsync();
                    break;
            }
            // filter by search
            if (!string.IsNullOrEmpty(filterParam.Search))
                products = products.Where(p => p.Name.ToLower().Contains(filterParam.Search)).ToList();
            
            // filter by brand and type
            if (filterParam.brandId != null) products = products.Where(p => p.ProductBrandId == filterParam.brandId).ToList();
            if (filterParam.typeId != null) products = products.Where(p => p.ProductTypeId == filterParam.typeId).ToList();
            
            // pagination var entries = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            products = products.Skip((filterParam.pageIndex -1) * filterParam.PageSize).Take(filterParam.PageSize).ToList();
           
            return products;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var productTypes = await _context.ProductTypes.ToListAsync();
            return productTypes;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var productBrands = await _context.ProductBrands.ToListAsync();
            return productBrands;
        }

        public async Task<int> CountAsync(ProductFilterParams filterParam)
        {
            if (filterParam.sort == null) filterParam.sort = "name";
            var products = await _context.Products.
                   Include(p => p.ProductBrand).
                   Include(p => p.ProductType).
                   OrderBy(p => p.Name).
                   ToListAsync();
            
            // filter by search
            if (!string.IsNullOrEmpty(filterParam.Search))
                products = products.Where(p => p.Name == filterParam.Search).ToList();

            if (filterParam.brandId != null) products = products.Where(p => p.ProductBrandId == filterParam.brandId).ToList();
            if (filterParam.typeId != null) products = products.Where(p => p.ProductTypeId == filterParam.typeId).ToList();

            return products.Count();
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShoppingAPI.API.Dtos;
using ShoppingAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ShoppingAPI.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }
    }
}

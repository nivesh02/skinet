using API.Dtos;
using AutoMapper;
using Core.Entity;
using Microsoft.Extensions.Configuration;

namespace API.Helper
{
    public class ProductResolverUrl : IValueResolver<Product, ProductToReturnDtos, string>
    {
        private readonly IConfiguration _config;
        public ProductResolverUrl(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"]+source.PictureUrl;
            }
            return null;
        }
    }
}
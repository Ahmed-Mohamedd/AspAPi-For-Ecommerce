using AutoMapper;
using Microsoft.Extensions.Configuration;
using SmartCart.Api.Dtos;
using SmartCart.DAl.Entities;


namespace Talabat.API.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration Configuration)
        {
            configuration=Configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}

using AutoMapper;
using SmartCart.Api.Dtos;
using SmartCart.DAl.Entities;
using Talabat.API.Helpers;
using Talabat.DAL.Entities.Identity;

namespace SmartCart.Api.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.DAL.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, SmartCart.DAl.Entities.Order_Aggregate.Address>();

        }
    }
}

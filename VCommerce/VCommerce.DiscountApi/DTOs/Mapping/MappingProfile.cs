using AutoMapper;
using VCommerce.CartApi.DTOs;
using VCommerce.DiscountApi.Models;

namespace VCommerce.DiscountApi.DTOs.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponDTO, Coupon>().ReverseMap();
    }
}

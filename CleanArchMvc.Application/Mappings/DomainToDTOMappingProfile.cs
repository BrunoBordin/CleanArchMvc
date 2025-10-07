using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            
            // Loyalty Card System Mappings
            CreateMap<LoyaltyCard, LoyaltyCardDTO>().ReverseMap();
            CreateMap<Network, NetworkDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<CustomerLoyaltyCard, CustomerLoyaltyCardDTO>()
                .ForMember(dest => dest.RemainingStamps, opt => opt.MapFrom(src => src.GetRemainingStamps()))
                .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.IsExpired()))
                .ReverseMap();
            CreateMap<LoyaltyCardRedemption, LoyaltyCardRedemptionDTO>()
                .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.IsExpired()))
                .ReverseMap();
        }
    }
}
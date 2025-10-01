using AutoMapper;
using MyWebGameShop.Models;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.CartItems, 
                opt => opt.MapFrom(src => src.CartItems))
            .ForMember(dest => dest.Subscriptions, 
                opt => opt.MapFrom(src => src.SubscriptionUserInfos))
            .ForMember(dest => dest.Recommendations, 
                opt => opt.Ignore());
    }
}
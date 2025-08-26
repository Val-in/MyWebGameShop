using AutoMapper;
using MyWebGameShop.Models;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.MappingProfile;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<SubscriptionUserInfo, SubscriptionsViewModel>()
            .ForMember(dest => dest.SubscriptionType, 
                opt => opt.MapFrom(src => src.Subscription.SubscriptionType));
    }
}
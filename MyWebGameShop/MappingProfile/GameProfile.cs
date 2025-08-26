using AutoMapper;
using MyWebGameShop.Models;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.MappingProfile;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDetailsViewModel>()
            .ForMember(dest => dest.Category, 
                opt => opt.MapFrom(src => src.Category.Name));
        /*dest — это свойство целевого объекта (Destination), т.е. куда маппируем.
        opt — это объект настроек (Options), с помощью которого указываем как именно маппить.*/
    }
}
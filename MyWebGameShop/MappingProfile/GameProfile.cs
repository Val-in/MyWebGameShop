using AutoMapper;
using MyWebGameShop.Models;
using MyWebGameShop.ViewModels;

namespace MyWebGameShop.MappingProfile;

public class GameProfile : Profile
{
    public GameProfile()
    {
        // Game -> GameDetailsViewModel
        CreateMap<Game, GameDetailsViewModel>()
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));

        // Product -> GameDetailsViewModel (для каталога товаров)
        CreateMap<Product, GameDetailsViewModel>()
            .ForMember(d => d.Title,      o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Price,      o => o.MapFrom(s => s.Price))
            .ForMember(d => d.ImageUrl,   o => o.MapFrom(s => s.ImageUrl))
            .ForMember(d => d.Category,   o => o.MapFrom(s => s.Category.Name));
    }
}
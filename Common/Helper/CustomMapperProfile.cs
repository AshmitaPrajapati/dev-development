using API.Database.Entities;
using API.DataTransferObjects.ConfigurationDtos;
using API.DataTransferObjects.MenuDtos;
using API.DataTransferObjects.MenuItemDtos;
using AutoMapper;

namespace API.Helper
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile() 
        {
            CreateMap<MenuItemEntity, MenuItemsDto>().ReverseMap();
            CreateMap<SubMenuItemEntity, SubMenuItemsDto>().ReverseMap();
            CreateMap<DemoConfigEntity, ConfigurationDto>().ReverseMap();
        }
    }
}

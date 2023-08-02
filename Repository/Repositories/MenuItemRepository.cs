using API.Database;
using API.DataTransferObjects.MenuDtos;
using API.DataTransferObjects.MenuItemDtos;
using API.Services.Repositories.Interfaces;
using API.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static API.Shared.ApiFunctions;

namespace API.Services.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuItemRepository(ApplicationDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MenuItemsDto>>> GetAllMenuItems() 
        {
            var menuItemsEntities = await _dbContext.MenuItemEntities.ToListAsync();

            var menuItemsDtoList = _mapper.Map<List<MenuItemsDto>>(menuItemsEntities);

            return ApiSuccessResponse(menuItemsDtoList);
        }

        public async Task<ApiResponse<List<SubMenuItemsDto>>> GetAllSubMenuItems()
        {
            var subMenuItemsEntities = await _dbContext.SubMenuItemEntities.ToListAsync();

            var subMenuItemDtoList = _mapper.Map<List<SubMenuItemsDto>>(subMenuItemsEntities);

            return ApiSuccessResponse(subMenuItemDtoList);
        }
    }
}

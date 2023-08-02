using API.Database.Entities;
using API.DataTransferObjects.MenuDtos;
using API.DataTransferObjects.MenuItemDtos;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;

namespace API.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ApiResponse<List<MenuItemsDto>>> GetAllMenuItems()
        {
            return await _menuItemRepository.GetAllMenuItems();
        }

        public async Task<ApiResponse<List<SubMenuItemsDto>>> GetAllSubMenuItems()
        {
            return await _menuItemRepository.GetAllSubMenuItems();
        }
    }
}

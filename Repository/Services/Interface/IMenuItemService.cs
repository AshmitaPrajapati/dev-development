using API.Database.Entities;
using API.DataTransferObjects.MenuDtos;
using API.DataTransferObjects.MenuItemDtos;
using API.Shared;

namespace API.Services.Interface
{
    public interface IMenuItemService
    {
        Task<ApiResponse<List<MenuItemsDto>>> GetAllMenuItems();

        Task<ApiResponse<List<SubMenuItemsDto>>> GetAllSubMenuItems();
    }
}

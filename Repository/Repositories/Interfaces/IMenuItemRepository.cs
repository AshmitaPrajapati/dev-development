using API.DataTransferObjects.MenuDtos;
using API.DataTransferObjects.MenuItemDtos;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<ApiResponse<List<MenuItemsDto>>> GetAllMenuItems();

        Task<ApiResponse<List<SubMenuItemsDto>>> GetAllSubMenuItems();
    }
}

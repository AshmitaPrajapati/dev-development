using API.Shared;
using DAL.DataTransferObjects.QuickBookDtos;
using DAL.Model.QuickBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IQuickBookRepository
    {
        Task<ApiResponse<ItemQueryResponseDto>> GetItemByQuery(string accessToken, string clientId, string clientSecret, string companyId);

        Task<ApiResponse<ItemResponse>> CreateItem(string accessToken, string companyId, ItemAddUpdateModel model);

        Task<ApiResponse<ItemResponse>> UpdateItem(string accessToken, string companyId, ItemAddUpdateModel model);

        Task<ApiResponse<ItemResponse>> GetItemById(string accessToken, string companyId, string Id);
    }
}

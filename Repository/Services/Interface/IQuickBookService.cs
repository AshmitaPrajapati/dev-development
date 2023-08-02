using API.Shared;
using DAL.DataTransferObjects.QuickBookDtos;
using DAL.Model.QuickBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services.Interface
{
    public interface IQuickBookService
    {
        Task<ApiResponse<ItemQueryResponseDto>> GetItemByQuery(string applicationName, string country);

        Task<ApiResponse<ItemResponse>> CreateItem(string applicationName, string country, ItemAddUpdateModel model);

        Task<ApiResponse<ItemResponse>> UpdateItem(string applicationName, string country, ItemAddUpdateModel model);

        Task<ApiResponse<ItemResponse>> GetItemById(string accessTokenResult, string companyId, string Id);

        string GetAuthToken(string clientId, string clientSecret, string refreshToken);
    }
}

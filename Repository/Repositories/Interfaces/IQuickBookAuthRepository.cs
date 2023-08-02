using API.DataTransferObjects.QuickBookDtos;
using API.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IQuickBookAuthRepository
    {
        Task<ApiResponse<QuickBookAuthTokenDto>> Auth(string clientId, string clientSecret, string refreshToken);
    }
}

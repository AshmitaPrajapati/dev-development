using API.DataTransferObjects.QuickBookDtos;
using API.Shared;
using Microsoft.Extensions.Configuration;
using Repository.Repositories.Base.QuickBook;
using Repository.Repositories.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class QuickBookAuthRepository : QuickBookBaseRepository,  IQuickBookAuthRepository
    {
        public QuickBookAuthRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<ApiResponse<QuickBookAuthTokenDto>> Auth(string clientId, string clientSecret, string refreshToken)
        {
            var query = new List<Parameter>()
            {
                    new Parameter("grant_type","refresh_token", ParameterType.GetOrPost),
                    new Parameter("refresh_token",refreshToken, ParameterType.GetOrPost)
            };

            return await SendAuthRequest<QuickBookAuthTokenDto>(null, null, query, clientId, clientSecret, false, Method.POST);
        }
    }
}

using API.Database;
using API.DataTransferObjects.GraphDtos;
using API.Model.AzureCredencials;
using API.Services.Repositories.Interfaces;
using API.Shared;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static API.Shared.ApiFunctions;

namespace API.Services.Repositories
{
    public class GraphRepository : IGraphRepository
    {
        private static GraphServiceClient graphClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private static string clientId;
        private static string clientSecret;
        private static string tenantId;
        private static string aadInstance;
        private static string graphResource;
        private static string graphAPIEndpoint;
        private static string authority;
        private readonly ApplicationDbContext _dbContext;

        public GraphRepository(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _dbContext = dbContext;

            SetAzureADOptions();
        }

        public void SetAzureADOptions()
        {
            var graphConfiguration = _dbContext.DemoConfigEntities.FirstOrDefault(x => x.SubMenuItemId == 27);

            var config = "[" + graphConfiguration.Configuration.Remove(graphConfiguration.Configuration.Length - 1, 1) + "]";
            var configData = JsonConvert.DeserializeObject<List<AzureCredencial>>(config);

            clientId = configData[0].ClientId;
            clientSecret = configData[0].ClientSecret;
            tenantId = configData[0].TenantId;
            //aadInstance = _configuration.GetSection("AzureAactiveDirectory:Instance");
            aadInstance = _configuration.GetSection("AzureAactiveDirectory:Instance").ToString();
            graphResource = _configuration.GetSection("AzureAactiveDirectory:GraphResource").ToString();
            graphAPIEndpoint = $"{_configuration.GetSection("AzureAactiveDirectory:GraphResource").ToString()}{_configuration.GetSection("AzureAactiveDirectory:GraphResourceEndPoint").ToString()}";
            authority = $"{aadInstance}{tenantId}";
        }

        public async Task<ApiResponse<string>> GetAuthenticationToken()
        {
            var token = string.Empty;

            try
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(authority);

                ClientCredential clientCred = new ClientCredential(clientId, clientSecret);

                AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(graphResource, clientCred);
                token = authenticationResult.AccessToken;

                var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token.ToString());
                    return Task.FromResult(0);
                });

                return ApiSuccessResponse(token);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public async Task<ApiResponse<GraphUserResponseDto>> GetAllUsers(string token)
        {
            try
            {
                var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token.ToString());
                    return Task.FromResult(0);
                });

                graphClient = new GraphServiceClient(graphAPIEndpoint, delegateAuthProvider);
                var users = await graphClient.Users.Request().GetAsync();

                var userDto = GetUserData(users);

                return ApiSuccessResponse(userDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<GraphUserResponseDto>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public GraphUserResponseDto GetUserData(IGraphServiceUsersCollectionPage users)
        {
            var grophUserDto = new GraphUserResponseDto()
            {
                Id = users.CurrentPage[0].Id,
                DisplayName = users.CurrentPage[0].DisplayName,
                PreferredLanguage = users.CurrentPage[0].PreferredLanguage,
                UserPrincipalName = users.CurrentPage[0].UserPrincipalName
            };

            return grophUserDto;
        }
    }
}

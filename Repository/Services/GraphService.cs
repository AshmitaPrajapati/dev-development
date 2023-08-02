using API.DataTransferObjects.GraphDtos;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;

namespace API.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;

        public GraphService(IGraphRepository graphRepository) 
        {
            _graphRepository = graphRepository;
        }

        public async Task<ApiResponse<string>> GetAuthenticationToken()
        { 
            return await _graphRepository.GetAuthenticationToken();
        }

        public async Task<ApiResponse<GraphUserResponseDto>> GetAllUsers(string token)
        {
            return await _graphRepository.GetAllUsers(token);
        }
    }
}

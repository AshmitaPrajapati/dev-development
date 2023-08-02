using API.DataTransferObjects.GraphDtos;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface IGraphRepository
    {
        Task<ApiResponse<string>> GetAuthenticationToken();

        Task<ApiResponse<GraphUserResponseDto>> GetAllUsers(string token);
    }
}

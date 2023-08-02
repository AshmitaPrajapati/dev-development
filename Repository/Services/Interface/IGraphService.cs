using API.DataTransferObjects.GraphDtos;
using API.Shared;

namespace API.Services.Interface
{
    public interface IGraphService
    {
        Task<ApiResponse<string>> GetAuthenticationToken();

        Task<ApiResponse<GraphUserResponseDto>> GetAllUsers(string token);
    }
}

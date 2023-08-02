using API.DataTransferObjects.ConfigurationDtos;
using API.Model.Configuration;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<ApiResponse<ConfigurationDto>> GetAllConfiguration(int SubmenuItemId);

        Task<ApiResponse<ConfigurationDto>> AddUpdateConfiguration(AddConfigurationModel addConfigurationObj, int SubmenuItemId);
    }
}

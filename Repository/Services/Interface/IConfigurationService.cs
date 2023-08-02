using API.DataTransferObjects.ConfigurationDtos;
using API.Model.Configuration;
using API.Shared;

namespace Repository.Services.Interface
{
    public interface IConfigurationService
    {
        Task<ApiResponse<ConfigurationDto>> GetAllConfiguration(int SubmenuItemId);

        Task<ApiResponse<ConfigurationDto>> AddUpdateConfiguration(AddConfigurationModel addConfigurationObj, int SubmenuItemId);
    }
}

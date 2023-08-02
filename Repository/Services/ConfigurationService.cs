using API.DataTransferObjects.ConfigurationDtos;
using API.Model.Configuration;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Repository.Services.Interface;

namespace Repository.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository) 
        {
            _configurationRepository = configurationRepository;
        }
        
        public async Task<ApiResponse<ConfigurationDto>> GetAllConfiguration(int SubmenuItemId)
        {
            return await _configurationRepository.GetAllConfiguration(SubmenuItemId);
        }

        public async Task<ApiResponse<ConfigurationDto>> AddUpdateConfiguration(AddConfigurationModel addConfigurationObj, int SubmenuItemId)
        {
            return await _configurationRepository.AddUpdateConfiguration(addConfigurationObj, SubmenuItemId);
        }
    }
}

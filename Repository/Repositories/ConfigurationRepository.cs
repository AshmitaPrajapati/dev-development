using API.Database;
using API.DataTransferObjects.ConfigurationDtos;
using API.Model.Configuration;
using API.Services.Repositories.Interfaces;
using API.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static API.Shared.ApiFunctions;

namespace API.Services.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ConfigurationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ConfigurationDto>> GetAllConfiguration(int SubmenuItemId)
        {
            var demoConfigEntities = await _dbContext.DemoConfigEntities.FirstOrDefaultAsync(x => x.SubMenuItemId == SubmenuItemId);
            var configurationDto = new ConfigurationDto();

            configurationDto.Result = ConvertStringToArrayOfObject(demoConfigEntities.Configuration);
            configurationDto.SubMenuItemId = demoConfigEntities.SubMenuItemId;
            configurationDto.Key = _dbContext.SubMenuItemEntities.FirstOrDefault(x => x.Id == SubmenuItemId).Key.ToString();

            if (configurationDto.Key == "User" || configurationDto.Key == "GoogleSheet" || configurationDto.Key == "QuickBook")
            {
                var demoConfig = _dbContext.DemoConfigEntities.Where(x => x.SubMenuItemId == SubmenuItemId).ToList().Last();

                IDictionary<string, object>[] subListData = ConvertStringToArrayOfObject(demoConfig.Configuration);

                if (configurationDto.Key == "User")
                {
                    configurationDto.GoogleAuthData = subListData;
                }
                else if (configurationDto.Key == "GoogleSheet")
                {
                    configurationDto.GoogleSubSheetData = subListData;
                }
                else if (configurationDto.Key == "QuickBook")
                {
                    configurationDto.QuickBookApplicationCredencialData = subListData;
                }
            }

            return ApiSuccessResponse(configurationDto);
        }

        public IDictionary<string, object>[] ConvertStringToArrayOfObject(string configEntity)
        {
            var config = "[" + configEntity.Remove(configEntity.Length - 1, 1) + "]";
            var configData = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(config);
            var finalValue = configData.ToArray();
            return finalValue;
        }

        public async Task<ApiResponse<ConfigurationDto>> AddUpdateConfiguration(AddConfigurationModel addConfigurationObj, int SubmenuItemId)
        {
            string configuration = string.Empty;

            var existingKeyValuePair = await _dbContext.DemoConfigEntities.FirstOrDefaultAsync(x => x.SubMenuItemId == SubmenuItemId);

            if (SubmenuItemId == 10)
            {
                foreach (var id in addConfigurationObj.keyValue)
                {
                    string newPair = string.Format("Key:\"{0}\",Value:\"{1}\"", id.Key, id.Value);

                    configuration += "{" + newPair + "},";
                }

                existingKeyValuePair.Configuration = configuration;
                await _dbContext.SaveChangesAsync();

            }
            else if (SubmenuItemId == 8)
            {
                string newPair = string.Empty;

                foreach (var item in addConfigurationObj.keyValue)
                {
                    if (item.IsSMS == true && item.IsWhatsap == true)
                    {
                        newPair = string.Format("Key:\"{0}\",From:\"{1}\",ConatctNo:\"{2}\",AuthToken:\"{3}\",AccountSID:\"{4}\",isSMS:\"{5}\",isWhatsap:\"{6}\"", item.Key, item.From, item.ConatctNo, item.AuthToken, item.AccountSID, item.IsSMS, item.IsWhatsap);
                    }
                    else if (item.IsSMS == true)
                    {
                        newPair = string.Format("Key:\"{0}\",From:\"{1}\",ConatctNo:\"{2}\",AuthToken:\"{3}\",AccountSID:\"{4}\",isSMS:\"{5}\",isWhatsap:\"{6}\"", item.Key, item.From, item.ConatctNo, item.AuthToken, item.AccountSID, item.IsSMS, false);
                    }
                    else
                    {
                        newPair = string.Format("Key:\"{0}\",From:\"{1}\",ConatctNo:\"{2}\",AuthToken:\"{3}\",AccountSID:\"{4}\",isSMS:\"{5}\",isWhatsap:\"{6}\"", item.Key, item.From, item.ConatctNo, item.AuthToken, item.AccountSID, false, item.IsWhatsap);
                    }
                    configuration += "{" + newPair + "},";
                }

                existingKeyValuePair.Configuration = configuration;
                await _dbContext.SaveChangesAsync();

            }
            else if (SubmenuItemId == 20)
            {
                string newPair = string.Empty;

                //Update UsersConfig
                foreach (var id in addConfigurationObj.keyValue)
                {
                    newPair = string.Format("Key:\"{0}\",Value:\"{1}\"", id.Key, id.Value);

                    configuration += "{" + newPair + "},";
                }

                existingKeyValuePair.Configuration = configuration;
                await _dbContext.SaveChangesAsync();

                //Update GoogleAuthConfig
                var googleAuthConfigEntity = _dbContext.DemoConfigEntities.ToList().Last(x => x.SubMenuItemId == SubmenuItemId);
                newPair = string.Empty;
                configuration = string.Empty;

                foreach (var id in addConfigurationObj.googleAuthModels.keyValue)
                {
                    newPair = string.Format("Key:\"{0}\",Value:\"{1}\"", id.Key, id.Value);
                    configuration += "{" + newPair + "},";
                }

                googleAuthConfigEntity.Configuration = configuration;
                await _dbContext.SaveChangesAsync();
            }
            else if (SubmenuItemId == 11)
            {
                string newPair = string.Empty;

                //Update GoogleSheetConfig
                if (addConfigurationObj.keyValue != null)
                {
                    foreach (var item in addConfigurationObj.keyValue)
                    {
                        newPair = string.Format("Key:\"{0}\",SheetId:\"{1}\",SheetName:\"{2}\"", item.Key, item.SheetId, item.SheetName);

                        configuration += "{" + newPair + "},";
                    }

                    existingKeyValuePair.Configuration = configuration;
                    await _dbContext.SaveChangesAsync();
                }

                //Update SubSheetConfig
                var subSheetEntity = _dbContext.DemoConfigEntities.ToList().Last(x => x.SubMenuItemId == SubmenuItemId);
                newPair = string.Empty;
                configuration = string.Empty;

                if (addConfigurationObj.googleSubSheetModel != null)
                {
                    foreach (var item in addConfigurationObj.googleSubSheetModel.subSheetModels)
                    {
                        newPair = string.Format("SheetName:\"{0}\",SubSheetName:\"{1}\",SubSheetUrl:\"{2}\"", item.SheetName, item.SubSheetName, item.SubSheetUrl);
                        configuration += "{" + newPair + "},";
                    }

                    subSheetEntity.Configuration = configuration;
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (SubmenuItemId == 27)
            {
                string newPair = string.Empty;

                foreach (var item in addConfigurationObj.keyValue)
                {
                    newPair = string.Format("Key:\"{0}\",ClientId:\"{1}\",ClientSecret:\"{2}\",TenantId:\"{3}\"", item.Key, item.ClientId, item.ClientSecret, item.TenantId);

                    configuration += "{" + newPair + "},";
                }

                existingKeyValuePair.Configuration = configuration;
                await _dbContext.SaveChangesAsync();
            }
            else if (SubmenuItemId == 9)
            {
                string newPair = string.Empty;

                //Update ApplicationName
                if (addConfigurationObj.keyValue != null)
                {
                    foreach (var item in addConfigurationObj.keyValue)
                    {
                        newPair = string.Format("Key:\"{0}\",Application:\"{1}\"", item.Key, item.Application);

                        configuration += "{" + newPair + "},";
                    }

                    existingKeyValuePair.Configuration = configuration;
                    await _dbContext.SaveChangesAsync();
                }

                //Update Application Credencial
                var subQuickBookEntity = _dbContext.DemoConfigEntities.ToList().Last(x => x.SubMenuItemId == SubmenuItemId);

                newPair = string.Empty;
                configuration = string.Empty;

                if (addConfigurationObj.quickBookModel != null)
                {
                    foreach (var item in addConfigurationObj.quickBookModel.quickBookConfigModels)
                    {
                        newPair = string.Format("Application:\"{0}\",ClientId:\"{1}\",ClientScreat:\"{2}\",RefreshToken:\"{3}\",CompanyId:\"{4}\",CountryName:\"{5}\"", item.Application, item.ClientId, item.ClientScreat, item.RefreshToken, item.CompanyId, item.CountryName);
                        configuration += "{" + newPair + "},";
                    }

                    subQuickBookEntity.Configuration = configuration;
                    await _dbContext.SaveChangesAsync();
                }
            }

            var demoConfigDto = _mapper.Map<ConfigurationDto>(existingKeyValuePair);

            return ApiSuccessResponse(demoConfigDto);
        }
    }
}

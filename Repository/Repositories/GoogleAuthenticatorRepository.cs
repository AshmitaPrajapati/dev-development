using API.Database;
using API.DataTransferObjects.GoogleAuthenticationDtos;
using API.Model.GoogleAuthentication;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static API.Shared.ApiFunctions;

namespace API.Services.Repositories
{
    public class GoogleAuthenticatorRepository : IGoogleAuthenticatorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GoogleAuthenticatorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<ApiResponse<GoogleAuthenticationDto>>> Login(LoginModel login)
        {
            var demoConfigEntities = _dbContext.DemoConfigEntities.Where(x => x.SubMenuItemId == 20).ToList().Last();
            
            var config = "[" + demoConfigEntities.Configuration.Remove(demoConfigEntities.Configuration.Length - 1, 1) + "]";
            var configData = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(config);
            var finalValue = configData.ToArray();
            var googleAuthKey = finalValue.Last().Values.Last();

            string UserUniqueKey = (login.UserName + googleAuthKey);

            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();

            var setupInfo = TwoFacAuth.GenerateSetupCode("VisionAuthDemo.com", login.UserName, ConvertSecretToBytes(UserUniqueKey, false), 300);
            
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(30);

            var googleAuthenticationDemo = new GoogleAuthenticationDto();

            googleAuthenticationDemo.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            googleAuthenticationDemo.SetupCode = setupInfo.ManualEntryKey;
            googleAuthenticationDemo.UserUniqueKey = UserUniqueKey;

            return ApiSuccessResponse(googleAuthenticationDemo);
        }

        private static byte[] ConvertSecretToBytes(string secret, bool secretIsBase32)
        {
            return secretIsBase32 ? Base32Encoding.ToBytes(secret) : Encoding.UTF8.GetBytes(secret);
        }

        public async Task<ApiResponse<string>> TwoFactorAuthenticate(string codeDigit, string userUniqueKey)
        {
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(userUniqueKey, codeDigit, false);

            if (isValid) 
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = true,
                    ResponseData = "IsValidTwoFactorAuthentication"
                };
            } 
            else 
            {
                return new ApiResponse<string>()
                {
                    IsSuccess = false,
                    ResponseData = "Google Two Factor PIN is expired or wrong"
                };
            }
        }
    }
}

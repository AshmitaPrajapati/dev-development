using API.DataTransferObjects.GoogleAuthenticationDtos;
using API.Model.GoogleAuthentication;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class GoogleAuthenticatorService : IGoogleAuthenticatorService
    {
        private readonly IGoogleAuthenticatorRepository _googleAuthenticatorRepository;

        public GoogleAuthenticatorService(IGoogleAuthenticatorRepository googleAuthenticatorRepository) 
        {
            _googleAuthenticatorRepository = googleAuthenticatorRepository;
        }

        public async Task<ActionResult<ApiResponse<GoogleAuthenticationDto>>> Login(LoginModel login)
        { 
            return await _googleAuthenticatorRepository.Login(login);
        }

        public async Task<ApiResponse<string>> TwoFactorAuthenticate(string codeDigit, string userUniqueKey)
        {
            return await _googleAuthenticatorRepository.TwoFactorAuthenticate(codeDigit, userUniqueKey);
        }
    }
}

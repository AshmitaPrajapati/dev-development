using API.DataTransferObjects.GoogleAuthenticationDtos;
using API.Model.GoogleAuthentication;
using API.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interface
{
    public interface IGoogleAuthenticatorService
    {
        Task<ActionResult<ApiResponse<GoogleAuthenticationDto>>> Login(LoginModel login);

        Task<ApiResponse<string>> TwoFactorAuthenticate(string codeDigit, string userUniqueKey);
    }
}

using API.DataTransferObjects.TwilioDtos;
using API.Model.Twilio;
using API.Shared;

namespace API.Services.Repositories.Interfaces
{
    public interface ITwilioRepository
    {
        Task<ApiResponse<ResponseVMDto>> SendSMS(TwilioSMS twilioSMS);

        Task<ApiResponse<ResponseVMDto>> SendWhatsappMessage(TwilioWhatsapp twilioWhatsapp);
    }
}

using API.DataTransferObjects.TwilioDtos;
using API.Model.Twilio;
using API.Shared;

namespace API.Services.Interface
{
    public interface ITwilioService
    {
        Task<ApiResponse<ResponseVMDto>> SendSMS(TwilioSMS twilioSMS);

        Task<ApiResponse<ResponseVMDto>> SendWhatsappMessage(TwilioWhatsapp twilioWhatsapp);
    }
}

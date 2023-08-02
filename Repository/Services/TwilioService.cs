using API.DataTransferObjects.TwilioDtos;
using API.Model.Twilio;
using API.Services.Interface;
using API.Services.Repositories.Interfaces;
using API.Shared;

namespace API.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly ITwilioRepository _twilioRepository;

        public TwilioService(ITwilioRepository twilioRepository) 
        {
            _twilioRepository = twilioRepository;
        }

        public async Task<ApiResponse<ResponseVMDto>> SendSMS(TwilioSMS twilioSMS)
        {
            return await _twilioRepository.SendSMS(twilioSMS);
        }

        public async Task<ApiResponse<ResponseVMDto>> SendWhatsappMessage(TwilioWhatsapp twilioWhatsapp)
        {
            return await _twilioRepository.SendWhatsappMessage(twilioWhatsapp);
        }

     }
}

using API.DataTransferObjects.TwilioDtos;
using API.Model.Twilio;
using API.Services.Repositories.Base.TwilioBaseRepository;
using API.Services.Repositories.Interfaces;
using API.Shared;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace API.Services.Repositories
{
    public class TwilioRepository : TwilioBaseRepository, ITwilioRepository
    {
        private readonly IConfiguration _configuration;

        public TwilioRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<ApiResponse<ResponseVMDto>> SendSMS(TwilioSMS twilioSMS)
        {
            var query = new List<Parameter>()
            {
                      new Parameter("Body", twilioSMS.Body, ParameterType.GetOrPost),
                      new Parameter("To", twilioSMS.To, ParameterType.GetOrPost),
                      new Parameter("From", twilioSMS.From, ParameterType.GetOrPost),
                      new Parameter("AccountSID", twilioSMS.AccountSID, ParameterType.QueryString)
            };

            return await SendRequest<ResponseVMDto>($"{twilioSMS.AccountSID}/Messages.json", query, twilioSMS, Method.POST);
        }

        public async Task<ApiResponse<ResponseVMDto>> SendWhatsappMessage(TwilioWhatsapp twilioWhatsapp)
        {
            var query = new List<Parameter>()
            {
                      new Parameter("Body", twilioWhatsapp.Body, ParameterType.GetOrPost),
                      new Parameter("To", "whatsapp:"+twilioWhatsapp.To, ParameterType.GetOrPost),
                      new Parameter("From","whatsapp:" +twilioWhatsapp.From, ParameterType.GetOrPost)
            };

            if (!string.IsNullOrEmpty(twilioWhatsapp.MediaUrl))
                query.Add(new Parameter("MediaUrl", twilioWhatsapp.MediaUrl, ParameterType.GetOrPost));

            return await SendRequest<ResponseVMDto>($"{twilioWhatsapp.AccountSID}/Messages.json", query, twilioWhatsapp, Method.POST);
        }
    }
}

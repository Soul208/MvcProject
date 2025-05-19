using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace Demo.Presentation.Helpers
{
    public class SmsService : ISmsService
    {
        private readonly IOptions<SmsSettings> _options;

        public SmsService(IOptions<SmsSettings> options)
        {
            _options = options;
        }
        public MessageResource SendSms(SmsMessage smsMessage)
        {
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            var message = MessageResource.Create(
                body: smsMessage.Body, 
                from: new Twilio.Types.PhoneNumber(_options.Value.TwilioPhoneNumber),
                to:smsMessage.PhoneNumber
                );
            return message;


    }   }
}

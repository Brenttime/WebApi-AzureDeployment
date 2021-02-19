using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace WebApi.TwilioAddon
{
    public class TwiText
    {
       private string accountSid; 
       private string authToken;
        public TwiText(string acctId, string aToken)
        {
            accountSid = acctId;
            authToken = aToken;
            TwilioClient.Init(accountSid, authToken);
        }

        public void Message()
        {
            List<string> numbers = new List<string>(new string[] { "+16107643555", "+14843633620", "+17179654573" });
            MessageResource message = null;
            foreach (var number in numbers) {
                   message = MessageResource.Create(
                   body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                   from: new Twilio.Types.PhoneNumber("+12405415184"),
                   to: new Twilio.Types.PhoneNumber(number)
               );
            }
            try
            {
                Console.WriteLine(message.Sid);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
//Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
//Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
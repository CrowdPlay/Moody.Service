using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moody.Data;
using Moody.Models.Requests;

namespace Moody.Service.Controllers
{
    public class SmsController : ApiController
    {
        public HttpResponseMessage Post(InboundMessage message)
        {
            var service = new UserService();
            var parts = message.MessageText.Split('-');
            var user = new RequestUserWithoutRoom
            {
                Mood = parts[1].Trim(),
                TwitterHandle = parts[0].Trim()
            };
            service.Upsert(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
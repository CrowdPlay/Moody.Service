using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moody.Data;
using Moody.Models.Requests;

namespace Moody.Service.Controllers
{
    public class UserWithoutRoomController : ApiController
    {
        public HttpResponseMessage Put(RequestUserWithoutRoom user)
        {
            var service = new UserService();
            service.Upsert(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

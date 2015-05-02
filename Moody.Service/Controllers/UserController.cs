using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Requests;

namespace Moody.Service.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage Put(RequestUser user)
        {
            var service = new UserService();
            service.Upsert(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public JsonResult<RequestUser> Get(string id)
        {
            var service = new UserService();
            var converter = new UserResponseConverter();

            var user = service.GetByHandle(id);
            return Json(converter.Convert(user));
        }
    }
}

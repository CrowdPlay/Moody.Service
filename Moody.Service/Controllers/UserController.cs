using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Requests;
using Moody.Service.Converters;

namespace Moody.Service.Controllers
{
    public class UserController : ApiController
    {
        public JsonResult<int> Put(RequestUser user)
        {
            var service = new UserService();
            var roomid = service.Upsert(user);
            return Json(roomid);
        }

        public JsonResult<RequestUser> Get(string id)
        {
            var service = new UserService();
            var converter = new UserResponseConverter();
            var user = service.GetByHandle(id);
            var response = converter.Convert(user);

            return Json(response);
        }
    }
}

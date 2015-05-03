using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Data;
using Moody.Models.Requests;
using Moody.Service.Converters;

namespace Moody.Service.Controllers
{
    public class RoomInfoController : ApiController
    {
        public JsonResult<RequestRoom> Get(int id)
        {
            var service = new RoomService();
            var converter = new RoomResponseConverter();
            var room = service.GetById(id);
            var response = converter.Convert(room);

            return Json(response);
        }
    }
}

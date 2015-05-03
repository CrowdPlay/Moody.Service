using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Data;

namespace Moody.Service.Controllers
{
    public class GetAllRoomController : ApiController
    {
        public JsonResult<IEnumerable<int>> Get()
        {
            var service = new RoomService();
            var rooms = service.GetAll();

            return Json(rooms);
        }
    }
}

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Data;
using Moody.Models.Requests;
using Moody.Service.Converters;

namespace Moody.Service.Controllers
{
    public class RoomController : ApiController
    {
        public JsonResult<RequestTrack> Get(int id)
        {
            var service = new RoomService();
            var converter = new TrackResponseConverter();
            var track = service.GetCurrentTrack(id);
            var response = converter.Convert(track);

            return Json(response);
        }

        public JsonResult<IEnumerable<Room>> GetAll()
        {
            var service = new RoomService();
            var rooms = service.GetAll();

            return Json(rooms);
        }
    }
}

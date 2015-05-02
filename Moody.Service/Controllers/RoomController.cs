using System.Web.Http;
using System.Web.Http.Results;
using Moody.Service.Models;

namespace Moody.Service.Controllers
{
    public class RoomController : ApiController
    {
        public JsonResult<Track> Get(int id)
        {
            return Json(new Track(43221994));
        }
    }
}

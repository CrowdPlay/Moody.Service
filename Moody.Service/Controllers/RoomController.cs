using System.Web.Http;
using System.Web.Http.Results;
using Moody.Service.Models;

namespace Moody.Service.Controllers
{
    public class RoomController : ApiController
    {
        public JsonResult<Track> Get(int id)
        {
            //TODO: pull track based on mood from db
            return Json(new Track(43221994));
        }
    }
}

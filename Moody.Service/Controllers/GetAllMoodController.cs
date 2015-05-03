using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Moody.Data;
using Moody.Models.Data;

namespace Moody.Service.Controllers
{
    public class GetAllMoodController : ApiController
    {
        public JsonResult<IEnumerable<string>> Get()
        {
            var service = new MoodService();
            var moods = service.GetAll();

            return Json(moods);
        }
    }
}

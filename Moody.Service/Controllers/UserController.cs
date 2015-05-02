using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}

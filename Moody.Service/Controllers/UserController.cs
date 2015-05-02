using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moody.Service.Models;

namespace Moody.Service.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage Put(User user)
        {
            //TODO: update user in DB
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

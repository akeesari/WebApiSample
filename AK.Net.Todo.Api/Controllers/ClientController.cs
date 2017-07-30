using AK.Net.Todo.Api.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AK.Net.Todo.Api.Controllers
{
    public class ClientController : BaseApiController
    {
        //    // POST api/Admin/RegisterClient
        //    [System.Web.Http.Route("registerclient")]
        //    [System.Web.Http.AllowAnonymous]
        //    public async Task<IHttpActionResult> RegisterClient(Client client)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        client.Secret = new PasswordHasher().HashPassword(client.Secret);
        //        await _repo.RegisterClient(client);

        //        return Ok();
        //    }
    }
}

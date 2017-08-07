using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using AK.Net.Todo.Api.Controllers;
using AK.Net.Todo.Api.Models;

namespace Axa.Ppp.Dha.Api.Controllers
{
    [RoutePrefix("api/client")]
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class ClientController : BaseApiController
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        // GET: api/client
        [Route("GetClients")]
        public IQueryable<Client> GetClients()
        {
            return _dbContext.Client;
        }

        // GET: api/client/5
        [ResponseType(typeof(Client))]
        [Route("getclient")]
        public async Task<IHttpActionResult> GetClient(int id)
        {
            var client = await _dbContext.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/client/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(string name, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ClientExists(client.Name))
            {
                return BadRequest();
            }

            _dbContext.Entry(client).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(client);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/client
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient([FromBody] Client client)
        {
            
            if (ClientExists(client.Name))
            {
                return Conflict();
            }
            client.ClientId = Guid.NewGuid().ToString();
            client.Secret = new PasswordHasher().HashPassword(client.Secret);
            client.CreatedOn = DateTime.Now.ToShortDateString();
            //client.RefreshTokenLifeTime = DateTime.Now.AddDays(30).ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Client.Add(client);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(client);
            //return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/client/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client client = await _dbContext.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _dbContext.Client.Remove(client);
            await _dbContext.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(string name)
        {
            return _dbContext.Client.Count(e => e.Name == name) > 0;
        }
    }
}
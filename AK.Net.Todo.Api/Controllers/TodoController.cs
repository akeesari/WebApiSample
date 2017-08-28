using AK.Net.Todo.Data.Repository;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System;
using System.Linq;

namespace AK.Net.Todo.Api.Controllers
{
    //[Authorize]
    [RoutePrefix("api/todo")]
    public class TodoController : BaseApiController
    {
        // GET api/todo
        public async Task<IEnumerable<Data.Todo>> Get()
        {
            var repo = new TodoRepository();
            return await repo.GetTodosAsync();
        }

        // GET api/todo/5
        public async Task<Data.Todo> Get(int id)
        {
            var repo = new TodoRepository();
            return await repo.GetTodoAsync(id);

        }

        // POST api/todo
        public async Task<HttpResponseMessage> Post([FromBody]Data.Todo todo)
        {
            try
            {
                var repo = new TodoRepository();
                // Make sure it's not duplicate
                var todos = await repo.GetTodosAsync();
                if (todos != null & todos.Where(to => to.Name == todo.Name).Any())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate entry not allowed.");
                }

                var status = await repo.AddTodoAsync(todo);

                if (status)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, todo);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Data.Todo todo)
        {
            var repo = new TodoRepository();
            var status = repo.UpdateTodoAsync(todo);
        }

        // DELETE api/todo/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var repo = new TodoRepository();
                var todo = await repo.GetTodoAsync(id);
                if (todo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var status = await repo.DeleteTodoAsync(todo);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
//using System.Net;
using System.Web;
using System.Web.Mvc;
using AK.Net.Todo.App.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace AK.Net.Todo.App.Controllers
{
    public class TodoController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TodoViewModels
        public async Task<ActionResult> Index()
        {

            const string clientUrl = "api/todo";

            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(clientUrl).Result;

                response.EnsureSuccessStatusCode();
                var todos = response.Content.ReadAsAsync<List<TodoViewModel>>().Result;
                //if (response.IsSuccessStatusCode)
                //{

                //}
                return View(todos);
            }

        }

        // GET: Todo/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var todo = await GetTodo(id);

            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // GET: TodoViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IsClosed")] TodoViewModel todoViewModel)
        {
            if (ModelState.IsValid)
            {

                const string clientUrl = "api/todo";

                using (var httpClient = GetHttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync(clientUrl, todoViewModel);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync();
                        var newClient = JsonConvert.DeserializeObject<TodoViewModel>(responseBody.Result);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(todoViewModel);
        }

        private async Task<TodoViewModel> GetTodo(int? id)
        {
            string clientUrl = "api/todo?id=" + id;
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(clientUrl).Result;

                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<TodoViewModel>().Result;
            }
        }

        // GET: Todo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var todo = await GetTodo(id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);

        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IsClosed")] TodoViewModel todoViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(todoViewModel).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                using (var httpClient = GetHttpClient())
                {

                    var postResponse = await httpClient.PutAsJsonAsync("api/todo?id=" + todoViewModel.Id, todoViewModel);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    

        // GET: Todo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var todo = await GetTodo(id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string clientUrl = "api/todo?id=" + id;
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.DeleteAsync(clientUrl).Result;

                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
        }
    
    }
}

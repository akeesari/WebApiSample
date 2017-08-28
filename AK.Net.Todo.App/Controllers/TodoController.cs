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
                if (response.IsSuccessStatusCode)
                {
                    
                    //foreach (var todo in todos)
                    //{
                    //    Console.WriteLine("ClientId: {0}", client.ClientId);
                    //}
                }
                return View(todos);
            }
            
        }

        // GET: TodoViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TodoViewModel todoViewModel = await db.Todos.FindAsync(id);
            //if (todoViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new TodoViewModel());
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
                //db.Todos.Add(todoViewModel);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(todoViewModel);
        }

        // GET: TodoViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoViewModel todoViewModel =  new TodoViewModel();
            if (todoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(todoViewModel);
        }

        // POST: TodoViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IsClosed")] TodoViewModel todoViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(todoViewModel);
        }

        // GET: TodoViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoViewModel todoViewModel = new TodoViewModel();
            if (todoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(todoViewModel);
        }

        // POST: TodoViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TodoViewModel todoViewModel = new TodoViewModel();
            //db.Todos.Remove(todoViewModel);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

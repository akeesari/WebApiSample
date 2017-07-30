using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Net.Todo.Data.Repository
{
    public class TodoRepository : ITodoRepository
    {
        public async Task<List<Todo>> GetTodosAsync()
        {
            using (var ctx = new TodoContext())
            {
                return await ctx.Todos.ToListAsync();                                
            }
            
        }
        public async Task<Todo> GetTodoAsync(int id)
        {
            using (var ctx = new TodoContext())
            {
                return await ctx.Todos.SingleOrDefaultAsync(t=> t.Id == id );
            }
        }
        //public async Task<int> DeleteTodoAsync(int id)
        //{
        //    using (var ctx = new TodoContext())
        //    {
        //        var todo = from t in ctx.Todos
        //                   where t.Id == id 
        //                   select t;
        //        ctx.Todos.Remove(todo.Single());
        //        var todo = ctx.Todos.Single(t => t.Id == id);                
        //        ctx.Entry(todo).State = EntityState.Deleted;
        //        var status = ctx.SaveChangesAsync();
        //        return await status;
        //    }
        //}
        public async Task<bool> DeleteTodoAsync( Todo todo)
        {
            using (var ctx = new TodoContext())
            {
                ctx.Entry(todo).State = EntityState.Deleted;
                var status =await ctx.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> AddTodoAsync(Todo todo)
        {
            using (var ctx = new TodoContext())
            {
                ctx.Entry(todo).State = EntityState.Added;
                var status = await ctx.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateTodoAsync(Todo newTodo)
        {
            using (var ctx = new TodoContext())
            {
                var existingTodo = ctx.Todos.Single(t => t.Id == newTodo.Id);
                existingTodo.Name = newTodo.Name;
                existingTodo.IsClosed= newTodo.IsClosed;
                ctx.Entry(existingTodo).State = EntityState.Modified;
                var status = await ctx.SaveChangesAsync();
                return true;
            }
        }
        public IQueryable<Todo> GetTodos()
        {
            using (var ctx = new TodoContext())
            {
                return ctx.Todos;
            }
        }

    }
}

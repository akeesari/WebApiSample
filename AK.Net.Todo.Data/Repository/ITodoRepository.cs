using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AK.Net.Todo.Data.Repository
{
    public interface ITodoRepository
    {
        Task<bool> AddTodoAsync(Todo todo);
        //Task<int> DeleteTodoAsync(int id);
        Task<bool> DeleteTodoAsync(Todo todo);
        Task<Todo> GetTodoAsync(int id);
        IQueryable<Todo> GetTodos();
        Task<List<Todo>> GetTodosAsync();
        Task<bool> UpdateTodoAsync(Todo newTodo);
    }
}
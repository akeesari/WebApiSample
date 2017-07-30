using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AK.Net.Todo.Data.Repository;

namespace AK.Net.Todo.Test
{
    [TestClass]
    public class TodoRepositoryTest
    {
        [TestMethod]
        public void TestGetTaskAsync()
        {
            var repo = new TodoRepository();
            var result = repo.GetTodoAsync(1);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void TestGetTasksAsync()
        {
            var repo = new TodoRepository();
            var result = repo.GetTodosAsync();
            Assert.IsNotNull(result);

        }
    }
}

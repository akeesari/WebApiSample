using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace WebApiTest
{
  public class Program
    {
        
        public static void Main(string[] args)
        {
            TestClient();

            Console.WriteLine("Enter any key to exit:");
            Console.ReadLine();

        }
        private static void TestTodo()
        {
            var TestTodo = new TestTodo();
            TestTodo.TestTodoApi();
        }
        private static void TestClient()
        {
            var test = new TestClient();
            //test.GetClients();
            //test.GetClient();
            test.AddClient();
        }
    }
}

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
            TestTodoAPI();
            Console.WriteLine("Enter any key to exit:");
            Console.ReadLine();

        }
    }
}

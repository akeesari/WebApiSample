
using AK.Net.Todo.Data.Migrations;
using System;
using System.Data.Entity;
namespace AK.Net.Todo.Data
{
    public partial class TodoContext : DbContext
    {
        public TodoContext()
            : base("name=TodoContext")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }
        //static TodoContext()
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoContext, Configuration>());
        //}
        //public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            TodoContextMapping.Configure(modelBuilder);
           
        }
    }
}


using AK.Net.Todo.Data.Migrations;
using System;
using System.Data.Entity;
namespace AK.Net.Todo.Data
{
    public partial class TodoContextMapping 
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            MapTodo(modelBuilder);
        }

        static void MapTodo(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
               .Property(e => e.Name)
               .IsUnicode(false);

            //modelBuilder.Entity<Todo>().ToTable("Todo", "Todo");
        }

    }
}

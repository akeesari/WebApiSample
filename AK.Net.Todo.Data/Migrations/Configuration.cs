namespace AK.Net.Todo.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }
#if DEBUG
        protected override void Seed(AK.Net.Todo.Data.TodoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Todos.AddOrUpdate(
              p => p.Name,
              new Todo { Name= "Start Learning API" },
              new Todo { Name= "Do quick tutorial" },
              new Todo { Name= "Create more APIs" }
            );
            //
        }
    }
    #endif

}

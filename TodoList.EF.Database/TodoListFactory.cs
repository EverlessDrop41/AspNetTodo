using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Database
{
    public class TodoListFactory
    {
        public TodoListContext Create() { 
            var optionsBuilder = new DbContextOptionsBuilder<TodoListContext>();

            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TodoListDb;Trusted_Connection=True;MultipleActiveResultSets=True;");

            return new TodoListContext(optionsBuilder.Options);
        }
    }
}

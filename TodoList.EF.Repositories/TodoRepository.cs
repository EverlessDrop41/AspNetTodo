using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;
using TodoList.EF.Model;
using TodoList.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace TodoList.EF.Repositories
{
    public class TodoRepository : IRepository<Todo>
    {
        private static List<Todo> _todoList = new List<Todo>()
        {
            new Todo() { Id = 0, Name = "Wizardry", Completed = false },
            new Todo() { Id = 1, Name = "Something", Completed = true },
            new Todo() { Id = 2, Name = "The Dishes", Completed = false }
        };

        TodoListContext _dbContext;

        public TodoRepository(TodoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Todo> AsQueryable()
        {
            return _dbContext.Todos.AsQueryable();
        }

        public bool Has(int id)
        {
            return _todoList.Any(x => x.Id == id);
        }

        public AddDTO Create(Todo item)
        {
            string error = "";

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                error = "Name is required";
            }

            if (!item.Completed.HasValue)
            {
                item.Completed = false;
            }

            if (string.IsNullOrWhiteSpace(error))
            {
                var newTodo = new Todo(item.Name, item.Completed.Value);
                _dbContext.Todos.Add(newTodo);
                _dbContext.SaveChanges();
                return new AddDTO() { Success = true, Id = item.Id};
            }

            return new AddDTO() { Success = false, ErrorMessage = error };
        }

        public SuccessDTO Update(int id, Func<Todo, Todo> getNew)
        {
            try
            {
                Todo theTodo = _dbContext.Todos.Where(x => x.Id == id && x.IsDeleted == false).First();
                Todo newTodo = getNew(theTodo);
                theTodo.Name = newTodo.Name;
                theTodo.Completed = newTodo.Completed;
                _dbContext.SaveChanges();
                return new SuccessDTO() { Success = true };
            }
            catch (Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
            {
                return new SuccessDTO() { Success = false, ErrorMessage = "Cannot find todo with that id" };
            }
        }

        public SuccessDTO Delete(int id)
        {
            try
            {
                Todo theTodo = _dbContext.Todos.Where(x => x.Id == id).First();
                theTodo.IsDeleted = true;
                _dbContext.SaveChanges();
                return new SuccessDTO() { Success = true };
            }
            catch (Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
            {
                return new SuccessDTO() { Success = false, ErrorMessage = "Cannot find todo with that id" };
            }
        }
    }
}

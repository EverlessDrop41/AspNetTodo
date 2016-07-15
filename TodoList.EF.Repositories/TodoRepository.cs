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

        TodoListFactory _dbFactory;

        public TodoRepository(TodoListFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IQueryable<Todo> AsQueryable()
        {
            var db = _dbFactory.Create();
                return db.Todos.AsQueryable();
        }

        public bool Has(int id)
        {
            using (var db = _dbFactory.Create())
                return db.Todos.Any(x => x.Id == id);
        }

        public AddDTO Create(Todo item)
        {
            using (var db = _dbFactory.Create())
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
                    db.Todos.Add(newTodo);
                    db.SaveChanges();
                    return new AddDTO() { Success = true, Id = item.Id };
                }
                return new AddDTO() { Success = false, ErrorMessage = error };
            }
        }

        public SuccessDTO Update(int id, Func<Todo, Todo> getNew)
        {
            using (var db = _dbFactory.Create())
            {
                try
                {
                    Todo theTodo = db.Todos.Where(x => x.Id == id && x.IsDeleted == false).First();
                    Todo newTodo = getNew(theTodo);
                    theTodo.Name = newTodo.Name;
                    theTodo.Completed = newTodo.Completed;
                    db.SaveChanges();
                    return new SuccessDTO() { Success = true };
                }
                catch (Exception e) when (e is ArgumentNullException
                    || e is InvalidOperationException
                    || e is InvalidCastException)
                {
                    return new SuccessDTO() { Success = false, ErrorMessage = $"Cannot find todo with an id of {id}. Exception Message: {e.Message}" };
                }
            }
            
        }

        public SuccessDTO Delete(int id)
        {
            using (var db = _dbFactory.Create())
            {
                try
                {
                    Todo theTodo = db.Todos.Where(x => x.Id == id).First();
                    theTodo.IsDeleted = true;
                    db.SaveChanges();
                    return new SuccessDTO() { Success = true };
                }
                catch (Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
                {
                    return new SuccessDTO() { Success = false, ErrorMessage = "Cannot find todo with that id" };
                }
            }
        }     
    }
}

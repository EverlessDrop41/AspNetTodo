using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;
using TodoList.EF.Model;

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

        public IQueryable<Todo> AsQueryable()
        {
            return _todoList.AsQueryable();
        }

        public bool Has(int id)
        {
            return _todoList.Any(x => x.Id == id);
        }

        public AddDTO Create(Todo item)
        {
            var data = AsQueryable();

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
                item.Id = _todoList.Count();
                _todoList.Add(item);
                return new AddDTO() { Success = true, Id = item.Id};
            }

            return new AddDTO() { Success = false, ErrorMessage = error };
        }

        public SuccessDTO Update(int id, Func<Todo, Todo> getNew)
        {
            var data = AsQueryable().Where(x => x.Id == id && x.IsDeleted == false);

            if (data.Any())
            {
                try
                {
                    Todo todo = data.Select(x => new Todo(x.Name, x.Completed.Value) {Id = id}).ToList()[0];

                    _todoList[todo.Id] = getNew(todo);
                    return new SuccessDTO() { Success = true };
                }
                catch (Exception)
                {
                    return new SuccessDTO() { Success = false, ErrorMessage = "Error finding todo"};
                } 
            }

            return new SuccessDTO() { Success = false, ErrorMessage = "Cannot find todo with that id" };
        }

        public SuccessDTO Delete(int id)
        {
            var data = AsQueryable();

            if (Has(id))
            {
                try
                {
                    Todo todo = data.Select(x => new Todo(x.Name, x.Completed.Value) { Id = id }).ToList()[0];

                    _todoList[todo.Id].IsDeleted = true;
                    return new SuccessDTO() { Success = true };
                }
                catch (Exception)
                {
                    return new SuccessDTO() { Success = false, ErrorMessage = "Error finding todo" };
                }
            }

            return new SuccessDTO() { Success = false, ErrorMessage = "Cannout find todo to delete" };
        }
    }
}

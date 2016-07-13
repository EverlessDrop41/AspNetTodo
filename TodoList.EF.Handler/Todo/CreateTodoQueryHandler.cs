using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Repositories;

namespace TodoList.EF.Handler.Todo
{
    public class CreateTodoQueryHandler : IQueryHandler<CreateTodoQuery, AddDTO>
    {
        TodoRepository _todoRepo;

        public CreateTodoQueryHandler(TodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }
        public AddDTO Execute(CreateTodoQuery query)
        {
            return _todoRepo.Create(new Model.Todo() {
                Name = query.NewTodo.Name,
                Completed = query.NewTodo.Completed
            });
        }
    }
}

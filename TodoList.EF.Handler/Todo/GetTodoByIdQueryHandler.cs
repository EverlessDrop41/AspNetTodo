using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO.Todo;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Repositories;

namespace TodoList.EF.Handler.Todo
{
    public class GetTodoByIdQueryHandler : IQueryHandler<GetTodoByIdQuery, SingleTodoDTO>
    {
        TodoRepository _todoRepo;

        public GetTodoByIdQueryHandler(TodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public SingleTodoDTO Execute(GetTodoByIdQuery query)
        {
            var data = _todoRepo.AsQueryable();

            if (_todoRepo.Has(query.Id))
            {
                data = data.Where(x => x.Id == query.Id);

                var todo = data.ToArray()[0];

                TodoDTO todoDTO = new TodoDTO()
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Completed = todo.Completed
                };

                return new SingleTodoDTO()
                {
                    Success = true,
                    todo = todoDTO
                };
            }

            return SingleTodoDTO.NotFound;
        }
    }
}

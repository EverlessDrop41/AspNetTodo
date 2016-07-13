using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO.Todo;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Repositories;

namespace TodoList.EF.Handler.Todo
{
    public class GetTodosQueryHandler : IQueryHandler<GetTodosQuery, TodoListDTO>
    {
        private static TodoRepository _todoRepo;

        public GetTodosQueryHandler(TodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public TodoListDTO Execute(GetTodosQuery query)
        {
            var data = _todoRepo.AsQueryable();

            data = data.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                data = data.Where(x => x.Name.Contains(query.SearchTerm));
            }

            if (query.IsCompleted.HasValue)
            {
                data = data.Where(x => x.Completed == query.IsCompleted.Value);
            }

            List<TodoDTO> todos = data.Select(x => new TodoDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Completed = x.Completed
            }).ToList();

            return new TodoListDTO()
            {
                Todos = todos
            };
        }
    }
}

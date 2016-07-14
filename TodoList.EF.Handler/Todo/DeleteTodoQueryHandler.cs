using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Handler;
using TodoList.EF.Repositories;

namespace TodoList.EF.Handler.Todo
{
    public class DeleteTodoQueryHandler : IQueryHandler<DeleteTodoQuery, SuccessDTO>
    {
        TodoRepository _todoRepo;

        public DeleteTodoQueryHandler(TodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public SuccessDTO Execute(DeleteTodoQuery query)
        {
            return _todoRepo.Delete(query.Id);
        }
    }
}

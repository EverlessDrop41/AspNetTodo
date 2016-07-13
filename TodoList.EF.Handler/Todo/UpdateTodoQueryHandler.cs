using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Repositories;

namespace TodoList.EF.Handler.Todo
{
    public class UpdateTodoQueryHandler : IQueryHandler<UpdateTodoQuery, SuccessDTO>
    {
        TodoRepository _todoRepo;

        public UpdateTodoQueryHandler(TodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public SuccessDTO Execute(UpdateTodoQuery query)
        {
            if (query.Id.HasValue)
            {
                bool updateName = query.NewTodo.Name != null;
                bool updateCompleted = query.NewTodo.Completed.HasValue;

                return _todoRepo.Update(query.Id.Value, x => new Model.Todo() {
                    Id = x.Id,
                    Name = updateName ? query.NewTodo.Name : x.Name,
                    Completed = updateCompleted ? query.NewTodo.Completed.Value : x.Completed,
                    IsDeleted = x.IsDeleted
                });
            }  

            return SuccessDTO.NotFound;
        }
    }
}

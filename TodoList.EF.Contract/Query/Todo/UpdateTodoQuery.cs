using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.EF.Contract.DTO;

namespace TodoList.EF.Contract.Query.Todo
{
    public class UpdateTodoQuery : IQuery<SuccessDTO>
    {
        public int? Id { get; set; }

        [FromBody]
        public TodoQueryObject NewTodo { get; set; }
    }
    
}

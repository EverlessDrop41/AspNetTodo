using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;

namespace TodoList.EF.Contract.Query.Todo
{
    public class CreateTodoQuery : IQuery<AddDTO>
    {
        [FromBody]
        public TodoQueryObject NewTodo { get; set; }
    }
}

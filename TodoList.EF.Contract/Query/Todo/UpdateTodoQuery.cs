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
        public TodoUpdateObject NewTodo { get; set; }
    }

    public struct TodoUpdateObject
    {
        public string Name { get; set; }
        public bool? Completed { get; set; }
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO.Todo;

namespace TodoList.EF.Contract.Query.Todo
{
    public class GetTodosQuery : IQuery<TodoListDTO>
    {
        public string SearchTerm { get; set; }
        public bool? IsCompleted { get; set; }
    }
}

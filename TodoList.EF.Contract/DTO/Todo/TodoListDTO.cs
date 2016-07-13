using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO.Todo
{
    public class TodoListDTO
    {
        public IEnumerable<TodoDTO> Todos { get; set; }
    }
}

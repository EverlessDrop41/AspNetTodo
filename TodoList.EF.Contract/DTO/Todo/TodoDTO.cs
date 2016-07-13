using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO.Todo
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Completed { get; set; }
    }
}

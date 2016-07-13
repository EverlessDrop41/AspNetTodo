using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO.Todo
{
    public class SingleTodoDTO : SuccessDTO
    {
        public static SingleTodoDTO NotFound = 
            new SingleTodoDTO() { Success = false, ErrorMessage = "Not Found", todo = null };
        public TodoDTO todo;
    }
}

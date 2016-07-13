using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;

namespace TodoList.EF.Contract.Query.Todo
{
    public class DeleteTodoQuery : IQuery<SuccessDTO>
    {
        public int Id { get; set; }
    }
}

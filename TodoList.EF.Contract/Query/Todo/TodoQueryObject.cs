using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.Query.Todo
{
    public struct TodoQueryObject
    {
        public string Name { get; set; }
        public bool? Completed { get; set; }
    }
}

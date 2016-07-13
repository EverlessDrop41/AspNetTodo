using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO
{
    public class SuccessDTO
    {
        public bool? Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

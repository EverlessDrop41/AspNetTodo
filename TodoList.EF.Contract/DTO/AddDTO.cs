using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO
{
    public class AddDTO : SuccessDTO
    {
        public int? Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Contract.DTO
{
    public class SuccessDTO
    {
        public static SuccessDTO NotFound = new SuccessDTO() { Success = false, ErrorMessage = "Not Found" };
        public static SuccessDTO GenerateError (string Message)
        {
            return new SuccessDTO() { Success = false, ErrorMessage = Message };
        }
        public bool? Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

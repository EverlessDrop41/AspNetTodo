using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.EF.Contract.DTO.Todo;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Handler;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.EF.API.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {
        IQueryHandler<GetTodosQuery, TodoListDTO> _getTodosQuery;
        IQueryHandler<GetTodoByIdQuery, SingleTodoDTO> _getTodoByIdQuery;

        public TodoController(
            IQueryHandler<GetTodosQuery, TodoListDTO> getTodosQuery,
            IQueryHandler<GetTodoByIdQuery, SingleTodoDTO> getTodoByIdQuery)
        {
            _getTodosQuery = getTodosQuery;
            _getTodoByIdQuery = getTodoByIdQuery;
        }

        // GET: api/todo
        [HttpGet]
        public IActionResult Get(GetTodosQuery query)
        {
            return Ok(_getTodosQuery.Execute(query));
        }

        // GET api/todo/5
        [HttpGet("{Id}")]
        public IActionResult GetById(GetTodoByIdQuery query)
        {
            return Ok(_getTodoByIdQuery.Execute(query));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.EF.Contract.DTO.Todo;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Handler;
using TodoList.EF.Contract.DTO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.EF.API.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {
        IQueryHandler<GetTodosQuery, TodoListDTO> _getTodosQuery;
        IQueryHandler<GetTodoByIdQuery, SingleTodoDTO> _getTodoByIdQuery;
        IQueryHandler<UpdateTodoQuery, SuccessDTO> _updateTodoQuery;
        IQueryHandler<CreateTodoQuery, AddDTO> _createTodoQuery;
        IQueryHandler<DeleteTodoQuery, SuccessDTO> _deleteTodoQuery;

        public TodoController(
            IQueryHandler<GetTodosQuery, TodoListDTO> getTodosQuery,
            IQueryHandler<GetTodoByIdQuery, SingleTodoDTO> getTodoByIdQuery,
            IQueryHandler<UpdateTodoQuery, SuccessDTO> updateTodoQuery,
            IQueryHandler<CreateTodoQuery, AddDTO> createTodoQuery,
            IQueryHandler<DeleteTodoQuery, SuccessDTO> deleteTodoQuery)
        {
            _getTodosQuery = getTodosQuery;
            _getTodoByIdQuery = getTodoByIdQuery;
            _updateTodoQuery = updateTodoQuery;
            _createTodoQuery = createTodoQuery;
            _deleteTodoQuery = deleteTodoQuery;
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

        [HttpPost]
        public IActionResult CreateTodo(CreateTodoQuery query)
        {
            return Ok(_createTodoQuery.Execute(query));
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateTodo(UpdateTodoQuery query)
        {
            return Ok(_updateTodoQuery.Execute(query));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteTodo(DeleteTodoQuery query)
        {
            return Ok(_deleteTodoQuery.Execute(query));
        }
    }
}

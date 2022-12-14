using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myAPIproject.Data;
using NuGet.Protocol;
using TodoApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {


            var ItemLists = new {

                TConnectID = new {
                    Type = "string",
                    digits = 20,
                    M_O = "M"
                 },

                Name = new
                {
                    Type = "string",
                    digits = 20,
                    M_O = "M"
                },

                Age = new
                {
                    Type = "Number",
                    digits = 2,
                    M_O = "M"
                },

                Gender = new
                {
                    Type = "string",
                    digits = 20,
                    M_O = "M"
                },

            };

            Console.WriteLine(ItemLists);




            //IDictionary<string, string, int, string> dataMap = new();
            //dataMap.Add("TConnectID", "string", 20, "M");
            //dataMap.Add("Name", "string", 20, "M");
            //dataMap.Add("Age", "Number", 2, "M");
            //dataMap.Add("Gender", "string", 20, "M");

            //foreach(var _data in dataMap)
            //{
            //    Console.WriteLine(_data);
            //}


            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }


        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItemDTO todoDTO)
        {
            if (id != todoDTO.id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.name = todoDTO.name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        //{
        //    var todoItem = new TodoItem
        //    {
        //        name = todoDTO.name
        //    };

        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(
        //        nameof(GetTodoItem),
        //        new { id = todoItem.id },
        //        ItemToDTO(todoItem));

        //}

        [HttpPost]
        public HttpResponseMessage PostTodoItem(TodoItem todoItem)
        {

            //[
            //  {
            //    "TConnectID": "0001",
            //    "Name": "Panyathep Sethsathira",
            //    "Age": 44,
            //    "Gender": "M"
            //  },
            //  {
            //    "TConnectID": "0002",
            //    "Name": "Name1 LastName1",
            //    "Age": 44,
            //    "Gender": "F"
            //  }
            //]

            if (ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            else
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                Console.WriteLine("invalid input");
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        //{
                //_context.TodoItems.Add(todoItem);
                //await _context.SaveChangesAsync();

                //return CreatedAtAction(
                //    nameof(GetTodoItem),
                //    new { id = todoItem.id },
                //    ItemToDTO(todoItem));

            }

        //[HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        //{
        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.id }, todoItem);
        //}

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                id = todoItem.id,
                name = todoItem.name
            };
    }

}

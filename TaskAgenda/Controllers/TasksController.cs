using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAgenda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TaskAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksDbContext context;
        public TasksController(TasksDbContext context)
        {
            this.context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return context.Tasks;
        }

        // GET: api/Tasks
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Tasks.FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }


        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            //if (!ModelState.IsValid)
            //{

            //}
            context.Products.Add(product);
            context.SaveChanges();
        }

        // PUT: api/Tasks
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return Ok(task);
            }
            task.Id = id;
            context.Tasks.Update(task);
            context.SaveChanges();
            return Ok(task);
        }

        // DELETE: api/ApiWithActions
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Tasks.FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}

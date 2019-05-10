using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: api/Tasks/2 -----2 - view detail on task with id 2
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
        // GET: api/Tasks/filtered
        [HttpGet("filtered")]
        public IEnumerable<Task> GetfilteredDate([FromBody] IntervalDate interval)
        {
            DbSet<Task> list = TasksDbContext.Taskss;
            IList<Task> result = new List<Task>();

            foreach (Task task in list)
            {
                if (task.Deadline > interval.begin && task.Deadline < interval.end)
                {
                    System.Diagnostics.Debug.WriteLine(task.Status);
                    result.Add(task);
                }
            }

            return result;
        }
        
     

        public class IntervalDate
        {
            public DateTime begin { get; set; } 
            public DateTime end { get; set; }

        }
       
        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Task task)
        {
         
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        // PUT: api/Tasks/
        [HttpPut("{id}")]
        public IActionResult Put(int id,  [FromBody] Task task)
        {
            
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return Ok(task);
            }
            if (task.Status == "Closed")
            {
                task.DateTimeClosedAt = task.Deadline;
                context.SaveChanges();
                return Ok(task);
            }
            /*else
            {
                task.DateTimeClosedAt  = null;
            }*/
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

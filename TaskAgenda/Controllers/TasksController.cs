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


        /// <summary>
        /// Gets all the tasks.
        /// </summary>
        /// <param name="from">Optional, filter by minimum Deadline.</param>
        /// <param name="to">Optional, filter by maximum Deadline.</param>
        /// <returns>A list of Task objects.</returns>
        [HttpGet]
        public IEnumerable<Task> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Task> result = context.Tasks.Include(t => t.Comments);
            if (from == null && to == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(t => t.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(t => t.Deadline <= to);
            }
            return result;
        }


        // GET: api/Tasks/2 -----2 - view detail on task with id 2
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }

        /// <summary>
        /// Add a task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST / tasks 
        ///         {
        ///             "title": "Rapoarte- errori",
        ///             "description": "Erori muuuulte",
        ///             "dateTimeAdded": "2019/05/04 12:00",
        ///             "deadline": "2019/05/31 17:00",
        ///             "importance": "High",
        ///             "status": "In_progress",
        ///             "dateTimeClosedAt": "2019/05/31 18:00",
        ///             "comments":[
        ///	                {
        ///		                "text": "to many errors",
        ///		                "positive": true
        ///                 }
        ///	                ]
        ///         }
        /// </remarks>
        /// <param name="task"></param>
        // POST: api/Tasks
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [HttpPost]
        public void Post([FromBody] Task task)
        {

            context.Tasks.Add(task);
            context.SaveChanges();
        }

        // PUT: api/Tasks/
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
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

                task.DateTimeClosedAt = DateTime.Now;
                context.SaveChanges();
                return Ok(task);
            }
            else
            {
                task.DateTimeClosedAt = null;
                    
            }
            task.Id = id;
            context.Tasks.Update(task);
            //context.SaveChanges();
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

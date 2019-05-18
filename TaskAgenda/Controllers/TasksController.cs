using System;
using System.Collections.Generic;
using System.Linq;
using TaskAgenda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAgenda.Services;
using TaskAgenda.ViewModels;

namespace TaskAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService taskService;
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }


        /// <summary>
        /// Gets all the tasks.
        /// </summary>
        /// <param name="from">Optional, filter by minimum Deadline.</param>
        /// <param name="to">Optional, filter by maximum Deadline.</param>
        /// <returns>A list of Task objects.</returns>
        [HttpGet]
        public IEnumerable<TaskGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to) //cu viewModel - voi avea in loc de<Task> -> <TaskGetModel>
        {
            return taskService.GetAll(from, to);  //nu mai merge si atunci in Service trb sa am la GetAll ( <TaskGetModel>)    
        }


        // GET: api/Tasks/2 -----2 - view detail on task with id 2
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = taskService.GetById(id);

            if (found == null)
            {
                return NotFound();  //tine de logica UI (ce sa imi returneze pe UI). Fiind Http -> este cu not found si ok
            }

            return Ok(found);
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
        public void Post([FromBody] TaskPostModel task)
        {
            taskService.Create(task);
        }

        // PUT: api/Tasks/
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
        {
            var result = taskService.Upsert(id, task);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = taskService.Delete(id);
            if (result == null)
            {
                return NotFound();
            } 
            return Ok(result);  //o sa imi returneze chiar obiectul sters
        }
    }
}

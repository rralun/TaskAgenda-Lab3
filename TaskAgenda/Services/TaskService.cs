using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using TaskAgenda.Models;
using TaskAgenda.ViewModels;

namespace TaskAgenda.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<TaskGetModel> GetAll(DateTime? from=null, DateTime? to=null);
        Task GetById(int id);
        Task Create(TaskPostModel task);
        Task Upsert(int id, Task task);
        Task Delete(int id);

    }
    public class TaskService : ITaskService
    {
        private TasksDbContext context;

        //constructorul
        public TaskService(TasksDbContext context)
        {
            this.context = context;
        }
        public Task Create(TaskPostModel task)
        {
            Task toAdd = TaskPostModel.ToTask(task);
            context.Tasks.Add(toAdd);
            context.SaveChanges();
            return toAdd;
        }

        public Task Delete(int id)
        {
            var existing = context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();

            return existing;
        }
        public IEnumerable<TaskGetModel> GetAll(DateTime? from=null, DateTime? to=null)
        {
            IQueryable<Task> result = context
                .Tasks
                .Include(t => t.Comments);
            if (from == null && to == null)
            {
                return result.Select(t => TaskGetModel.FromTask(t));
                        }
            if (from != null)
            {
                result = result.Where(t => t.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(t => t.Deadline <= to);
            }
            return result.Select(t => TaskGetModel.FromTask(t));
        }

        public Task GetById(int id)
        {
            // sau context.Tasks.Find()
            return context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.Id == id);
        }

        public Task Upsert(int id, Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return task;
            }
            if (task.Status == "Closed")
            {

                task.DateTimeClosedAt = DateTime.Now;
                context.SaveChanges();
                return task;
            }
            else
            {
                task.DateTimeClosedAt = null;

            }
            task.Id = id;
            context.Tasks.Update(task);
            //context.SaveChanges();
            return task;
        }
    }
}

using TaskAgenda.ViewModels;
using TaskAgenda.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;

namespace TaskAgenda.Services
{
    public interface ICommentService
    {

        IEnumerable<CommentGetModel> GetAllFiltered(string filter);
  
        
    }
    public class CommentService : ICommentService
    {
        private TasksDbContext context;

        //constructorul
        public CommentService (TasksDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentGetModel> GetAllFiltered(string filter)
        {
            bool filterComment = true;
            if (string.IsNullOrEmpty(filter))
                filterComment = false;

            var 
            IQueryable<CommentGetModel> result = context
                .Comments
                .Select(c => CommentGetModel.DinTask(c))
                .Where(c => c.Text.Contains(filter));
            //if (filter.Equals(null))
            //{
            //    return result.Select(t => CommentGetModel.DinTask(comment);
            //}
          
                return result;

        }

        public IEnumerable<CommentGetModel> GetAllComentsAndTask()
        {
            var order = from comment in context.Comments
                        join task in context.Tasks
                        on comment.TaskId equals task.Id
                        select new CommentGetModel()
                        {
                           Id = comment.Id,
                           Text = comment.Text,
                           Important = comment.Important,
                           TaskId = comment.TaskId
                        };

            return context.Comments.Select(comment => CommentGetModel.DinTask(comment));
        }
    }
}

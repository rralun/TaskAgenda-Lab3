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
            IQueryable<CommentGetModel> result = context
                .Comments
                .Select(c => CommentGetModel.DinTask(c))
                .Where(c => c.Text.Contains(filter));
            return result;

        }
    }
}

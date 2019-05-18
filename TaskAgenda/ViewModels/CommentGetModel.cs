using System;
using System.Collections.Generic;
using System.Linq;
using TaskAgenda.Models;
//using System.Threading.Tasks;

namespace TaskAgenda.ViewModels
{
    public class CommentGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int TaskId { get; set; }

        public static CommentGetModel DinTask(Comment comment)
        {
            return new CommentGetModel
            {
                Id = comment.Id,
                Text = comment.Text,
                Important = comment.Important,
                TaskId = comment.TaskId
            };
        }

    }

}

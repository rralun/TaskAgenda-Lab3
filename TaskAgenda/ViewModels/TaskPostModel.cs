using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
//using System.Threading.Tasks;
using TaskAgenda.Models;

namespace TaskAgenda.ViewModels
{
    public class TaskPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        // importance: low, medium, high
        [EnumDataType(typeof(Importance))]
        public string Importance { get; set; } //vor fi string ca sa imi ia valorile ca si string nu ca 0,1,2
        // status: open, in progress, closed
        [EnumDataType(typeof( Status))]
        public string Status { get; set; }
        public DateTime? DateTimeClosedAt { get; set; }


        public static Task ToTask (TaskPostModel task)
        {
            //va trebui sa imi "calculez" enumurile : cazul meu importance si status
            //pentru importance
            //Importance importance = Models.Importance.Low;
            //if (task.Importance == "Medium")
            //{
            //    importance = Models.Importance.Medium;
            //}
            //else if(task.Importance == "High")
            //{
            //    importance = Models.Importance.High;
            //}

            ////pentru status
            //Status status= Models.Status.Open;
            //if (task.Status == "In_Progress")
            //{
            //    status = Models.Status.In_progress;
            //}
            //else if(task.Status == "Closed")
            //{
            //    status = Models.Status.Closed;
            //}

            return new Task
            {
                Title = task.Title,
                Description = task.Description,
                DateTimeAdded = task.DateTimeAdded,
                Deadline = task.Deadline,
                Importance = task.Importance,
                Status = task.Status,
                DateTimeClosedAt = task.DateTimeClosedAt
            };  
        }

    }
}

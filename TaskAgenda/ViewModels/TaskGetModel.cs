using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using TaskAgenda.Models;

namespace TaskAgenda.ViewModels
{
    public class TaskGetModel
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        //imi pot adauga de ex: numberOfComments. Si nu am nevoie de migratie pt ca nu schimb Baza de Date
        public int NumberOfComments { get; set; }
       

        public static TaskGetModel FromTask(Task task)
        {
            return new TaskGetModel
            {
                Title = task.Title,                    //imi mapeaza functia asta pe fiecare element din result 
                Description = task.Description,        //(adica pe fiecare <Task>): title, description, dateTimeAdded
                DateTimeAdded = task.DateTimeAdded,    //pt fiecare task t imi da un TaskGetModel, cu campurile completate aici (cele 3)
                NumberOfComments = task.Comments.Count           //cu count imi adun nr de comentarii. PT ASTA TRB SA AM PUS "INCLUDE"
                                                                //altfel comments e null si va da eroare
            };
        }
    }
}

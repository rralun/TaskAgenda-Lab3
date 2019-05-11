using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgenda.Models
{
    public enum Importance
    {
        Low,
        Medium,
        High
    }
    public enum Status
    {
        Open,
        In_progress,
        Closed
    }
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        // importance: low, medium, high
        [EnumDataType(typeof(Importance))]
        public string Importance { get; set; }
        // status: open, in progress, closed
        //[StringRange(AllowableValues = new[] { "Open", "In progress", "Closed" }, ErrorMessage = "Gender must be either 'open', 'in progress' or 'closed'")]
        [EnumDataType(typeof(Status))]
        public string Status { get; set; }
        public DateTime? DateTimeClosedAt { get; set; }

        public List<Comment> Comments { get; set; }

        /*
        public class StringRangeAttribute : ValidationAttribute
        {
            public string[] AllowableValues { get; set; }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                if (value.ToString() == "Low" || value.ToString() == "Medium" || value.ToString() == "High")
                {
                    return ValidationResult.Success;
                }
                if (value.ToString() == "Open" || value.ToString() == "In progress" || value.ToString() == "Closed")
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Please enter a correct value. ");
            }
        }*/

    }
}

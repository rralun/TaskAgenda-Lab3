using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TaskAgenda.Models
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {
        }

        // DbSet = Repository
        public DbSet<Task> Tasks { get; set; }

        public List<Comment> Comments { get; set; }

    }
}

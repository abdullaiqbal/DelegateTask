using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskDelegates.Models;

namespace TaskDelegates.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<customer> Customers { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<ViewModelNew.Models.StudentCourse> StudentCourse { get; set; }
    }
}

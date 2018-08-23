using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Data.Maps;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data
{
    public class GrandCircusLmsContext : DbContext
    {
        public GrandCircusLmsContext(): base("GrandCircusLMS")
        {
            //Drop the database and recreate on each run
            //Database.SetInitializer(new DropCreateDatabaseAlways<GrandCircusLmsContext>());
            // Create the DB if it doesn't exist.  
            //Database.SetInitializer(new CreateDatabaseIfNotExists<GrandCircusLmsContext>());
            //Will Drop and recreate if model changes.
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GrandCircusLmsContext>());
            //Custom Initializer
            Database.SetInitializer(new GrandCircusLmsInitializer());

        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new EnrollmentMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new InstructorMap());
            modelBuilder.Configurations.Add(new ProgramManagerMap());
            base.OnModelCreating(modelBuilder);
            
        }
    }
}

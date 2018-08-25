using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data.Interfaces
{
    public interface IGrandCircusLMSContext
    {
        IDbSet<Location> Locations { get; set; }
        IDbSet<Course> Courses { get; set; }
        IDbSet<Student> Students { get; set; }
        IDbSet<Instructor> Instructors { get; set; }
        IDbSet<Enrollment> Enrollments { get; set; }
        IDbSet<ProgramManager> ProgramManagers { get; set; }
    }
}

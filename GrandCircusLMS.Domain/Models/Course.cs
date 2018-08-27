using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Domain.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public int ProgramManagerId { get; set; }
        public virtual ProgramManager ProgramManager { get; set; }
    }
}

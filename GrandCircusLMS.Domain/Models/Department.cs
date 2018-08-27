using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Domain.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }

    }
}

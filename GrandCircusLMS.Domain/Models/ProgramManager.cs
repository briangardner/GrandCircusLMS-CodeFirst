using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Domain.Models
{
    public class ProgramManager : Person
    {
        public int CourseId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}

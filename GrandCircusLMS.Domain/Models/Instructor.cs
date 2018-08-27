using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Domain.Models
{
    public class Instructor : Person
    {
        public string OfficeNumber { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}

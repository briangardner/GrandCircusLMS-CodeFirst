using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Domain.Models
{
    public class Student : Person
    {
        public string DeclaredMajor { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

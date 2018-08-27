using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data.Maps
{
    internal class EnrollmentMap : BaseEntityMap<Enrollment>
    {
        public EnrollmentMap()
        {
            HasRequired(x => x.Course).WithMany(x => x.Enrollments).HasForeignKey(x => x.CourseId);
            HasRequired(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId);
        }
    }
}

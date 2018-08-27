using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data.Maps
{
    class DepartmentMap : BaseEntityMap<Department>
    {
        public DepartmentMap()
        {
            HasMany(x => x.Courses)
                .WithRequired(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);
            HasMany(x => x.Instructors)
                .WithRequired(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}

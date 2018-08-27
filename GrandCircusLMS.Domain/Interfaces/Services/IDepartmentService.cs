using System.Collections.Generic;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Domain.Interfaces.Services
{
    public interface IDepartmentService
    {
        ICollection<Enrollment> GetDepartmentGrades(Department dept);
    }
}
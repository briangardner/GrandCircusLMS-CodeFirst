using System.Collections.Generic;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Domain.Interfaces.Services
{
    public interface IStudentService
    {
        ICollection<Student> GetStudentsPassingCourse(Course course);
    }
}
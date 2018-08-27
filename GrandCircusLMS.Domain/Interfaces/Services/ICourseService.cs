using System.Collections.Generic;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Domain.Interfaces.Services
{
    public interface ICourseService
    {
        ICollection<Student> GetStudentsPassing(Course course);
        ICollection<Student> GetStudentsFailing(Course course);
        ICollection<Student> GetStudentsWithoutGrade(Course course);
    }
}
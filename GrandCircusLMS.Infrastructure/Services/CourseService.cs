using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Domain.Enums;
using GrandCircusLMS.Domain.Interfaces.Services;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Infrastructure.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IGrandCircusLmsContext _context;

        public CourseService(IGrandCircusLmsContext context)
        {
            _context = context;
        }

        public ICollection<Student> GetStudentsPassing(Course course)
        {
            // 2 Ways to do LINQ queries
            /*var passing2 = _context.Enrollments
                .Where(e => e.CourseId == course.Id && e.Grade != Grade.F)
                .Select(e => e.Student);*/
            var passing = from e in _context.Enrollments
                where e.CourseId == course.Id
                      && (e.Grade.HasValue && e.Grade != Grade.F)
                select e.Student;
            return passing.ToList();
        }

        public ICollection<Student> GetStudentsFailing(Course course)
        {
            var failing = _context.Enrollments
                .Where(e => e.CourseId == course.Id && (e.Grade.HasValue && e.Grade == Grade.F))
                .Select(e => e.Student);
            return failing.ToList();
        }

        public ICollection<Student> GetStudentsWithoutGrade(Course course)
        {
            var missingGrade = _context.Enrollments
                .Where(e => e.CourseId == course.Id && !e.Grade.HasValue)
                .Select(e => e.Student);
            return missingGrade.ToList();
        }
    }
}

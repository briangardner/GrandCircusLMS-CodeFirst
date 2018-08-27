using System.Collections.Generic;
using System.Linq;
using GrandCircusLMS.Domain.Enums;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Interfaces.Services;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Infrastructure.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Student> GetStudentsPassing(Course course)
        {
            var courseLookup = _unitOfWork.Repository<Course>()
                .GetSingleIncluding(course.Id, c => c.Enrollments, c => c.Enrollments.Select(x => x.Student));
            
            var passing = from e in courseLookup.Enrollments
                where IsPassing(e)
                select e.Student;
            return passing.ToList();
        }

        public ICollection<Student> GetStudentsFailing(Course course)
        {
            var courseLookup = _unitOfWork.Repository<Course>()
                .GetSingleIncluding(course.Id, c => c.Enrollments, c => c.Enrollments.Select(x => x.Student));
            var failing = courseLookup.Enrollments
                .Where(IsFailing)
                .Select(e => e.Student);
            return failing.ToList();
        }

        public ICollection<Student> GetStudentsWithoutGrade(Course course)
        {
            var courseLookup = _unitOfWork.Repository<Course>()
                .GetSingleIncluding(course.Id, c => c.Enrollments, c => c.Enrollments.Select(x => x.Student));
            var missingGrade = courseLookup.Enrollments
                .Where(IsMissingGrade)
                .Select(e => e.Student);
            return missingGrade.ToList();
        }

        private bool IsPassing(Enrollment e)
        {
            return e.Grade.HasValue && e.Grade != Grade.F;
        }

        private bool IsFailing(Enrollment e)
        {
            return e.Grade.HasValue && e.Grade == Grade.F;
        }

        private bool IsMissingGrade(Enrollment e)
        {
            return !e.Grade.HasValue;
        }
    }
}

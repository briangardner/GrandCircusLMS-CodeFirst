using System.Collections.Generic;
using System.Linq;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Interfaces.Services;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ICollection<Enrollment> GetDepartmentGrades(Department dept)
        {
            var department = _unitOfWork.Repository<Department>().GetSingleIncluding(dept.Id, d => d.Courses,
                d => d.Courses.Select(c => c.Enrollments),
                d => d.Courses.Select(c => c.Enrollments.Select(e => e.Student)));
            var enrollments = department.Courses.SelectMany(c => c.Enrollments).Where(e => e.Grade.HasValue).OrderBy(e => e.Grade);
            return enrollments.ToList();
        }
    }
}
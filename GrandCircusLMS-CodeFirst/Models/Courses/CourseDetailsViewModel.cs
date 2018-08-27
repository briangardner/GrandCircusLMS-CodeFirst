using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS_CodeFirst.Models.Courses
{
    public class CourseDetailsViewModel
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public string LocationName { get; set; }
        public string ProgramManagerName { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<Student> StudentsPassing { get; set; }
        public ICollection<Student> StudentsFailing { get; set; }
        public ICollection<Student> StudentsMissingGrade { get; set; }
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Domain.Interfaces.Services;
using GrandCircusLMS.Domain.Models;
using GrandCircusLMS_CodeFirst.Controllers;
using GrandCircusLMS_CodeFirst.Models.Courses;
using Moq;
using NUnit.Framework;

namespace GrandCircusLMS_CodeFirst.Tests.Controllers
{
    [TestFixture]
    public class CoursesControllerTests
    {
        private readonly List<Student> _passingStudents = new List<Student>()
        {
            new Student()
            {
                FirstName = "Bobby",
                LastName = "Tables",
                DeclaredMajor = "Database Security"
            }
        };

        private readonly IQueryable<Course> _courses = new List<Course>()
        {
            new Course()
            {
                Id = 5,
                Name = "Test Course",
                Instructors = new List<Instructor>(),
                Location = new Location()
                {
                    Name = "Test Location"
                },
                ProgramManager = new ProgramManager()
                {
                    FirstName = "Test",
                    LastName = "Program Manager"
                }
            }
        }.AsQueryable();

        private Mock<IGrandCircusLmsContext> _contextMock;
        private Mock<ICourseService> _courseServiceMock;
        private Mock<IDbSet<Course>> _courseSetMock;

        [SetUp]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _courseServiceMock.Setup(x => x.GetStudentsPassing(It.IsAny<Course>())).Returns(_passingStudents);

            _courseSetMock = new Mock<IDbSet<Course>>();
            _courseSetMock.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(_courses.Provider);
            _courseSetMock.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(_courses.Expression);
            _courseSetMock.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(_courses.ElementType);
            _courseSetMock.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(_courses.GetEnumerator);
            _courseSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(_courses.First);
            

            _contextMock = new Mock<IGrandCircusLmsContext>();
            _contextMock.Setup(x => x.Courses).Returns(_courseSetMock.Object);
        }

        [Test]
        public void Should_Return_ViewModel_With_CourseDetails()
        {
            var controller = new CoursesController(_contextMock.Object, _courseServiceMock.Object);
            var result = (controller.CourseDetails(5) as ViewResult)?.ViewData.Model as CourseDetailsViewModel;

            Assert.AreEqual(_passingStudents, result?.StudentsPassing);
            
        }
    }
}

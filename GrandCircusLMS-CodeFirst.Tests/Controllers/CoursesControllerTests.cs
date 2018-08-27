using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Domain.Interfaces;
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

        private Course testCourse = new Course()
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
        };

        private Mock<ICourseService> _courseServiceMock;
        private Mock<IRepository<Course>> _courseRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _courseServiceMock.Setup(x => x.GetStudentsPassing(It.IsAny<Course>())).Returns(_passingStudents);

            
            _courseRepositoryMock = new Mock<IRepository<Course>>();
            _courseRepositoryMock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(testCourse);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Repository<Course>()).Returns(_courseRepositoryMock.Object);

        }

        [Test]
        public void Should_Return_ViewModel_With_CourseDetails()
        {
            var controller = new CoursesController(_courseServiceMock.Object, _unitOfWorkMock.Object);
            var result = (controller.CourseDetails(5) as ViewResult)?.ViewData.Model as CourseDetailsViewModel;

            Assert.AreEqual(_passingStudents, result?.StudentsPassing);
            
        }
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Domain.Enums;
using GrandCircusLMS.Domain.Models;
using GrandCircusLMS.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace GrandCircusLMS.Infrastructure.Tests.Services
{
    [TestFixture]
    public class CourseServiceTests
    {
        private readonly IQueryable<Enrollment> _enrollments = new List<Enrollment>()
        {
            new Enrollment()
            {
                CourseId = 1,
                Grade = Grade.F,
                Student = new Student()
                {
                    FirstName = "Failing",
                    LastName = "Student"
                }
            },
            new Enrollment()
            {
                CourseId = 1,
                Grade = Grade.B,
                Student = new Student()
                {
                    FirstName = "Passing",
                    LastName = "Student"
                }
            },
            new Enrollment()
            {
                CourseId = 1,
                Student = new Student()
                {
                    FirstName = "Missing Grade",
                    LastName = "Student"
                }
            }
        }.AsQueryable();

        private readonly Course testCourse = new Course()
        {
            Id = 1,
            Credits = 1,
            LocationId = 1,
            ProgramManagerId = 1
        };
        
        private readonly Mock<IDbSet<Enrollment>> _mockEnrollments = new Mock<IDbSet<Enrollment>>();
        private Mock<IGrandCircusLmsContext> _contextMock;
        

        [SetUp]
        public void Setup()
        {
            

            _mockEnrollments.As<IQueryable<Enrollment>>().Setup(m => m.Provider).Returns(_enrollments.Provider);
            _mockEnrollments.As<IQueryable<Enrollment>>().Setup(m => m.Expression).Returns(_enrollments.Expression);
            _mockEnrollments.As<IQueryable<Enrollment>>().Setup(m => m.ElementType).Returns(_enrollments.ElementType);
            _mockEnrollments.As<IQueryable<Enrollment>>().Setup(m => m.GetEnumerator()).Returns(_enrollments.GetEnumerator);


            _contextMock = new Mock<IGrandCircusLmsContext>();
            _contextMock.Setup(x => x.Enrollments).Returns(_mockEnrollments.Object);
        }

        [Test]
        public void Should_Return_Students_With_Passing_Grade()
        {
            var service = new CourseService(_contextMock.Object);
            var passing = service.GetStudentsPassing(testCourse);
            var student = passing.First();
            Assert.AreEqual("Passing", student.FirstName);

        }

        [Test]
        public void Should_Return_Students_With_Failing_Grade()
        {
            var service = new CourseService(_contextMock.Object);
            var students = service.GetStudentsFailing(testCourse);
            var student = students.First();
            Assert.AreEqual("Failing", student.FirstName);

        }

        [Test]
        public void Should_Return_Students_With_Missing_Grade()
        {
            var service = new CourseService(_contextMock.Object);
            var students = service.GetStudentsWithoutGrade(testCourse);
            var student = students.First();
            Assert.AreEqual("Missing Grade", student.FirstName);

        }
    }
}

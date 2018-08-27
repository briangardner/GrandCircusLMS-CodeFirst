using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using GrandCircusLMS.Data.Interfaces;
using GrandCircusLMS.Domain.Enums;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Models;
using GrandCircusLMS.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace GrandCircusLMS.Infrastructure.Tests.Services
{
    [TestFixture]
    public class CourseServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Course>> _courseRepositoryMock;

        private readonly Course testCourse = new Course()
        {
            Id = 1,
            Credits = 1,
            LocationId = 1,
            ProgramManagerId = 1,
            Enrollments = new List<Enrollment>()
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
            }
        };
        

        [SetUp]
        public void Setup()
        {
            _courseRepositoryMock = new Mock<IRepository<Course>>();
            _courseRepositoryMock.Setup(x =>
                    x.GetSingleIncluding(It.IsAny<int>(), It.IsAny<Expression<Func<Course, object>>[]>()))
                .Returns(testCourse);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Repository<Course>()).Returns(_courseRepositoryMock.Object);


        }

        [Test]
        public void Should_Return_Students_With_Passing_Grade()
        {
            var service = new CourseService(_unitOfWorkMock.Object);
            var passing = service.GetStudentsPassing(testCourse);
            var student = passing.First();
            Assert.AreEqual("Passing", student.FirstName);

        }

        [Test]
        public void Should_Return_Students_With_Failing_Grade()
        {
            var service = new CourseService(_unitOfWorkMock.Object);
            var students = service.GetStudentsFailing(testCourse);
            var student = students.First();
            Assert.AreEqual("Failing", student.FirstName);

        }

        [Test]
        public void Should_Return_Students_With_Missing_Grade()
        {
            var service = new CourseService(_unitOfWorkMock.Object);
            var students = service.GetStudentsWithoutGrade(testCourse);
            var student = students.First();
            Assert.AreEqual("Missing Grade", student.FirstName);

        }
    }
}

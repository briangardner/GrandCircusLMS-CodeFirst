using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Enums;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Models;
using GrandCircusLMS.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace GrandCircusLMS.Infrastructure.Tests.Services
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Department>> _departmentRepoMock;

        private Department TestDepartment = new Department()
        {
            Id = 1,
            Name = "Computer Science Department",
            Courses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Name = "Course 1",
                    Enrollments = new List<Enrollment>()
                    {
                        new Enrollment()
                        {
                            Id = 1,
                            Grade = Grade.A,
                            Student = new Student()
                            {
                                FirstName = "Bobby",
                                LastName = "Tables"
                            }
                        },
                        new Enrollment()
                        {
                            Id = 1,
                            Grade = Grade.B,
                            Student = new Student()
                            {
                                FirstName = "Bobby22",
                                LastName = "Tables7"
                            }
                        }
                    }
                },
                new Course()
                {
                    Id = 1,
                    Name = "Course 2",
                    Enrollments = new List<Enrollment>()
                    {
                        new Enrollment()
                        {
                            Id = 1,
                            Student = new Student()
                            {
                                FirstName = "Robby",
                                LastName = "Tables"
                            }
                        },
                        new Enrollment()
                        {
                            Id = 1,
                            Grade = Grade.B,
                            Student = new Student()
                            {
                                FirstName = "Tommy",
                                LastName = "Tables"
                            }
                        }
                    }
                }
            }
        };

        [SetUp]
        public void Setup()
        {
            _departmentRepoMock = new Mock<IRepository<Department>>();
            _departmentRepoMock.Setup(x =>
                    x.GetSingleIncluding(It.IsAny<int>(), It.IsAny<Expression<Func<Department, object>>[]>()))
                .Returns(TestDepartment);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Repository<Department>()).Returns(_departmentRepoMock.Object);
        }

        [Test]
        public void Should_Get_List_Of_Enrollments()
        {
            var service = new DepartmentService(_unitOfWorkMock.Object);
            var enrollments = service.GetDepartmentGrades(new Department() {Id = 77});
            Assert.IsNotNull(enrollments);
        }


    }
}

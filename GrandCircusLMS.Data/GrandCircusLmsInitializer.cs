using System;
using System.Collections.Generic;
using System.Data.Entity;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data
{
    class GrandCircusLmsInitializer : DropCreateDatabaseIfModelChanges<GrandCircusLmsContext>
    {
        protected override void Seed(GrandCircusLmsContext context)
        {
            //This is where we start to seed our data.
            var instructor = new Instructor()
            {
                Id = 1,
                DateOfBirth = new DateTime(1981, 12, 26),
                FirstName = "Brian",
                LastName = "Gardner",
                SocialSecurityNumber = "111-11-1111",
                OfficeNumber = "42"
            };
            context.Instructors.Add(instructor);
            context.SaveChanges();

            var pm = new ProgramManager()
            {
                Id = 1,
                DateOfBirth = new DateTime(1989,7,1),
                FirstName = "Kelsey",
                LastName = "Perdue",
                SocialSecurityNumber = "222-22-2222",
            };
            context.ProgramManagers.Add(pm);
            context.SaveChanges();

            var location = new Location()
            {
                Id = 1,
                Name = "Grand Rapids",
                Address = "40 Pearl",
                City = "Grand Rapids",
                State = "Michigan"
            };
            context.Locations.Add(location);
            context.SaveChanges();

            var course = new Course()
            {
                Id = 1,
                Credits = 4,
                Name = "C# for Ducks",
                Instructors = new List<Instructor>() {instructor},
                Location = location,
                ProgramManager = pm
            };
            context.Courses.Add(course);
            context.SaveChanges();

            context.Students.Add(new Student()
            {
                Id = 1,
                DateOfBirth = new DateTime(1985, 1, 1),
                DeclaredMajor = "Marketing",
                FirstName = "Sean",
                LastName = "Armstrong",
                SocialSecurityNumber = "111-11-1112"
            });
            context.Students.Add(new Student()
            {
                Id = 2,
                DateOfBirth = new DateTime(1987, 2, 16),
                DeclaredMajor = "Culinary Arts",
                FirstName = "Mike",
                LastName = "Cacciano",
                SocialSecurityNumber = "111-11-1113"
            });
            context.Students.Add(new Student()
            {
                Id = 3,
                DateOfBirth = new DateTime(1992, 3, 17),
                DeclaredMajor = "Coffee Arts",
                FirstName = "Michael",
                LastName = "Clark",
                SocialSecurityNumber = "111-11-1114"
            });
            context.Students.Add(new Student()
            {
                Id = 4,
                DateOfBirth = new DateTime(1989, 4, 17),
                DeclaredMajor = "Media Relations",
                FirstName = "Aquoinette",
                LastName = "Blair",
                SocialSecurityNumber = "111-11-1115"
            });
            context.Students.Add(new Student()
            {
                Id = 5,
                DateOfBirth = new DateTime(1993, 4, 1),
                DeclaredMajor = "User Interfaces",
                FirstName = "Rabin",
                LastName = "Rai",
                SocialSecurityNumber = "111-11-1116"
            });
            context.Students.Add(new Student()
            {
                Id = 6,
                DateOfBirth = new DateTime(1991, 5, 2),
                DeclaredMajor = "Radio Arts",
                FirstName = "Bradley",
                LastName = "Freestone",
                SocialSecurityNumber = "111-11-1117"
            });
            context.Students.Add(new Student()
            {
                Id = 7,
                DateOfBirth = new DateTime(1991, 5, 2),
                DeclaredMajor = "Brisket",
                FirstName = "Sean",
                LastName = "Sculley",
                SocialSecurityNumber = "111-11-1118"
            });
            context.Students.Add(new Student()
            {
                Id = 8,
                DateOfBirth = new DateTime(1993, 5, 23),
                DeclaredMajor = "Biking",
                FirstName = "Ross",
                LastName = "Hinman",
                SocialSecurityNumber = "111-11-1119"
            });
            context.Students.Add(new Student()
            {
                Id = 9,
                DateOfBirth = new DateTime(1990, 6, 23),
                DeclaredMajor = "Cultural Studies",
                FirstName = "Chris",
                LastName = "Butcher",
                SocialSecurityNumber = "111-11-1120"
            });
            context.Students.Add(new Student()
            {
                Id = 10,
                DateOfBirth = new DateTime(1992, 8, 23),
                DeclaredMajor = "Video Game Art",
                FirstName = "Catherine",
                LastName = "Stafford",
                SocialSecurityNumber = "111-11-1121"
            });
            context.Students.Add(new Student()
            {
                Id = 11,
                DateOfBirth = new DateTime(1999, 1, 23),
                DeclaredMajor = "Smash Brothers",
                FirstName = "Jacob",
                LastName = "Hands",
                SocialSecurityNumber = "111-11-1122"
            });
            context.SaveChanges();

            context.Enrollments.AddRange(
                new List<Enrollment>
                {
                    new Enrollment()
                    {
                        Id = 1,
                        CourseId = 1,
                        StudentId = 1
                    },
                    new Enrollment()
                    {
                        Id = 2,
                        CourseId = 1,
                        StudentId = 2
                    },
                    new Enrollment()
                    {
                        Id = 3,
                        CourseId = 1,
                        StudentId = 3
                    },
                    new Enrollment()
                    {
                        Id = 4,
                        CourseId = 1,
                        StudentId = 4
                    },
                    new Enrollment()
                    {
                        Id = 5,
                        CourseId = 1,
                        StudentId = 5
                    },
                    new Enrollment()
                    {
                        Id = 6,
                        CourseId = 1,
                        StudentId = 6
                    },
                    new Enrollment()
                    {
                        Id = 7,
                        CourseId = 1,
                        StudentId = 7
                    },
                    new Enrollment()
                    {
                        Id = 8,
                        CourseId = 1,
                        StudentId = 8
                    },
                    
                    new Enrollment()
                    {
                        Id = 9,
                        CourseId = 1,
                        StudentId = 9
                    },
                    new Enrollment()
                    {
                        Id = 10,
                        CourseId = 1,
                        StudentId = 10
                    },
                    new Enrollment()
                    {
                        Id = 11,
                        CourseId = 1,
                        StudentId = 11
                    },

                }

            );
            context.SaveChanges();


            base.Seed(context);
        }
    }
}

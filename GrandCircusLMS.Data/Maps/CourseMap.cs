﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data.Maps
{
    internal class CourseMap : BaseEntityMap<Course>
    {
        public CourseMap()
        {
            
            HasMany(x => x.Enrollments)
                .WithRequired(x => x.Course)
                .HasForeignKey(x => x.CourseId);
            HasMany(x => x.Instructors)
                .WithMany(x => x.Courses)
                .Map(map =>
                {
                    map.MapRightKey("CourseId");
                    map.MapLeftKey("InstructorId");
                    map.ToTable("CourseInstructors");
                });

        }
    }
}

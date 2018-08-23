using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS.Data.Maps
{
    internal class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}

using GrandCircusLMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Data.Maps
{
    internal class ProgramManagerMap : BaseEntityMap<ProgramManager>
    {
        public ProgramManagerMap()
        {
            HasMany(x => x.Courses)
                .WithRequired(x => x.ProgramManager)
                .HasForeignKey(x => x.ProgramManagerId);
        }
    }
}

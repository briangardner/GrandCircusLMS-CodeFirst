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
    internal class LocationMap : BaseEntityMap<Location>
    {
        public LocationMap()
        {
            HasMany(x => x.Courses)
                .WithRequired(x => x.Location)
                .HasForeignKey(z =>z.LocationId);
        }
    }
}

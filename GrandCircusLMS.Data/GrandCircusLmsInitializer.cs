using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandCircusLMS.Data
{
    class GrandCircusLmsInitializer : CreateDatabaseIfNotExists<GrandCircusLmsContext>
    {
        protected override void Seed(GrandCircusLmsContext context)
        {
            //This is where we start to seed our data.
            base.Seed(context);
        }
    }
}

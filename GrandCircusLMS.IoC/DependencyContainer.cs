using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandCircusLMS.Data;
using GrandCircusLMS.Data.Interfaces;
using SimpleInjector;

namespace GrandCircusLMS.IoC
{
    public class DependencyContainer
    {
        public static void SetupContainer(Container container)
        {
            container.Register<IGrandCircusLmsContext, GrandCircusLmsContext>(Lifestyle.Scoped);
        }

    }
}

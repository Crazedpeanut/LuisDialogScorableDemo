using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScorableDemp.Modules
{
    public class UtilityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new Logger()).As<ILogger>();
        }
    }
}
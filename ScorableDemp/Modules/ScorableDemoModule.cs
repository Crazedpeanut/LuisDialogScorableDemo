using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;
using ScorableDemp.Luis;
using ScorableDemp.Scorable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScorableDemp.Modules
{
    public class ScorableDemoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new ListSongs())
                .As<ILuisIntentHandlerDialog>();

            builder
               .Register(c => new LuisIntentHandlerDialogFactory(c.Resolve<IEnumerable<ILuisIntentHandlerDialog>>()))
               .InstancePerLifetimeScope();

            LuisModelAttribute luisModelAttribute = new LuisModelAttribute("329382da-1aff-4671-9be4-e0d9bd1144e0", "9aceb8ad11e04e1d82c880bd16f4525f", LuisApiVersion.V2, "southeastasia.api.cognitive.microsoft.com");
            builder
                .Register(c => new LuisService(luisModelAttribute))
                .As<ILuisService>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => new LuisScorable(c.Resolve<ILuisService>(), c.Resolve<IDialogTask>(), c.Resolve<ILogger>(), c.Resolve<LuisIntentHandlerDialogFactory>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => new GlobalMessageScorable(c.Resolve<IDialogTask>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();
        }
    }
}
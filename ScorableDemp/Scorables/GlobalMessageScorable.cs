using Microsoft.Bot.Builder.Scorables.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs.Internals;
using ScorableDemp.Luis;
using Microsoft.Bot.Builder.Dialogs;
using ScorableDemp.Dialogs;

namespace ScorableDemp.Scorable
{
    public class GlobalMessageScorable : ScorableBase<IActivity, string, double>
    {
        List<string> quitKeywords = new List<string>() { "quit", "exit", "leave", "bye" };
        IDialogTask task;

        public GlobalMessageScorable(IDialogTask dialogTask)
        {
            task = dialogTask;
        }

        protected override Task DoneAsync(IActivity item, string state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected override double GetScore(IActivity item, string state)
        {
            return 1;
        }

        protected override bool HasScore(IActivity item, string state)
        {
            return string.IsNullOrEmpty(state) ? false : true;
        }

        protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
        {
           switch(state.ToLower())
            {
                case "quit":
                    {
                        this.task.Reset();
                    } break;
                    
            }
        }

        protected override async Task<string> PrepareAsync(IActivity item, CancellationToken token)
        {
            if(item.Type == ActivityTypes.Message)
            {
                var message = item as IMessageActivity;

                if (quitKeywords.Contains(message.Text)) return "quit";
            }

            return null;
        }
    }
}